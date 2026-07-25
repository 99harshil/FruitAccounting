namespace FruitAccounting.UI
{
    partial class PaymentForm
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
            panelInput = new Panel();
            chkReturn = new CheckBox();
            dtpReturnDate = new DateTimePicker();
            lblRemarks = new Label();
            txtRemarks = new TextBox();
            lblTotalAmount = new Label();
            txtTotalAmount = new TextBox();
            lblAmountWords = new Label();
            lblBranch = new Label();
            txtBranch = new TextBox();
            lblBank = new Label();
            txtBankName = new TextBox();
            lblChequeNo = new Label();
            txtChequeNo = new TextBox();
            lblHamali = new Label();
            txtHamali = new TextBox();
            lblTds = new Label();
            txtTdsAmt = new TextBox();
            lblVatav = new Label();
            txtVatav = new TextBox();
            lblPaidAmount = new Label();
            txtPaidAmount = new TextBox();
            btnNewAccount = new Button();
            cmbAccountCode = new ComboBox();
            cmbAccountName = new ComboBox();
            lblAccount = new Label();
            btnNewDaybook = new Button();
            cmbDaybook = new ComboBox();
            lblDaybook = new Label();
            lblTime = new Label();
            txtTime = new TextBox();
            dtpPaymentDate = new DateTimePicker();
            lblPaymentDate = new Label();
            txtPaymentNo = new TextBox();
            lblPaymentNo = new Label();
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
            panelInput.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();
            //
            // panelInput
            //
            panelInput.BorderStyle = BorderStyle.FixedSingle;
            panelInput.Controls.Add(chkReturn);
            panelInput.Controls.Add(dtpReturnDate);
            panelInput.Controls.Add(lblRemarks);
            panelInput.Controls.Add(txtRemarks);
            panelInput.Controls.Add(lblTotalAmount);
            panelInput.Controls.Add(txtTotalAmount);
            panelInput.Controls.Add(lblAmountWords);
            panelInput.Controls.Add(lblBranch);
            panelInput.Controls.Add(txtBranch);
            panelInput.Controls.Add(lblBank);
            panelInput.Controls.Add(txtBankName);
            panelInput.Controls.Add(lblChequeNo);
            panelInput.Controls.Add(txtChequeNo);
            panelInput.Controls.Add(lblHamali);
            panelInput.Controls.Add(txtHamali);
            panelInput.Controls.Add(lblTds);
            panelInput.Controls.Add(txtTdsAmt);
            panelInput.Controls.Add(lblVatav);
            panelInput.Controls.Add(txtVatav);
            panelInput.Controls.Add(lblPaidAmount);
            panelInput.Controls.Add(txtPaidAmount);
            panelInput.Controls.Add(btnNewAccount);
            panelInput.Controls.Add(cmbAccountCode);
            panelInput.Controls.Add(cmbAccountName);
            panelInput.Controls.Add(lblAccount);
            panelInput.Controls.Add(btnNewDaybook);
            panelInput.Controls.Add(cmbDaybook);
            panelInput.Controls.Add(lblDaybook);
            panelInput.Controls.Add(lblTime);
            panelInput.Controls.Add(txtTime);
            panelInput.Controls.Add(dtpPaymentDate);
            panelInput.Controls.Add(lblPaymentDate);
            panelInput.Controls.Add(txtPaymentNo);
            panelInput.Controls.Add(lblPaymentNo);
            panelInput.Location = new Point(12, 12);
            panelInput.Name = "panelInput";
            panelInput.Size = new Size(520, 400);
            panelInput.TabIndex = 0;
            //
            // lblPaymentNo
            //
            lblPaymentNo.AutoSize = true;
            lblPaymentNo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPaymentNo.ForeColor = Color.Red;
            lblPaymentNo.Location = new Point(10, 17);
            lblPaymentNo.Name = "lblPaymentNo";
            lblPaymentNo.Size = new Size(58, 15);
            lblPaymentNo.TabIndex = 0;
            lblPaymentNo.Text = "Rec. No.";
            //
            // txtPaymentNo
            //
            txtPaymentNo.BackColor = Color.FromArgb(224, 224, 224);
            txtPaymentNo.BorderStyle = BorderStyle.FixedSingle;
            txtPaymentNo.Location = new Point(90, 14);
            txtPaymentNo.Name = "txtPaymentNo";
            txtPaymentNo.ReadOnly = true;
            txtPaymentNo.Size = new Size(70, 23);
            txtPaymentNo.TabIndex = 1;
            //
            // lblPaymentDate
            //
            lblPaymentDate.AutoSize = true;
            lblPaymentDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPaymentDate.ForeColor = Color.Red;
            lblPaymentDate.Location = new Point(180, 17);
            lblPaymentDate.Name = "lblPaymentDate";
            lblPaymentDate.Size = new Size(60, 15);
            lblPaymentDate.TabIndex = 2;
            lblPaymentDate.Text = "Date";
            //
            // dtpPaymentDate
            //
            dtpPaymentDate.Format = DateTimePickerFormat.Short;
            dtpPaymentDate.Location = new Point(265, 14);
            dtpPaymentDate.Name = "dtpPaymentDate";
            dtpPaymentDate.Size = new Size(110, 23);
            dtpPaymentDate.TabIndex = 3;
            dtpPaymentDate.ValueChanged += AmountField_Changed;
            //
            // txtTime
            //
            txtTime.BackColor = Color.FromArgb(224, 224, 224);
            txtTime.BorderStyle = BorderStyle.FixedSingle;
            txtTime.Location = new Point(440, 14);
            txtTime.Name = "txtTime";
            txtTime.ReadOnly = true;
            txtTime.Size = new Size(65, 23);
            txtTime.TabIndex = 4;
            //
            // lblTime
            //
            lblTime.AutoSize = true;
            lblTime.Location = new Point(400, 17);
            lblTime.Name = "lblTime";
            lblTime.Size = new Size(34, 15);
            lblTime.TabIndex = 5;
            lblTime.Text = "Time";
            //
            // lblDaybook
            //
            lblDaybook.AutoSize = true;
            lblDaybook.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDaybook.ForeColor = Color.Red;
            lblDaybook.Location = new Point(10, 47);
            lblDaybook.Name = "lblDaybook";
            lblDaybook.Size = new Size(56, 15);
            lblDaybook.TabIndex = 6;
            lblDaybook.Text = "Daybook";
            //
            // cmbDaybook
            //
            cmbDaybook.BackColor = Color.FromArgb(255, 255, 192);
            cmbDaybook.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDaybook.FlatStyle = FlatStyle.Flat;
            cmbDaybook.FormattingEnabled = true;
            cmbDaybook.Location = new Point(90, 44);
            cmbDaybook.Name = "cmbDaybook";
            cmbDaybook.Size = new Size(360, 23);
            cmbDaybook.TabIndex = 7;
            //
            // btnNewDaybook
            //
            btnNewDaybook.BackColor = Color.LightSteelBlue;
            btnNewDaybook.FlatStyle = FlatStyle.Popup;
            btnNewDaybook.Location = new Point(455, 43);
            btnNewDaybook.Name = "btnNewDaybook";
            btnNewDaybook.Size = new Size(55, 23);
            btnNewDaybook.TabIndex = 8;
            btnNewDaybook.Text = "New";
            btnNewDaybook.UseVisualStyleBackColor = false;
            btnNewDaybook.Click += btnNewDaybook_Click;
            //
            // lblAccount
            //
            lblAccount.AutoSize = true;
            lblAccount.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAccount.ForeColor = Color.Red;
            lblAccount.Location = new Point(10, 77);
            lblAccount.Name = "lblAccount";
            lblAccount.Size = new Size(52, 15);
            lblAccount.TabIndex = 9;
            lblAccount.Text = "Account";
            //
            // cmbAccountCode
            //
            cmbAccountCode.BackColor = Color.FromArgb(255, 255, 192);
            cmbAccountCode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAccountCode.FlatStyle = FlatStyle.Flat;
            cmbAccountCode.FormattingEnabled = true;
            cmbAccountCode.Location = new Point(90, 74);
            cmbAccountCode.Name = "cmbAccountCode";
            cmbAccountCode.Size = new Size(90, 23);
            cmbAccountCode.TabIndex = 10;
            cmbAccountCode.SelectedIndexChanged += cmbAccountCode_SelectedIndexChanged;
            //
            // cmbAccountName
            //
            cmbAccountName.BackColor = Color.FromArgb(255, 255, 192);
            cmbAccountName.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAccountName.FlatStyle = FlatStyle.Flat;
            cmbAccountName.FormattingEnabled = true;
            cmbAccountName.Location = new Point(185, 74);
            cmbAccountName.Name = "cmbAccountName";
            cmbAccountName.Size = new Size(265, 23);
            cmbAccountName.TabIndex = 11;
            cmbAccountName.SelectedIndexChanged += cmbAccountName_SelectedIndexChanged;
            //
            // btnNewAccount
            //
            btnNewAccount.BackColor = Color.LightSteelBlue;
            btnNewAccount.FlatStyle = FlatStyle.Popup;
            btnNewAccount.Location = new Point(455, 73);
            btnNewAccount.Name = "btnNewAccount";
            btnNewAccount.Size = new Size(55, 23);
            btnNewAccount.TabIndex = 12;
            btnNewAccount.Text = "New";
            btnNewAccount.UseVisualStyleBackColor = false;
            btnNewAccount.Click += btnNewAccount_Click;
            //
            // lblPaidAmount
            //
            lblPaidAmount.AutoSize = true;
            lblPaidAmount.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPaidAmount.ForeColor = Color.Red;
            lblPaidAmount.Location = new Point(10, 107);
            lblPaidAmount.Name = "lblPaidAmount";
            lblPaidAmount.Size = new Size(75, 15);
            lblPaidAmount.TabIndex = 12;
            lblPaidAmount.Text = "Paid Amount";
            //
            // txtPaidAmount
            //
            txtPaidAmount.BackColor = Color.FromArgb(224, 224, 224);
            txtPaidAmount.BorderStyle = BorderStyle.FixedSingle;
            txtPaidAmount.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtPaidAmount.ForeColor = Color.Red;
            txtPaidAmount.Location = new Point(90, 104);
            txtPaidAmount.Name = "txtPaidAmount";
            txtPaidAmount.Size = new Size(120, 23);
            txtPaidAmount.TabIndex = 13;
            txtPaidAmount.Text = "0";
            txtPaidAmount.TextChanged += AmountField_Changed;
            //
            // lblVatav
            //
            lblVatav.AutoSize = true;
            lblVatav.Location = new Point(230, 107);
            lblVatav.Name = "lblVatav";
            lblVatav.Size = new Size(38, 15);
            lblVatav.TabIndex = 14;
            lblVatav.Text = "Vatav";
            //
            // txtVatav
            //
            txtVatav.BackColor = Color.FromArgb(224, 224, 224);
            txtVatav.BorderStyle = BorderStyle.FixedSingle;
            txtVatav.Location = new Point(300, 104);
            txtVatav.Name = "txtVatav";
            txtVatav.Size = new Size(100, 23);
            txtVatav.TabIndex = 15;
            txtVatav.Text = "0";
            txtVatav.TextChanged += AmountField_Changed;
            //
            // lblHamali
            //
            lblHamali.AutoSize = true;
            lblHamali.Location = new Point(10, 138);
            lblHamali.Name = "lblHamali";
            lblHamali.Size = new Size(28, 15);
            lblHamali.TabIndex = 16;
            lblHamali.Text = "Hamali";
            //
            // txtHamali
            //
            txtHamali.BackColor = Color.FromArgb(224, 224, 224);
            txtHamali.BorderStyle = BorderStyle.FixedSingle;
            txtHamali.Location = new Point(90, 135);
            txtHamali.Name = "txtHamali";
            txtHamali.Size = new Size(120, 23);
            txtHamali.TabIndex = 17;
            txtHamali.Text = "0";
            txtHamali.TextChanged += AmountField_Changed;
            //
            // lblTds
            //
            lblTds.AutoSize = true;
            lblTds.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTds.ForeColor = Color.Red;
            lblTds.Location = new Point(230, 138);
            lblTds.Name = "lblTds";
            lblTds.Size = new Size(30, 15);
            lblTds.TabIndex = 18;
            lblTds.Text = "TDS";
            //
            // txtTdsAmt
            //
            txtTdsAmt.BackColor = Color.WhiteSmoke;
            txtTdsAmt.BorderStyle = BorderStyle.FixedSingle;
            txtTdsAmt.Location = new Point(300, 135);
            txtTdsAmt.Name = "txtTdsAmt";
            txtTdsAmt.ReadOnly = true;
            txtTdsAmt.Size = new Size(100, 23);
            txtTdsAmt.TabIndex = 19;
            txtTdsAmt.Text = "0";
            //
            // lblChequeNo
            //
            lblChequeNo.AutoSize = true;
            lblChequeNo.Location = new Point(10, 168);
            lblChequeNo.Name = "lblChequeNo";
            lblChequeNo.Size = new Size(66, 15);
            lblChequeNo.TabIndex = 20;
            lblChequeNo.Text = "Cheque No.";
            //
            // txtChequeNo
            //
            txtChequeNo.BackColor = Color.FromArgb(224, 224, 224);
            txtChequeNo.BorderStyle = BorderStyle.FixedSingle;
            txtChequeNo.Location = new Point(90, 165);
            txtChequeNo.Name = "txtChequeNo";
            txtChequeNo.Size = new Size(120, 23);
            txtChequeNo.TabIndex = 21;
            //
            // lblBank
            //
            lblBank.AutoSize = true;
            lblBank.Location = new Point(230, 168);
            lblBank.Name = "lblBank";
            lblBank.Size = new Size(35, 15);
            lblBank.TabIndex = 22;
            lblBank.Text = "Bank";
            //
            // txtBankName
            //
            txtBankName.BackColor = Color.FromArgb(224, 224, 224);
            txtBankName.BorderStyle = BorderStyle.FixedSingle;
            txtBankName.Location = new Point(300, 165);
            txtBankName.Name = "txtBankName";
            txtBankName.Size = new Size(210, 23);
            txtBankName.TabIndex = 23;
            //
            // lblBranch
            //
            lblBranch.AutoSize = true;
            lblBranch.Location = new Point(10, 199);
            lblBranch.Name = "lblBranch";
            lblBranch.Size = new Size(43, 15);
            lblBranch.TabIndex = 24;
            lblBranch.Text = "Branch";
            //
            // txtBranch
            //
            txtBranch.BackColor = Color.FromArgb(224, 224, 224);
            txtBranch.BorderStyle = BorderStyle.FixedSingle;
            txtBranch.Location = new Point(90, 196);
            txtBranch.Name = "txtBranch";
            txtBranch.Size = new Size(420, 23);
            txtBranch.TabIndex = 25;
            //
            // lblAmountWords
            //
            lblAmountWords.AutoSize = true;
            lblAmountWords.Font = new Font("Segoe UI", 8.5F, FontStyle.Italic);
            lblAmountWords.ForeColor = Color.DarkRed;
            lblAmountWords.Location = new Point(10, 228);
            lblAmountWords.MaximumSize = new Size(500, 0);
            lblAmountWords.Name = "lblAmountWords";
            lblAmountWords.Size = new Size(58, 14);
            lblAmountWords.TabIndex = 26;
            lblAmountWords.Text = "Rupees Zero Only.";
            //
            // txtTotalAmount
            //
            txtTotalAmount.BackColor = Color.WhiteSmoke;
            txtTotalAmount.BorderStyle = BorderStyle.FixedSingle;
            txtTotalAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            txtTotalAmount.ForeColor = Color.Red;
            txtTotalAmount.Location = new Point(120, 251);
            txtTotalAmount.Name = "txtTotalAmount";
            txtTotalAmount.ReadOnly = true;
            txtTotalAmount.Size = new Size(150, 25);
            txtTotalAmount.TabIndex = 27;
            txtTotalAmount.Text = "0";
            //
            // lblTotalAmount
            //
            lblTotalAmount.AutoSize = true;
            lblTotalAmount.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalAmount.ForeColor = Color.Red;
            lblTotalAmount.Location = new Point(10, 257);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(87, 15);
            lblTotalAmount.TabIndex = 28;
            lblTotalAmount.Text = "Total Amount";
            //
            // txtRemarks
            //
            txtRemarks.BackColor = Color.FromArgb(224, 224, 224);
            txtRemarks.BorderStyle = BorderStyle.FixedSingle;
            txtRemarks.Location = new Point(90, 287);
            txtRemarks.Multiline = true;
            txtRemarks.Name = "txtRemarks";
            txtRemarks.Size = new Size(420, 60);
            txtRemarks.TabIndex = 29;
            //
            // lblRemarks
            //
            lblRemarks.AutoSize = true;
            lblRemarks.Location = new Point(10, 290);
            lblRemarks.Name = "lblRemarks";
            lblRemarks.Size = new Size(53, 15);
            lblRemarks.TabIndex = 30;
            lblRemarks.Text = "Remarks";
            //
            // dtpReturnDate
            //
            dtpReturnDate.Enabled = false;
            dtpReturnDate.Format = DateTimePickerFormat.Short;
            dtpReturnDate.Location = new Point(220, 360);
            dtpReturnDate.Name = "dtpReturnDate";
            dtpReturnDate.ShowCheckBox = true;
            dtpReturnDate.Checked = false;
            dtpReturnDate.Size = new Size(120, 23);
            dtpReturnDate.TabIndex = 31;
            //
            // chkReturn
            //
            chkReturn.AutoSize = true;
            chkReturn.Location = new Point(90, 363);
            chkReturn.Name = "chkReturn";
            chkReturn.Size = new Size(65, 19);
            chkReturn.TabIndex = 32;
            chkReturn.Text = "Return";
            chkReturn.UseVisualStyleBackColor = true;
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
            panelButtons.Location = new Point(12, 420);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(690, 40);
            panelButtons.TabIndex = 1;
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
            // PaymentForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(714, 472);
            Controls.Add(panelButtons);
            Controls.Add(panelInput);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PaymentForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Payment";
            Load += PaymentForm_Load;
            panelInput.ResumeLayout(false);
            panelInput.PerformLayout();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelInput;
        private Label lblPaymentNo;
        private TextBox txtPaymentNo;
        private Label lblPaymentDate;
        private DateTimePicker dtpPaymentDate;
        private TextBox txtTime;
        private Label lblTime;
        private Label lblDaybook;
        private ComboBox cmbDaybook;
        private Button btnNewDaybook;
        private Label lblAccount;
        private ComboBox cmbAccountCode;
        private ComboBox cmbAccountName;
        private Button btnNewAccount;
        private Label lblPaidAmount;
        private TextBox txtPaidAmount;
        private Label lblVatav;
        private TextBox txtVatav;
        private Label lblHamali;
        private TextBox txtHamali;
        private Label lblTds;
        private TextBox txtTdsAmt;
        private Label lblChequeNo;
        private TextBox txtChequeNo;
        private Label lblBank;
        private TextBox txtBankName;
        private Label lblBranch;
        private TextBox txtBranch;
        private Label lblAmountWords;
        private TextBox txtTotalAmount;
        private Label lblTotalAmount;
        private TextBox txtRemarks;
        private Label lblRemarks;
        private DateTimePicker dtpReturnDate;
        private CheckBox chkReturn;
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
