namespace FruitAccounting.UI
{
    partial class CreateUserForm
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

        private void InitializeComponent()
        {
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblDisplayName = new Label();
            txtDisplayName = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblConfirmPassword = new Label();
            txtConfirmPassword = new TextBox();
            lblRole = new Label();
            cmbRole = new ComboBox();
            chkIsActive = new CheckBox();
            btnCreate = new Button();
            btnCancel = new Button();
            lblError = new Label();
            SuspendLayout();

            //
            // lblUsername
            //
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUsername.Location = new Point(37, 22);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(65, 15);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Username";
            lblUsername.TextAlign = ContentAlignment.MiddleRight;

            //
            // txtUsername
            //
            txtUsername.BackColor = Color.FromArgb(224, 224, 224);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Location = new Point(108, 19);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(180, 23);
            txtUsername.TabIndex = 2;

            //
            // lblDisplayName
            //
            lblDisplayName.AutoSize = true;
            lblDisplayName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDisplayName.Location = new Point(23, 51);
            lblDisplayName.Name = "lblDisplayName";
            lblDisplayName.Size = new Size(79, 15);
            lblDisplayName.TabIndex = 3;
            lblDisplayName.Text = "Display Name";
            lblDisplayName.TextAlign = ContentAlignment.MiddleRight;

            //
            // txtDisplayName
            //
            txtDisplayName.BackColor = Color.FromArgb(224, 224, 224);
            txtDisplayName.BorderStyle = BorderStyle.FixedSingle;
            txtDisplayName.Location = new Point(108, 48);
            txtDisplayName.Name = "txtDisplayName";
            txtDisplayName.Size = new Size(180, 23);
            txtDisplayName.TabIndex = 4;

            //
            // lblPassword
            //
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPassword.Location = new Point(35, 80);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(67, 15);
            lblPassword.TabIndex = 5;
            lblPassword.Text = "Password";
            lblPassword.TextAlign = ContentAlignment.MiddleRight;

            //
            // txtPassword
            //
            txtPassword.BackColor = Color.FromArgb(224, 224, 224);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Location = new Point(108, 77);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(180, 23);
            txtPassword.TabIndex = 6;
            txtPassword.UseSystemPasswordChar = true;

            //
            // lblConfirmPassword
            //
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblConfirmPassword.Location = new Point(-2, 109);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(104, 15);
            lblConfirmPassword.TabIndex = 7;
            lblConfirmPassword.Text = "Confirm Password";
            lblConfirmPassword.TextAlign = ContentAlignment.MiddleRight;

            //
            // txtConfirmPassword
            //
            txtConfirmPassword.BackColor = Color.FromArgb(224, 224, 224);
            txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
            txtConfirmPassword.Location = new Point(108, 106);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(180, 23);
            txtConfirmPassword.TabIndex = 8;
            txtConfirmPassword.UseSystemPasswordChar = true;

            //
            // lblRole
            //
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRole.Location = new Point(62, 138);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(40, 15);
            lblRole.TabIndex = 9;
            lblRole.Text = "Role";
            lblRole.TextAlign = ContentAlignment.MiddleRight;

            //
            // cmbRole
            //
            cmbRole.BackColor = Color.FromArgb(224, 224, 224);
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.Location = new Point(108, 135);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(180, 23);
            cmbRole.TabIndex = 10;

            //
            // chkIsActive
            //
            chkIsActive.AutoSize = true;
            chkIsActive.Location = new Point(108, 164);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(65, 19);
            chkIsActive.TabIndex = 11;
            chkIsActive.Text = "Is Active";
            chkIsActive.UseVisualStyleBackColor = true;
            chkIsActive.Checked = true;

            //
            // btnCreate
            //
            btnCreate.BackColor = Color.CornflowerBlue;
            btnCreate.FlatStyle = FlatStyle.Popup;
            btnCreate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCreate.Location = new Point(95, 200);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(75, 26);
            btnCreate.TabIndex = 12;
            btnCreate.Text = "&Create";
            btnCreate.UseVisualStyleBackColor = false;
            btnCreate.Click += btnCreate_Click;

            //
            // btnCancel
            //
            btnCancel.BackColor = Color.CornflowerBlue;
            btnCancel.FlatStyle = FlatStyle.Popup;
            btnCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancel.Location = new Point(185, 200);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 26);
            btnCancel.TabIndex = 13;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;

            //
            // lblError
            //
            lblError.AutoSize = false;
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(20, 220);
            lblError.Name = "lblError";
            lblError.Size = new Size(310, 50);
            lblError.TabIndex = 14;
            lblError.Text = "";

            //
            // CreateUserForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(350, 300);
            Controls.Add(lblError);
            Controls.Add(btnCancel);
            Controls.Add(btnCreate);
            Controls.Add(chkIsActive);
            Controls.Add(cmbRole);
            Controls.Add(lblRole);
            Controls.Add(txtConfirmPassword);
            Controls.Add(lblConfirmPassword);
            Controls.Add(txtPassword);
            Controls.Add(lblPassword);
            Controls.Add(txtDisplayName);
            Controls.Add(lblDisplayName);
            Controls.Add(txtUsername);
            Controls.Add(lblUsername);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CreateUserForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Create User";
            Load += CreateUserForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void CreateUserForm_Load(object sender, EventArgs e)
        {
            cmbRole.DataSource = Enum.GetValues(typeof(Data.Enums.UserRole));
            cmbRole.SelectedIndex = 0;
        }

        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblDisplayName;
        private TextBox txtDisplayName;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblConfirmPassword;
        private TextBox txtConfirmPassword;
        private Label lblRole;
        private ComboBox cmbRole;
        private CheckBox chkIsActive;
        private Button btnCreate;
        private Button btnCancel;
        private Label lblError;
    }
}
