using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;
using FruitAccounting.Data.Enums;
using FruitAccounting.Data.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.UI
{
    public partial class  MainShell : Form
    {
        private readonly UserPreferencesService _preferencesService;
        private readonly User _loggedInUser;
        private readonly FinancialYear _financialYear;
        private readonly Company _company;
        private ToolStrip? _toolbar;
        private Dictionary<string, Form> _openForms = new();

        public MainShell(UserPreferencesService preferencesService, User loggedInUser, FinancialYear financialYear, Company company)
        {
            InitializeComponent();
            _preferencesService = preferencesService;
            _loggedInUser = loggedInUser;
            _financialYear = financialYear;
            _company = company;

            KeyPreview = true;
            KeyDown += MainShell_KeyDown;
        }

        private async void MainShell_Load(object sender, EventArgs e)
        {
            UpdateTitleBar();
            CreateMenuBar();
            UpdateStatusBar();
            ApplyRoleBasedMenuVisibility();
        }

        private void UpdateTitleBar()
        {
            Text = $"Fruit Accounting System | FY {_financialYear.Code} | {_loggedInUser.DisplayName} ({_loggedInUser.Role})";
        }

        private void CreateMenuBar()
        {
            // Clear existing menu items (except the top-level menus from Designer)
            menuItemMain.HideDropDown();
            menuItemMaster.HideDropDown();
            menuItemAccount.HideDropDown();
            menuItemDomestic.HideDropDown();
            menuItemDesavar.HideDropDown();
            menuItemUtility.HideDropDown();
            menuItemExit.HideDropDown();

            menuItemMain.DropDownItems.Clear();
            menuItemMaster.DropDownItems.Clear();
            menuItemAccount.DropDownItems.Clear();
            menuItemDomestic.DropDownItems.Clear();
            menuItemDesavar.DropDownItems.Clear();
            menuItemUtility.DropDownItems.Clear();
            menuItemExit.DropDownItems.Clear();

            // Main Menu
            var userMgrMenu = new ToolStripMenuItem("User Manager");
            userMgrMenu.DropDownItems.Add("Create User", null, (s, e) => ShowForm("CreateUser"));
            userMgrMenu.DropDownItems.Add("User Rights", null, (s, e) => ShowForm("UserRights"));
            userMgrMenu.DropDownItems.Add("Delete User", null, (s, e) => ShowForm("DeleteUser"));
            userMgrMenu.DropDownItems.Add("Company Rights", null, (s, e) => ShowForm("CompanyRights"));
            menuItemMain.DropDownItems.Add(userMgrMenu);
            menuItemMain.DropDownItems.Add(new ToolStripSeparator());
            menuItemMain.DropDownItems.Add("Log Off", null, (s, e) => LogOff());

            // Master
            var accountSubMenu = new ToolStripMenuItem("Account");
            accountSubMenu.DropDownItems.Add("Main Group", null, (s, e) => ShowForm("MainGroup"));
            accountSubMenu.DropDownItems.Add("Sub Group", null, (s, e) => ShowForm("SubGroup"));
            accountSubMenu.DropDownItems.Add("Account", null, (s, e) => ShowForm("Account"));
            accountSubMenu.DropDownItems.Add("Daybook", null, (s, e) => ShowForm("Daybook"));
            menuItemMaster.DropDownItems.Add(accountSubMenu);

            var productSubMenu = new ToolStripMenuItem("Product");
            productSubMenu.DropDownItems.Add("Group", null, (s, e) => ShowForm("ProductGroup"));
            productSubMenu.DropDownItems.Add("Category", null, (s, e) => ShowForm("ProductCategory"));
            productSubMenu.DropDownItems.Add("Item", null, (s, e) => ShowForm("Item"));
            productSubMenu.DropDownItems.Add("Count", null, (s, e) => ShowForm("ItemCount"));
            menuItemMaster.DropDownItems.Add(productSubMenu);

            menuItemMaster.DropDownItems.Add("Region", null, (s, e) => ShowForm("Region"));
            menuItemMaster.DropDownItems.Add("Country", null, (s, e) => ShowForm("Country"));

            // Account
            var receiptSubMenu = new ToolStripMenuItem("Receipt");
            receiptSubMenu.DropDownItems.Add("Cash", null, (s, e) => ShowForm("CashReceipt"));
            receiptSubMenu.DropDownItems.Add("Bank", null, (s, e) => ShowForm("BankReceipt"));
            menuItemAccount.DropDownItems.Add(receiptSubMenu);

            var paymentSubMenu = new ToolStripMenuItem("Payment");
            paymentSubMenu.DropDownItems.Add("Cash", null, (s, e) => ShowForm("CashPayment"));
            paymentSubMenu.DropDownItems.Add("Bank", null, (s, e) => ShowForm("BankPayment"));
            paymentSubMenu.DropDownItems.Add("TDS Payment", null, (s, e) => ShowForm("TDSPayment"));
            paymentSubMenu.DropDownItems.Add("Freight Payment", null, (s, e) => ShowForm("FreightPayment"));
            menuItemAccount.DropDownItems.Add(paymentSubMenu);

            menuItemAccount.DropDownItems.Add("Journal", null, (s, e) => ShowForm("Journal"));
            menuItemAccount.DropDownItems.Add("Bank Reconciliation", null, (s, e) => ShowForm("BankReconciliation"));
            menuItemAccount.DropDownItems.Add("Expense Entry", null, (s, e) => ShowForm("ExpenseEntry"));
            menuItemAccount.DropDownItems.Add("TDS Return", null, (s, e) => ShowForm("TDSReturn"));

            var accountReportsSubMenu = new ToolStripMenuItem("Reports");
            accountReportsSubMenu.DropDownItems.Add("Bank Register", null, (s, e) => ShowForm("BankRegister"));
            var cashRegisterSubMenu = new ToolStripMenuItem("Cash Register");
            cashRegisterSubMenu.DropDownItems.Add("All", null, (s, e) => ShowForm("CashRegisterAll"));
            cashRegisterSubMenu.DropDownItems.Add("User Wise", null, (s, e) => ShowForm("CashRegisterUserWise"));
            accountReportsSubMenu.DropDownItems.Add(cashRegisterSubMenu);
            accountReportsSubMenu.DropDownItems.Add("Journal Register", null, (s, e) => ShowForm("JournalRegister"));
            menuItemAccount.DropDownItems.Add(accountReportsSubMenu);

            // Domestic
            menuItemDomestic.DropDownItems.Add("Purchase", null, (s, e) => ShowForm("Purchase"));
            menuItemDomestic.DropDownItems.Add("Sales", null, (s, e) => ShowForm("Sales"));

            var crateSubMenu = new ToolStripMenuItem("Crate");
            crateSubMenu.DropDownItems.Add("Receipt", null, (s, e) => ShowForm("CrateReceipt"));
            crateSubMenu.DropDownItems.Add("Delivery", null, (s, e) => ShowForm("CrateDelivery"));
            crateSubMenu.DropDownItems.Add("Crate Amount Conversion", null, (s, e) => ShowForm("CrateAmountConversion"));
            menuItemDomestic.DropDownItems.Add(crateSubMenu);

            menuItemDomestic.DropDownItems.Add("Lot Split", null, (s, e) => ShowForm("LotSplit"));
            menuItemDomestic.DropDownItems.Add("Lot Transfer", null, (s, e) => ShowForm("LotTransfer"));
            menuItemDomestic.DropDownItems.Add("Lot Merge", null, (s, e) => ShowForm("LotMerge"));
            menuItemDomestic.DropDownItems.Add("Cold Store", null, (s, e) => ShowForm("ColdStore"));

            var domesticReportsSubMenu = new ToolStripMenuItem("Reports");
            domesticReportsSubMenu.DropDownItems.Add("Stock", null, (s, e) => ShowForm("Stock"));
            domesticReportsSubMenu.DropDownItems.Add("Chithi", null, (s, e) => ShowForm("Chithi"));
            domesticReportsSubMenu.DropDownItems.Add("Chitha", null, (s, e) => ShowForm("Chitha"));
            menuItemDomestic.DropDownItems.Add(domesticReportsSubMenu);

            // Desavar
            menuItemDesavar.DropDownItems.Add("Sales", null, (s, e) => ShowForm("DesavarSales"));
            var desavarReportsSubMenu = new ToolStripMenuItem("Reports");
            desavarReportsSubMenu.DropDownItems.Add("Register", null, (s, e) => ShowForm("DesavarRegister"));
            menuItemDesavar.DropDownItems.Add(desavarReportsSubMenu);

            // Utility
            var calcItem = new ToolStripMenuItem("Calculator", null, (s, e) => ShowCalculator());
            calcItem.ShortcutKeys = Keys.F8;
            menuItemUtility.DropDownItems.Add(calcItem);
            menuItemUtility.DropDownItems.Add("Company Change", null, (s, e) => ShowForm("CompanyChange"));
            menuItemUtility.DropDownItems.Add("New Year", null, (s, e) => ShowForm("NewYear"));
            menuItemUtility.DropDownItems.Add("Data Check", null, (s, e) => ShowForm("DataCheck"));
            menuItemUtility.DropDownItems.Add("Account Merging", null, (s, e) => ShowForm("AccountMerging"));
            menuItemUtility.DropDownItems.Add("Pending Cheque", null, (s, e) => ShowForm("PendingCheque"));
            menuItemUtility.DropDownItems.Add("OHCS Update", null, (s, e) => ShowForm("OHCSUpdate"));
            menuItemUtility.DropDownItems.Add("RD Update", null, (s, e) => ShowForm("RDUpdate"));
            menuItemUtility.DropDownItems.Add("Stock Set", null, (s, e) => ShowForm("StockSet"));
            menuItemUtility.DropDownItems.Add("Activity Report", null, (s, e) => ShowForm("ActivityReport"));

            // Exit
            menuItemExit.Click += (s, e) => ExitApplication();
        }

        private async Task CreateToolbar()
        {
            _toolbar = new ToolStrip
            {
                AutoSize = false,
                Height = 70,
                Padding = new Padding(5),
                ImageScalingSize = new Size(32, 32)
            };

            var buttons = await _preferencesService.GetToolbarButtonsAsync(_loggedInUser.UserId);

            foreach (var buttonName in buttons)
            {
                if (buttonName == "+")
                {
                    _toolbar.Items.Add(new ToolStripSeparator());
                    var customizeBtn = new ToolStripButton("+", null, (s, e) => CustomizeToolbar())
                    {
                        Font = new Font("Segoe UI", 11, FontStyle.Bold),
                        DisplayStyle = ToolStripItemDisplayStyle.Text,
                        AutoSize = false,
                        Width = 45,
                        Height = 40,
                        TextImageRelation = TextImageRelation.ImageAboveText,
                        ToolTipText = "Customize Toolbar"
                    };
                    _toolbar.Items.Add(customizeBtn);
                }
                else
                {
                    var btn = new ToolStripButton(buttonName, null, (s, e) => ToolbarButtonClicked(buttonName))
                    {
                        Font = new Font("Segoe UI", 10, FontStyle.Regular),
                        DisplayStyle = ToolStripItemDisplayStyle.Text,
                        AutoSize = false,
                        Width = 110,
                        Height = 60,
                        TextAlign = ContentAlignment.MiddleCenter,
                        TextImageRelation = TextImageRelation.ImageAboveText
                    };
                    _toolbar.Items.Add(btn);
                }
            }

            Controls.Add(_toolbar);
        }

        private void UpdateStatusBar()
        {
            lblStatusCompany.Text = $"{_company.Name}--{_financialYear.Code}";
            lblStatusUser.Text = _loggedInUser.DisplayName;
            lblStatusDateTime.Text = $"{_financialYear.StartDate:dd-MMM-yyyy} to {_financialYear.EndDate:dd-MMM-yyyy}";
        }

        private void ApplyRoleBasedMenuVisibility()
        {
            // Implement role-based menu visibility
            // For now, keep all menus visible
            // In future, hide menus based on user role
            if (_loggedInUser.Role == UserRole.Readonly)
            {
                // Hide entry menus, show only reports
            }
        }

        private void ToolbarButtonClicked(string buttonName)
        {
            ShowForm(buttonName);
        }

        private async void CustomizeToolbar()
        {
            var buttons = await _preferencesService.GetToolbarButtonsAsync(_loggedInUser.UserId);
            using var form = new ToolbarCustomizationForm(_preferencesService, _loggedInUser.UserId, buttons);
            if (form.ShowDialog() == DialogResult.OK)
            {
                // Recreate toolbar
                if (_toolbar != null)
                {
                    Controls.Remove(_toolbar);
                    _toolbar.Dispose();
                }
                await CreateToolbar();
            }
        }

        private void ShowCalculator()
        {
            try
            {
                System.Diagnostics.Process.Start("calc.exe");
            }
            catch
            {
                MessageBox.Show("Unable to open calculator.", "Error");
            }
        }

        private void ShowForm(string formKey)
        {
            if (_openForms.ContainsKey(formKey) && !_openForms[formKey].IsDisposed)
            {
                _openForms[formKey].BringToFront();
                return;
            }

            Form? newForm = null;
            switch (formKey)
            {
                // User Manager forms
                case "CreateUser":
                    var userService = Program.ServiceProvider?.GetService(typeof(UserService)) as UserService;
                    if (userService != null)
                        newForm = new CreateUserForm(userService);
                    break;
                case "UserRights":
                    userService = Program.ServiceProvider?.GetService(typeof(UserService)) as UserService;
                    if (userService != null)
                        newForm = new UserRightsForm(userService);
                    break;
                case "DeleteUser":
                    userService = Program.ServiceProvider?.GetService(typeof(UserService)) as UserService;
                    if (userService != null)
                        newForm = new DeleteUserForm(userService);
                    break;
                case "CompanyRights":
                    userService = Program.ServiceProvider?.GetService(typeof(UserService)) as UserService;
                    var contextFactory = Program.ServiceProvider?.GetService(typeof(IDbContextFactory<FruitAccountingContext>)) as IDbContextFactory<FruitAccountingContext>;
                    if (userService != null && contextFactory != null)
                        newForm = new CompanyRightsForm(userService, contextFactory);
                    break;
                // Master forms
                case "MainGroup":
                    var accountGroupService = Program.ServiceProvider?.GetService(typeof(AccountGroupService)) as AccountGroupService;
                    if (accountGroupService != null)
                        newForm = new MainGroupForm(accountGroupService, _company.CompanyId);
                    break;
                case "SubGroup":
                    accountGroupService = Program.ServiceProvider?.GetService(typeof(AccountGroupService)) as AccountGroupService;
                    if (accountGroupService != null)
                        newForm = new SubGroupForm(accountGroupService, _company.CompanyId);
                    break;
                case "Account":
                    var accountService = Program.ServiceProvider?.GetService(typeof(AccountService)) as AccountService;
                    accountGroupService = Program.ServiceProvider?.GetService(typeof(AccountGroupService)) as AccountGroupService;
                    var regionServiceForAccount = Program.ServiceProvider?.GetService(typeof(RegionService)) as RegionService;
                    if (accountService != null && accountGroupService != null && regionServiceForAccount != null)
                        newForm = new AccountForm(accountService, accountGroupService, regionServiceForAccount, _company.CompanyId);
                    break;
                case "Country":
                    var countryService = Program.ServiceProvider?.GetService(typeof(CountryService)) as CountryService;
                    if (countryService != null)
                        newForm = new CountryForm(countryService, _company.CompanyId);
                    break;
                case "ProductGroup":
                    var itemGroupService = Program.ServiceProvider?.GetService(typeof(ItemGroupService)) as ItemGroupService;
                    if (itemGroupService != null)
                        newForm = new ItemGroupForm(itemGroupService, _company.CompanyId);
                    break;
                case "ProductCategory":
                    var itemCategoryService = Program.ServiceProvider?.GetService(typeof(ItemCategoryService)) as ItemCategoryService;
                    if (itemCategoryService != null)
                        newForm = new ItemCategoryForm(itemCategoryService, _company.CompanyId);
                    break;
                case "Item":
                    var itemService = Program.ServiceProvider?.GetService(typeof(ItemService)) as ItemService;
                    itemGroupService = Program.ServiceProvider?.GetService(typeof(ItemGroupService)) as ItemGroupService;
                    itemCategoryService = Program.ServiceProvider?.GetService(typeof(ItemCategoryService)) as ItemCategoryService;
                    if (itemService != null && itemGroupService != null && itemCategoryService != null)
                        newForm = new ItemForm(itemService, itemGroupService, itemCategoryService, _company.CompanyId);
                    break;
                case "ItemCount":
                    var itemCountService = Program.ServiceProvider?.GetService(typeof(ItemCountService)) as ItemCountService;
                    itemGroupService = Program.ServiceProvider?.GetService(typeof(ItemGroupService)) as ItemGroupService;
                    if (itemCountService != null && itemGroupService != null)
                        newForm = new ItemCountForm(itemCountService, itemGroupService, _company.CompanyId);
                    break;
                case "Daybook":
                    var daybookService = Program.ServiceProvider?.GetService(typeof(DaybookService)) as DaybookService;
                    var accountServiceForDaybook = Program.ServiceProvider?.GetService(typeof(AccountService)) as AccountService;
                    var accountGroupServiceForDaybook = Program.ServiceProvider?.GetService(typeof(AccountGroupService)) as AccountGroupService;
                    var regionServiceForDaybook = Program.ServiceProvider?.GetService(typeof(RegionService)) as RegionService;
                    if (daybookService != null && accountServiceForDaybook != null && accountGroupServiceForDaybook != null && regionServiceForDaybook != null)
                        newForm = new DaybookForm(daybookService, accountServiceForDaybook, accountGroupServiceForDaybook, regionServiceForDaybook, _company.CompanyId);
                    break;
                case "Region":
                    var regionService = Program.ServiceProvider?.GetService(typeof(RegionService)) as RegionService;
                    if (regionService != null)
                        newForm = new RegionForm(regionService, _company.CompanyId);
                    break;
                // Transaction forms
                case "Purchase":
                    newForm = new Form { Text = "Purchase Entry", MdiParent = this };
                    break;
                case "Sales":
                    newForm = new Form { Text = "Sales Entry", MdiParent = this };
                    break;
                case "Stock":
                    newForm = new Form { Text = "Stock Report", MdiParent = this };
                    break;
                case "Chithi":
                    newForm = new Form { Text = "Chithi Report", MdiParent = this };
                    break;
                case "Chitha":
                    newForm = new Form { Text = "Chitha Report", MdiParent = this };
                    break;
                case "CashPayment":
                    var paymentServiceCash = Program.ServiceProvider?.GetService(typeof(PaymentService)) as PaymentService;
                    var accountServiceForPayCash = Program.ServiceProvider?.GetService(typeof(AccountService)) as AccountService;
                    var daybookServiceForPayCash = Program.ServiceProvider?.GetService(typeof(DaybookService)) as DaybookService;
                    var accountGroupServiceForPayCash = Program.ServiceProvider?.GetService(typeof(AccountGroupService)) as AccountGroupService;
                    var regionServiceForPayCash = Program.ServiceProvider?.GetService(typeof(RegionService)) as RegionService;
                    if (paymentServiceCash != null && accountServiceForPayCash != null && daybookServiceForPayCash != null && accountGroupServiceForPayCash != null && regionServiceForPayCash != null)
                        newForm = new PaymentForm(paymentServiceCash, accountServiceForPayCash, daybookServiceForPayCash, accountGroupServiceForPayCash, regionServiceForPayCash,
                            _company.CompanyId, _financialYear.FinancialYearId, 'C', _loggedInUser.UserId);
                    break;
                case "CashReceipt":
                    var receiptServiceCash = Program.ServiceProvider?.GetService(typeof(ReceiptService)) as ReceiptService;
                    var accountServiceCash = Program.ServiceProvider?.GetService(typeof(AccountService)) as AccountService;
                    var daybookServiceCash = Program.ServiceProvider?.GetService(typeof(DaybookService)) as DaybookService;
                    var accountGroupServiceCash = Program.ServiceProvider?.GetService(typeof(AccountGroupService)) as AccountGroupService;
                    var regionServiceCash = Program.ServiceProvider?.GetService(typeof(RegionService)) as RegionService;
                    if (receiptServiceCash != null && accountServiceCash != null && daybookServiceCash != null && accountGroupServiceCash != null && regionServiceCash != null)
                        newForm = new ReceiptForm(receiptServiceCash, accountServiceCash, daybookServiceCash, accountGroupServiceCash, regionServiceCash,
                            _company.CompanyId, _financialYear.FinancialYearId, 'C', _loggedInUser.UserId);
                    break;
                case "BankPayment":
                    var paymentServiceBank = Program.ServiceProvider?.GetService(typeof(PaymentService)) as PaymentService;
                    var accountServiceForPayBank = Program.ServiceProvider?.GetService(typeof(AccountService)) as AccountService;
                    var daybookServiceForPayBank = Program.ServiceProvider?.GetService(typeof(DaybookService)) as DaybookService;
                    var accountGroupServiceForPayBank = Program.ServiceProvider?.GetService(typeof(AccountGroupService)) as AccountGroupService;
                    var regionServiceForPayBank = Program.ServiceProvider?.GetService(typeof(RegionService)) as RegionService;
                    if (paymentServiceBank != null && accountServiceForPayBank != null && daybookServiceForPayBank != null && accountGroupServiceForPayBank != null && regionServiceForPayBank != null)
                        newForm = new PaymentForm(paymentServiceBank, accountServiceForPayBank, daybookServiceForPayBank, accountGroupServiceForPayBank, regionServiceForPayBank,
                            _company.CompanyId, _financialYear.FinancialYearId, 'B', _loggedInUser.UserId);
                    break;
                case "TDSPayment":
                    var tdsPaymentService = Program.ServiceProvider?.GetService(typeof(TdsPaymentService)) as TdsPaymentService;
                    var accountServiceForTds = Program.ServiceProvider?.GetService(typeof(AccountService)) as AccountService;
                    var daybookServiceForTds = Program.ServiceProvider?.GetService(typeof(DaybookService)) as DaybookService;
                    var accountGroupServiceForTds = Program.ServiceProvider?.GetService(typeof(AccountGroupService)) as AccountGroupService;
                    var regionServiceForTds = Program.ServiceProvider?.GetService(typeof(RegionService)) as RegionService;
                    if (tdsPaymentService != null && accountServiceForTds != null && daybookServiceForTds != null && accountGroupServiceForTds != null && regionServiceForTds != null)
                        newForm = new TdsPaymentForm(tdsPaymentService, accountServiceForTds, daybookServiceForTds, accountGroupServiceForTds, regionServiceForTds,
                            _company.CompanyId, _financialYear.FinancialYearId, _loggedInUser.UserId);
                    break;
                case "BankReconciliation":
                    var bankReconciliationService = Program.ServiceProvider?.GetService(typeof(BankReconciliationService)) as BankReconciliationService;
                    if (bankReconciliationService != null)
                        newForm = new BankReconciliationForm(bankReconciliationService, _company.CompanyId, _financialYear.FinancialYearId);
                    break;
                case "Journal":
                    var journalService = Program.ServiceProvider?.GetService(typeof(JournalService)) as JournalService;
                    var accountServiceForJournal = Program.ServiceProvider?.GetService(typeof(AccountService)) as AccountService;
                    if (journalService != null && accountServiceForJournal != null)
                        newForm = new JournalForm(journalService, accountServiceForJournal,
                            _company.CompanyId, _financialYear.FinancialYearId, _loggedInUser.UserId);
                    break;
                case "BankReceipt":
                    var receiptServiceBank = Program.ServiceProvider?.GetService(typeof(ReceiptService)) as ReceiptService;
                    var accountServiceBank = Program.ServiceProvider?.GetService(typeof(AccountService)) as AccountService;
                    var daybookServiceBank = Program.ServiceProvider?.GetService(typeof(DaybookService)) as DaybookService;
                    var accountGroupServiceBank = Program.ServiceProvider?.GetService(typeof(AccountGroupService)) as AccountGroupService;
                    var regionServiceBank = Program.ServiceProvider?.GetService(typeof(RegionService)) as RegionService;
                    if (receiptServiceBank != null && accountServiceBank != null && daybookServiceBank != null && accountGroupServiceBank != null && regionServiceBank != null)
                        newForm = new ReceiptForm(receiptServiceBank, accountServiceBank, daybookServiceBank, accountGroupServiceBank, regionServiceBank,
                            _company.CompanyId, _financialYear.FinancialYearId, 'B', _loggedInUser.UserId);
                    break;
                default:
                    newForm = new Form { Text = formKey, MdiParent = this };
                    break;
            }

            if (newForm != null)
            {
                _openForms[formKey] = newForm;
                newForm.FormClosed += (s, e) => _openForms.Remove(formKey);
                if (newForm.MdiParent == null)
                    newForm.ShowDialog();
                else
                    newForm.Show();
            }
        }

        private void LogOff()
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ExitApplication()
        {
            Application.Exit();
        }

        private void MainShell_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.X)
            {
                ExitApplication();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F8)
            {
                ShowCalculator();
                e.Handled = true;
            }
        }
    }
}
