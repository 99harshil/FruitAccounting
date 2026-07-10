using System;
using System.IO;
using System.Text.Json;
using FruitAccounting.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql;

namespace FruitAccounting.Data.Context;

public class FruitAccountingContextFactory : IDesignTimeDbContextFactory<FruitAccountingContext>
{
    public FruitAccountingContext CreateDbContext(string[] args)
    {
        // Must mirror the enum registrations in FruitAccounting.UI/Program.cs exactly.
        // Without these, Npgsql doesn't know these columns are native Postgres enum
        // types and models them as plain integer, which produces migrations that
        // would convert real enum columns to integer.
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(ResolveConnectionString());
        dataSourceBuilder.MapEnum<VoucherType>("voucher_type");
        dataSourceBuilder.MapEnum<UserRole>("user_role");
        dataSourceBuilder.MapEnum<PurchaseMode>("purchase_mode");
        dataSourceBuilder.MapEnum<DrCr>("dr_cr");
        dataSourceBuilder.MapEnum<PaymentMode>("payment_mode");
        dataSourceBuilder.MapEnum<CrateTxnType>("crate_txn_type");
        dataSourceBuilder.MapEnum<ColdTxnType>("cold_txn_type");
        dataSourceBuilder.MapEnum<LotOpType>("lot_op_type");
        dataSourceBuilder.MapEnum<DispatchStatus>("dispatch_status");
        var dataSource = dataSourceBuilder.Build();

        var optionsBuilder = new DbContextOptionsBuilder<FruitAccountingContext>();
        optionsBuilder.UseNpgsql(dataSource);
        return new FruitAccountingContext(optionsBuilder.Options);
    }

    private static string ResolveConnectionString()
    {
        var overrideConnectionString = Environment.GetEnvironmentVariable("FRUITACCOUNTING_CONNECTIONSTRING");
        if (!string.IsNullOrWhiteSpace(overrideConnectionString))
            return overrideConnectionString;

        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null && !File.Exists(Path.Combine(dir.FullName, "FruitAccounting.UI", "appsettings.json")))
            dir = dir.Parent;

        if (dir == null)
        {
            throw new InvalidOperationException(
                "Could not locate FruitAccounting.UI/appsettings.json by walking up from the build output. " +
                "Set the FRUITACCOUNTING_CONNECTIONSTRING environment variable instead.");
        }

        var appsettingsPath = Path.Combine(dir.FullName, "FruitAccounting.UI", "appsettings.json");
        using var doc = JsonDocument.Parse(File.ReadAllText(appsettingsPath));

        var connectionString = doc.RootElement
            .GetProperty("ConnectionStrings")
            .GetProperty("DefaultConnection")
            .GetString();

        return connectionString
            ?? throw new InvalidOperationException($"DefaultConnection missing from {appsettingsPath}");
    }
}
