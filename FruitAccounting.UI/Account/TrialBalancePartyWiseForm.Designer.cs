namespace FruitAccounting.UI
{
    partial class TrialBalancePartyWiseForm
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
            lblAccountCode = new Label();
            cmbAccountCode = new ComboBox();
            lblAccountName = new Label();
            cmbAccountName = new ComboBox();
            btnLoad = new Button();
            rdbDetail = new RadioButton();
            rdbOnlyOp = new RadioButton();
            rdbCrClosing = new RadioButton();
            rdbDbClosing = new RadioButton();
            rdbOnlyCl = new RadioButton();
            rdbNoTransaction = new RadioButton();
            dgvTrialBalance = new DataGridView();
            btnClose = new Button();
            btnPrint = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvTrialBalance).BeginInit();
            SuspendLayout();

            // lblPeriod
            lblPeriod.AutoSize = true;
            lblPeriod.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPeriod.Location = new Point(12, 18);
            lblPeriod.Name = "lblPeriod";
            lblPeriod.Size = new Size(45, 15);
            lblPeriod.TabIndex = 0;
            lblPeriod.Text = "Period";

            // dtpFromDate
            dtpFromDate.Format = DateTimePickerFormat.Custom;
            dtpFromDate.CustomFormat = "dd/MM/yyyy";
            dtpFromDate.Location = new Point(60, 15);
            dtpFromDate.Name = "dtpFromDate";
            dtpFromDate.Size = new Size(100, 23);
            dtpFromDate.TabIndex = 1;

            // dtpToDate
            dtpToDate.Format = DateTimePickerFormat.Custom;
            dtpToDate.CustomFormat = "dd/MM/yyyy";
            dtpToDate.Location = new Point(165, 15);
            dtpToDate.Name = "dtpToDate";
            dtpToDate.Size = new Size(100, 23);
            dtpToDate.TabIndex = 2;

            // lblAccountCode
            lblAccountCode.AutoSize = true;
            lblAccountCode.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAccountCode.Location = new Point(275, 18);
            lblAccountCode.Name = "lblAccountCode";
            lblAccountCode.Size = new Size(40, 15);
            lblAccountCode.TabIndex = 3;
            lblAccountCode.Text = "Code";

            // cmbAccountCode
            cmbAccountCode.BackColor = Color.FromArgb(255, 255, 192);
            cmbAccountCode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAccountCode.FlatStyle = FlatStyle.Flat;
            cmbAccountCode.FormattingEnabled = true;
            cmbAccountCode.Location = new Point(318, 15);
            cmbAccountCode.Name = "cmbAccountCode";
            cmbAccountCode.Size = new Size(100, 23);
            cmbAccountCode.TabIndex = 4;
            cmbAccountCode.SelectedIndexChanged += cmbAccountCode_SelectedIndexChanged;

            // lblAccountName
            lblAccountName.AutoSize = true;
            lblAccountName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAccountName.Location = new Point(425, 18);
            lblAccountName.Name = "lblAccountName";
            lblAccountName.Size = new Size(28, 15);
            lblAccountName.TabIndex = 5;
            lblAccountName.Text = "A/c";

            // cmbAccountName
            cmbAccountName.BackColor = Color.FromArgb(255, 255, 192);
            cmbAccountName.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAccountName.FlatStyle = FlatStyle.Flat;
            cmbAccountName.FormattingEnabled = true;
            cmbAccountName.Location = new Point(456, 15);
            cmbAccountName.Name = "cmbAccountName";
            cmbAccountName.Size = new Size(200, 23);
            cmbAccountName.TabIndex = 6;
            cmbAccountName.SelectedIndexChanged += cmbAccountName_SelectedIndexChanged;

            // btnLoad
            btnLoad.BackColor = Color.LightBlue;
            btnLoad.FlatStyle = FlatStyle.Popup;
            btnLoad.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLoad.Location = new Point(663, 15);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(70, 23);
            btnLoad.TabIndex = 7;
            btnLoad.Text = "Load";
            btnLoad.UseVisualStyleBackColor = false;
            btnLoad.Click += btnLoad_Click;

            // rdbDetail
            rdbDetail.AutoSize = true;
            rdbDetail.Checked = true;
            rdbDetail.Location = new Point(12, 45);
            rdbDetail.Name = "rdbDetail";
            rdbDetail.Size = new Size(53, 19);
            rdbDetail.TabIndex = 8;
            rdbDetail.TabStop = true;
            rdbDetail.Text = "Detail";
            rdbDetail.UseVisualStyleBackColor = true;
            rdbDetail.CheckedChanged += rdbDetail_CheckedChanged;

            // rdbOnlyOp
            rdbOnlyOp.AutoSize = true;
            rdbOnlyOp.Location = new Point(80, 45);
            rdbOnlyOp.Name = "rdbOnlyOp";
            rdbOnlyOp.Size = new Size(80, 19);
            rdbOnlyOp.TabIndex = 9;
            rdbOnlyOp.Text = "Only Op.";
            rdbOnlyOp.UseVisualStyleBackColor = true;
            rdbOnlyOp.CheckedChanged += rdbOnlyOp_CheckedChanged;

            // rdbCrClosing
            rdbCrClosing.AutoSize = true;
            rdbCrClosing.Location = new Point(170, 45);
            rdbCrClosing.Name = "rdbCrClosing";
            rdbCrClosing.Size = new Size(88, 19);
            rdbCrClosing.TabIndex = 10;
            rdbCrClosing.Text = "Cr. Closing";
            rdbCrClosing.UseVisualStyleBackColor = true;
            rdbCrClosing.CheckedChanged += rdbCrClosing_CheckedChanged;

            // rdbDbClosing
            rdbDbClosing.AutoSize = true;
            rdbDbClosing.Location = new Point(270, 45);
            rdbDbClosing.Name = "rdbDbClosing";
            rdbDbClosing.Size = new Size(90, 19);
            rdbDbClosing.TabIndex = 11;
            rdbDbClosing.Text = "Db. Closing";
            rdbDbClosing.UseVisualStyleBackColor = true;
            rdbDbClosing.CheckedChanged += rdbDbClosing_CheckedChanged;

            // rdbOnlyCl
            rdbOnlyCl.AutoSize = true;
            rdbOnlyCl.Location = new Point(375, 45);
            rdbOnlyCl.Name = "rdbOnlyCl";
            rdbOnlyCl.Size = new Size(73, 19);
            rdbOnlyCl.TabIndex = 12;
            rdbOnlyCl.Text = "Only Cl.";
            rdbOnlyCl.UseVisualStyleBackColor = true;
            rdbOnlyCl.CheckedChanged += rdbOnlyCl_CheckedChanged;

            // rdbNoTransaction
            rdbNoTransaction.AutoSize = true;
            rdbNoTransaction.Location = new Point(460, 45);
            rdbNoTransaction.Name = "rdbNoTransaction";
            rdbNoTransaction.Size = new Size(110, 19);
            rdbNoTransaction.TabIndex = 13;
            rdbNoTransaction.Text = "No Transaction";
            rdbNoTransaction.UseVisualStyleBackColor = true;
            rdbNoTransaction.CheckedChanged += rdbNoTransaction_CheckedChanged;

            // dgvTrialBalance
            dgvTrialBalance.AllowUserToAddRows = false;
            dgvTrialBalance.AllowUserToDeleteRows = false;
            dgvTrialBalance.AllowUserToResizeRows = false;
            dgvTrialBalance.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvTrialBalance.BackgroundColor = Color.White;
            dgvTrialBalance.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTrialBalance.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvTrialBalance.EnableHeadersVisualStyles = false;
            dgvTrialBalance.Location = new Point(12, 75);
            dgvTrialBalance.Name = "dgvTrialBalance";
            dgvTrialBalance.ReadOnly = true;
            dgvTrialBalance.RowHeadersVisible = false;
            dgvTrialBalance.Size = new Size(1000, 450);
            dgvTrialBalance.TabIndex = 14;

            // btnClose
            btnClose.BackColor = Color.LightGreen;
            btnClose.FlatStyle = FlatStyle.Popup;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClose.Location = new Point(558, 536);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(70, 28);
            btnClose.TabIndex = 15;
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
            btnPrint.TabIndex = 16;
            btnPrint.Text = "Print";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;

            // TrialBalancePartyWiseForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(1050, 590);
            Controls.Add(btnPrint);
            Controls.Add(btnClose);
            Controls.Add(dgvTrialBalance);
            Controls.Add(rdbNoTransaction);
            Controls.Add(rdbOnlyCl);
            Controls.Add(rdbDbClosing);
            Controls.Add(rdbCrClosing);
            Controls.Add(rdbOnlyOp);
            Controls.Add(rdbDetail);
            Controls.Add(btnLoad);
            Controls.Add(cmbAccountName);
            Controls.Add(lblAccountName);
            Controls.Add(cmbAccountCode);
            Controls.Add(lblAccountCode);
            Controls.Add(dtpToDate);
            Controls.Add(dtpFromDate);
            Controls.Add(lblPeriod);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TrialBalancePartyWiseForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Trial Balance (Party Wise)";
            Load += TrialBalancePartyWiseForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTrialBalance).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPeriod;
        private DateTimePicker dtpFromDate;
        private DateTimePicker dtpToDate;
        private Label lblAccountCode;
        private ComboBox cmbAccountCode;
        private Label lblAccountName;
        private ComboBox cmbAccountName;
        private Button btnLoad;
        private RadioButton rdbDetail;
        private RadioButton rdbOnlyOp;
        private RadioButton rdbCrClosing;
        private RadioButton rdbDbClosing;
        private RadioButton rdbOnlyCl;
        private RadioButton rdbNoTransaction;
        private DataGridView dgvTrialBalance;
        private Button btnClose;
        private Button btnPrint;
    }
}
