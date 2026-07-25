namespace FruitAccounting.UI
{
    partial class ChangePasswordForm
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
            lblCurrentPassword = new Label();
            txtCurrentPassword = new TextBox();
            lblNewPassword = new Label();
            txtNewPassword = new TextBox();
            lblConfirmPassword = new Label();
            txtConfirmPassword = new TextBox();
            btnOK = new Button();
            btnCancel = new Button();
            lblError = new Label();
            SuspendLayout();
            //
            // lblCurrentPassword
            //
            lblCurrentPassword.AutoSize = true;
            lblCurrentPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCurrentPassword.Location = new Point(23, 22);
            lblCurrentPassword.Name = "lblCurrentPassword";
            lblCurrentPassword.Size = new Size(93, 15);
            lblCurrentPassword.TabIndex = 1;
            lblCurrentPassword.Text = "Enter Password";
            lblCurrentPassword.TextAlign = ContentAlignment.MiddleRight;
            //
            // txtCurrentPassword
            //
            txtCurrentPassword.BackColor = Color.FromArgb(224, 224, 224);
            txtCurrentPassword.BorderStyle = BorderStyle.FixedSingle;
            txtCurrentPassword.Location = new Point(125, 19);
            txtCurrentPassword.Name = "txtCurrentPassword";
            txtCurrentPassword.Size = new Size(150, 23);
            txtCurrentPassword.TabIndex = 2;
            txtCurrentPassword.UseSystemPasswordChar = true;
            //
            // lblNewPassword
            //
            lblNewPassword.AutoSize = true;
            lblNewPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNewPassword.Location = new Point(27, 51);
            lblNewPassword.Name = "lblNewPassword";
            lblNewPassword.Size = new Size(89, 15);
            lblNewPassword.TabIndex = 3;
            lblNewPassword.Text = "New Password";
            lblNewPassword.TextAlign = ContentAlignment.MiddleRight;
            //
            // txtNewPassword
            //
            txtNewPassword.BackColor = Color.FromArgb(224, 224, 224);
            txtNewPassword.BorderStyle = BorderStyle.FixedSingle;
            txtNewPassword.Location = new Point(125, 48);
            txtNewPassword.Name = "txtNewPassword";
            txtNewPassword.Size = new Size(150, 23);
            txtNewPassword.TabIndex = 4;
            txtNewPassword.UseSystemPasswordChar = true;
            //
            // lblConfirmPassword
            //
            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblConfirmPassword.Location = new Point(8, 80);
            lblConfirmPassword.Name = "lblConfirmPassword";
            lblConfirmPassword.Size = new Size(108, 15);
            lblConfirmPassword.TabIndex = 5;
            lblConfirmPassword.Text = "Confirm Password";
            lblConfirmPassword.TextAlign = ContentAlignment.MiddleRight;
            //
            // txtConfirmPassword
            //
            txtConfirmPassword.BackColor = Color.FromArgb(224, 224, 224);
            txtConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
            txtConfirmPassword.Location = new Point(125, 77);
            txtConfirmPassword.Name = "txtConfirmPassword";
            txtConfirmPassword.Size = new Size(150, 23);
            txtConfirmPassword.TabIndex = 6;
            txtConfirmPassword.UseSystemPasswordChar = true;
            //
            // btnOK
            //
            btnOK.BackColor = Color.CornflowerBlue;
            btnOK.FlatStyle = FlatStyle.Popup;
            btnOK.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnOK.Location = new Point(75, 120);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 26);
            btnOK.TabIndex = 7;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = false;
            btnOK.Click += btnOK_Click;
            //
            // btnCancel
            //
            btnCancel.BackColor = Color.CornflowerBlue;
            btnCancel.FlatStyle = FlatStyle.Popup;
            btnCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancel.Location = new Point(170, 120);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 26);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Close";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            //
            // lblError
            //
            lblError.AutoSize = true;
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(125, 102);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 15);
            lblError.TabIndex = 9;
            //
            // ChangePasswordForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(314, 161);
            Controls.Add(lblError);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(txtConfirmPassword);
            Controls.Add(lblConfirmPassword);
            Controls.Add(txtNewPassword);
            Controls.Add(lblNewPassword);
            Controls.Add(txtCurrentPassword);
            Controls.Add(lblCurrentPassword);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ChangePasswordForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Change Password";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCurrentPassword;
        private TextBox txtCurrentPassword;
        private Label lblNewPassword;
        private TextBox txtNewPassword;
        private Label lblConfirmPassword;
        private TextBox txtConfirmPassword;
        private Button btnOK;
        private Button btnCancel;
        private Label lblError;
    }
}
