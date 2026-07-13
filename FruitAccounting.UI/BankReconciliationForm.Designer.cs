namespace FruitAccounting.UI
{
    partial class BankReconciliationForm
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
            lblBankName = new Label();
            cmbBankName = new ComboBox();
            chkPending = new CheckBox();
            dgvLines = new DataGridView();
            colVNo = new DataGridViewTextBoxColumn();
            colChqNo = new DataGridViewTextBoxColumn();
            colDate = new DataGridViewTextBoxColumn();
            colAccount = new DataGridViewTextBoxColumn();
            colReceive = new DataGridViewTextBoxColumn();
            colPayment = new DataGridViewTextBoxColumn();
            colClDate = new DataGridViewMaskedDateColumn();
            btnClose = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvLines).BeginInit();
            SuspendLayout();
            //
            // lblBankName
            //
            lblBankName.AutoSize = true;
            lblBankName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBankName.Location = new Point(12, 18);
            lblBankName.Name = "lblBankName";
            lblBankName.Size = new Size(75, 15);
            lblBankName.TabIndex = 0;
            lblBankName.Text = "Bank Name :-";
            //
            // cmbBankName
            //
            cmbBankName.BackColor = Color.FromArgb(255, 255, 192);
            cmbBankName.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBankName.FlatStyle = FlatStyle.Flat;
            cmbBankName.FormattingEnabled = true;
            cmbBankName.Location = new Point(95, 15);
            cmbBankName.Name = "cmbBankName";
            cmbBankName.Size = new Size(300, 23);
            cmbBankName.TabIndex = 1;
            cmbBankName.SelectedIndexChanged += cmbBankName_SelectedIndexChanged;
            //
            // chkPending
            //
            chkPending.AutoSize = true;
            chkPending.Location = new Point(420, 18);
            chkPending.Name = "chkPending";
            chkPending.Size = new Size(70, 19);
            chkPending.TabIndex = 2;
            chkPending.Text = "Pending";
            chkPending.UseVisualStyleBackColor = true;
            chkPending.CheckedChanged += chkPending_CheckedChanged;
            //
            // dgvLines
            //
            dgvLines.AllowUserToAddRows = false;
            dgvLines.AllowUserToDeleteRows = false;
            dgvLines.AllowUserToResizeRows = false;
            dgvLines.BackgroundColor = Color.White;
            dgvLines.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLines.Columns.AddRange(new DataGridViewColumn[] { colVNo, colChqNo, colDate, colAccount, colReceive, colPayment, colClDate });
            dgvLines.EnableHeadersVisualStyles = false;
            dgvLines.Location = new Point(12, 50);
            dgvLines.Name = "dgvLines";
            dgvLines.RowHeadersVisible = false;
            dgvLines.Size = new Size(760, 380);
            dgvLines.TabIndex = 3;
            dgvLines.CellBeginEdit += dgvLines_CellBeginEdit;
            dgvLines.CellEndEdit += dgvLines_CellEndEdit;
            //
            // colVNo
            //
            colVNo.HeaderText = "VNo";
            colVNo.Name = "colVNo";
            colVNo.ReadOnly = true;
            colVNo.Width = 50;
            //
            // colChqNo
            //
            colChqNo.HeaderText = "Chq. No.";
            colChqNo.Name = "colChqNo";
            colChqNo.ReadOnly = true;
            colChqNo.Width = 80;
            //
            // colDate
            //
            colDate.HeaderText = "Date";
            colDate.Name = "colDate";
            colDate.ReadOnly = true;
            colDate.Width = 90;
            //
            // colAccount
            //
            colAccount.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colAccount.HeaderText = "Account";
            colAccount.Name = "colAccount";
            colAccount.ReadOnly = true;
            //
            // colReceive
            //
            colReceive.HeaderText = "Receive";
            colReceive.Name = "colReceive";
            colReceive.ReadOnly = true;
            colReceive.Width = 90;
            //
            // colPayment
            //
            colPayment.HeaderText = "Payment";
            colPayment.Name = "colPayment";
            colPayment.ReadOnly = true;
            colPayment.Width = 90;
            //
            // colClDate
            //
            colClDate.HeaderText = "CL. Date";
            colClDate.Name = "colClDate";
            colClDate.Width = 90;
            //
            // btnClose
            //
            btnClose.BackColor = Color.LightSteelBlue;
            btnClose.FlatStyle = FlatStyle.Popup;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClose.Location = new Point(697, 440);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 28);
            btnClose.TabIndex = 4;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            //
            // BankReconciliationForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(784, 481);
            Controls.Add(btnClose);
            Controls.Add(dgvLines);
            Controls.Add(chkPending);
            Controls.Add(cmbBankName);
            Controls.Add(lblBankName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "BankReconciliationForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bank Reconciliation";
            Load += BankReconciliationForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLines).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblBankName;
        private ComboBox cmbBankName;
        private CheckBox chkPending;
        private DataGridView dgvLines;
        private DataGridViewTextBoxColumn colVNo;
        private DataGridViewTextBoxColumn colChqNo;
        private DataGridViewTextBoxColumn colDate;
        private DataGridViewTextBoxColumn colAccount;
        private DataGridViewTextBoxColumn colReceive;
        private DataGridViewTextBoxColumn colPayment;
        private DataGridViewMaskedDateColumn colClDate;
        private Button btnClose;
    }
}
