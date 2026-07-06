using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class FinancialYearManagementForm : Form
    {
        private readonly FinancialYearService _financialYearService;
        private readonly Company _company;
        public long SelectedFinancialYearId { get; private set; }

        public FinancialYearManagementForm(FinancialYearService financialYearService, Company company)
        {
            InitializeComponent();
            _financialYearService = financialYearService;
            _company = company;
            lblError.Visible = false;
        }

        private async void FinancialYearManagementForm_Load(object sender, EventArgs e)
        {
            lblCompanyName.Text = $"Company: {_company.Name}";
            await LoadFinancialYears();
        }

        private async Task LoadFinancialYears()
        {
            try
            {
                var financialYears = await _financialYearService.GetFinancialYearsByCompanyAsync(_company.CompanyId);
                lstFinancialYears.Items.Clear();
                lstFinancialYears.DisplayMember = "DisplayText";
                lstFinancialYears.ValueMember = "FinancialYearId";

                foreach (var fy in financialYears)
                {
                    var item = new FinancialYearListItem
                    {
                        FinancialYearId = fy.FinancialYearId,
                        Code = fy.Code,
                        StartDate = fy.StartDate,
                        EndDate = fy.EndDate,
                        IsActive = fy.IsActive,
                        IsClosed = fy.IsClosed
                    };
                    lstFinancialYears.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error loading financial years: " + ex.Message;
                lblError.Visible = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using var form = new AddFinancialYearForm(_financialYearService, _company.CompanyId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                _ = LoadFinancialYears();
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (lstFinancialYears.SelectedItem == null)
            {
                MessageBox.Show("Please select a financial year to edit.", "Info");
                return;
            }

            var item = (FinancialYearListItem)lstFinancialYears.SelectedItem;
            var fy = await _financialYearService.GetFinancialYearAsync(item.FinancialYearId);

            if (fy != null)
            {
                using var form = new EditFinancialYearForm(_financialYearService, fy);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    await LoadFinancialYears();
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstFinancialYears.SelectedItem == null)
            {
                MessageBox.Show("Please select a financial year to delete.", "Info");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this financial year?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var item = (FinancialYearListItem)lstFinancialYears.SelectedItem;
            var (success, message) = await _financialYearService.DeleteFinancialYearAsync(item.FinancialYearId);

            if (success)
            {
                MessageBox.Show(message, "Success");
                await LoadFinancialYears();
            }
            else
            {
                MessageBox.Show(message, "Error");
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (lstFinancialYears.SelectedItem == null)
            {
                MessageBox.Show("Please select a financial year.", "Info");
                return;
            }

            var item = (FinancialYearListItem)lstFinancialYears.SelectedItem;
            SelectedFinancialYearId = item.FinancialYearId;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private class FinancialYearListItem
        {
            public long FinancialYearId { get; set; }
            public string Code { get; set; } = string.Empty;
            public DateOnly StartDate { get; set; }
            public DateOnly EndDate { get; set; }
            public bool IsActive { get; set; }
            public bool IsClosed { get; set; }

            public string DisplayText => $"{Code} ({StartDate:dd-MMM-yyyy} to {EndDate:dd-MMM-yyyy}) - {(IsActive ? "Active" : "Inactive")}{(IsClosed ? " (Closed)" : "")}";
        }
    }
}
