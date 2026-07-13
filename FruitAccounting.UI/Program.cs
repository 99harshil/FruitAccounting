using FruitAccounting.Core.services;
using FruitAccounting.Data.Context;
using FruitAccounting.Data.Enums;
using FruitAccounting.Data.Entities;
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
        public static IServiceProvider? ServiceProvider { get; private set; }

        [STAThread]
        static async Task Main()
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
                    services.AddScoped<FinancialYearService>();
                    services.AddScoped<UserPreferencesService>();
                    services.AddScoped<UserService>();
                    services.AddScoped<AccountGroupService>();
                    services.AddScoped<DaybookService>();
                    services.AddScoped<RegionService>();
                    services.AddScoped<AccountService>();
                    services.AddScoped<CountryService>();
                    services.AddScoped<ItemGroupService>();
                    services.AddScoped<ItemCategoryService>();
                    services.AddScoped<ItemService>();
                    services.AddScoped<ItemCountService>();
                    services.AddScoped<ReceiptService>();
                    services.AddScoped<PaymentService>();
                    services.AddScoped<TdsPaymentService>();
                    services.AddScoped<BankReconciliationService>();
                    services.AddScoped<JournalService>();
                    services.AddTransient<LoginForm>();
                    services.AddTransient<Form1>();
                })
                .Build();

            ServiceProvider = host.Services;

            // Warm up EF Core model compilation at startup so first login is instant
            using (var warmupScope = host.Services.CreateScope())
            {
                var contextFactory = warmupScope.ServiceProvider.GetRequiredService<IDbContextFactory<FruitAccountingContext>>();
                using var warmupContext = contextFactory.CreateDbContext();
                _ = warmupContext.Users.AsNoTracking().FirstOrDefault();
            }

            bool continueLoop = true;
            while (continueLoop)
            {
                using var scope = host.Services.CreateScope();

                using var loginForm = scope.ServiceProvider.GetRequiredService<LoginForm>();
                if (loginForm.ShowDialog() != DialogResult.OK)
                {
                    continueLoop = false;
                    break;   // user cancelled — exit cleanly
                }

                var loggedInUser = loginForm.LoggedInUser!;
                var financialYearService = scope.ServiceProvider.GetRequiredService<FinancialYearService>();
                var userPreferencesService = scope.ServiceProvider.GetRequiredService<UserPreferencesService>();
                var appContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<FruitAccountingContext>>();

                // Get user's first company from database
                using var context = appContextFactory.CreateDbContext();
                var userCompany = await context.Companies.AsNoTracking().FirstOrDefaultAsync();

                if (userCompany == null)
                {
                    MessageBox.Show("No company found in database.", "Error");
                    continueLoop = false;
                    break;
                }

                using var fyForm = new FinancialYearManagementForm(financialYearService, userCompany);
                if (fyForm.ShowDialog() != DialogResult.OK)
                    continue;   // user cancelled — show login again

                var selectedFinancialYearId = fyForm.SelectedFinancialYearId;
                var selectedFinancialYear = await financialYearService.GetFinancialYearAsync(selectedFinancialYearId);

                if (selectedFinancialYear == null)
                {
                    MessageBox.Show("Error loading financial year.", "Error");
                    continue;
                }

                // Show main shell
                using var mainShell = new MainShell(userPreferencesService, loggedInUser, selectedFinancialYear, userCompany);
                if (mainShell.ShowDialog() != DialogResult.OK)
                    continue;   // User logged off — show login again
            }
        }
    }
}
