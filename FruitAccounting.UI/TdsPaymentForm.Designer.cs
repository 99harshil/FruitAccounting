namespace FruitAccounting.UI
{
    partial class TdsPaymentForm
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
            panelHeader = new Panel();
            lblTdsPaymentNo = new Label();
            txtTdsPaymentNo = new TextBox();
            lblPaymentDate = new Label();
            dtpPaymentDate = new DateTimePicker();
            lblBsrCode = new Label();
            txtBsrCode = new TextBox();
            lblChallanSerialNo = new Label();
            txtChallanSerialNo = new TextBox();
            lblTdsAccount = new Label();
            cmbTdsAccount = new ComboBox();
            btnNewTdsAccount = new Button();
            lblInterestRate = new Label();
            txtInterestRatePct = new TextBox();
            lblDaybook = new Label();
            cmbDaybook = new ComboBox();
            dgvDeductions = new DataGridView();
            colInclude = new DataGridViewCheckBoxColumn();
            colBillNo = new DataGridViewTextBoxColumn();
            colDedDate = new DataGridViewTextBoxColumn();
            colParty = new DataGridViewTextBoxColumn();
            colAmount = new DataGridViewTextBoxColumn();
            colDeductionId = new DataGridViewTextBoxColumn();
            panelFooter = new Panel();
            lblTax = new Label();
            txtTax = new TextBox();
            lblInterestAmount = new Label();
            txtInterestAmount = new TextBox();
            cmbInterestAccount = new ComboBox();
            lblFeesAmount = new Label();
            txtFeesAmount = new TextBox();
            cmbFeesAccount = new ComboBox();
            lblPenaltyAmount = new Label();
            txtPenaltyAmount = new TextBox();
            cmbPenaltyAccount = new ComboBox();
            lblTotalAmount = new Label();
            txtTotalAmount = new TextBox();
            lblRemarks = new Label();
            txtRemarks = new TextBox();
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
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDeductions).BeginInit();
            panelFooter.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();
            //
            // panelHeader
            //
            panelHeader.BorderStyle = BorderStyle.FixedSingle;
            panelHeader.Controls.Add(lblTdsPaymentNo);
            panelHeader.Controls.Add(txtTdsPaymentNo);
            panelHeader.Controls.Add(lblPaymentDate);
            panelHeader.Controls.Add(dtpPaymentDate);
            panelHeader.Controls.Add(lblBsrCode);
            panelHeader.Controls.Add(txtBsrCode);
            panelHeader.Controls.Add(lblChallanSerialNo);
            panelHeader.Controls.Add(txtChallanSerialNo);
            panelHeader.Controls.Add(lblTdsAccount);
            panelHeader.Controls.Add(cmbTdsAccount);
            panelHeader.Controls.Add(btnNewTdsAccount);
            panelHeader.Controls.Add(lblInterestRate);
            panelHeader.Controls.Add(txtInterestRatePct);
            panelHeader.Controls.Add(lblDaybook);
            panelHeader.Controls.Add(cmbDaybook);
            panelHeader.Location = new Point(12, 12);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(900, 100);
            panelHeader.TabIndex = 0;
            //
            // lblTdsPaymentNo
            //
            lblTdsPaymentNo.AutoSize = true;
            lblTdsPaymentNo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTdsPaymentNo.ForeColor = Color.Red;
            lblTdsPaymentNo.Location = new Point(10, 15);
            lblTdsPaymentNo.Name = "lblTdsPaymentNo";
            lblTdsPaymentNo.Size = new Size(50, 15);
            lblTdsPaymentNo.TabIndex = 0;
            lblTdsPaymentNo.Text = "Sr. No.";
            //
            // txtTdsPaymentNo
            //
            txtTdsPaymentNo.BackColor = Color.FromArgb(224, 224, 224);
            txtTdsPaymentNo.BorderStyle = BorderStyle.FixedSingle;
            txtTdsPaymentNo.Location = new Point(90, 12);
            txtTdsPaymentNo.Name = "txtTdsPaymentNo";
            txtTdsPaymentNo.ReadOnly = true;
            txtTdsPaymentNo.Size = new Size(70, 23);
            txtTdsPaymentNo.TabIndex = 1;
            //
            // lblPaymentDate
            //
            lblPaymentDate.AutoSize = true;
            lblPaymentDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPaymentDate.ForeColor = Color.Red;
            lblPaymentDate.Location = new Point(180, 15);
            lblPaymentDate.Name = "lblPaymentDate";
            lblPaymentDate.Size = new Size(40, 15);
            lblPaymentDate.TabIndex = 2;
            lblPaymentDate.Text = "Date";
            //
            // dtpPaymentDate
            //
            dtpPaymentDate.Format = DateTimePickerFormat.Short;
            dtpPaymentDate.Location = new Point(230, 12);
            dtpPaymentDate.Name = "dtpPaymentDate";
            dtpPaymentDate.Size = new Size(110, 23);
            dtpPaymentDate.TabIndex = 3;
            //
            // lblBsrCode
            //
            lblBsrCode.AutoSize = true;
            lblBsrCode.Location = new Point(360, 15);
            lblBsrCode.Name = "lblBsrCode";
            lblBsrCode.Size = new Size(60, 15);
            lblBsrCode.TabIndex = 4;
            lblBsrCode.Text = "BSR Code";
            //
            // txtBsrCode
            //
            txtBsrCode.BackColor = Color.FromArgb(224, 224, 224);
            txtBsrCode.BorderStyle = BorderStyle.FixedSingle;
            txtBsrCode.Location = new Point(430, 12);
            txtBsrCode.MaxLength = 7;
            txtBsrCode.Name = "txtBsrCode";
            txtBsrCode.Size = new Size(90, 23);
            txtBsrCode.TabIndex = 5;
            //
            // lblChallanSerialNo
            //
            lblChallanSerialNo.AutoSize = true;
            lblChallanSerialNo.Location = new Point(540, 15);
            lblChallanSerialNo.Name = "lblChallanSerialNo";
            lblChallanSerialNo.Size = new Size(60, 15);
            lblChallanSerialNo.TabIndex = 6;
            lblChallanSerialNo.Text = "Ch.Sr.No";
            //
            // txtChallanSerialNo
            //
            txtChallanSerialNo.BackColor = Color.FromArgb(224, 224, 224);
            txtChallanSerialNo.BorderStyle = BorderStyle.FixedSingle;
            txtChallanSerialNo.Location = new Point(610, 12);
            txtChallanSerialNo.MaxLength = 10;
            txtChallanSerialNo.Name = "txtChallanSerialNo";
            txtChallanSerialNo.Size = new Size(90, 23);
            txtChallanSerialNo.TabIndex = 7;
            //
            // lblTdsAccount
            //
            lblTdsAccount.AutoSize = true;
            lblTdsAccount.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTdsAccount.ForeColor = Color.Red;
            lblTdsAccount.Location = new Point(10, 47);
            lblTdsAccount.Name = "lblTdsAccount";
            lblTdsAccount.Size = new Size(55, 15);
            lblTdsAccount.TabIndex = 8;
            lblTdsAccount.Text = "TDS A/c";
            //
            // cmbTdsAccount
            //
            cmbTdsAccount.BackColor = Color.FromArgb(255, 255, 192);
            cmbTdsAccount.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTdsAccount.FlatStyle = FlatStyle.Flat;
            cmbTdsAccount.FormattingEnabled = true;
            cmbTdsAccount.Location = new Point(90, 44);
            cmbTdsAccount.Name = "cmbTdsAccount";
            cmbTdsAccount.Size = new Size(190, 23);
            cmbTdsAccount.TabIndex = 9;
            //
            // btnNewTdsAccount
            //
            btnNewTdsAccount.BackColor = Color.LightSteelBlue;
            btnNewTdsAccount.FlatStyle = FlatStyle.Popup;
            btnNewTdsAccount.Location = new Point(285, 43);
            btnNewTdsAccount.Name = "btnNewTdsAccount";
            btnNewTdsAccount.Size = new Size(55, 23);
            btnNewTdsAccount.TabIndex = 10;
            btnNewTdsAccount.Text = "New";
            btnNewTdsAccount.UseVisualStyleBackColor = false;
            btnNewTdsAccount.Click += btnNewTdsAccount_Click;
            //
            // lblInterestRate
            //
            lblInterestRate.AutoSize = true;
            lblInterestRate.Location = new Point(360, 47);
            lblInterestRate.Name = "lblInterestRate";
            lblInterestRate.Size = new Size(65, 15);
            lblInterestRate.TabIndex = 11;
            lblInterestRate.Text = "Interest %";
            //
            // txtInterestRatePct
            //
            txtInterestRatePct.BackColor = Color.FromArgb(224, 224, 224);
            txtInterestRatePct.BorderStyle = BorderStyle.FixedSingle;
            txtInterestRatePct.Location = new Point(430, 44);
            txtInterestRatePct.Name = "txtInterestRatePct";
            txtInterestRatePct.Size = new Size(90, 23);
            txtInterestRatePct.TabIndex = 12;
            txtInterestRatePct.Text = "0";
            //
            // lblDaybook
            //
            lblDaybook.AutoSize = true;
            lblDaybook.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDaybook.ForeColor = Color.Red;
            lblDaybook.Location = new Point(10, 77);
            lblDaybook.Name = "lblDaybook";
            lblDaybook.Size = new Size(56, 15);
            lblDaybook.TabIndex = 13;
            lblDaybook.Text = "Daybook";
            //
            // cmbDaybook
            //
            cmbDaybook.BackColor = Color.FromArgb(255, 255, 192);
            cmbDaybook.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDaybook.FlatStyle = FlatStyle.Flat;
            cmbDaybook.FormattingEnabled = true;
            cmbDaybook.Location = new Point(90, 74);
            cmbDaybook.Name = "cmbDaybook";
            cmbDaybook.Size = new Size(260, 23);
            cmbDaybook.TabIndex = 14;
            //
            // dgvDeductions
            //
            dgvDeductions.AllowUserToAddRows = true;
            dgvDeductions.AllowUserToDeleteRows = false;
            dgvDeductions.AllowUserToResizeRows = false;
            dgvDeductions.BackgroundColor = Color.White;
            dgvDeductions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDeductions.Columns.AddRange(new DataGridViewColumn[] { colInclude, colBillNo, colDedDate, colParty, colAmount, colDeductionId });
            dgvDeductions.Location = new Point(12, 118);
            dgvDeductions.Name = "dgvDeductions";
            dgvDeductions.RowHeadersVisible = false;
            dgvDeductions.Size = new Size(900, 260);
            dgvDeductions.TabIndex = 1;
            dgvDeductions.CellValueChanged += dgvDeductions_CellValueChanged;
            dgvDeductions.CurrentCellDirtyStateChanged += dgvDeductions_CurrentCellDirtyStateChanged;
            dgvDeductions.CellBeginEdit += dgvDeductions_CellBeginEdit;
            dgvDeductions.DefaultValuesNeeded += dgvDeductions_DefaultValuesNeeded;
            //
            // colInclude
            //
            colInclude.HeaderText = "Y/N";
            colInclude.Name = "colInclude";
            colInclude.Width = 40;
            //
            // colBillNo
            //
            colBillNo.HeaderText = "Bill No";
            colBillNo.Name = "colBillNo";
            colBillNo.Width = 80;
            //
            // colDedDate
            //
            colDedDate.HeaderText = "Date";
            colDedDate.Name = "colDedDate";
            colDedDate.Width = 90;
            //
            // colParty
            //
            colParty.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colParty.HeaderText = "Party";
            colParty.Name = "colParty";
            //
            // colAmount
            //
            colAmount.HeaderText = "Amount";
            colAmount.Name = "colAmount";
            colAmount.Width = 100;
            //
            // colDeductionId
            //
            colDeductionId.HeaderText = "Id";
            colDeductionId.Name = "colDeductionId";
            colDeductionId.Visible = false;
            //
            // panelFooter
            //
            panelFooter.BorderStyle = BorderStyle.FixedSingle;
            panelFooter.Controls.Add(lblTax);
            panelFooter.Controls.Add(txtTax);
            panelFooter.Controls.Add(lblInterestAmount);
            panelFooter.Controls.Add(txtInterestAmount);
            panelFooter.Controls.Add(cmbInterestAccount);
            panelFooter.Controls.Add(lblFeesAmount);
            panelFooter.Controls.Add(txtFeesAmount);
            panelFooter.Controls.Add(cmbFeesAccount);
            panelFooter.Controls.Add(lblPenaltyAmount);
            panelFooter.Controls.Add(txtPenaltyAmount);
            panelFooter.Controls.Add(cmbPenaltyAccount);
            panelFooter.Controls.Add(lblTotalAmount);
            panelFooter.Controls.Add(txtTotalAmount);
            panelFooter.Controls.Add(lblRemarks);
            panelFooter.Controls.Add(txtRemarks);
            panelFooter.Location = new Point(12, 386);
            panelFooter.Name = "panelFooter";
            panelFooter.Size = new Size(900, 130);
            panelFooter.TabIndex = 2;
            //
            // lblTax
            //
            lblTax.AutoSize = true;
            lblTax.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTax.Location = new Point(10, 15);
            lblTax.Name = "lblTax";
            lblTax.Size = new Size(30, 15);
            lblTax.TabIndex = 0;
            lblTax.Text = "Tax";
            //
            // txtTax
            //
            txtTax.BackColor = Color.WhiteSmoke;
            txtTax.BorderStyle = BorderStyle.FixedSingle;
            txtTax.Location = new Point(90, 12);
            txtTax.Name = "txtTax";
            txtTax.ReadOnly = true;
            txtTax.Size = new Size(110, 23);
            txtTax.TabIndex = 1;
            txtTax.Text = "0";
            //
            // lblInterestAmount
            //
            lblInterestAmount.AutoSize = true;
            lblInterestAmount.Location = new Point(10, 45);
            lblInterestAmount.Name = "lblInterestAmount";
            lblInterestAmount.Size = new Size(50, 15);
            lblInterestAmount.TabIndex = 2;
            lblInterestAmount.Text = "Interest";
            //
            // txtInterestAmount
            //
            txtInterestAmount.BackColor = Color.FromArgb(224, 224, 224);
            txtInterestAmount.BorderStyle = BorderStyle.FixedSingle;
            txtInterestAmount.Location = new Point(90, 42);
            txtInterestAmount.Name = "txtInterestAmount";
            txtInterestAmount.Size = new Size(110, 23);
            txtInterestAmount.TabIndex = 3;
            txtInterestAmount.Text = "0";
            txtInterestAmount.TextChanged += AmountField_Changed;
            //
            // cmbInterestAccount
            //
            cmbInterestAccount.BackColor = Color.FromArgb(255, 255, 192);
            cmbInterestAccount.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbInterestAccount.FlatStyle = FlatStyle.Flat;
            cmbInterestAccount.FormattingEnabled = true;
            cmbInterestAccount.Location = new Point(210, 42);
            cmbInterestAccount.Name = "cmbInterestAccount";
            cmbInterestAccount.Size = new Size(220, 23);
            cmbInterestAccount.TabIndex = 4;
            //
            // lblFeesAmount
            //
            lblFeesAmount.AutoSize = true;
            lblFeesAmount.Location = new Point(450, 45);
            lblFeesAmount.Name = "lblFeesAmount";
            lblFeesAmount.Size = new Size(35, 15);
            lblFeesAmount.TabIndex = 5;
            lblFeesAmount.Text = "Fees";
            //
            // txtFeesAmount
            //
            txtFeesAmount.BackColor = Color.FromArgb(224, 224, 224);
            txtFeesAmount.BorderStyle = BorderStyle.FixedSingle;
            txtFeesAmount.Location = new Point(500, 42);
            txtFeesAmount.Name = "txtFeesAmount";
            txtFeesAmount.Size = new Size(110, 23);
            txtFeesAmount.TabIndex = 6;
            txtFeesAmount.Text = "0";
            txtFeesAmount.TextChanged += AmountField_Changed;
            //
            // cmbFeesAccount
            //
            cmbFeesAccount.BackColor = Color.FromArgb(255, 255, 192);
            cmbFeesAccount.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFeesAccount.FlatStyle = FlatStyle.Flat;
            cmbFeesAccount.FormattingEnabled = true;
            cmbFeesAccount.Location = new Point(620, 42);
            cmbFeesAccount.Name = "cmbFeesAccount";
            cmbFeesAccount.Size = new Size(200, 23);
            cmbFeesAccount.TabIndex = 7;
            //
            // lblPenaltyAmount
            //
            lblPenaltyAmount.AutoSize = true;
            lblPenaltyAmount.Location = new Point(10, 75);
            lblPenaltyAmount.Name = "lblPenaltyAmount";
            lblPenaltyAmount.Size = new Size(75, 15);
            lblPenaltyAmount.TabIndex = 8;
            lblPenaltyAmount.Text = "Other Penalty";
            //
            // txtPenaltyAmount
            //
            txtPenaltyAmount.BackColor = Color.FromArgb(224, 224, 224);
            txtPenaltyAmount.BorderStyle = BorderStyle.FixedSingle;
            txtPenaltyAmount.Location = new Point(90, 72);
            txtPenaltyAmount.Name = "txtPenaltyAmount";
            txtPenaltyAmount.Size = new Size(110, 23);
            txtPenaltyAmount.TabIndex = 9;
            txtPenaltyAmount.Text = "0";
            txtPenaltyAmount.TextChanged += AmountField_Changed;
            //
            // cmbPenaltyAccount
            //
            cmbPenaltyAccount.BackColor = Color.FromArgb(255, 255, 192);
            cmbPenaltyAccount.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPenaltyAccount.FlatStyle = FlatStyle.Flat;
            cmbPenaltyAccount.FormattingEnabled = true;
            cmbPenaltyAccount.Location = new Point(210, 72);
            cmbPenaltyAccount.Name = "cmbPenaltyAccount";
            cmbPenaltyAccount.Size = new Size(220, 23);
            cmbPenaltyAccount.TabIndex = 10;
            //
            // lblTotalAmount
            //
            lblTotalAmount.AutoSize = true;
            lblTotalAmount.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalAmount.ForeColor = Color.Red;
            lblTotalAmount.Location = new Point(450, 75);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(45, 15);
            lblTotalAmount.TabIndex = 11;
            lblTotalAmount.Text = "Total";
            //
            // txtTotalAmount
            //
            txtTotalAmount.BackColor = Color.WhiteSmoke;
            txtTotalAmount.BorderStyle = BorderStyle.FixedSingle;
            txtTotalAmount.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtTotalAmount.ForeColor = Color.Red;
            txtTotalAmount.Location = new Point(500, 72);
            txtTotalAmount.Name = "txtTotalAmount";
            txtTotalAmount.ReadOnly = true;
            txtTotalAmount.Size = new Size(110, 23);
            txtTotalAmount.TabIndex = 12;
            txtTotalAmount.Text = "0";
            //
            // lblRemarks
            //
            lblRemarks.AutoSize = true;
            lblRemarks.Location = new Point(10, 103);
            lblRemarks.Name = "lblRemarks";
            lblRemarks.Size = new Size(53, 15);
            lblRemarks.TabIndex = 13;
            lblRemarks.Text = "Remarks";
            //
            // txtRemarks
            //
            txtRemarks.BackColor = Color.FromArgb(224, 224, 224);
            txtRemarks.BorderStyle = BorderStyle.FixedSingle;
            txtRemarks.Location = new Point(90, 100);
            txtRemarks.Name = "txtRemarks";
            txtRemarks.Size = new Size(730, 23);
            txtRemarks.TabIndex = 14;
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
            panelButtons.Location = new Point(12, 524);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(900, 40);
            panelButtons.TabIndex = 3;
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
            // TdsPaymentForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(924, 576);
            Controls.Add(panelButtons);
            Controls.Add(panelFooter);
            Controls.Add(dgvDeductions);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TdsPaymentForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TDS Payment";
            Load += TdsPaymentForm_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDeductions).EndInit();
            panelFooter.ResumeLayout(false);
            panelFooter.PerformLayout();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private Label lblTdsPaymentNo;
        private TextBox txtTdsPaymentNo;
        private Label lblPaymentDate;
        private DateTimePicker dtpPaymentDate;
        private Label lblBsrCode;
        private TextBox txtBsrCode;
        private Label lblChallanSerialNo;
        private TextBox txtChallanSerialNo;
        private Label lblTdsAccount;
        private ComboBox cmbTdsAccount;
        private Button btnNewTdsAccount;
        private Label lblInterestRate;
        private TextBox txtInterestRatePct;
        private Label lblDaybook;
        private ComboBox cmbDaybook;
        private DataGridView dgvDeductions;
        private DataGridViewCheckBoxColumn colInclude;
        private DataGridViewTextBoxColumn colBillNo;
        private DataGridViewTextBoxColumn colDedDate;
        private DataGridViewTextBoxColumn colParty;
        private DataGridViewTextBoxColumn colAmount;
        private DataGridViewTextBoxColumn colDeductionId;
        private Panel panelFooter;
        private Label lblTax;
        private TextBox txtTax;
        private Label lblInterestAmount;
        private TextBox txtInterestAmount;
        private ComboBox cmbInterestAccount;
        private Label lblFeesAmount;
        private TextBox txtFeesAmount;
        private ComboBox cmbFeesAccount;
        private Label lblPenaltyAmount;
        private TextBox txtPenaltyAmount;
        private ComboBox cmbPenaltyAccount;
        private Label lblTotalAmount;
        private TextBox txtTotalAmount;
        private Label lblRemarks;
        private TextBox txtRemarks;
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
