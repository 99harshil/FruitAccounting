using FruitAccounting.Core.services;
using FruitAccounting.Data.Context;
using FruitAccounting.Data.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace FruitAccounting.UI
{
    internal static class Program
    {
        public static string ConnectionString { get; private set; } = string.Empty;

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            ConnectionString = config.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException(
                    "DefaultConnection missing from appsettings.json");

            var dataSourceBuilder = new NpgsqlDataSourceBuilder(ConnectionString);
            dataSourceBuilder.MapEnum<VoucherType>("voucher_type");
            dataSourceBuilder.MapEnum<UserRole>("user_role");
            dataSourceBuilder.MapEnum<PurchaseMode>("purchase_mode");
            dataSourceBuilder.MapEnum<DrCr>("dr_cr");
            dataSourceBuilder.MapEnum<PaymentMode>("payment_mode");
            dataSourceBuilder.MapEnum<CrateTxnType>("crate_txn_type");
            dataSourceBuilder.MapEnum<ColdTxnType>("cold_txn_type");
            dataSourceBuilder.MapEnum<LotOpType>("lot_op_type");
            dataSourceBuilder.MapEnum<DispatchStatus>("dispatch_status");
            using var dataSource = dataSourceBuilder.Build();

            using var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton(dataSource);
                    services.AddDbContextFactory<FruitAccountingContext>((sp, options) =>
                        options.UseNpgsql(sp.GetRequiredService<NpgsqlDataSource>()));
                    services.AddScoped<AuthService>();
                    services.AddTransient<LoginForm>();
                    services.AddTransient<Form1>();
                })
                .Build();

            // Warm up EF Core model compilation at startup so first login is instant
            using (var warmupScope = host.Services.CreateScope())
            {
                var contextFactory = warmupScope.ServiceProvider.GetRequiredService<IDbContextFactory<FruitAccountingContext>>();
                using var warmupContext = contextFactory.CreateDbContext();
                _ = warmupContext.Users.AsNoTracking().FirstOrDefault();
            }

            using var scope = host.Services.CreateScope();

            using var loginForm = scope.ServiceProvider.GetRequiredService<LoginForm>();
            if (loginForm.ShowDialog() != DialogResult.OK)
                return;   // user cancelled — exit cleanly

            var loggedInUser = loginForm.LoggedInUser!;

            // Main shell comes next (Phase 1 Step 2)
            // For now, prove login works:
            MessageBox.Show(
                $"Welcome, {loggedInUser.DisplayName}!\nRole: {loggedInUser.Role}",
                "Login Successful");
        }
    }
}
