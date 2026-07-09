using FruitAccounting.Core.services;
using FruitAccounting.Data.Context;
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
        using var provider = services.BuildServiceProvider();

        long companyId;
        using (var context = provider.GetRequiredService<IDbContextFactory<FruitAccountingContext>>().CreateDbContext())
        {
            var company = context.Companies.AsNoTracking().OrderBy(c => c.CompanyId).FirstOrDefault();
            if (company == null)
            {
                Console.WriteLine("No company found in the database. Nothing to test against.");
                return;
            }
            companyId = company.CompanyId;
            Console.WriteLine($"Using company: {company.Name} (Id={companyId})\n");
        }

        var menu = new (string Label, Action Open)[]
        {
            ("Main Group",     () => new MainGroupForm(provider.GetRequiredService<AccountGroupService>(), companyId).ShowDialog()),
            ("Sub Group",      () => new SubGroupForm(provider.GetRequiredService<AccountGroupService>(), companyId).ShowDialog()),
            ("Account",        () => new AccountForm(
                                        provider.GetRequiredService<AccountService>(),
                                        provider.GetRequiredService<AccountGroupService>(),
                                        provider.GetRequiredService<RegionService>(),
                                        companyId).ShowDialog()),
            ("Daybook",        () => new DaybookForm(provider.GetRequiredService<DaybookService>(), companyId).ShowDialog()),
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

        while (true)
        {
            Console.WriteLine("Which form do you want to open?");
            for (int i = 0; i < menu.Length; i++)
                Console.WriteLine($"  {i + 1}. {menu[i].Label}");
            Console.WriteLine("  0. Exit");
            Console.Write("> ");

            var input = Console.ReadLine();
            if (input == "0" || string.IsNullOrWhiteSpace(input))
                break;

            if (int.TryParse(input, out int choice) && choice >= 1 && choice <= menu.Length)
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
