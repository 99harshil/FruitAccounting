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

        private void InitializeComponent()
        {
            lblTitle = new Label();
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

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(100, 20);
            lblTitle.Size = new Size(172, 25);
            lblTitle.Text = "Change Password";

            lblCurrentPassword.AutoSize = true;
            lblCurrentPassword.Location = new Point(20, 70);
            lblCurrentPassword.Size = new Size(114, 15);
            lblCurrentPassword.Text = "Current Password";

            txtCurrentPassword.Location = new Point(20, 90);
            txtCurrentPassword.Size = new Size(200, 23);
            txtCurrentPassword.UseSystemPasswordChar = true;

            lblNewPassword.AutoSize = true;
            lblNewPassword.Location = new Point(20, 125);
            lblNewPassword.Size = new Size(88, 15);
            lblNewPassword.Text = "New Password";

            txtNewPassword.Location = new Point(20, 145);
            txtNewPassword.Size = new Size(200, 23);
            txtNewPassword.UseSystemPasswordChar = true;

            lblConfirmPassword.AutoSize = true;
            lblConfirmPassword.Location = new Point(20, 180);
            lblConfirmPassword.Size = new Size(125, 15);
            lblConfirmPassword.Text = "Confirm Password";

            txtConfirmPassword.Location = new Point(20, 200);
            txtConfirmPassword.Size = new Size(200, 23);
            txtConfirmPassword.UseSystemPasswordChar = true;

            btnOK.Location = new Point(50, 250);
            btnOK.Size = new Size(75, 23);
            btnOK.Text = "Update";
            btnOK.Click += this.btnOK_Click;

            btnCancel.Location = new Point(150, 250);
            btnCancel.Size = new Size(75, 23);
            btnCancel.Text = "Cancel";
            btnCancel.Click += this.btnCancel_Click;

            lblError.AutoSize = true;
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(20, 290);

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(350, 330);
            Controls.Add(lblError);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(txtConfirmPassword);
            Controls.Add(lblConfirmPassword);
            Controls.Add(txtNewPassword);
            Controls.Add(lblNewPassword);
            Controls.Add(txtCurrentPassword);
            Controls.Add(lblCurrentPassword);
            Controls.Add(lblTitle);
            Name = "ChangePasswordForm";
            Text = "Change Password";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
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
