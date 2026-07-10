<#
.SYNOPSIS
  Applies EF Core migrations and verifies they reproduce the dev database schema exactly.

.DESCRIPTION
  1. (Optional) Creates a new migration and applies it to your real dev database.
  2. Snapshots your dev database schema.
  3. Spins up a throwaway Postgres container, applies ALL migrations to it from scratch.
  4. Snapshots that schema and diffs it against your dev database.
  5. Reports PASS/FAIL and cleans up the throwaway container.

  Run it with no arguments any time you just want to check your migrations are in sync.
  Pass -AddMigration <Name> when you've added new functionality and need a new migration.

.EXAMPLE
  .\Verify-DbMigrations.ps1

.EXAMPLE
  .\Verify-DbMigrations.ps1 -AddMigration "AddAccountMaster"
#>

[CmdletBinding()]
param(
    [string]$AddMigration,
    [string]$DevHost = "localhost",
    [int]$DevPort = 5432,
    [string]$DevDb = "fruit_accounting_dev",
    [string]$DevUser = "fa_app",
    [string]$DevPassword = "root",
    [int]$TestPort = 5433,
    [string]$TestDb = "fruit_accounting_test",
    [switch]$SkipCleanup
)

$ErrorActionPreference = "Stop"

$repoRoot = Split-Path -Parent $PSScriptRoot
$dataProject = Join-Path $repoRoot "FruitAccounting.Data"
$startupProject = Join-Path $repoRoot "FruitAccounting.UI"
$outDir = Join-Path $PSScriptRoot ".db-check-output"
New-Item -ItemType Directory -Force -Path $outDir | Out-Null
$baselineSql = Join-Path $outDir "schema_dev_baseline.sql"
$migratedSql = Join-Path $outDir "schema_from_migrations.sql"
$containerName = "fa-migration-verify"

function Assert-Command {
    param([string]$Name)
    if (-not (Get-Command $Name -ErrorAction SilentlyContinue)) {
        throw "'$Name' not found on PATH. Install it before running this script."
    }
}

Assert-Command dotnet
Assert-Command docker
Assert-Command pg_dump

try {
    if ($AddMigration) {
        Write-Host "==> Creating migration '$AddMigration'..." -ForegroundColor Cyan
        dotnet ef migrations add $AddMigration --project $dataProject --startup-project $startupProject
        if ($LASTEXITCODE -ne 0) { throw "dotnet ef migrations add failed" }

        Write-Host "==> Applying it to your dev database ($DevDb)..." -ForegroundColor Cyan
        dotnet ef database update --project $dataProject --startup-project $startupProject
        if ($LASTEXITCODE -ne 0) { throw "dotnet ef database update (dev) failed" }
    }

    Write-Host "==> Snapshotting current schema on ${DevHost}:${DevPort}/${DevDb}..." -ForegroundColor Cyan
    $env:PGPASSWORD = $DevPassword
    pg_dump --schema-only -h $DevHost -p $DevPort -U $DevUser -d $DevDb -f $baselineSql
    if ($LASTEXITCODE -ne 0) { throw "pg_dump of dev database failed" }

    Write-Host "==> Starting throwaway Postgres container on port $TestPort..." -ForegroundColor Cyan
    docker rm -f $containerName 2>$null | Out-Null
    docker run --name $containerName `
        -e POSTGRES_DB=$TestDb -e POSTGRES_USER=$DevUser -e POSTGRES_PASSWORD=$DevPassword `
        -p "${TestPort}:5432" -d postgres:16 | Out-Null

    Write-Host "==> Waiting for it to accept connections..." -ForegroundColor Cyan
    $ready = $false
    for ($i = 0; $i -lt 30; $i++) {
        docker exec $containerName pg_isready -U $DevUser -d $TestDb *> $null
        if ($LASTEXITCODE -eq 0) { $ready = $true; break }
        Start-Sleep -Seconds 1
    }
    if (-not $ready) { throw "Postgres container did not become ready in time" }

    Write-Host "==> Applying all migrations to the throwaway database..." -ForegroundColor Cyan
    $env:FRUITACCOUNTING_CONNECTIONSTRING = "Host=$DevHost;Port=$TestPort;Database=$TestDb;Username=$DevUser;Password=$DevPassword;"
    dotnet ef database update --project $dataProject --startup-project $startupProject
    $migrateExit = $LASTEXITCODE
    Remove-Item Env:\FRUITACCOUNTING_CONNECTIONSTRING
    if ($migrateExit -ne 0) { throw "dotnet ef database update (throwaway) failed" }

    Write-Host "==> Snapshotting the migrated schema..." -ForegroundColor Cyan
    pg_dump --schema-only -h $DevHost -p $TestPort -U $DevUser -d $TestDb -f $migratedSql
    if ($LASTEXITCODE -ne 0) { throw "pg_dump of throwaway database failed" }

    Write-Host "==> Comparing..." -ForegroundColor Cyan
    $stripHeader = {
        param($path)
        Get-Content $path | Where-Object { $_ -notmatch '^-- (Dumped|Started|Completed)' }
    }
    $baseline = & $stripHeader $baselineSql
    $migrated = & $stripHeader $migratedSql
    $diff = Compare-Object -ReferenceObject $baseline -DifferenceObject $migrated

    Write-Host ""
    if ($diff) {
        Write-Host "MISMATCH -- dev database and migrations disagree:" -ForegroundColor Red
        $diff | Format-Table -AutoSize
        Write-Host "Full dumps saved at:`n  $baselineSql`n  $migratedSql" -ForegroundColor Yellow
        exit 1
    }
    else {
        Write-Host "PASS -- migrations reproduce your dev database schema exactly." -ForegroundColor Green
    }
}
finally {
    if (-not $SkipCleanup) {
        Write-Host "==> Cleaning up throwaway container..." -ForegroundColor DarkGray
        docker rm -f $containerName 2>$null | Out-Null
    }
    Remove-Item Env:\PGPASSWORD -ErrorAction SilentlyContinue
}
