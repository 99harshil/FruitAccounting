namespace FruitAccounting.UI
{
    partial class TradingAccountForm
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
            lblPeriod = new Label();
            dtpFromDate = new DateTimePicker();
            dtpToDate = new DateTimePicker();
            dgvTrading = new DataGridView();
            colExpenseName = new DataGridViewTextBoxColumn();
            colExpenseAmount = new DataGridViewTextBoxColumn();
            colIncomeName = new DataGridViewTextBoxColumn();
            colIncomeAmount = new DataGridViewTextBoxColumn();
            btnClose = new Button();
            btnPrint = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvTrading).BeginInit();
            SuspendLayout();

            // lblPeriod
            lblPeriod.AutoSize = true;
            lblPeriod.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPeriod.Location = new Point(560, 18);
            lblPeriod.Name = "lblPeriod";
            lblPeriod.Size = new Size(48, 15);
            lblPeriod.TabIndex = 0;
            lblPeriod.Text = "Period :";

            // dtpFromDate
            dtpFromDate.Format = DateTimePickerFormat.Custom;
            dtpFromDate.CustomFormat = "dd/MM/yyyy";
            dtpFromDate.Location = new Point(614, 14);
            dtpFromDate.Name = "dtpFromDate";
            dtpFromDate.Size = new Size(105, 23);
            dtpFromDate.TabIndex = 1;

            // dtpToDate
            dtpToDate.Format = DateTimePickerFormat.Custom;
            dtpToDate.CustomFormat = "dd/MM/yyyy";
            dtpToDate.Location = new Point(725, 14);
            dtpToDate.Name = "dtpToDate";
            dtpToDate.Size = new Size(105, 23);
            dtpToDate.TabIndex = 2;
            dtpToDate.ValueChanged += dtpToDate_ValueChanged;

            // dgvTrading
            dgvTrading.AllowUserToAddRows = false;
            dgvTrading.AllowUserToDeleteRows = false;
            dgvTrading.AllowUserToResizeRows = false;
            dgvTrading.BackgroundColor = Color.White;
            dgvTrading.BorderStyle = BorderStyle.FixedSingle;
            dgvTrading.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dgvTrading.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTrading.ColumnHeadersHeight = 26;
            dgvTrading.Columns.AddRange(new DataGridViewColumn[] { colExpenseName, colExpenseAmount, colIncomeName, colIncomeAmount });
            dgvTrading.EnableHeadersVisualStyles = false;
            dgvTrading.Location = new Point(12, 48);
            dgvTrading.Name = "dgvTrading";
            dgvTrading.ReadOnly = true;
            dgvTrading.RowHeadersVisible = false;
            dgvTrading.ScrollBars = ScrollBars.Vertical;
            dgvTrading.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvTrading.Size = new Size(842, 470);
            dgvTrading.TabIndex = 3;
            dgvTrading.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvTrading.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvTrading.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // colExpenseName
            colExpenseName.HeaderText = "TR.EXPENSE";
            colExpenseName.Name = "colExpenseName";
            colExpenseName.ReadOnly = true;
            colExpenseName.SortMode = DataGridViewColumnSortMode.NotSortable;
            colExpenseName.Width = 270;

            // colExpenseAmount
            colExpenseAmount.HeaderText = "";
            colExpenseAmount.Name = "colExpenseAmount";
            colExpenseAmount.ReadOnly = true;
            colExpenseAmount.SortMode = DataGridViewColumnSortMode.NotSortable;
            colExpenseAmount.Width = 150;
            colExpenseAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // colIncomeName
            colIncomeName.HeaderText = "TR.INCOME";
            colIncomeName.Name = "colIncomeName";
            colIncomeName.ReadOnly = true;
            colIncomeName.SortMode = DataGridViewColumnSortMode.NotSortable;
            colIncomeName.Width = 270;

            // colIncomeAmount
            colIncomeAmount.HeaderText = "";
            colIncomeAmount.Name = "colIncomeAmount";
            colIncomeAmount.ReadOnly = true;
            colIncomeAmount.SortMode = DataGridViewColumnSortMode.NotSortable;
            colIncomeAmount.Width = 150;
            colIncomeAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // btnClose
            btnClose.BackColor = Color.LightGreen;
            btnClose.FlatStyle = FlatStyle.Popup;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClose.Location = new Point(348, 530);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(78, 28);
            btnClose.TabIndex = 4;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;

            // btnPrint
            btnPrint.BackColor = Color.LightGreen;
            btnPrint.FlatStyle = FlatStyle.Popup;
            btnPrint.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPrint.Location = new Point(440, 530);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(78, 28);
            btnPrint.TabIndex = 5;
            btnPrint.Text = "Print";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;

            // TradingAccountForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(866, 572);
            Controls.Add(btnPrint);
            Controls.Add(btnClose);
            Controls.Add(dgvTrading);
            Controls.Add(dtpToDate);
            Controls.Add(dtpFromDate);
            Controls.Add(lblPeriod);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TradingAccountForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Trading A/c";
            Load += TradingAccountForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTrading).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPeriod;
        private DateTimePicker dtpFromDate;
        private DateTimePicker dtpToDate;
        private DataGridView dgvTrading;
        private DataGridViewTextBoxColumn colExpenseName;
        private DataGridViewTextBoxColumn colExpenseAmount;
        private DataGridViewTextBoxColumn colIncomeName;
        private DataGridViewTextBoxColumn colIncomeAmount;
        private Button btnClose;
        private Button btnPrint;
    }
}
