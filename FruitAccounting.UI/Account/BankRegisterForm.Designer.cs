namespace FruitAccounting.UI
{
    partial class BankRegisterForm
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
            dgvRegister = new DataGridView();
            colSrNo = new DataGridViewTextBoxColumn();
            colVoucherCode = new DataGridViewTextBoxColumn();
            colDate = new DataGridViewTextBoxColumn();
            colDetail = new DataGridViewTextBoxColumn();
            colChequeOrUser = new DataGridViewTextBoxColumn();
            colDebit = new DataGridViewTextBoxColumn();
            colCredit = new DataGridViewTextBoxColumn();
            colBalance = new DataGridViewTextBoxColumn();
            btnClose = new Button();
            btnPrint = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvRegister).BeginInit();
            SuspendLayout();

            // lblCode
            lblCode.AutoSize = true;
            lblCode.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCode.Location = new Point(12, 18);
            lblCode.Name = "lblCode";
            lblCode.Size = new Size(40, 15);
            lblCode.TabIndex = 0;
            lblCode.Text = "Code";

            // cmbAccountCode
            cmbAccountCode.BackColor = Color.FromArgb(255, 255, 192);
            cmbAccountCode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAccountCode.FlatStyle = FlatStyle.Flat;
            cmbAccountCode.FormattingEnabled = true;
            cmbAccountCode.Location = new Point(55, 15);
            cmbAccountCode.Name = "cmbAccountCode";
            cmbAccountCode.Size = new Size(90, 23);
            cmbAccountCode.TabIndex = 1;
            cmbAccountCode.SelectedIndexChanged += cmbAccountCode_SelectedIndexChanged;

            // lblAc
            lblAc.AutoSize = true;
            lblAc.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAc.Location = new Point(155, 18);
            lblAc.Name = "lblAc";
            lblAc.Size = new Size(28, 15);
            lblAc.TabIndex = 2;
            lblAc.Text = "A/c";

            // cmbAccountName
            cmbAccountName.BackColor = Color.FromArgb(255, 255, 192);
            cmbAccountName.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAccountName.FlatStyle = FlatStyle.Flat;
            cmbAccountName.FormattingEnabled = true;
            cmbAccountName.Location = new Point(185, 15);
            cmbAccountName.Name = "cmbAccountName";
            cmbAccountName.Size = new Size(250, 23);
            cmbAccountName.TabIndex = 3;
            cmbAccountName.SelectedIndexChanged += cmbAccountName_SelectedIndexChanged;

            // lblPeriod
            lblPeriod.AutoSize = true;
            lblPeriod.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPeriod.Location = new Point(450, 18);
            lblPeriod.Name = "lblPeriod";
            lblPeriod.Size = new Size(45, 15);
            lblPeriod.TabIndex = 4;
            lblPeriod.Text = "Period";

            // dtpFromDate
            dtpFromDate.Format = DateTimePickerFormat.Custom;
            dtpFromDate.CustomFormat = "dd/MM/yyyy";
            dtpFromDate.Location = new Point(500, 15);
            dtpFromDate.Name = "dtpFromDate";
            dtpFromDate.Size = new Size(100, 23);
            dtpFromDate.TabIndex = 5;
            dtpFromDate.ValueChanged += dtpFromDate_ValueChanged;

            // dtpToDate
            dtpToDate.Format = DateTimePickerFormat.Custom;
            dtpToDate.CustomFormat = "dd/MM/yyyy";
            dtpToDate.Location = new Point(610, 15);
            dtpToDate.Name = "dtpToDate";
            dtpToDate.Size = new Size(100, 23);
            dtpToDate.TabIndex = 6;
            dtpToDate.ValueChanged += dtpToDate_ValueChanged;

            // lblMobile
            lblMobile.AutoSize = true;
            lblMobile.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMobile.Location = new Point(12, 47);
            lblMobile.Name = "lblMobile";
            lblMobile.Size = new Size(50, 15);
            lblMobile.TabIndex = 7;
            lblMobile.Text = "Mobile";

            // txtMobile
            txtMobile.BackColor = Color.White;
            txtMobile.Location = new Point(65, 44);
            txtMobile.Name = "txtMobile";
            txtMobile.ReadOnly = true;
            txtMobile.Size = new Size(150, 23);
            txtMobile.TabIndex = 8;

            // dgvRegister
            dgvRegister.AllowUserToAddRows = false;
            dgvRegister.AllowUserToDeleteRows = false;
            dgvRegister.AllowUserToResizeRows = false;
            dgvRegister.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvRegister.BackgroundColor = Color.White;
            dgvRegister.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRegister.Columns.AddRange(new DataGridViewColumn[] { colSrNo, colVoucherCode, colDate, colDetail, colChequeOrUser, colDebit, colCredit, colBalance });
            dgvRegister.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvRegister.EnableHeadersVisualStyles = false;
            dgvRegister.Location = new Point(12, 76);
            dgvRegister.Name = "dgvRegister";
            dgvRegister.ReadOnly = true;
            dgvRegister.RowHeadersVisible = false;
            dgvRegister.Size = new Size(1026, 450);
            dgvRegister.TabIndex = 9;

            // colSrNo
            colSrNo.HeaderText = "Sr. No";
            colSrNo.Name = "colSrNo";
            colSrNo.ReadOnly = true;
            colSrNo.Width = 50;

            // colVoucherCode
            colVoucherCode.HeaderText = "";
            colVoucherCode.Name = "colVoucherCode";
            colVoucherCode.ReadOnly = true;
            colVoucherCode.Width = 45;

            // colDate
            colDate.HeaderText = "Date";
            colDate.Name = "colDate";
            colDate.ReadOnly = true;
            colDate.Width = 85;

            // colDetail
            colDetail.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colDetail.HeaderText = "Detail";
            colDetail.Name = "colDetail";
            colDetail.ReadOnly = true;

            // colChequeOrUser
            colChequeOrUser.HeaderText = "Chq. No.";
            colChequeOrUser.Name = "colChequeOrUser";
            colChequeOrUser.ReadOnly = true;
            colChequeOrUser.Width = 100;

            // colDebit
            colDebit.HeaderText = "Debit";
            colDebit.Name = "colDebit";
            colDebit.ReadOnly = true;
            colDebit.Width = 100;

            // colCredit
            colCredit.HeaderText = "Credit";
            colCredit.Name = "colCredit";
            colCredit.ReadOnly = true;
            colCredit.Width = 100;

            // colBalance
            colBalance.HeaderText = "Balance";
            colBalance.Name = "colBalance";
            colBalance.ReadOnly = true;
            colBalance.Width = 120;

            // btnClose
            btnClose.BackColor = Color.LightGreen;
            btnClose.FlatStyle = FlatStyle.Popup;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClose.Location = new Point(558, 536);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(70, 28);
            btnClose.TabIndex = 10;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;

            // btnPrint
            btnPrint.BackColor = Color.LightGreen;
            btnPrint.FlatStyle = FlatStyle.Popup;
            btnPrint.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPrint.Location = new Point(636, 536);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(70, 28);
            btnPrint.TabIndex = 11;
            btnPrint.Text = "Print";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;

            // BankRegisterForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(1050, 590);
            Controls.Add(btnPrint);
            Controls.Add(btnClose);
            Controls.Add(dgvRegister);
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
            Name = "BankRegisterForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bank Register";
            Load += BankRegisterForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvRegister).EndInit();
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
        private DataGridView dgvRegister;
        private DataGridViewTextBoxColumn colSrNo;
        private DataGridViewTextBoxColumn colVoucherCode;
        private DataGridViewTextBoxColumn colDate;
        private DataGridViewTextBoxColumn colDetail;
        private DataGridViewTextBoxColumn colChequeOrUser;
        private DataGridViewTextBoxColumn colDebit;
        private DataGridViewTextBoxColumn colCredit;
        private DataGridViewTextBoxColumn colBalance;
        private Button btnClose;
        private Button btnPrint;
    }
}
