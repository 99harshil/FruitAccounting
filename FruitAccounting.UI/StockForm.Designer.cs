namespace FruitAccounting.UI
{
    partial class StockForm
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
            lblSearch = new Label();
            txtSearch = new TextBox();
            chkAvailableOnly = new CheckBox();
            dgvStock = new DataGridView();
            colLotNo = new DataGridViewTextBoxColumn();
            colItem = new DataGridViewTextBoxColumn();
            colSupplier = new DataGridViewTextBoxColumn();
            colPurDate = new DataGridViewTextBoxColumn();
            colPurchasedQty = new DataGridViewTextBoxColumn();
            colSoldQty = new DataGridViewTextBoxColumn();
            colBalanceQty = new DataGridViewTextBoxColumn();
            btnSell = new Button();
            btnClose = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvStock).BeginInit();
            SuspendLayout();
            //
            // lblSearch
            //
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSearch.Location = new Point(12, 18);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(52, 15);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "Search";
            //
            // txtSearch
            //
            txtSearch.BackColor = Color.FromArgb(224, 224, 224);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Location = new Point(95, 15);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(300, 23);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            //
            // chkAvailableOnly
            //
            chkAvailableOnly.AutoSize = true;
            chkAvailableOnly.Checked = true;
            chkAvailableOnly.CheckState = CheckState.Checked;
            chkAvailableOnly.Location = new Point(420, 18);
            chkAvailableOnly.Name = "chkAvailableOnly";
            chkAvailableOnly.Size = new Size(140, 19);
            chkAvailableOnly.TabIndex = 2;
            chkAvailableOnly.Text = "Show only available";
            chkAvailableOnly.UseVisualStyleBackColor = true;
            chkAvailableOnly.CheckedChanged += chkAvailableOnly_CheckedChanged;
            //
            // dgvStock
            //
            dgvStock.AllowUserToAddRows = false;
            dgvStock.AllowUserToDeleteRows = false;
            dgvStock.AllowUserToResizeRows = false;
            dgvStock.BackgroundColor = Color.White;
            dgvStock.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStock.Columns.AddRange(new DataGridViewColumn[] { colLotNo, colItem, colSupplier, colPurDate, colPurchasedQty, colSoldQty, colBalanceQty });
            dgvStock.EnableHeadersVisualStyles = false;
            dgvStock.Location = new Point(12, 50);
            dgvStock.MultiSelect = false;
            dgvStock.Name = "dgvStock";
            dgvStock.ReadOnly = true;
            dgvStock.RowHeadersVisible = false;
            dgvStock.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStock.Size = new Size(860, 420);
            dgvStock.TabIndex = 3;
            dgvStock.CellDoubleClick += dgvStock_CellDoubleClick;
            //
            // colLotNo
            //
            colLotNo.HeaderText = "Lot No";
            colLotNo.Name = "colLotNo";
            colLotNo.ReadOnly = true;
            colLotNo.Width = 70;
            //
            // colItem
            //
            colItem.HeaderText = "Item";
            colItem.Name = "colItem";
            colItem.ReadOnly = true;
            colItem.Width = 130;
            //
            // colSupplier
            //
            colSupplier.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colSupplier.HeaderText = "Supplier";
            colSupplier.Name = "colSupplier";
            colSupplier.ReadOnly = true;
            //
            // colPurDate
            //
            colPurDate.HeaderText = "Pur. Date";
            colPurDate.Name = "colPurDate";
            colPurDate.ReadOnly = true;
            colPurDate.Width = 90;
            //
            // colPurchasedQty
            //
            colPurchasedQty.HeaderText = "Purchased Qty";
            colPurchasedQty.Name = "colPurchasedQty";
            colPurchasedQty.ReadOnly = true;
            colPurchasedQty.Width = 100;
            //
            // colSoldQty
            //
            colSoldQty.HeaderText = "Sold Qty";
            colSoldQty.Name = "colSoldQty";
            colSoldQty.ReadOnly = true;
            colSoldQty.Width = 90;
            //
            // colBalanceQty
            //
            colBalanceQty.HeaderText = "Balance Qty";
            colBalanceQty.Name = "colBalanceQty";
            colBalanceQty.ReadOnly = true;
            colBalanceQty.Width = 100;
            //
            // btnSell
            //
            btnSell.BackColor = Color.LightSteelBlue;
            btnSell.FlatStyle = FlatStyle.Popup;
            btnSell.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSell.Location = new Point(700, 480);
            btnSell.Name = "btnSell";
            btnSell.Size = new Size(80, 28);
            btnSell.TabIndex = 4;
            btnSell.Text = "Sell";
            btnSell.UseVisualStyleBackColor = false;
            btnSell.Click += btnSell_Click;
            //
            // btnClose
            //
            btnClose.BackColor = Color.LightSteelBlue;
            btnClose.FlatStyle = FlatStyle.Popup;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClose.Location = new Point(792, 480);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(80, 28);
            btnClose.TabIndex = 5;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            //
            // StockForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(884, 521);
            Controls.Add(btnClose);
            Controls.Add(btnSell);
            Controls.Add(dgvStock);
            Controls.Add(chkAvailableOnly);
            Controls.Add(txtSearch);
            Controls.Add(lblSearch);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StockForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Stock";
            Load += StockForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvStock).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblSearch;
        private TextBox txtSearch;
        private CheckBox chkAvailableOnly;
        private DataGridView dgvStock;
        private DataGridViewTextBoxColumn colLotNo;
        private DataGridViewTextBoxColumn colItem;
        private DataGridViewTextBoxColumn colSupplier;
        private DataGridViewTextBoxColumn colPurDate;
        private DataGridViewTextBoxColumn colPurchasedQty;
        private DataGridViewTextBoxColumn colSoldQty;
        private DataGridViewTextBoxColumn colBalanceQty;
        private Button btnSell;
        private Button btnClose;
    }
}
