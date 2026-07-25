using FruitAccounting.Core.services;
using FruitAccounting.Data.Enums;

namespace FruitAccounting.UI
{
    public partial class CreateUserForm : Form
    {
        private readonly UserService _userService;

        public CreateUserForm(UserService userService)
        {
            InitializeComponent();
            _userService = userService;
            lblError.Visible = false;
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            var (success, message) = await _userService.CreateUserAsync(
                txtUsername.Text.Trim(),
                txtDisplayName.Text.Trim(),
                txtPassword.Text,
                (UserRole)cmbRole.SelectedItem
            );

            if (success)
            {
                MessageBox.Show(message, "Success");
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                lblError.Text = message;
                lblError.Visible = true;
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                lblError.Text = "Username is required";
                lblError.Visible = true;
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblError.Text = "Password is required";
                lblError.Visible = true;
                return false;
            }

            if (txtPassword.Text.Length < 6)
            {
                lblError.Text = "Password must be at least 6 characters";
                lblError.Visible = true;
                return false;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                lblError.Text = "Passwords do not match";
                lblError.Visible = true;
                return false;
            }

            lblError.Visible = false;
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
