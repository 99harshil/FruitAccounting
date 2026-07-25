namespace FruitAccounting.UI
{
    partial class LedgerReportForm
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
            lblParty = new Label();
            cmbAccountCode = new ComboBox();
            cmbAccountName = new ComboBox();
            lblPeriod = new Label();
            dtpFromDate = new DateTimePicker();
            dtpToDate = new DateTimePicker();
            rbAll = new RadioButton();
            rbPurchase = new RadioButton();
            rbSales = new RadioButton();
            rbReceipt = new RadioButton();
            rbPayment = new RadioButton();
            rbJV = new RadioButton();
            rbWithoutOpening = new RadioButton();
            rbSummary = new RadioButton();
            dgvLedger = new DataGridView();
            colSrNo = new DataGridViewTextBoxColumn();
            colDate = new DataGridViewTextBoxColumn();
            colDetail = new DataGridViewTextBoxColumn();
            colChequeOrUser = new DataGridViewTextBoxColumn();
            colDebit = new DataGridViewTextBoxColumn();
            colCredit = new DataGridViewTextBoxColumn();
            colBalance = new DataGridViewTextBoxColumn();
            btnClose = new Button();
            btnPrint = new Button();
            btnWhatsapp = new Button();
            btnDetail = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvLedger).BeginInit();
            SuspendLayout();
            //
            // lblParty
            //
            lblParty.AutoSize = true;
            lblParty.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblParty.Location = new Point(12, 18);
            lblParty.Name = "lblParty";
            lblParty.Size = new Size(40, 15);
            lblParty.TabIndex = 0;
            lblParty.Text = "Party";
            //
            // cmbAccountCode
            //
            cmbAccountCode.BackColor = Color.FromArgb(255, 255, 192);
            cmbAccountCode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAccountCode.FlatStyle = FlatStyle.Flat;
            cmbAccountCode.FormattingEnabled = true;
            cmbAccountCode.Location = new Point(60, 15);
            cmbAccountCode.Name = "cmbAccountCode";
            cmbAccountCode.Size = new Size(90, 23);
            cmbAccountCode.TabIndex = 1;
            cmbAccountCode.SelectedIndexChanged += cmbAccountCode_SelectedIndexChanged;
            //
            // cmbAccountName
            //
            cmbAccountName.BackColor = Color.FromArgb(255, 255, 192);
            cmbAccountName.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAccountName.FlatStyle = FlatStyle.Flat;
            cmbAccountName.FormattingEnabled = true;
            cmbAccountName.Location = new Point(155, 15);
            cmbAccountName.Name = "cmbAccountName";
            cmbAccountName.Size = new Size(240, 23);
            cmbAccountName.TabIndex = 2;
            cmbAccountName.SelectedIndexChanged += cmbAccountName_SelectedIndexChanged;
            //
            // lblPeriod
            //
            lblPeriod.AutoSize = true;
            lblPeriod.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPeriod.Location = new Point(410, 18);
            lblPeriod.Name = "lblPeriod";
            lblPeriod.Size = new Size(45, 15);
            lblPeriod.TabIndex = 3;
            lblPeriod.Text = "Period";
            //
            // dtpFromDate
            //
            dtpFromDate.Format = DateTimePickerFormat.Custom;
            dtpFromDate.CustomFormat = "dd/MM/yyyy";
            dtpFromDate.Location = new Point(465, 15);
            dtpFromDate.Name = "dtpFromDate";
            dtpFromDate.Size = new Size(100, 23);
            dtpFromDate.TabIndex = 4;
            dtpFromDate.ValueChanged += dtpFromDate_ValueChanged;
            //
            // dtpToDate
            //
            dtpToDate.Format = DateTimePickerFormat.Custom;
            dtpToDate.CustomFormat = "dd/MM/yyyy";
            dtpToDate.Location = new Point(575, 15);
            dtpToDate.Name = "dtpToDate";
            dtpToDate.Size = new Size(100, 23);
            dtpToDate.TabIndex = 5;
            dtpToDate.ValueChanged += dtpToDate_ValueChanged;
            //
            // rbAll
            //
            rbAll.AutoSize = true;
            rbAll.Checked = true;
            rbAll.Location = new Point(60, 44);
            rbAll.Name = "rbAll";
            rbAll.Size = new Size(39, 19);
            rbAll.TabIndex = 6;
            rbAll.TabStop = true;
            rbAll.Text = "All";
            rbAll.UseVisualStyleBackColor = true;
            rbAll.CheckedChanged += rbViewMode_CheckedChanged;
            //
            // rbPurchase
            //
            rbPurchase.AutoSize = true;
            rbPurchase.Location = new Point(130, 44);
            rbPurchase.Name = "rbPurchase";
            rbPurchase.Size = new Size(75, 19);
            rbPurchase.TabIndex = 7;
            rbPurchase.Text = "Purchase";
            rbPurchase.UseVisualStyleBackColor = true;
            rbPurchase.CheckedChanged += rbViewMode_CheckedChanged;
            //
            // rbSales
            //
            rbSales.AutoSize = true;
            rbSales.Location = new Point(215, 44);
            rbSales.Name = "rbSales";
            rbSales.Size = new Size(52, 19);
            rbSales.TabIndex = 8;
            rbSales.Text = "Sales";
            rbSales.UseVisualStyleBackColor = true;
            rbSales.CheckedChanged += rbViewMode_CheckedChanged;
            //
            // rbReceipt
            //
            rbReceipt.AutoSize = true;
            rbReceipt.Location = new Point(280, 44);
            rbReceipt.Name = "rbReceipt";
            rbReceipt.Size = new Size(67, 19);
            rbReceipt.TabIndex = 9;
            rbReceipt.Text = "Receipt";
            rbReceipt.UseVisualStyleBackColor = true;
            rbReceipt.CheckedChanged += rbViewMode_CheckedChanged;
            //
            // rbPayment
            //
            rbPayment.AutoSize = true;
            rbPayment.Location = new Point(330, 44);
            rbPayment.Name = "rbPayment";
            rbPayment.Size = new Size(72, 19);
            rbPayment.TabIndex = 10;
            rbPayment.Text = "Payment";
            rbPayment.UseVisualStyleBackColor = true;
            rbPayment.CheckedChanged += rbViewMode_CheckedChanged;
            //
            // rbJV
            //
            rbJV.AutoSize = true;
            rbJV.Location = new Point(410, 44);
            rbJV.Name = "rbJV";
            rbJV.Size = new Size(40, 19);
            rbJV.TabIndex = 11;
            rbJV.Text = "JV";
            rbJV.UseVisualStyleBackColor = true;
            rbJV.CheckedChanged += rbViewMode_CheckedChanged;
            //
            // rbWithoutOpening
            //
            rbWithoutOpening.AutoSize = true;
            rbWithoutOpening.Location = new Point(460, 44);
            rbWithoutOpening.Name = "rbWithoutOpening";
            rbWithoutOpening.Size = new Size(94, 19);
            rbWithoutOpening.TabIndex = 12;
            rbWithoutOpening.Text = "W/o Opening";
            rbWithoutOpening.UseVisualStyleBackColor = true;
            rbWithoutOpening.CheckedChanged += rbViewMode_CheckedChanged;
            //
            // rbSummary
            //
            rbSummary.AutoSize = true;
            rbSummary.Location = new Point(564, 44);
            rbSummary.Name = "rbSummary";
            rbSummary.Size = new Size(72, 19);
            rbSummary.TabIndex = 13;
            rbSummary.Text = "Summary";
            rbSummary.UseVisualStyleBackColor = true;
            rbSummary.CheckedChanged += rbViewMode_CheckedChanged;
            //
            // dgvLedger
            //
            dgvLedger.AllowUserToAddRows = false;
            dgvLedger.AllowUserToDeleteRows = false;
            dgvLedger.AllowUserToResizeRows = false;
            dgvLedger.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvLedger.BackgroundColor = Color.White;
            dgvLedger.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLedger.Columns.AddRange(new DataGridViewColumn[] { colSrNo, colDate, colDetail, colChequeOrUser, colDebit, colCredit, colBalance });
            dgvLedger.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvLedger.EnableHeadersVisualStyles = false;
            dgvLedger.Location = new Point(12, 76);
            dgvLedger.Name = "dgvLedger";
            dgvLedger.ReadOnly = true;
            dgvLedger.RowHeadersVisible = false;
            dgvLedger.Size = new Size(796, 434);
            dgvLedger.TabIndex = 13;
            //
            // colSrNo
            //
            colSrNo.HeaderText = "Sr. No";
            colSrNo.Name = "colSrNo";
            colSrNo.ReadOnly = true;
            colSrNo.Width = 50;
            //
            // colDate
            //
            colDate.HeaderText = "Date";
            colDate.Name = "colDate";
            colDate.ReadOnly = true;
            colDate.Width = 85;
            //
            // colDetail
            //
            colDetail.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDetail.HeaderText = "Detail";
            colDetail.Name = "colDetail";
            colDetail.ReadOnly = true;
            //
            // colChequeOrUser
            //
            colChequeOrUser.HeaderText = "Cheque No.";
            colChequeOrUser.Name = "colChequeOrUser";
            colChequeOrUser.ReadOnly = true;
            colChequeOrUser.Width = 110;
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
            // btnDetail
            //
            btnDetail.BackColor = Color.LightSteelBlue;
            btnDetail.FlatStyle = FlatStyle.Popup;
            btnDetail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDetail.Location = new Point(12, 520);
            btnDetail.Name = "btnDetail";
            btnDetail.Size = new Size(80, 28);
            btnDetail.TabIndex = 14;
            btnDetail.Text = "Detail";
            btnDetail.UseVisualStyleBackColor = false;
            btnDetail.Click += btnDetail_Click;
            //
            // btnWhatsapp
            //
            btnWhatsapp.BackColor = Color.LightGreen;
            btnWhatsapp.FlatStyle = FlatStyle.Popup;
            btnWhatsapp.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnWhatsapp.Location = new Point(536, 520);
            btnWhatsapp.Name = "btnWhatsapp";
            btnWhatsapp.Size = new Size(90, 28);
            btnWhatsapp.TabIndex = 15;
            btnWhatsapp.Text = "WhatsApp";
            btnWhatsapp.UseVisualStyleBackColor = false;
            btnWhatsapp.Click += btnWhatsapp_Click;
            //
            // btnPrint
            //
            btnPrint.BackColor = Color.LightGreen;
            btnPrint.FlatStyle = FlatStyle.Popup;
            btnPrint.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPrint.Location = new Point(632, 520);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(80, 28);
            btnPrint.TabIndex = 16;
            btnPrint.Text = "Print";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;
            //
            // btnClose
            //
            btnClose.BackColor = Color.LightGreen;
            btnClose.FlatStyle = FlatStyle.Popup;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClose.Location = new Point(718, 520);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(80, 28);
            btnClose.TabIndex = 17;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            //
            // LedgerReportForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(820, 560);
            Controls.Add(btnPrint);
            Controls.Add(btnWhatsapp);
            Controls.Add(btnDetail);
            Controls.Add(btnClose);
            Controls.Add(dgvLedger);
            Controls.Add(rbSummary);
            Controls.Add(rbWithoutOpening);
            Controls.Add(rbJV);
            Controls.Add(rbPayment);
            Controls.Add(rbReceipt);
            Controls.Add(rbSales);
            Controls.Add(rbPurchase);
            Controls.Add(rbAll);
            Controls.Add(dtpToDate);
            Controls.Add(dtpFromDate);
            Controls.Add(lblPeriod);
            Controls.Add(cmbAccountName);
            Controls.Add(cmbAccountCode);
            Controls.Add(lblParty);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LedgerReportForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ledger";
            Load += LedgerReportForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLedger).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblParty;
        private ComboBox cmbAccountCode;
        private ComboBox cmbAccountName;
        private Label lblPeriod;
        private DateTimePicker dtpFromDate;
        private DateTimePicker dtpToDate;
        private RadioButton rbAll;
        private RadioButton rbPurchase;
        private RadioButton rbSales;
        private RadioButton rbReceipt;
        private RadioButton rbPayment;
        private RadioButton rbJV;
        private RadioButton rbWithoutOpening;
        private RadioButton rbSummary;
        private DataGridView dgvLedger;
        private DataGridViewTextBoxColumn colSrNo;
        private DataGridViewTextBoxColumn colDate;
        private DataGridViewTextBoxColumn colDetail;
        private DataGridViewTextBoxColumn colChequeOrUser;
        private DataGridViewTextBoxColumn colDebit;
        private DataGridViewTextBoxColumn colCredit;
        private DataGridViewTextBoxColumn colBalance;
        private Button btnClose;
        private Button btnPrint;
        private Button btnWhatsapp;
        private Button btnDetail;
    }
}
