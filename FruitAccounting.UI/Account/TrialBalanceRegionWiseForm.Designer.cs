namespace FruitAccounting.UI
{
    partial class TrialBalanceRegionWiseForm
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
            lblRegion = new Label();
            cmbRegion = new ComboBox();
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

            // lblRegion
            lblRegion.AutoSize = true;
            lblRegion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRegion.Location = new Point(275, 18);
            lblRegion.Name = "lblRegion";
            lblRegion.Size = new Size(49, 15);
            lblRegion.TabIndex = 3;
            lblRegion.Text = "Region";

            // cmbRegion
            cmbRegion.BackColor = Color.FromArgb(255, 255, 192);
            cmbRegion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRegion.FlatStyle = FlatStyle.Flat;
            cmbRegion.FormattingEnabled = true;
            cmbRegion.Location = new Point(325, 15);
            cmbRegion.Name = "cmbRegion";
            cmbRegion.Size = new Size(200, 23);
            cmbRegion.TabIndex = 4;

            // btnLoad
            btnLoad = new Button();
            btnLoad.BackColor = Color.LightBlue;
            btnLoad.FlatStyle = FlatStyle.Popup;
            btnLoad.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLoad.Location = new Point(530, 15);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(70, 23);
            btnLoad.TabIndex = 5;
            btnLoad.Text = "Load";
            btnLoad.UseVisualStyleBackColor = false;
            btnLoad.Click += btnLoad_Click;

            // rdbDetail
            rdbDetail.AutoSize = true;
            rdbDetail.Checked = true;
            rdbDetail.Location = new Point(12, 45);
            rdbDetail.Name = "rdbDetail";
            rdbDetail.Size = new Size(53, 19);
            rdbDetail.TabIndex = 5;
            rdbDetail.TabStop = true;
            rdbDetail.Text = "Detail";
            rdbDetail.UseVisualStyleBackColor = true;
            rdbDetail.CheckedChanged += rdbDetail_CheckedChanged;

            // rdbOnlyOp
            rdbOnlyOp.AutoSize = true;
            rdbOnlyOp.Location = new Point(80, 45);
            rdbOnlyOp.Name = "rdbOnlyOp";
            rdbOnlyOp.Size = new Size(80, 19);
            rdbOnlyOp.TabIndex = 6;
            rdbOnlyOp.Text = "Only Op.";
            rdbOnlyOp.UseVisualStyleBackColor = true;
            rdbOnlyOp.CheckedChanged += rdbOnlyOp_CheckedChanged;

            // rdbCrClosing
            rdbCrClosing.AutoSize = true;
            rdbCrClosing.Location = new Point(170, 45);
            rdbCrClosing.Name = "rdbCrClosing";
            rdbCrClosing.Size = new Size(88, 19);
            rdbCrClosing.TabIndex = 7;
            rdbCrClosing.Text = "Cr. Closing";
            rdbCrClosing.UseVisualStyleBackColor = true;
            rdbCrClosing.CheckedChanged += rdbCrClosing_CheckedChanged;

            // rdbDbClosing
            rdbDbClosing.AutoSize = true;
            rdbDbClosing.Location = new Point(270, 45);
            rdbDbClosing.Name = "rdbDbClosing";
            rdbDbClosing.Size = new Size(90, 19);
            rdbDbClosing.TabIndex = 8;
            rdbDbClosing.Text = "Db. Closing";
            rdbDbClosing.UseVisualStyleBackColor = true;
            rdbDbClosing.CheckedChanged += rdbDbClosing_CheckedChanged;

            // rdbOnlyCl
            rdbOnlyCl.AutoSize = true;
            rdbOnlyCl.Location = new Point(375, 45);
            rdbOnlyCl.Name = "rdbOnlyCl";
            rdbOnlyCl.Size = new Size(73, 19);
            rdbOnlyCl.TabIndex = 9;
            rdbOnlyCl.Text = "Only Cl.";
            rdbOnlyCl.UseVisualStyleBackColor = true;
            rdbOnlyCl.CheckedChanged += rdbOnlyCl_CheckedChanged;

            // rdbNoTransaction
            rdbNoTransaction.AutoSize = true;
            rdbNoTransaction.Location = new Point(460, 45);
            rdbNoTransaction.Name = "rdbNoTransaction";
            rdbNoTransaction.Size = new Size(110, 19);
            rdbNoTransaction.TabIndex = 10;
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
            dgvTrialBalance.TabIndex = 11;

            // btnClose
            btnClose.BackColor = Color.LightGreen;
            btnClose.FlatStyle = FlatStyle.Popup;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClose.Location = new Point(558, 536);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(70, 28);
            btnClose.TabIndex = 12;
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
            btnPrint.TabIndex = 13;
            btnPrint.Text = "Print";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;

            // TrialBalanceRegionWiseForm
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
            Controls.Add(cmbRegion);
            Controls.Add(lblRegion);
            Controls.Add(dtpToDate);
            Controls.Add(dtpFromDate);
            Controls.Add(lblPeriod);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TrialBalanceRegionWiseForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Trial Balance (Region Wise)";
            Load += TrialBalanceRegionWiseForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTrialBalance).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPeriod;
        private DateTimePicker dtpFromDate;
        private DateTimePicker dtpToDate;
        private Label lblRegion;
        private ComboBox cmbRegion;
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
