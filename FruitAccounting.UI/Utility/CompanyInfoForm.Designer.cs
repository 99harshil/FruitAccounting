namespace FruitAccounting.UI
{
    partial class CompanyInfoForm
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
            lblName = new Label();
            txtName = new TextBox();
            lblAddress1 = new Label();
            txtAddress1 = new TextBox();
            lblAddress2 = new Label();
            txtAddress2 = new TextBox();
            lblCity = new Label();
            txtCity = new TextBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblPanNo = new Label();
            txtPanNo = new TextBox();
            lblGstin = new Label();
            txtGstin = new TextBox();
            lblApmcLicenceNo = new Label();
            txtApmcLicenceNo = new TextBox();
            lblApmcPct = new Label();
            txtApmcPct = new TextBox();
            lblBankName = new Label();
            txtBankName = new TextBox();
            lblBankAccountNo = new Label();
            txtBankAccountNo = new TextBox();
            lblBankIfsc = new Label();
            txtBankIfsc = new TextBox();
            btnSave = new Button();
            btnClose = new Button();
            SuspendLayout();
            //
            // lblName
            //
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblName.Location = new Point(12, 15);
            lblName.Name = "lblName";
            lblName.Size = new Size(70, 15);
            lblName.TabIndex = 0;
            lblName.Text = "Company";
            //
            // txtName
            //
            txtName.BackColor = Color.WhiteSmoke;
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Location = new Point(140, 12);
            txtName.Name = "txtName";
            txtName.ReadOnly = true;
            txtName.Size = new Size(300, 23);
            txtName.TabIndex = 1;
            //
            // lblAddress1
            //
            lblAddress1.AutoSize = true;
            lblAddress1.Location = new Point(12, 46);
            lblAddress1.Name = "lblAddress1";
            lblAddress1.Size = new Size(58, 15);
            lblAddress1.TabIndex = 2;
            lblAddress1.Text = "Address 1";
            //
            // txtAddress1
            //
            txtAddress1.BackColor = Color.FromArgb(224, 224, 224);
            txtAddress1.BorderStyle = BorderStyle.FixedSingle;
            txtAddress1.Location = new Point(140, 43);
            txtAddress1.Name = "txtAddress1";
            txtAddress1.Size = new Size(300, 23);
            txtAddress1.TabIndex = 3;
            //
            // lblAddress2
            //
            lblAddress2.AutoSize = true;
            lblAddress2.Location = new Point(12, 77);
            lblAddress2.Name = "lblAddress2";
            lblAddress2.Size = new Size(58, 15);
            lblAddress2.TabIndex = 4;
            lblAddress2.Text = "Address 2";
            //
            // txtAddress2
            //
            txtAddress2.BackColor = Color.FromArgb(224, 224, 224);
            txtAddress2.BorderStyle = BorderStyle.FixedSingle;
            txtAddress2.Location = new Point(140, 74);
            txtAddress2.Name = "txtAddress2";
            txtAddress2.Size = new Size(300, 23);
            txtAddress2.TabIndex = 5;
            //
            // lblCity
            //
            lblCity.AutoSize = true;
            lblCity.Location = new Point(12, 108);
            lblCity.Name = "lblCity";
            lblCity.Size = new Size(27, 15);
            lblCity.TabIndex = 6;
            lblCity.Text = "City";
            //
            // txtCity
            //
            txtCity.BackColor = Color.FromArgb(224, 224, 224);
            txtCity.BorderStyle = BorderStyle.FixedSingle;
            txtCity.Location = new Point(140, 105);
            txtCity.Name = "txtCity";
            txtCity.Size = new Size(300, 23);
            txtCity.TabIndex = 7;
            //
            // lblPhone
            //
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(12, 139);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(40, 15);
            lblPhone.TabIndex = 8;
            lblPhone.Text = "Phone";
            //
            // txtPhone
            //
            txtPhone.BackColor = Color.FromArgb(224, 224, 224);
            txtPhone.BorderStyle = BorderStyle.FixedSingle;
            txtPhone.Location = new Point(140, 136);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(300, 23);
            txtPhone.TabIndex = 9;
            //
            // lblEmail
            //
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(12, 170);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(37, 15);
            lblEmail.TabIndex = 10;
            lblEmail.Text = "Email";
            //
            // txtEmail
            //
            txtEmail.BackColor = Color.FromArgb(224, 224, 224);
            txtEmail.BorderStyle = BorderStyle.FixedSingle;
            txtEmail.Location = new Point(140, 167);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(300, 23);
            txtEmail.TabIndex = 11;
            //
            // lblPanNo
            //
            lblPanNo.AutoSize = true;
            lblPanNo.Location = new Point(12, 201);
            lblPanNo.Name = "lblPanNo";
            lblPanNo.Size = new Size(43, 15);
            lblPanNo.TabIndex = 12;
            lblPanNo.Text = "PAN No";
            //
            // txtPanNo
            //
            txtPanNo.BackColor = Color.FromArgb(224, 224, 224);
            txtPanNo.BorderStyle = BorderStyle.FixedSingle;
            txtPanNo.Location = new Point(140, 198);
            txtPanNo.Name = "txtPanNo";
            txtPanNo.Size = new Size(150, 23);
            txtPanNo.TabIndex = 13;
            //
            // lblGstin
            //
            lblGstin.AutoSize = true;
            lblGstin.Location = new Point(300, 201);
            lblGstin.Name = "lblGstin";
            lblGstin.Size = new Size(38, 15);
            lblGstin.TabIndex = 14;
            lblGstin.Text = "GSTIN";
            //
            // txtGstin
            //
            txtGstin.BackColor = Color.FromArgb(224, 224, 224);
            txtGstin.BorderStyle = BorderStyle.FixedSingle;
            txtGstin.Location = new Point(360, 198);
            txtGstin.Name = "txtGstin";
            txtGstin.Size = new Size(150, 23);
            txtGstin.TabIndex = 15;
            //
            // lblApmcLicenceNo
            //
            lblApmcLicenceNo.AutoSize = true;
            lblApmcLicenceNo.Location = new Point(12, 232);
            lblApmcLicenceNo.Name = "lblApmcLicenceNo";
            lblApmcLicenceNo.Size = new Size(97, 15);
            lblApmcLicenceNo.TabIndex = 16;
            lblApmcLicenceNo.Text = "APMC Licence No";
            //
            // txtApmcLicenceNo
            //
            txtApmcLicenceNo.BackColor = Color.FromArgb(224, 224, 224);
            txtApmcLicenceNo.BorderStyle = BorderStyle.FixedSingle;
            txtApmcLicenceNo.Location = new Point(140, 229);
            txtApmcLicenceNo.Name = "txtApmcLicenceNo";
            txtApmcLicenceNo.Size = new Size(150, 23);
            txtApmcLicenceNo.TabIndex = 17;
            //
            // lblApmcPct
            //
            lblApmcPct.AutoSize = true;
            lblApmcPct.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblApmcPct.ForeColor = Color.DarkRed;
            lblApmcPct.Location = new Point(300, 232);
            lblApmcPct.Name = "lblApmcPct";
            lblApmcPct.Size = new Size(60, 15);
            lblApmcPct.TabIndex = 18;
            lblApmcPct.Text = "APMC %";
            //
            // txtApmcPct
            //
            txtApmcPct.BackColor = Color.FromArgb(224, 224, 224);
            txtApmcPct.BorderStyle = BorderStyle.FixedSingle;
            txtApmcPct.Location = new Point(360, 229);
            txtApmcPct.Name = "txtApmcPct";
            txtApmcPct.Size = new Size(80, 23);
            txtApmcPct.TabIndex = 19;
            txtApmcPct.Text = "0";
            //
            // lblBankName
            //
            lblBankName.AutoSize = true;
            lblBankName.Location = new Point(12, 263);
            lblBankName.Name = "lblBankName";
            lblBankName.Size = new Size(72, 15);
            lblBankName.TabIndex = 20;
            lblBankName.Text = "Bank Name";
            //
            // txtBankName
            //
            txtBankName.BackColor = Color.FromArgb(224, 224, 224);
            txtBankName.BorderStyle = BorderStyle.FixedSingle;
            txtBankName.Location = new Point(140, 260);
            txtBankName.Name = "txtBankName";
            txtBankName.Size = new Size(300, 23);
            txtBankName.TabIndex = 21;
            //
            // lblBankAccountNo
            //
            lblBankAccountNo.AutoSize = true;
            lblBankAccountNo.Location = new Point(12, 294);
            lblBankAccountNo.Name = "lblBankAccountNo";
            lblBankAccountNo.Size = new Size(84, 15);
            lblBankAccountNo.TabIndex = 22;
            lblBankAccountNo.Text = "Bank A/c No.";
            //
            // txtBankAccountNo
            //
            txtBankAccountNo.BackColor = Color.FromArgb(224, 224, 224);
            txtBankAccountNo.BorderStyle = BorderStyle.FixedSingle;
            txtBankAccountNo.Location = new Point(140, 291);
            txtBankAccountNo.Name = "txtBankAccountNo";
            txtBankAccountNo.Size = new Size(150, 23);
            txtBankAccountNo.TabIndex = 23;
            //
            // lblBankIfsc
            //
            lblBankIfsc.AutoSize = true;
            lblBankIfsc.Location = new Point(300, 294);
            lblBankIfsc.Name = "lblBankIfsc";
            lblBankIfsc.Size = new Size(33, 15);
            lblBankIfsc.TabIndex = 24;
            lblBankIfsc.Text = "IFSC";
            //
            // txtBankIfsc
            //
            txtBankIfsc.BackColor = Color.FromArgb(224, 224, 224);
            txtBankIfsc.BorderStyle = BorderStyle.FixedSingle;
            txtBankIfsc.Location = new Point(360, 291);
            txtBankIfsc.Name = "txtBankIfsc";
            txtBankIfsc.Size = new Size(150, 23);
            txtBankIfsc.TabIndex = 25;
            //
            // btnSave
            //
            btnSave.BackColor = Color.LightSteelBlue;
            btnSave.FlatStyle = FlatStyle.Popup;
            btnSave.Location = new Point(280, 335);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(80, 28);
            btnSave.TabIndex = 26;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            //
            // btnClose
            //
            btnClose.BackColor = Color.LightSteelBlue;
            btnClose.FlatStyle = FlatStyle.Popup;
            btnClose.Location = new Point(380, 335);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(80, 28);
            btnClose.TabIndex = 27;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            //
            // CompanyInfoForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(524, 379);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblAddress1);
            Controls.Add(txtAddress1);
            Controls.Add(lblAddress2);
            Controls.Add(txtAddress2);
            Controls.Add(lblCity);
            Controls.Add(txtCity);
            Controls.Add(lblPhone);
            Controls.Add(txtPhone);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblPanNo);
            Controls.Add(txtPanNo);
            Controls.Add(lblGstin);
            Controls.Add(txtGstin);
            Controls.Add(lblApmcLicenceNo);
            Controls.Add(txtApmcLicenceNo);
            Controls.Add(lblApmcPct);
            Controls.Add(txtApmcPct);
            Controls.Add(lblBankName);
            Controls.Add(txtBankName);
            Controls.Add(lblBankAccountNo);
            Controls.Add(txtBankAccountNo);
            Controls.Add(lblBankIfsc);
            Controls.Add(txtBankIfsc);
            Controls.Add(btnSave);
            Controls.Add(btnClose);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CompanyInfoForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Company Information";
            Load += CompanyInfoForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblName;
        private TextBox txtName;
        private Label lblAddress1;
        private TextBox txtAddress1;
        private Label lblAddress2;
        private TextBox txtAddress2;
        private Label lblCity;
        private TextBox txtCity;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPanNo;
        private TextBox txtPanNo;
        private Label lblGstin;
        private TextBox txtGstin;
        private Label lblApmcLicenceNo;
        private TextBox txtApmcLicenceNo;
        private Label lblApmcPct;
        private TextBox txtApmcPct;
        private Label lblBankName;
        private TextBox txtBankName;
        private Label lblBankAccountNo;
        private TextBox txtBankAccountNo;
        private Label lblBankIfsc;
        private TextBox txtBankIfsc;
        private Button btnSave;
        private Button btnClose;
    }
}
