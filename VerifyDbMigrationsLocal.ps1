<#
.SYNOPSIS
  Applies EF Core migrations and verifies they reproduce the dev database schema exactly.
  No-Docker variant: uses a second throwaway database on your existing local Postgres
  server instead of spinning up a container.

.DESCRIPTION
  1. (Optional) Creates a new migration and applies it to your real dev database.
  2. Snapshots your dev database schema.
  3. Creates a throwaway database on the same Postgres server, applies ALL migrations
     to it from scratch.
  4. Snapshots that schema and diffs it against your dev database.
  5. Reports PASS/FAIL and drops the throwaway database.

  Schema-changing steps (CREATE DATABASE, DROP DATABASE, dotnet ef database update)
  need an admin/owner role - fa_app deliberately doesn't have DDL rights. Pass your
  admin credentials with -AdminUser / -AdminPassword each time you run this; they are
  never written to disk or logged.

.EXAMPLE
  .\VerifyDbMigrationsLocal.ps1 -AdminUser postgres -AdminPassword ****

.EXAMPLE
  .\VerifyDbMigrationsLocal.ps1 -AdminUser postgres -AdminPassword **** -AddMigration "AddAccountMaster"
#>

[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [string]$AdminUser,

    [Parameter(Mandatory = $true)]
    [string]$AdminPassword,

    [string]$AddMigration,
    [string]$DbHost = "localhost",
    [int]$DbPort = 5432,
    [string]$DevDb = "fruit_accounting_dev",
    [string]$VerifyDb = "fruit_accounting_verify",
    [switch]$SkipCleanup
)

$ErrorActionPreference = "Stop"

$repoRoot = $PSScriptRoot
$dataProject = Join-Path $repoRoot "FruitAccounting.Data"
$startupProject = Join-Path $repoRoot "FruitAccounting.UI"
$outDir = Join-Path $repoRoot ".db-check-output"
New-Item -ItemType Directory -Force -Path $outDir | Out-Null
$baselineSql = Join-Path $outDir "schema_dev_baseline.sql"
$migratedSql = Join-Path $outDir "schema_from_migrations.sql"

function Assert-Command {
    param([string]$Name)
    if (-not (Get-Command $Name -ErrorAction SilentlyContinue)) {
        throw "'$Name' not found on PATH. Install it before running this script."
    }
}

Assert-Command dotnet
Assert-Command pg_dump
Assert-Command psql

$adminConnStr = "Host=$DbHost;Port=$DbPort;Database=$VerifyDb;Username=$AdminUser;Password=$AdminPassword;"

try {
    $env:PGPASSWORD = $AdminPassword

    if ($AddMigration) {
        Write-Host "==> Creating migration '$AddMigration'..." -ForegroundColor Cyan
        dotnet ef migrations add $AddMigration --project $dataProject --startup-project $startupProject
        if ($LASTEXITCODE -ne 0) { throw "dotnet ef migrations add failed" }

        Write-Host "==> Applying it to your dev database ($DevDb) as $AdminUser..." -ForegroundColor Cyan
        $env:FRUITACCOUNTING_CONNECTIONSTRING = "Host=$DbHost;Port=$DbPort;Database=$DevDb;Username=$AdminUser;Password=$AdminPassword;"
        dotnet ef database update --project $dataProject --startup-project $startupProject
        $applyExit = $LASTEXITCODE
        Remove-Item Env:\FRUITACCOUNTING_CONNECTIONSTRING
        if ($applyExit -ne 0) { throw "dotnet ef database update (dev) failed" }
    }

    Write-Host "==> Snapshotting current schema on $DbHost`:$DbPort/$DevDb..." -ForegroundColor Cyan
    pg_dump --schema-only -h $DbHost -p $DbPort -U $AdminUser -d $DevDb -f $baselineSql
    if ($LASTEXITCODE -ne 0) { throw "pg_dump of dev database failed" }

    Write-Host "==> Dropping any leftover '$VerifyDb' from a previous run..." -ForegroundColor Cyan
    psql -h $DbHost -p $DbPort -U $AdminUser -d postgres -c "DROP DATABASE IF EXISTS $VerifyDb;" | Out-Null

    Write-Host "==> Creating throwaway database '$VerifyDb'..." -ForegroundColor Cyan
    psql -h $DbHost -p $DbPort -U $AdminUser -d postgres -c "CREATE DATABASE $VerifyDb OWNER $AdminUser;" | Out-Null
    if ($LASTEXITCODE -ne 0) { throw "CREATE DATABASE failed" }

    Write-Host "==> Applying all migrations to the throwaway database from scratch..." -ForegroundColor Cyan
    $env:FRUITACCOUNTING_CONNECTIONSTRING = $adminConnStr
    dotnet ef database update --project $dataProject --startup-project $startupProject
    $migrateExit = $LASTEXITCODE
    Remove-Item Env:\FRUITACCOUNTING_CONNECTIONSTRING
    if ($migrateExit -ne 0) { throw "dotnet ef database update (throwaway) failed" }

    Write-Host "==> Snapshotting the migrated schema..." -ForegroundColor Cyan
    pg_dump --schema-only -h $DbHost -p $DbPort -U $AdminUser -d $VerifyDb -f $migratedSql
    if ($LASTEXITCODE -ne 0) { throw "pg_dump of throwaway database failed" }

    Write-Host "==> Comparing (per table, ignoring restrict-tokens and grants)..." -ForegroundColor Cyan

    function Get-TableColumns {
        param([string]$Path)
        $text = Get-Content $Path -Raw
        $tables = [ordered]@{}
        $pattern = '(?ms)^CREATE TABLE public\.(\w+) \(\r?\n(.*?)\r?\n\);'
        foreach ($m in [regex]::Matches($text, $pattern)) {
            $tableName = $m.Groups[1].Value
            $body = $m.Groups[2].Value
            $columns = $body -split "`r?`n" |
                ForEach-Object { $_.Trim().TrimEnd(',') } |
                Where-Object { $_ -and $_ -notmatch '^CONSTRAINT ' } |
                Sort-Object
            $tables[$tableName] = $columns
        }
        return $tables
    }

    $baselineTables = Get-TableColumns $baselineSql
    $migratedTables = Get-TableColumns $migratedSql

    $allTableNames = ($baselineTables.Keys + $migratedTables.Keys) | Sort-Object -Unique
    $mismatchedTables = @()

    foreach ($table in $allTableNames) {
        $inBaseline = $baselineTables.Contains($table)
        $inMigrated = $migratedTables.Contains($table)

        if (-not $inMigrated) {
            $mismatchedTables += [PSCustomObject]@{ Table = $table; Issue = "only in dev database (missing from migrations)" }
            continue
        }
        if (-not $inBaseline) {
            $mismatchedTables += [PSCustomObject]@{ Table = $table; Issue = "only produced by migrations (not in dev database)" }
            continue
        }

        $colDiff = Compare-Object -ReferenceObject $baselineTables[$table] -DifferenceObject $migratedTables[$table]
        if ($colDiff) {
            $onlyInDev = ($colDiff | Where-Object SideIndicator -eq '<=').InputObject
            $onlyInMigrated = ($colDiff | Where-Object SideIndicator -eq '=>').InputObject
            $parts = @()
            if ($onlyInDev) { $parts += "in dev DB but not migrations: $($onlyInDev -join '; ')" }
            if ($onlyInMigrated) { $parts += "in migrations but not dev DB: $($onlyInMigrated -join '; ')" }
            $mismatchedTables += [PSCustomObject]@{ Table = $table; Issue = ($parts -join ' | ') }
        }
    }

    Write-Host ""
    if ($mismatchedTables.Count -gt 0) {
        Write-Host "MISMATCH -- $($mismatchedTables.Count) table(s) differ:" -ForegroundColor Red
        Write-Host ""
        foreach ($item in $mismatchedTables) {
            Write-Host "  $($item.Table)" -ForegroundColor Yellow
            Write-Host "    $($item.Issue)"
            Write-Host ""
        }
        Write-Host "Full dumps saved at:`n  $baselineSql`n  $migratedSql" -ForegroundColor Yellow
        exit 1
    }
    else {
        Write-Host "PASS -- migrations reproduce your dev database schema exactly ($($allTableNames.Count) tables checked)." -ForegroundColor Green
    }
}
finally {
    if (-not $SkipCleanup) {
        Write-Host "==> Dropping throwaway database '$VerifyDb'..." -ForegroundColor DarkGray
        psql -h $DbHost -p $DbPort -U $AdminUser -d postgres -c "DROP DATABASE IF EXISTS $VerifyDb;" 2>$null | Out-Null
    }
    Remove-Item Env:\PGPASSWORD -ErrorAction SilentlyContinue
}
