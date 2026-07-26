using FruitAccounting.Core.services;
using FruitAccounting.Data.Context;
using FruitAccounting.Data.Entities;
using FruitAccounting.Data.Enums;
using FruitAccounting.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace FruitAccounting.DevHarness;

// Opens any master form directly against the real database, skipping Login / Company
// Selection / Financial Year Selection. Uses the first company found. For quickly
// checking a form's behavior without clicking through the full app each time.
internal static class Program
{
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("DefaultConnection missing from appsettings.json");

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
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

        var services = new ServiceCollection();
        services.AddSingleton(dataSource);
        services.AddDbContextFactory<FruitAccountingContext>((sp, options) =>
            options.UseNpgsql(sp.GetRequiredService<NpgsqlDataSource>()));
        services.AddScoped<AccountGroupService>();
        services.AddScoped<AccountService>();
        services.AddScoped<RegionService>();
        services.AddScoped<CountryService>();
        services.AddScoped<DaybookService>();
        services.AddScoped<ItemGroupService>();
        services.AddScoped<ItemCategoryService>();
        services.AddScoped<ItemService>();
        services.AddScoped<ItemCountService>();
        services.AddScoped<ReceiptService>();
        services.AddScoped<PaymentService>();
        services.AddScoped<TdsPaymentService>();
        services.AddScoped<BankReconciliationService>();
        services.AddScoped<JournalService>();
        services.AddScoped<LedgerService>();
        services.AddScoped<LotService>();
        services.AddScoped<Tds194QService>();
        services.AddScoped<PurchaseService>();
        services.AddScoped<SalesService>();
        services.AddScoped<CompanyService>();
        services.AddScoped<UserPreferencesService>();
        using var provider = services.BuildServiceProvider();

        // MainShell's ShowForm dispatch and LedgerReportForm's drill-down both resolve services
        // via Program.ServiceProvider rather than through constructor injection - point it at
        // this harness's own container so those code paths work here too.
        FruitAccounting.UI.Program.ServiceProvider = provider;

        long companyId;
        long? financialYearId;
        Company? company;
        FinancialYear? financialYear;
        User? user;
        using (var context = provider.GetRequiredService<IDbContextFactory<FruitAccountingContext>>().CreateDbContext())
        {
            company = context.Companies.AsNoTracking().OrderBy(c => c.CompanyId).FirstOrDefault();
            if (company == null)
            {
                Console.WriteLine("No company found in the database. Nothing to test against.");
                return;
            }
            companyId = company.CompanyId;
            Console.WriteLine($"Using company: {company.Name} (Id={companyId})\n");

            financialYear = context.FinancialYears.AsNoTracking()
                .Where(fy => fy.CompanyId == companyId)
                .OrderByDescending(fy => fy.IsActive)
                .ThenByDescending(fy => fy.FinancialYearId)
                .FirstOrDefault();
            financialYearId = financialYear?.FinancialYearId;
            if (financialYearId == null)
                Console.WriteLine("No financial year found for this company - Receipt forms will be unavailable.\n");

            user = context.Users.AsNoTracking().OrderBy(u => u.UserId).FirstOrDefault();
            if (user == null)
                Console.WriteLine("No user found in the database - Dashboard will be unavailable.\n");
        }

        var menu = new List<(string Label, Action Open)>
        {
            ("Main Group",     () => new MainGroupForm(provider.GetRequiredService<AccountGroupService>(), companyId).ShowDialog()),
            ("Sub Group",      () => new SubGroupForm(provider.GetRequiredService<AccountGroupService>(), companyId).ShowDialog()),
            ("Account",        () => new AccountForm(
                                        provider.GetRequiredService<AccountService>(),
                                        provider.GetRequiredService<AccountGroupService>(),
                                        provider.GetRequiredService<RegionService>(),
                                        companyId).ShowDialog()),
            ("Daybook",        () => new DaybookForm(
                                        provider.GetRequiredService<DaybookService>(),
                                        provider.GetRequiredService<AccountService>(),
                                        provider.GetRequiredService<AccountGroupService>(),
                                        provider.GetRequiredService<RegionService>(),
                                        companyId).ShowDialog()),
            ("Region",         () => new RegionForm(provider.GetRequiredService<RegionService>(), companyId).ShowDialog()),
            ("Country",        () => new CountryForm(provider.GetRequiredService<CountryService>(), companyId).ShowDialog()),
            ("Item Group",     () => new ItemGroupForm(provider.GetRequiredService<ItemGroupService>(), companyId).ShowDialog()),
            ("Item Category",  () => new ItemCategoryForm(provider.GetRequiredService<ItemCategoryService>(), companyId).ShowDialog()),
            ("Item",           () => new ItemForm(
                                        provider.GetRequiredService<ItemService>(),
                                        provider.GetRequiredService<ItemGroupService>(),
                                        provider.GetRequiredService<ItemCategoryService>(),
                                        companyId).ShowDialog()),
            ("Item Count",     () => new ItemCountForm(
                                        provider.GetRequiredService<ItemCountService>(),
                                        provider.GetRequiredService<ItemGroupService>(),
                                        companyId).ShowDialog()),
        };

        if (financialYear != null && user != null)
        {
            menu.Add(("Dashboard (MainShell)", () => new MainShell(
                                        provider.GetRequiredService<UserPreferencesService>(),
                                        user, financialYear, company).ShowDialog()));
        }

        if (financialYearId.HasValue)
        {
            menu.Add(("Cash Receipt", () => new ReceiptForm(
                                        provider.GetRequiredService<ReceiptService>(),
                                        provider.GetRequiredService<AccountService>(),
                                        provider.GetRequiredService<DaybookService>(),
                                        provider.GetRequiredService<AccountGroupService>(),
                                        provider.GetRequiredService<RegionService>(),
                                        companyId, financialYearId.Value, 'C', null).ShowDialog()));
            menu.Add(("Bank Receipt", () => new ReceiptForm(
                                        provider.GetRequiredService<ReceiptService>(),
                                        provider.GetRequiredService<AccountService>(),
                                        provider.GetRequiredService<DaybookService>(),
                                        provider.GetRequiredService<AccountGroupService>(),
                                        provider.GetRequiredService<RegionService>(),
                                        companyId, financialYearId.Value, 'B', null).ShowDialog()));
            menu.Add(("Cash Payment", () => new PaymentForm(
                                        provider.GetRequiredService<PaymentService>(),
                                        provider.GetRequiredService<AccountService>(),
                                        provider.GetRequiredService<DaybookService>(),
                                        provider.GetRequiredService<AccountGroupService>(),
                                        provider.GetRequiredService<RegionService>(),
                                        companyId, financialYearId.Value, 'C', null).ShowDialog()));
            menu.Add(("Bank Payment", () => new PaymentForm(
                                        provider.GetRequiredService<PaymentService>(),
                                        provider.GetRequiredService<AccountService>(),
                                        provider.GetRequiredService<DaybookService>(),
                                        provider.GetRequiredService<AccountGroupService>(),
                                        provider.GetRequiredService<RegionService>(),
                                        companyId, financialYearId.Value, 'B', null).ShowDialog()));
            menu.Add(("TDS Payment", () => new TdsPaymentForm(
                                        provider.GetRequiredService<TdsPaymentService>(),
                                        provider.GetRequiredService<AccountService>(),
                                        provider.GetRequiredService<DaybookService>(),
                                        provider.GetRequiredService<AccountGroupService>(),
                                        provider.GetRequiredService<RegionService>(),
                                        companyId, financialYearId.Value, null).ShowDialog()));
            menu.Add(("Bank Reconciliation", () => new BankReconciliationForm(
                                        provider.GetRequiredService<BankReconciliationService>(),
                                        companyId, financialYearId.Value).ShowDialog()));
            menu.Add(("Journal", () => new JournalForm(
                                        provider.GetRequiredService<JournalService>(),
                                        provider.GetRequiredService<AccountService>(),
                                        companyId, financialYearId.Value, null).ShowDialog()));
            menu.Add(("Purchase", () => new PurchaseForm(
                                        provider.GetRequiredService<PurchaseService>(),
                                        provider.GetRequiredService<AccountService>(),
                                        provider.GetRequiredService<AccountGroupService>(),
                                        provider.GetRequiredService<RegionService>(),
                                        provider.GetRequiredService<ItemService>(),
                                        provider.GetRequiredService<LotService>(),
                                        companyId, financialYearId.Value, null).ShowDialog()));
            menu.Add(("Sales", () => new SalesForm(
                                        provider.GetRequiredService<SalesService>(),
                                        provider.GetRequiredService<AccountService>(),
                                        provider.GetRequiredService<AccountGroupService>(),
                                        provider.GetRequiredService<RegionService>(),
                                        provider.GetRequiredService<CompanyService>(),
                                        companyId, financialYearId.Value, null).ShowDialog()));
            menu.Add(("Stock", () => new StockForm(
                                        provider.GetRequiredService<LotService>(),
                                        provider.GetRequiredService<SalesService>(),
                                        provider.GetRequiredService<AccountService>(),
                                        provider.GetRequiredService<AccountGroupService>(),
                                        provider.GetRequiredService<RegionService>(),
                                        provider.GetRequiredService<CompanyService>(),
                                        companyId, financialYearId.Value, null).ShowDialog()));
            menu.Add(("Company Information", () => new CompanyInfoForm(
                                        provider.GetRequiredService<CompanyService>(),
                                        companyId).ShowDialog()));
            menu.Add(("Ledger (Selected)", () => new LedgerReportForm(
                                        provider.GetRequiredService<LedgerService>(),
                                        provider.GetRequiredService<AccountService>(),
                                        companyId, financialYearId.Value, financialYear!, user?.UserId).ShowDialog()));
        }

        while (true)
        {
            Console.WriteLine("Which form do you want to open?");
            for (int i = 0; i < menu.Count; i++)
                Console.WriteLine($"  {i + 1}. {menu[i].Label}");
            Console.WriteLine("  0. Exit");
            Console.Write("> ");

            var input = Console.ReadLine();
            if (input == "0" || string.IsNullOrWhiteSpace(input))
                break;

            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= menu.Count)
            {
                try
                {
                    menu[choice - 1].Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error opening form: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }

            Console.WriteLine();
        }
    }
}
