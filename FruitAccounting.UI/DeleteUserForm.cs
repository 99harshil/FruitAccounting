using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class DeleteUserForm : Form
    {
        private readonly UserService _userService;
        private List<User> _allUsers = new();

        public DeleteUserForm(UserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }

        private async void DeleteUserForm_Load(object sender, EventArgs e)
        {
            await LoadUsers();
            lstUsers.SelectedIndexChanged += LstUsers_SelectedIndexChanged;
        }

        private void LstUsers_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (lstUsers.SelectedItem is User selectedUser)
            {
                cmbUserId.SelectedItem = selectedUser;
            }
        }

        private async Task LoadUsers()
        {
            try
            {
                _allUsers = await _userService.GetAllUsersAsync();
                cmbUserId.DataSource = new List<User>(_allUsers);
                cmbUserId.DisplayMember = "DisplayName";
                cmbUserId.ValueMember = "UserId";

                lstUsers.DataSource = new List<User>(_allUsers);
                lstUsers.DisplayMember = "DisplayName";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error");
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (cmbUserId.SelectedIndex < 0 || cmbUserId.SelectedItem == null)
            {
                MessageBox.Show("Please select a user to delete", "Info");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this user?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var selectedUser = cmbUserId.SelectedItem as User;
            if (selectedUser == null)
            {
                MessageBox.Show("Invalid user selected", "Error");
                return;
            }

            var (success, message) = await _userService.DeleteUserAsync(selectedUser.UserId);

            if (success)
            {
                MessageBox.Show(message, "Success");
                await LoadUsers();
            }
            else
            {
                MessageBox.Show(message, "Error");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
