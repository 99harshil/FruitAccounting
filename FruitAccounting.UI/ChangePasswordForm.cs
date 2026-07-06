using FruitAccounting.Core.services;

namespace FruitAccounting.UI
{
    public partial class ChangePasswordForm : Form
    {
        private readonly AuthService _authService;
        private readonly string _username;

        public ChangePasswordForm(AuthService authService, string username)
        {
            InitializeComponent();
            _authService = authService;
            _username = username;
            lblError.Visible = false;
            AcceptButton = btnOK;
            CancelButton = btnCancel;
        }

        private async void btnOK_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            btnOK.Enabled = false;

            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text))
            {
                lblError.Text = "Current password is required.";
                lblError.Visible = true;
                btnOK.Enabled = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                lblError.Text = "New password is required.";
                lblError.Visible = true;
                btnOK.Enabled = true;
                return;
            }

            if (txtNewPassword.Text.Length < 6)
            {
                lblError.Text = "New password must be at least 6 characters.";
                lblError.Visible = true;
                btnOK.Enabled = true;
                return;
            }

            if (txtNewPassword.Text == txtCurrentPassword.Text)
            {
                lblError.Text = "New password must be different from current password.";
                lblError.Visible = true;
                btnOK.Enabled = true;
                return;
            }

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                lblError.Text = "Confirm password does not match new password.";
                lblError.Visible = true;
                btnOK.Enabled = true;
                return;
            }

            try
            {
                var (success, message) = await _authService.ChangePasswordAsync(
                    _username,
                    txtCurrentPassword.Text,
                    txtNewPassword.Text);

                if (success)
                {
                    MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    lblError.Text = message;
                    lblError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error: " + ex.Message;
                lblError.Visible = true;
            }
            finally
            {
                btnOK.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
