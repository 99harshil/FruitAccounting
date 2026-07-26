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
            lblCode = new Label();
            cmbAccountCode = new ComboBox();
            lblAc = new Label();
            cmbAccountName = new ComboBox();
            lblPeriod = new Label();
            dtpFromDate = new DateTimePicker();
            dtpToDate = new DateTimePicker();
            lblMobile = new Label();
            txtMobile = new TextBox();
            rbAll = new RadioButton();
            rbPurchase = new RadioButton();
            rbSales = new RadioButton();
            rbReceipt = new RadioButton();
            rbPayment = new RadioButton();
            rbWithoutOpening = new RadioButton();
            rbJV = new RadioButton();
            rbSummary = new RadioButton();
            dgvLedger = new DataGridView();
            colSrNo = new DataGridViewTextBoxColumn();
            colVoucherCode = new DataGridViewTextBoxColumn();
            colDate = new DataGridViewTextBoxColumn();
            colDetail = new DataGridViewTextBoxColumn();
            colChequeOrUser = new DataGridViewTextBoxColumn();
            colDebit = new DataGridViewTextBoxColumn();
            colCredit = new DataGridViewTextBoxColumn();
            colBalance = new DataGridViewTextBoxColumn();
            chkWeekTotal = new CheckBox();
            chkGrossAmount = new CheckBox();
            btnClose = new Button();
            btnPrint = new Button();
            btnDetail = new Button();
            btnChithi = new Button();
            btnEmail = new Button();
            btnWhatsapp = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvLedger).BeginInit();
            SuspendLayout();
            //
            // lblCode
            //
            lblCode.AutoSize = true;
            lblCode.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCode.Location = new Point(12, 18);
            lblCode.Name = "lblCode";
            lblCode.Size = new Size(40, 15);
            lblCode.TabIndex = 0;
            lblCode.Text = "Code";
            //
            // cmbAccountCode
            //
            cmbAccountCode.BackColor = Color.FromArgb(255, 255, 192);
            cmbAccountCode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAccountCode.FlatStyle = FlatStyle.Flat;
            cmbAccountCode.FormattingEnabled = true;
            cmbAccountCode.Location = new Point(55, 15);
            cmbAccountCode.Name = "cmbAccountCode";
            cmbAccountCode.Size = new Size(90, 23);
            cmbAccountCode.TabIndex = 1;
            cmbAccountCode.SelectedIndexChanged += cmbAccountCode_SelectedIndexChanged;
            //
            // lblAc
            //
            lblAc.AutoSize = true;
            lblAc.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAc.Location = new Point(155, 18);
            lblAc.Name = "lblAc";
            lblAc.Size = new Size(28, 15);
            lblAc.TabIndex = 2;
            lblAc.Text = "A/c";
            //
            // cmbAccountName
            //
            cmbAccountName.BackColor = Color.FromArgb(255, 255, 192);
            cmbAccountName.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAccountName.FlatStyle = FlatStyle.Flat;
            cmbAccountName.FormattingEnabled = true;
            cmbAccountName.Location = new Point(185, 15);
            cmbAccountName.Name = "cmbAccountName";
            cmbAccountName.Size = new Size(250, 23);
            cmbAccountName.TabIndex = 3;
            cmbAccountName.SelectedIndexChanged += cmbAccountName_SelectedIndexChanged;
            //
            // lblPeriod
            //
            lblPeriod.AutoSize = true;
            lblPeriod.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPeriod.Location = new Point(450, 18);
            lblPeriod.Name = "lblPeriod";
            lblPeriod.Size = new Size(45, 15);
            lblPeriod.TabIndex = 4;
            lblPeriod.Text = "Period";
            //
            // dtpFromDate
            //
            dtpFromDate.Format = DateTimePickerFormat.Custom;
            dtpFromDate.CustomFormat = "dd/MM/yyyy";
            dtpFromDate.Location = new Point(500, 15);
            dtpFromDate.Name = "dtpFromDate";
            dtpFromDate.Size = new Size(100, 23);
            dtpFromDate.TabIndex = 5;
            dtpFromDate.ValueChanged += dtpFromDate_ValueChanged;
            //
            // dtpToDate
            //
            dtpToDate.Format = DateTimePickerFormat.Custom;
            dtpToDate.CustomFormat = "dd/MM/yyyy";
            dtpToDate.Location = new Point(610, 15);
            dtpToDate.Name = "dtpToDate";
            dtpToDate.Size = new Size(100, 23);
            dtpToDate.TabIndex = 6;
            dtpToDate.ValueChanged += dtpToDate_ValueChanged;
            //
            // lblMobile
            //
            lblMobile.AutoSize = true;
            lblMobile.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMobile.Location = new Point(12, 47);
            lblMobile.Name = "lblMobile";
            lblMobile.Size = new Size(50, 15);
            lblMobile.TabIndex = 7;
            lblMobile.Text = "Mobile";
            //
            // txtMobile
            //
            txtMobile.BackColor = Color.White;
            txtMobile.Location = new Point(65, 44);
            txtMobile.Name = "txtMobile";
            txtMobile.ReadOnly = true;
            txtMobile.Size = new Size(150, 23);
            txtMobile.TabIndex = 8;
            //
            // rbAll
            //
            // NOTE: all radio buttons below are AutoSize, so their Location.X values include
            // generous gaps rather than tight estimated widths - AutoSize ignores the Designer
            // Size hint at runtime, so a tight layout here would overlap (as it once did).
            rbAll.AutoSize = true;
            rbAll.Checked = true;
            rbAll.Location = new Point(230, 47);
            rbAll.Name = "rbAll";
            rbAll.Size = new Size(39, 19);
            rbAll.TabIndex = 9;
            rbAll.TabStop = true;
            rbAll.Text = "All";
            rbAll.UseVisualStyleBackColor = true;
            rbAll.CheckedChanged += rbViewMode_CheckedChanged;
            //
            // rbPurchase
            //
            rbPurchase.AutoSize = true;
            rbPurchase.Location = new Point(280, 47);
            rbPurchase.Name = "rbPurchase";
            rbPurchase.Size = new Size(75, 19);
            rbPurchase.TabIndex = 10;
            rbPurchase.Text = "Purchase";
            rbPurchase.UseVisualStyleBackColor = true;
            rbPurchase.CheckedChanged += rbViewMode_CheckedChanged;
            //
            // rbSales
            //
            rbSales.AutoSize = true;
            rbSales.Location = new Point(360, 47);
            rbSales.Name = "rbSales";
            rbSales.Size = new Size(52, 19);
            rbSales.TabIndex = 11;
            rbSales.Text = "Sales";
            rbSales.UseVisualStyleBackColor = true;
            rbSales.CheckedChanged += rbViewMode_CheckedChanged;
            //
            // rbReceipt
            //
            rbReceipt.AutoSize = true;
            rbReceipt.Location = new Point(425, 47);
            rbReceipt.Name = "rbReceipt";
            rbReceipt.Size = new Size(67, 19);
            rbReceipt.TabIndex = 12;
            rbReceipt.Text = "Receipt";
            rbReceipt.UseVisualStyleBackColor = true;
            rbReceipt.CheckedChanged += rbViewMode_CheckedChanged;
            //
            // rbPayment
            //
            rbPayment.AutoSize = true;
            rbPayment.Location = new Point(500, 47);
            rbPayment.Name = "rbPayment";
            rbPayment.Size = new Size(72, 19);
            rbPayment.TabIndex = 13;
            rbPayment.Text = "Payment";
            rbPayment.UseVisualStyleBackColor = true;
            rbPayment.CheckedChanged += rbViewMode_CheckedChanged;
            //
            // rbWithoutOpening
            //
            rbWithoutOpening.AutoSize = true;
            rbWithoutOpening.Location = new Point(580, 47);
            rbWithoutOpening.Name = "rbWithoutOpening";
            rbWithoutOpening.Size = new Size(94, 19);
            rbWithoutOpening.TabIndex = 14;
            rbWithoutOpening.Text = "W/O Opening";
            rbWithoutOpening.UseVisualStyleBackColor = true;
            rbWithoutOpening.CheckedChanged += rbViewMode_CheckedChanged;
            //
            // rbJV
            //
            rbJV.AutoSize = true;
            rbJV.Location = new Point(685, 47);
            rbJV.Name = "rbJV";
            rbJV.Size = new Size(40, 19);
            rbJV.TabIndex = 15;
            rbJV.Text = "JV";
            rbJV.UseVisualStyleBackColor = true;
            rbJV.CheckedChanged += rbViewMode_CheckedChanged;
            //
            // rbSummary
            //
            rbSummary.AutoSize = true;
            rbSummary.Location = new Point(735, 47);
            rbSummary.Name = "rbSummary";
            rbSummary.Size = new Size(72, 19);
            rbSummary.TabIndex = 16;
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
            dgvLedger.Columns.AddRange(new DataGridViewColumn[] { colSrNo, colVoucherCode, colDate, colDetail, colChequeOrUser, colDebit, colCredit, colBalance });
            dgvLedger.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvLedger.EnableHeadersVisualStyles = false;
            dgvLedger.Location = new Point(12, 76);
            dgvLedger.Name = "dgvLedger";
            dgvLedger.ReadOnly = true;
            dgvLedger.RowHeadersVisible = false;
            dgvLedger.Size = new Size(1026, 450);
            dgvLedger.TabIndex = 17;
            dgvLedger.CellDoubleClick += dgvLedger_CellDoubleClick;
            //
            // colSrNo
            //
            colSrNo.HeaderText = "Sr. No";
            colSrNo.Name = "colSrNo";
            colSrNo.ReadOnly = true;
            colSrNo.Width = 50;
            //
            // colVoucherCode
            //
            colVoucherCode.HeaderText = "";
            colVoucherCode.Name = "colVoucherCode";
            colVoucherCode.ReadOnly = true;
            colVoucherCode.Width = 45;
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
            colChequeOrUser.HeaderText = "Chq. No.";
            colChequeOrUser.Name = "colChequeOrUser";
            colChequeOrUser.ReadOnly = true;
            colChequeOrUser.Width = 100;
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
            // chkWeekTotal
            //
            chkWeekTotal.AutoSize = true;
            chkWeekTotal.Location = new Point(12, 542);
            chkWeekTotal.Name = "chkWeekTotal";
            chkWeekTotal.Size = new Size(90, 19);
            chkWeekTotal.TabIndex = 18;
            chkWeekTotal.Text = "Week Total";
            chkWeekTotal.UseVisualStyleBackColor = true;
            chkWeekTotal.CheckedChanged += chkAggregation_CheckedChanged;
            //
            // chkGrossAmount
            //
            chkGrossAmount.AutoSize = true;
            chkGrossAmount.Location = new Point(115, 542);
            chkGrossAmount.Name = "chkGrossAmount";
            chkGrossAmount.Size = new Size(105, 19);
            chkGrossAmount.TabIndex = 19;
            chkGrossAmount.Text = "Gross Amount";
            chkGrossAmount.UseVisualStyleBackColor = true;
            chkGrossAmount.CheckedChanged += chkAggregation_CheckedChanged;
            //
            // btnClose
            //
            btnClose.BackColor = Color.LightGreen;
            btnClose.FlatStyle = FlatStyle.Popup;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClose.Location = new Point(558, 536);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(70, 28);
            btnClose.TabIndex = 20;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            //
            // btnPrint
            //
            btnPrint.BackColor = Color.LightGreen;
            btnPrint.FlatStyle = FlatStyle.Popup;
            btnPrint.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPrint.Location = new Point(636, 536);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(70, 28);
            btnPrint.TabIndex = 21;
            btnPrint.Text = "Print";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;
            //
            // btnDetail
            //
            btnDetail.BackColor = Color.LightSteelBlue;
            btnDetail.FlatStyle = FlatStyle.Popup;
            btnDetail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDetail.Location = new Point(714, 536);
            btnDetail.Name = "btnDetail";
            btnDetail.Size = new Size(70, 28);
            btnDetail.TabIndex = 22;
            btnDetail.Text = "Detail";
            btnDetail.UseVisualStyleBackColor = false;
            btnDetail.Click += btnDetail_Click;
            //
            // btnChithi
            //
            btnChithi.BackColor = Color.LightSteelBlue;
            btnChithi.FlatStyle = FlatStyle.Popup;
            btnChithi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnChithi.Location = new Point(792, 536);
            btnChithi.Name = "btnChithi";
            btnChithi.Size = new Size(70, 28);
            btnChithi.TabIndex = 23;
            btnChithi.Text = "Chithi";
            btnChithi.UseVisualStyleBackColor = false;
            btnChithi.Click += btnChithi_Click;
            //
            // btnEmail
            //
            btnEmail.BackColor = Color.LightSteelBlue;
            btnEmail.FlatStyle = FlatStyle.Popup;
            btnEmail.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEmail.Location = new Point(870, 536);
            btnEmail.Name = "btnEmail";
            btnEmail.Size = new Size(70, 28);
            btnEmail.TabIndex = 24;
            btnEmail.Text = "Email";
            btnEmail.UseVisualStyleBackColor = false;
            btnEmail.Click += btnEmail_Click;
            //
            // btnWhatsapp
            //
            btnWhatsapp.BackColor = Color.LightGreen;
            btnWhatsapp.FlatStyle = FlatStyle.Popup;
            btnWhatsapp.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnWhatsapp.Location = new Point(948, 536);
            btnWhatsapp.Name = "btnWhatsapp";
            btnWhatsapp.Size = new Size(90, 28);
            btnWhatsapp.TabIndex = 25;
            btnWhatsapp.Text = "WhatsApp";
            btnWhatsapp.UseVisualStyleBackColor = false;
            btnWhatsapp.Click += btnWhatsapp_Click;
            //
            // LedgerReportForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(1050, 590);
            Controls.Add(btnWhatsapp);
            Controls.Add(btnEmail);
            Controls.Add(btnChithi);
            Controls.Add(btnDetail);
            Controls.Add(btnPrint);
            Controls.Add(btnClose);
            Controls.Add(chkGrossAmount);
            Controls.Add(chkWeekTotal);
            Controls.Add(dgvLedger);
            Controls.Add(rbSummary);
            Controls.Add(rbJV);
            Controls.Add(rbWithoutOpening);
            Controls.Add(rbPayment);
            Controls.Add(rbReceipt);
            Controls.Add(rbSales);
            Controls.Add(rbPurchase);
            Controls.Add(rbAll);
            Controls.Add(txtMobile);
            Controls.Add(lblMobile);
            Controls.Add(dtpToDate);
            Controls.Add(dtpFromDate);
            Controls.Add(lblPeriod);
            Controls.Add(cmbAccountName);
            Controls.Add(lblAc);
            Controls.Add(cmbAccountCode);
            Controls.Add(lblCode);
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

        private Label lblCode;
        private ComboBox cmbAccountCode;
        private Label lblAc;
        private ComboBox cmbAccountName;
        private Label lblPeriod;
        private DateTimePicker dtpFromDate;
        private DateTimePicker dtpToDate;
        private Label lblMobile;
        private TextBox txtMobile;
        private RadioButton rbAll;
        private RadioButton rbPurchase;
        private RadioButton rbSales;
        private RadioButton rbReceipt;
        private RadioButton rbPayment;
        private RadioButton rbWithoutOpening;
        private RadioButton rbJV;
        private RadioButton rbSummary;
        private DataGridView dgvLedger;
        private DataGridViewTextBoxColumn colSrNo;
        private DataGridViewTextBoxColumn colVoucherCode;
        private DataGridViewTextBoxColumn colDate;
        private DataGridViewTextBoxColumn colDetail;
        private DataGridViewTextBoxColumn colChequeOrUser;
        private DataGridViewTextBoxColumn colDebit;
        private DataGridViewTextBoxColumn colCredit;
        private DataGridViewTextBoxColumn colBalance;
        private CheckBox chkWeekTotal;
        private CheckBox chkGrossAmount;
        private Button btnClose;
        private Button btnPrint;
        private Button btnDetail;
        private Button btnChithi;
        private Button btnEmail;
        private Button btnWhatsapp;
    }
}
