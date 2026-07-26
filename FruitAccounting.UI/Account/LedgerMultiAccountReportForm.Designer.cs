namespace FruitAccounting.UI
{
    partial class LedgerMultiAccountReportForm
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
            lblHeading = new Label();
            dgvLedger = new DataGridView();
            colDate = new DataGridViewTextBoxColumn();
            colDetail = new DataGridViewTextBoxColumn();
            colDebit = new DataGridViewTextBoxColumn();
            colCredit = new DataGridViewTextBoxColumn();
            colBalance = new DataGridViewTextBoxColumn();
            btnClose = new Button();
            btnPrint = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvLedger).BeginInit();
            SuspendLayout();
            //
            // lblHeading
            //
            lblHeading.AutoSize = true;
            lblHeading.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHeading.Location = new Point(12, 15);
            lblHeading.Name = "lblHeading";
            lblHeading.Size = new Size(58, 15);
            lblHeading.TabIndex = 0;
            lblHeading.Text = "Ledger";
            //
            // dgvLedger
            //
            dgvLedger.AllowUserToAddRows = false;
            dgvLedger.AllowUserToDeleteRows = false;
            dgvLedger.AllowUserToResizeRows = false;
            dgvLedger.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvLedger.BackgroundColor = Color.White;
            dgvLedger.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLedger.Columns.AddRange(new DataGridViewColumn[] { colDate, colDetail, colDebit, colCredit, colBalance });
            dgvLedger.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvLedger.EnableHeadersVisualStyles = false;
            dgvLedger.Location = new Point(12, 40);
            dgvLedger.Name = "dgvLedger";
            dgvLedger.ReadOnly = true;
            dgvLedger.RowHeadersVisible = false;
            dgvLedger.Size = new Size(1026, 480);
            dgvLedger.TabIndex = 1;
            //
            // colDate
            //
            colDate.HeaderText = "Date";
            colDate.Name = "colDate";
            colDate.ReadOnly = true;
            colDate.Width = 90;
            //
            // colDetail
            //
            colDetail.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDetail.HeaderText = "Detail";
            colDetail.Name = "colDetail";
            colDetail.ReadOnly = true;
            //
            // colDebit
            //
            colDebit.HeaderText = "Debit";
            colDebit.Name = "colDebit";
            colDebit.ReadOnly = true;
            colDebit.Width = 100;
            //
            // colCredit
            //
            colCredit.HeaderText = "Credit";
            colCredit.Name = "colCredit";
            colCredit.ReadOnly = true;
            colCredit.Width = 100;
            //
            // colBalance
            //
            colBalance.HeaderText = "Balance";
            colBalance.Name = "colBalance";
            colBalance.ReadOnly = true;
            colBalance.Width = 120;
            //
            // btnClose
            //
            btnClose.BackColor = Color.LightGreen;
            btnClose.FlatStyle = FlatStyle.Popup;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClose.Location = new Point(958, 528);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(80, 28);
            btnClose.TabIndex = 2;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            //
            // btnPrint
            //
            btnPrint.BackColor = Color.LightGreen;
            btnPrint.FlatStyle = FlatStyle.Popup;
            btnPrint.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPrint.Location = new Point(870, 528);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(80, 28);
            btnPrint.TabIndex = 3;
            btnPrint.Text = "Print";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;
            //
            // LedgerMultiAccountReportForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(1050, 568);
            Controls.Add(btnPrint);
            Controls.Add(btnClose);
            Controls.Add(dgvLedger);
            Controls.Add(lblHeading);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = false;
            Name = "LedgerMultiAccountReportForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ledger";
            Load += LedgerMultiAccountReportForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLedger).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblHeading;
        private DataGridView dgvLedger;
        private DataGridViewTextBoxColumn colDate;
        private DataGridViewTextBoxColumn colDetail;
        private DataGridViewTextBoxColumn colDebit;
        private DataGridViewTextBoxColumn colCredit;
        private DataGridViewTextBoxColumn colBalance;
        private Button btnClose;
        private Button btnPrint;
    }
}
