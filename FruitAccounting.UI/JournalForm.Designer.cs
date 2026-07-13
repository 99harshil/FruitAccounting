namespace FruitAccounting.UI
{
    partial class JournalForm
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
            lblVoucherNo = new Label();
            txtVoucherNo = new TextBox();
            lblVoucherDate = new Label();
            dtpVoucherDate = new DateTimePicker();
            dgvLines = new DataGridView();
            colCode = new DataGridViewTextBoxColumn();
            colAccount = new DataGridViewComboBoxColumn();
            colDebit = new DataGridViewTextBoxColumn();
            colCredit = new DataGridViewTextBoxColumn();
            colRemarks = new DataGridViewTextBoxColumn();
            lblTotalDebit = new Label();
            txtTotalDebit = new TextBox();
            lblTotalCredit = new Label();
            txtTotalCredit = new TextBox();
            panelButtons = new Panel();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnSave = new Button();
            btnPrevious = new Button();
            btnNext = new Button();
            btnFind = new Button();
            btnClose = new Button();
            btnPrint = new Button();
            btnWhatsapp = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvLines).BeginInit();
            panelButtons.SuspendLayout();
            SuspendLayout();
            //
            // lblVoucherNo
            //
            lblVoucherNo.AutoSize = true;
            lblVoucherNo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblVoucherNo.ForeColor = Color.Red;
            lblVoucherNo.Location = new Point(12, 15);
            lblVoucherNo.Name = "lblVoucherNo";
            lblVoucherNo.Size = new Size(75, 15);
            lblVoucherNo.TabIndex = 0;
            lblVoucherNo.Text = "Voucher No.";
            //
            // txtVoucherNo
            //
            txtVoucherNo.BackColor = Color.FromArgb(224, 224, 224);
            txtVoucherNo.BorderStyle = BorderStyle.FixedSingle;
            txtVoucherNo.Location = new Point(95, 12);
            txtVoucherNo.Name = "txtVoucherNo";
            txtVoucherNo.ReadOnly = true;
            txtVoucherNo.Size = new Size(90, 23);
            txtVoucherNo.TabIndex = 1;
            //
            // lblVoucherDate
            //
            lblVoucherDate.AutoSize = true;
            lblVoucherDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblVoucherDate.ForeColor = Color.Red;
            lblVoucherDate.Location = new Point(280, 15);
            lblVoucherDate.Name = "lblVoucherDate";
            lblVoucherDate.Size = new Size(35, 15);
            lblVoucherDate.TabIndex = 2;
            lblVoucherDate.Text = "Date";
            //
            // dtpVoucherDate
            //
            dtpVoucherDate.Format = DateTimePickerFormat.Short;
            dtpVoucherDate.Location = new Point(325, 12);
            dtpVoucherDate.Name = "dtpVoucherDate";
            dtpVoucherDate.Size = new Size(110, 23);
            dtpVoucherDate.TabIndex = 3;
            //
            // dgvLines
            //
            dgvLines.AllowUserToDeleteRows = false;
            dgvLines.AllowUserToResizeRows = false;
            dgvLines.BackgroundColor = Color.White;
            dgvLines.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLines.Columns.AddRange(new DataGridViewColumn[] { colCode, colAccount, colDebit, colCredit, colRemarks });
            dgvLines.EnableHeadersVisualStyles = false;
            dgvLines.Location = new Point(12, 50);
            dgvLines.Name = "dgvLines";
            dgvLines.RowHeadersVisible = false;
            dgvLines.Size = new Size(900, 320);
            dgvLines.TabIndex = 4;
            dgvLines.CellValueChanged += dgvLines_CellValueChanged;
            dgvLines.CurrentCellDirtyStateChanged += dgvLines_CurrentCellDirtyStateChanged;
            dgvLines.RowsRemoved += dgvLines_RowsRemoved;
            //
            // colCode
            //
            colCode.HeaderText = "Code";
            colCode.Name = "colCode";
            colCode.Width = 70;
            //
            // colAccount
            //
            colAccount.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colAccount.HeaderText = "Account";
            colAccount.Name = "colAccount";
            colAccount.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            colAccount.FlatStyle = FlatStyle.Flat;
            //
            // colDebit
            //
            colDebit.HeaderText = "Debit";
            colDebit.Name = "colDebit";
            colDebit.Width = 110;
            //
            // colCredit
            //
            colCredit.HeaderText = "Credit";
            colCredit.Name = "colCredit";
            colCredit.Width = 110;
            //
            // colRemarks
            //
            colRemarks.HeaderText = "Remarks";
            colRemarks.Name = "colRemarks";
            colRemarks.Width = 220;
            //
            // lblTotalDebit
            //
            lblTotalDebit.AutoSize = true;
            lblTotalDebit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalDebit.Location = new Point(12, 380);
            lblTotalDebit.Name = "lblTotalDebit";
            lblTotalDebit.Size = new Size(75, 15);
            lblTotalDebit.TabIndex = 5;
            lblTotalDebit.Text = "Total Debit";
            //
            // txtTotalDebit
            //
            txtTotalDebit.BackColor = Color.WhiteSmoke;
            txtTotalDebit.BorderStyle = BorderStyle.FixedSingle;
            txtTotalDebit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtTotalDebit.Location = new Point(12, 398);
            txtTotalDebit.Name = "txtTotalDebit";
            txtTotalDebit.ReadOnly = true;
            txtTotalDebit.Size = new Size(150, 23);
            txtTotalDebit.TabIndex = 6;
            txtTotalDebit.Text = "0";
            //
            // lblTotalCredit
            //
            lblTotalCredit.AutoSize = true;
            lblTotalCredit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalCredit.Location = new Point(180, 380);
            lblTotalCredit.Name = "lblTotalCredit";
            lblTotalCredit.Size = new Size(75, 15);
            lblTotalCredit.TabIndex = 7;
            lblTotalCredit.Text = "Total Credit";
            //
            // txtTotalCredit
            //
            txtTotalCredit.BackColor = Color.WhiteSmoke;
            txtTotalCredit.BorderStyle = BorderStyle.FixedSingle;
            txtTotalCredit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtTotalCredit.Location = new Point(180, 398);
            txtTotalCredit.Name = "txtTotalCredit";
            txtTotalCredit.ReadOnly = true;
            txtTotalCredit.Size = new Size(150, 23);
            txtTotalCredit.TabIndex = 8;
            txtTotalCredit.Text = "0";
            //
            // panelButtons
            //
            panelButtons.BorderStyle = BorderStyle.FixedSingle;
            panelButtons.Controls.Add(btnAdd);
            panelButtons.Controls.Add(btnUpdate);
            panelButtons.Controls.Add(btnDelete);
            panelButtons.Controls.Add(btnSave);
            panelButtons.Controls.Add(btnPrevious);
            panelButtons.Controls.Add(btnNext);
            panelButtons.Controls.Add(btnFind);
            panelButtons.Controls.Add(btnClose);
            panelButtons.Controls.Add(btnPrint);
            panelButtons.Controls.Add(btnWhatsapp);
            panelButtons.Location = new Point(12, 434);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(900, 40);
            panelButtons.TabIndex = 9;
            //
            // btnAdd
            //
            btnAdd.BackColor = Color.LightSteelBlue;
            btnAdd.FlatStyle = FlatStyle.Popup;
            btnAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAdd.Location = new Point(8, 6);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(60, 27);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            //
            // btnUpdate
            //
            btnUpdate.BackColor = Color.LightSteelBlue;
            btnUpdate.FlatStyle = FlatStyle.Popup;
            btnUpdate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnUpdate.Location = new Point(72, 6);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(60, 27);
            btnUpdate.TabIndex = 1;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            //
            // btnDelete
            //
            btnDelete.BackColor = Color.LightSteelBlue;
            btnDelete.FlatStyle = FlatStyle.Popup;
            btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDelete.Location = new Point(136, 6);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(60, 27);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            //
            // btnSave
            //
            btnSave.BackColor = Color.WhiteSmoke;
            btnSave.Enabled = false;
            btnSave.FlatStyle = FlatStyle.Popup;
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.Location = new Point(200, 6);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(60, 27);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            //
            // btnPrevious
            //
            btnPrevious.BackColor = Color.LightSteelBlue;
            btnPrevious.FlatStyle = FlatStyle.Popup;
            btnPrevious.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPrevious.Location = new Point(264, 6);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(70, 27);
            btnPrevious.TabIndex = 4;
            btnPrevious.Text = "Previous";
            btnPrevious.UseVisualStyleBackColor = false;
            btnPrevious.Click += btnPrevious_Click;
            //
            // btnNext
            //
            btnNext.BackColor = Color.LightSteelBlue;
            btnNext.FlatStyle = FlatStyle.Popup;
            btnNext.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnNext.Location = new Point(338, 6);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(60, 27);
            btnNext.TabIndex = 5;
            btnNext.Text = "Next";
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += btnNext_Click;
            //
            // btnFind
            //
            btnFind.BackColor = Color.LightSteelBlue;
            btnFind.FlatStyle = FlatStyle.Popup;
            btnFind.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnFind.Location = new Point(402, 6);
            btnFind.Name = "btnFind";
            btnFind.Size = new Size(55, 27);
            btnFind.TabIndex = 6;
            btnFind.Text = "Find";
            btnFind.UseVisualStyleBackColor = false;
            btnFind.Click += btnFind_Click;
            //
            // btnClose
            //
            btnClose.BackColor = Color.LightSteelBlue;
            btnClose.FlatStyle = FlatStyle.Popup;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClose.Location = new Point(461, 6);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(55, 27);
            btnClose.TabIndex = 7;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            //
            // btnPrint
            //
            btnPrint.BackColor = Color.LightSteelBlue;
            btnPrint.FlatStyle = FlatStyle.Popup;
            btnPrint.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPrint.Location = new Point(524, 6);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(55, 27);
            btnPrint.TabIndex = 8;
            btnPrint.Text = "Print";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;
            //
            // btnWhatsapp
            //
            btnWhatsapp.BackColor = Color.LightSteelBlue;
            btnWhatsapp.FlatStyle = FlatStyle.Popup;
            btnWhatsapp.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnWhatsapp.Location = new Point(587, 6);
            btnWhatsapp.Name = "btnWhatsapp";
            btnWhatsapp.Size = new Size(90, 27);
            btnWhatsapp.TabIndex = 9;
            btnWhatsapp.Text = "WhatsApp";
            btnWhatsapp.UseVisualStyleBackColor = false;
            btnWhatsapp.Click += btnWhatsapp_Click;
            //
            // JournalForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(924, 486);
            Controls.Add(panelButtons);
            Controls.Add(txtTotalCredit);
            Controls.Add(lblTotalCredit);
            Controls.Add(txtTotalDebit);
            Controls.Add(lblTotalDebit);
            Controls.Add(dgvLines);
            Controls.Add(dtpVoucherDate);
            Controls.Add(lblVoucherDate);
            Controls.Add(txtVoucherNo);
            Controls.Add(lblVoucherNo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "JournalForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Journal";
            Load += JournalForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvLines).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblVoucherNo;
        private TextBox txtVoucherNo;
        private Label lblVoucherDate;
        private DateTimePicker dtpVoucherDate;
        private DataGridView dgvLines;
        private DataGridViewTextBoxColumn colCode;
        private DataGridViewComboBoxColumn colAccount;
        private DataGridViewTextBoxColumn colDebit;
        private DataGridViewTextBoxColumn colCredit;
        private DataGridViewTextBoxColumn colRemarks;
        private Label lblTotalDebit;
        private TextBox txtTotalDebit;
        private Label lblTotalCredit;
        private TextBox txtTotalCredit;
        private Panel panelButtons;
        protected Button btnAdd;
        protected Button btnUpdate;
        protected Button btnDelete;
        protected Button btnSave;
        protected Button btnPrevious;
        protected Button btnNext;
        protected Button btnFind;
        protected Button btnClose;
        protected Button btnPrint;
        protected Button btnWhatsapp;
    }
}
