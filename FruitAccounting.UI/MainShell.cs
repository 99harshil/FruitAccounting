using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;
using FruitAccounting.Data.Enums;

namespace FruitAccounting.UI
{
    public partial class  MainShell : Form
    {
        private readonly UserPreferencesService _preferencesService;
        private readonly User _loggedInUser;
        private readonly FinancialYear _financialYear;
        private ToolStrip? _toolbar;
        private Dictionary<string, Form> _openForms = new();

        public MainShell(UserPreferencesService preferencesService, User loggedInUser, FinancialYear financialYear)
        {
            InitializeComponent();
            _preferencesService = preferencesService;
            _loggedInUser = loggedInUser;
            _financialYear = financialYear;

            IsMdiContainer = true;
            KeyPreview = true;
            KeyDown += MainShell_KeyDown;
        }

        private async void MainShell_Load(object sender, EventArgs e)
        {
            UpdateTitleBar();
            CreateMenuBar();
            await CreateToolbar();
            CreateStatusBar();
            ApplyRoleBasedMenuVisibility();
        }

        private void UpdateTitleBar()
        {
            Text = $"Fruit Accounting System | FY {_financialYear.Code} | {_loggedInUser.DisplayName} ({_loggedInUser.Role})";
        }

        private void CreateMenuBar()
        {
            var menuStrip = new MenuStrip();

            // Main Menu
            var mainMenu = new ToolStripMenuItem("Main");

            var userMgrMenu = new ToolStripMenuItem("User Manager");
            userMgrMenu.DropDownItems.Add("Create User", null, (s, e) => ShowForm("CreateUser"));
            userMgrMenu.DropDownItems.Add("User Rights", null, (s, e) => ShowForm("UserRights"));
            userMgrMenu.DropDownItems.Add("Delete User", null, (s, e) => ShowForm("DeleteUser"));
            userMgrMenu.DropDownItems.Add("Company Rights", null, (s, e) => ShowForm("CompanyRights"));
            mainMenu.DropDownItems.Add(userMgrMenu);

            mainMenu.DropDownItems.Add(new ToolStripSeparator());
            mainMenu.DropDownItems.Add("Log Off", null, (s, e) => LogOff());

            menuStrip.Items.Add(mainMenu);

            // Master
            var masterMenu = new ToolStripMenuItem("Master");
            var accountSubMenu = new ToolStripMenuItem("Account");
            accountSubMenu.DropDownItems.Add("Main Group", null, (s, e) => ShowForm("MainGroup"));
            accountSubMenu.DropDownItems.Add("Sub Group", null, (s, e) => ShowForm("SubGroup"));
            accountSubMenu.DropDownItems.Add("Account", null, (s, e) => ShowForm("Account"));
            accountSubMenu.DropDownItems.Add("Daybook", null, (s, e) => ShowForm("Daybook"));
            masterMenu.DropDownItems.Add(accountSubMenu);

            var productSubMenu = new ToolStripMenuItem("Product");
            productSubMenu.DropDownItems.Add("Group", null, (s, e) => ShowForm("ProductGroup"));
            productSubMenu.DropDownItems.Add("Category", null, (s, e) => ShowForm("ProductCategory"));
            productSubMenu.DropDownItems.Add("Item", null, (s, e) => ShowForm("Item"));
            productSubMenu.DropDownItems.Add("Count", null, (s, e) => ShowForm("ItemCount"));
            masterMenu.DropDownItems.Add(productSubMenu);

            masterMenu.DropDownItems.Add("Region", null, (s, e) => ShowForm("Region"));
            masterMenu.DropDownItems.Add("Country", null, (s, e) => ShowForm("Country"));
            menuStrip.Items.Add(masterMenu);

            // Account
            var accountMenu = new ToolStripMenuItem("Account");
            var receiptSubMenu = new ToolStripMenuItem("Receipt");
            receiptSubMenu.DropDownItems.Add("Cash", null, (s, e) => ShowForm("CashReceipt"));
            receiptSubMenu.DropDownItems.Add("Bank", null, (s, e) => ShowForm("BankReceipt"));
            accountMenu.DropDownItems.Add(receiptSubMenu);

            var paymentSubMenu = new ToolStripMenuItem("Payment");
            paymentSubMenu.DropDownItems.Add("Cash", null, (s, e) => ShowForm("CashPayment"));
            paymentSubMenu.DropDownItems.Add("Bank", null, (s, e) => ShowForm("BankPayment"));
            paymentSubMenu.DropDownItems.Add("TDS Payment", null, (s, e) => ShowForm("TDSPayment"));
            paymentSubMenu.DropDownItems.Add("Freight Payment", null, (s, e) => ShowForm("FreightPayment"));
            accountMenu.DropDownItems.Add(paymentSubMenu);

            accountMenu.DropDownItems.Add("Journal", null, (s, e) => ShowForm("Journal"));
            accountMenu.DropDownItems.Add("Bank Reconciliation", null, (s, e) => ShowForm("BankReconciliation"));
            accountMenu.DropDownItems.Add("Expense Entry", null, (s, e) => ShowForm("ExpenseEntry"));
            accountMenu.DropDownItems.Add("TDS Return", null, (s, e) => ShowForm("TDSReturn"));

            var accountReportsSubMenu = new ToolStripMenuItem("Reports");
            accountReportsSubMenu.DropDownItems.Add("Bank Register", null, (s, e) => ShowForm("BankRegister"));
            var cashRegisterSubMenu = new ToolStripMenuItem("Cash Register");
            cashRegisterSubMenu.DropDownItems.Add("All", null, (s, e) => ShowForm("CashRegisterAll"));
            cashRegisterSubMenu.DropDownItems.Add("User Wise", null, (s, e) => ShowForm("CashRegisterUserWise"));
            accountReportsSubMenu.DropDownItems.Add(cashRegisterSubMenu);
            accountReportsSubMenu.DropDownItems.Add("Journal Register", null, (s, e) => ShowForm("JournalRegister"));
            accountMenu.DropDownItems.Add(accountReportsSubMenu);

            menuStrip.Items.Add(accountMenu);

            // Domestic
            var domesticMenu = new ToolStripMenuItem("Domestic");
            domesticMenu.DropDownItems.Add("Purchase", null, (s, e) => ShowForm("Purchase"));
            domesticMenu.DropDownItems.Add("Sales", null, (s, e) => ShowForm("Sales"));

            var crateSubMenu = new ToolStripMenuItem("Crate");
            crateSubMenu.DropDownItems.Add("Receipt", null, (s, e) => ShowForm("CrateReceipt"));
            crateSubMenu.DropDownItems.Add("Delivery", null, (s, e) => ShowForm("CrateDelivery"));
            crateSubMenu.DropDownItems.Add("Crate Amount Conversion", null, (s, e) => ShowForm("CrateAmountConversion"));
            domesticMenu.DropDownItems.Add(crateSubMenu);

            domesticMenu.DropDownItems.Add("Lot Split", null, (s, e) => ShowForm("LotSplit"));
            domesticMenu.DropDownItems.Add("Lot Transfer", null, (s, e) => ShowForm("LotTransfer"));
            domesticMenu.DropDownItems.Add("Lot Merge", null, (s, e) => ShowForm("LotMerge"));
            domesticMenu.DropDownItems.Add("Cold Store", null, (s, e) => ShowForm("ColdStore"));

            var domesticReportsSubMenu = new ToolStripMenuItem("Reports");
            domesticReportsSubMenu.DropDownItems.Add("Stock", null, (s, e) => ShowForm("Stock"));
            domesticMenu.DropDownItems.Add(domesticReportsSubMenu);

            menuStrip.Items.Add(domesticMenu);

            // Desavar
            var desavarMenu = new ToolStripMenuItem("Desavar");
            desavarMenu.DropDownItems.Add("Sales", null, (s, e) => ShowForm("DesavarSales"));
            var desavarReportsSubMenu = new ToolStripMenuItem("Reports");
            desavarReportsSubMenu.DropDownItems.Add("Register", null, (s, e) => ShowForm("DesavarRegister"));
            desavarMenu.DropDownItems.Add(desavarReportsSubMenu);
            menuStrip.Items.Add(desavarMenu);

            // Utility
            var utilityMenu = new ToolStripMenuItem("Utility");
            var calcItem = new ToolStripMenuItem("Calculator", null, (s, e) => ShowCalculator());
            calcItem.ShortcutKeys = Keys.F8;
            utilityMenu.DropDownItems.Add(calcItem);
            utilityMenu.DropDownItems.Add("Company Change", null, (s, e) => ShowForm("CompanyChange"));
            utilityMenu.DropDownItems.Add("New Year", null, (s, e) => ShowForm("NewYear"));
            utilityMenu.DropDownItems.Add("Data Check", null, (s, e) => ShowForm("DataCheck"));
            utilityMenu.DropDownItems.Add("Account Merging", null, (s, e) => ShowForm("AccountMerging"));
            utilityMenu.DropDownItems.Add("Pending Cheque", null, (s, e) => ShowForm("PendingCheque"));
            utilityMenu.DropDownItems.Add("OHCS Update", null, (s, e) => ShowForm("OHCSUpdate"));
            utilityMenu.DropDownItems.Add("RD Update", null, (s, e) => ShowForm("RDUpdate"));
            utilityMenu.DropDownItems.Add("Stock Set", null, (s, e) => ShowForm("StockSet"));
            utilityMenu.DropDownItems.Add("Activity Report", null, (s, e) => ShowForm("ActivityReport"));
            menuStrip.Items.Add(utilityMenu);

            // Exit
            var exitItem = new ToolStripMenuItem("Exit", null, (s, e) => ExitApplication());
            exitItem.ShortcutKeys = Keys.Control | Keys.X;
            menuStrip.Items.Add(exitItem);

            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
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

        private void CreateStatusBar()
        {
            var statusBar = new StatusStrip();
            var userStatus = new ToolStripStatusLabel($"User: {_loggedInUser.DisplayName}");
            var fyStatus = new ToolStripStatusLabel($"FY: {_financialYear.Code}");
            var dateStatus = new ToolStripStatusLabel($"({_financialYear.StartDate:dd/MM/yyyy} - {_financialYear.EndDate:dd/MM/yyyy})");

            statusBar.Items.Add(userStatus);
            statusBar.Items.Add(new ToolStripStatusLabel { Spring = true });
            statusBar.Items.Add(fyStatus);
            statusBar.Items.Add(dateStatus);

            Controls.Add(statusBar);
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
                // Add all form instantiations here
                case "Purchase":
                    newForm = new Form { Text = "Purchase Entry", MdiParent = this };
                    break;
                case "Sales":
                    newForm = new Form { Text = "Sales Entry", MdiParent = this };
                    break;
                case "Stock":
                    newForm = new Form { Text = "Stock Report", MdiParent = this };
                    break;
                case "Journal":
                    newForm = new Form { Text = "Journal Entry", MdiParent = this };
                    break;
                case "CashPayment":
                    newForm = new Form { Text = "Cash Payment", MdiParent = this };
                    break;
                case "CashReceipt":
                    newForm = new Form { Text = "Cash Receipt", MdiParent = this };
                    break;
                case "BankPayment":
                    newForm = new Form { Text = "Bank Payment", MdiParent = this };
                    break;
                case "BankReceipt":
                    newForm = new Form { Text = "Bank Receipt", MdiParent = this };
                    break;
                default:
                    newForm = new Form { Text = formKey, MdiParent = this };
                    break;
            }

            if (newForm != null)
            {
                _openForms[formKey] = newForm;
                newForm.FormClosed += (s, e) => _openForms.Remove(formKey);
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
