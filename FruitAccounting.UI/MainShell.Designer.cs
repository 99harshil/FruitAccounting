namespace FruitAccounting.UI
{
    partial class MainShell
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            menuStripMain = new MenuStrip();
            menuItemMain = new ToolStripMenuItem();
            menuItemMaster = new ToolStripMenuItem();
            menuItemAccount = new ToolStripMenuItem();
            menuItemDomestic = new ToolStripMenuItem();
            menuItemDesavar = new ToolStripMenuItem();
            menuItemUtility = new ToolStripMenuItem();
            menuItemExit = new ToolStripMenuItem();
            flowLayoutPanelToolbar = new FlowLayoutPanel();
            btnPurchase = new Button();
            btnSales = new Button();
            btnSaleUpdate = new Button();
            btnCashPayment = new Button();
            btnCashReceipt = new Button();
            btnFreightPayment = new Button();
            btnBankPayment = new Button();
            btnBankReceipt = new Button();
            btnStock = new Button();
            btnChithi = new Button();
            btnChitha = new Button();
            btnExit = new Button();
            statusStripMain = new StatusStrip();
            lblStatusCompany = new ToolStripStatusLabel();
            lblStatusUser = new ToolStripStatusLabel();
            lblStatusDateTime = new ToolStripStatusLabel();

            menuStripMain.SuspendLayout();
            flowLayoutPanelToolbar.SuspendLayout();
            statusStripMain.SuspendLayout();
            SuspendLayout();

            //
            // menuStripMain
            //
            menuStripMain.BackColor = Color.White;
            menuStripMain.Items.AddRange(new ToolStripItem[] { menuItemMain, menuItemMaster, menuItemAccount, menuItemDomestic, menuItemDesavar, menuItemUtility, menuItemExit });
            menuStripMain.Location = new Point(0, 0);
            menuStripMain.Name = "menuStripMain";
            menuStripMain.Size = new Size(1200, 24);
            menuStripMain.TabIndex = 0;
            menuStripMain.Text = "menuStrip1";

            //
            // Menu Items
            //
            menuItemMain.Name = "menuItemMain";
            menuItemMain.Size = new Size(46, 20);
            menuItemMain.Text = "Main";
            menuItemMaster.Name = "menuItemMaster";
            menuItemMaster.Size = new Size(55, 20);
            menuItemMaster.Text = "Master";
            menuItemAccount.Name = "menuItemAccount";
            menuItemAccount.Size = new Size(64, 20);
            menuItemAccount.Text = "Account";
            menuItemDomestic.Name = "menuItemDomestic";
            menuItemDomestic.Size = new Size(71, 20);
            menuItemDomestic.Text = "Domestic";
            menuItemDesavar.Name = "menuItemDesavar";
            menuItemDesavar.Size = new Size(60, 20);
            menuItemDesavar.Text = "Desavar";
            menuItemUtility.Name = "menuItemUtility";
            menuItemUtility.Size = new Size(50, 20);
            menuItemUtility.Text = "Utility";
            menuItemExit.Name = "menuItemExit";
            menuItemExit.Size = new Size(38, 20);
            menuItemExit.Text = "Exit";

            //
            // flowLayoutPanelToolbar
            //
            flowLayoutPanelToolbar.BackColor = Color.FromArgb(240, 240, 240);
            flowLayoutPanelToolbar.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanelToolbar.Controls.Add(btnPurchase);
            flowLayoutPanelToolbar.Controls.Add(btnSales);
            flowLayoutPanelToolbar.Controls.Add(btnSaleUpdate);
            flowLayoutPanelToolbar.Controls.Add(btnCashPayment);
            flowLayoutPanelToolbar.Controls.Add(btnCashReceipt);
            flowLayoutPanelToolbar.Controls.Add(btnFreightPayment);
            flowLayoutPanelToolbar.Controls.Add(btnBankPayment);
            flowLayoutPanelToolbar.Controls.Add(btnBankReceipt);
            flowLayoutPanelToolbar.Controls.Add(btnStock);
            flowLayoutPanelToolbar.Controls.Add(btnChithi);
            flowLayoutPanelToolbar.Controls.Add(btnChitha);
            flowLayoutPanelToolbar.Controls.Add(btnExit);
            flowLayoutPanelToolbar.Dock = DockStyle.Top;
            flowLayoutPanelToolbar.Location = new Point(0, 24);
            flowLayoutPanelToolbar.Name = "flowLayoutPanelToolbar";
            flowLayoutPanelToolbar.Size = new Size(1200, 60);
            flowLayoutPanelToolbar.TabIndex = 1;
            flowLayoutPanelToolbar.WrapContents = false;

            // Toolbar Buttons
            btnPurchase.FlatStyle = FlatStyle.Flat;
            btnPurchase.FlatAppearance.BorderColor = Color.Gray;
            btnPurchase.FlatAppearance.BorderSize = 1;
            btnPurchase.Margin = new Padding(0, 5, 5, 5);
            btnPurchase.Size = new Size(85, 50);
            btnPurchase.Text = "Purchase";
            btnPurchase.BackColor = Color.FromArgb(240, 240, 240);
            btnPurchase.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            btnSales.FlatStyle = FlatStyle.Flat;
            btnSales.FlatAppearance.BorderColor = Color.Gray;
            btnSales.FlatAppearance.BorderSize = 1;
            btnSales.Margin = new Padding(0, 5, 5, 5);
            btnSales.Size = new Size(85, 50);
            btnSales.Text = "Sales";
            btnSales.BackColor = Color.FromArgb(240, 240, 240);
            btnSales.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            btnSaleUpdate.FlatStyle = FlatStyle.Flat;
            btnSaleUpdate.FlatAppearance.BorderColor = Color.Gray;
            btnSaleUpdate.FlatAppearance.BorderSize = 1;
            btnSaleUpdate.Margin = new Padding(0, 5, 5, 5);
            btnSaleUpdate.Size = new Size(85, 50);
            btnSaleUpdate.Text = "Sale Update";
            btnSaleUpdate.BackColor = Color.FromArgb(240, 240, 240);
            btnSaleUpdate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            btnCashPayment.FlatStyle = FlatStyle.Flat;
            btnCashPayment.FlatAppearance.BorderColor = Color.Gray;
            btnCashPayment.FlatAppearance.BorderSize = 1;
            btnCashPayment.Margin = new Padding(0, 5, 5, 5);
            btnCashPayment.Size = new Size(85, 50);
            btnCashPayment.Text = "Cash Payment";
            btnCashPayment.BackColor = Color.FromArgb(240, 240, 240);
            btnCashPayment.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            btnCashReceipt.FlatStyle = FlatStyle.Flat;
            btnCashReceipt.FlatAppearance.BorderColor = Color.Gray;
            btnCashReceipt.FlatAppearance.BorderSize = 1;
            btnCashReceipt.Margin = new Padding(0, 5, 5, 5);
            btnCashReceipt.Size = new Size(85, 50);
            btnCashReceipt.Text = "Cash Receipt";
            btnCashReceipt.BackColor = Color.FromArgb(240, 240, 240);
            btnCashReceipt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            btnFreightPayment.FlatStyle = FlatStyle.Flat;
            btnFreightPayment.FlatAppearance.BorderColor = Color.Gray;
            btnFreightPayment.FlatAppearance.BorderSize = 1;
            btnFreightPayment.Margin = new Padding(0, 5, 5, 5);
            btnFreightPayment.Size = new Size(85, 50);
            btnFreightPayment.Text = "Freight Payment";
            btnFreightPayment.BackColor = Color.FromArgb(240, 240, 240);
            btnFreightPayment.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            btnBankPayment.FlatStyle = FlatStyle.Flat;
            btnBankPayment.FlatAppearance.BorderColor = Color.Gray;
            btnBankPayment.FlatAppearance.BorderSize = 1;
            btnBankPayment.Margin = new Padding(0, 5, 5, 5);
            btnBankPayment.Size = new Size(85, 50);
            btnBankPayment.Text = "Bank Payment";
            btnBankPayment.BackColor = Color.FromArgb(240, 240, 240);
            btnBankPayment.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            btnBankReceipt.FlatStyle = FlatStyle.Flat;
            btnBankReceipt.FlatAppearance.BorderColor = Color.Gray;
            btnBankReceipt.FlatAppearance.BorderSize = 1;
            btnBankReceipt.Margin = new Padding(0, 5, 5, 5);
            btnBankReceipt.Size = new Size(85, 50);
            btnBankReceipt.Text = "Bank Receipt";
            btnBankReceipt.BackColor = Color.FromArgb(240, 240, 240);
            btnBankReceipt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            btnStock.FlatStyle = FlatStyle.Flat;
            btnStock.FlatAppearance.BorderColor = Color.Gray;
            btnStock.FlatAppearance.BorderSize = 1;
            btnStock.Margin = new Padding(0, 5, 5, 5);
            btnStock.Size = new Size(85, 50);
            btnStock.Text = "Stock";
            btnStock.BackColor = Color.FromArgb(240, 240, 240);
            btnStock.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            btnChithi.FlatStyle = FlatStyle.Flat;
            btnChithi.FlatAppearance.BorderColor = Color.Gray;
            btnChithi.FlatAppearance.BorderSize = 1;
            btnChithi.Margin = new Padding(0, 5, 5, 5);
            btnChithi.Size = new Size(85, 50);
            btnChithi.Text = "Chithi";
            btnChithi.BackColor = Color.FromArgb(240, 240, 240);
            btnChithi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            btnChitha.FlatStyle = FlatStyle.Flat;
            btnChitha.FlatAppearance.BorderColor = Color.Gray;
            btnChitha.FlatAppearance.BorderSize = 1;
            btnChitha.Margin = new Padding(0, 5, 5, 5);
            btnChitha.Size = new Size(85, 50);
            btnChitha.Text = "Chitha";
            btnChitha.BackColor = Color.FromArgb(240, 240, 240);
            btnChitha.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.FlatAppearance.BorderColor = Color.Gray;
            btnExit.FlatAppearance.BorderSize = 1;
            btnExit.Margin = new Padding(0, 5, 5, 5);
            btnExit.Size = new Size(85, 50);
            btnExit.Text = "Exit";
            btnExit.BackColor = Color.FromArgb(240, 240, 240);
            btnExit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExit.Click += (s, e) => ExitApplication();

            //
            // statusStripMain
            //
            statusStripMain.Items.AddRange(new ToolStripItem[] { lblStatusCompany, lblStatusUser, lblStatusDateTime });
            statusStripMain.Location = new Point(0, 678);
            statusStripMain.Name = "statusStripMain";
            statusStripMain.Size = new Size(1200, 22);
            statusStripMain.TabIndex = 2;
            statusStripMain.Text = "statusStrip1";

            //
            // Status Labels
            //
            lblStatusCompany.Name = "lblStatusCompany";
            lblStatusCompany.Size = new Size(1000, 17);
            lblStatusCompany.Spring = true;
            lblStatusCompany.Text = "COMPANY NAME--BGT26-27";
            lblStatusCompany.TextAlign = ContentAlignment.MiddleLeft;
            lblStatusCompany.BorderSides = ToolStripStatusLabelBorderSides.Right;

            lblStatusUser.Name = "lblStatusUser";
            lblStatusUser.Size = new Size(50, 17);
            lblStatusUser.Text = "admin";
            lblStatusUser.BorderSides = ToolStripStatusLabelBorderSides.Right;

            lblStatusDateTime.Name = "lblStatusDateTime";
            lblStatusDateTime.Size = new Size(120, 17);
            lblStatusDateTime.Text = "07/07/2026  12:19 P";

            //
            // MainShell
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 700);
            Controls.Add(flowLayoutPanelToolbar);
            Controls.Add(statusStripMain);
            Controls.Add(menuStripMain);
            IsMdiContainer = true;
            MainMenuStrip = menuStripMain;
            Name = "MainShell";
            Text = "Fruit Accounting System";
            WindowState = FormWindowState.Maximized;
            Load += MainShell_Load;

            menuStripMain.ResumeLayout(false);
            menuStripMain.PerformLayout();
            flowLayoutPanelToolbar.ResumeLayout(false);
            statusStripMain.ResumeLayout(false);
            statusStripMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStripMain;
        private ToolStripMenuItem menuItemMain;
        private ToolStripMenuItem menuItemMaster;
        private ToolStripMenuItem menuItemAccount;
        private ToolStripMenuItem menuItemDomestic;
        private ToolStripMenuItem menuItemDesavar;
        private ToolStripMenuItem menuItemUtility;
        private ToolStripMenuItem menuItemExit;

        private FlowLayoutPanel flowLayoutPanelToolbar;
        private Button btnPurchase;
        private Button btnSales;
        private Button btnSaleUpdate;
        private Button btnCashPayment;
        private Button btnCashReceipt;
        private Button btnFreightPayment;
        private Button btnBankPayment;
        private Button btnBankReceipt;
        private Button btnStock;
        private Button btnChithi;
        private Button btnChitha;
        private Button btnExit;

        private StatusStrip statusStripMain;
        private ToolStripStatusLabel lblStatusCompany;
        private ToolStripStatusLabel lblStatusUser;
        private ToolStripStatusLabel lblStatusDateTime;
    }
}
