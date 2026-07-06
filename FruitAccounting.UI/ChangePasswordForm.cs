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
            lblUsername.Text = $"Username: {_username}";
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
                lblError.Text = "Please enter your current password.";
                lblError.Visible = true;
                btnOK.Enabled = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                lblError.Text = "Please enter a new password.";
                lblError.Visible = true;
                btnOK.Enabled = true;
                return;
            }

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                lblError.Text = "New password and confirm password do not match.";
                lblError.Visible = true;
                txtConfirmPassword.Clear();
                txtConfirmPassword.Focus();
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
                    txtCurrentPassword.Clear();
                    txtCurrentPassword.Focus();
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
