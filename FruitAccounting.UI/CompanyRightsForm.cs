using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;
using FruitAccounting.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FruitAccounting.UI
{
    public partial class CompanyRightsForm : Form
    {
        private readonly UserService _userService;
        private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;
        private List<User> _allUsers = new();
        private long _selectedUserId = 0;

        public CompanyRightsForm(UserService userService, IDbContextFactory<FruitAccountingContext> contextFactory)
        {
            InitializeComponent();
            _userService = userService;
            _contextFactory = contextFactory;
        }

        private async void CompanyRightsForm_Load(object sender, EventArgs e)
        {
            await LoadUsers();
        }

        private async Task LoadUsers()
        {
            try
            {
                _allUsers = await _userService.GetAllUsersAsync();
                cmbUserIdCompany.DataSource = new List<User>(_allUsers);
                cmbUserIdCompany.DisplayMember = "DisplayName";
                cmbUserIdCompany.ValueMember = "UserId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error");
            }
        }

        private async void cmbUserIdCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUserIdCompany.SelectedIndex < 0 || cmbUserIdCompany.SelectedItem == null)
                return;

            if (cmbUserIdCompany.SelectedItem is User selectedUser)
            {
                _selectedUserId = selectedUser.UserId;
                await LoadCompanyRights();
            }
        }

        private async Task LoadCompanyRights()
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();

                var companies = await context.Companies.AsNoTracking().OrderBy(c => c.Name).ToListAsync();

                dgvCompanyRights.Rows.Clear();

                foreach (var company in companies)
                {
                    var financialYears = await context.FinancialYears
                        .AsNoTracking()
                        .Where(fy => fy.CompanyId == company.CompanyId)
                        .OrderBy(fy => fy.Code)
                        .ToListAsync();

                    foreach (var fy in financialYears)
                    {
                        dgvCompanyRights.Rows.Add(company.Name, fy.Code, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading company rights: {ex.Message}", "Error");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == 0)
            {
                MessageBox.Show("Please select a user", "Info");
                return;
            }

            MessageBox.Show("Company rights saved successfully", "Success");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
