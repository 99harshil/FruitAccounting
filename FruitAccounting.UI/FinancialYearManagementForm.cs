using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class FinancialYearManagementForm : Form
    {
        private readonly FinancialYearService _financialYearService;
        private readonly Company _company;
        public long? SelectedFinancialYearId { get; private set; }

        public FinancialYearManagementForm(FinancialYearService financialYearService, Company company)
        {
            InitializeComponent();
            _financialYearService = financialYearService;
            _company = company;
            lblCompanyName.Text = $"Company: {_company.Code} - {_company.Name}";
            lblError.Visible = false;
        }

        private async void FinancialYearManagementForm_Load(object sender, EventArgs e)
        {
            await LoadFinancialYears();
        }

        private async Task LoadFinancialYears()
        {
            try
            {
                lstFinancialYears.Items.Clear();
                var financialYears = await _financialYearService.GetFinancialYearsByCompanyAsync(_company.CompanyId);

                if (financialYears.Count == 0)
                {
                    lstFinancialYears.Items.Add("No financial years found");
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnSelect.Enabled = false;
                    return;
                }

                foreach (var fy in financialYears)
                {
                    var displayText = $"{fy.Code}  {(fy.IsActive ? "Active" : "Inactive")}";
                    lstFinancialYears.Items.Add(new FinancialYearItem(fy.FinancialYearId, fy.Code, displayText));
                }

                if (lstFinancialYears.Items.Count > 0)
                {
                    lstFinancialYears.SelectedIndex = 0;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                    btnSelect.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error loading financial years: " + ex.Message;
                lblError.Visible = true;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnSelect.Enabled = false;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new AddFinancialYearForm(_financialYearService, _company.CompanyId))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    MessageBox.Show("Financial year added. Refreshing list...", "Info");
                    _ = LoadFinancialYears();
                }
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (lstFinancialYears.SelectedItem is not FinancialYearItem selectedFY)
            {
                MessageBox.Show("Please select a financial year to edit.", "Info");
                return;
            }

            var financialYear = await _financialYearService.GetFinancialYearAsync(selectedFY.FinancialYearId);
            if (financialYear == null)
            {
                MessageBox.Show("Financial year not found.", "Error");
                return;
            }

            using (var form = new EditFinancialYearForm(_financialYearService, financialYear))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    MessageBox.Show("Financial year updated. Refreshing list...", "Info");
                    await LoadFinancialYears();
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstFinancialYears.SelectedItem is not FinancialYearItem selectedFY)
            {
                MessageBox.Show("Please select a financial year to delete.", "Info");
                return;
            }

            var result = MessageBox.Show(
                $"Are you sure you want to delete financial year '{selectedFY.Code}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            try
            {
                var (success, message) = await _financialYearService.DeleteFinancialYearAsync(selectedFY.FinancialYearId);
                if (success)
                {
                    MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadFinancialYears();
                }
                else
                {
                    MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error");
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (lstFinancialYears.SelectedItem is FinancialYearItem selectedFY)
            {
                SelectedFinancialYearId = selectedFY.FinancialYearId;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private class FinancialYearItem
        {
            public long FinancialYearId { get; set; }
            public string Code { get; set; }
            public string DisplayText { get; set; }

            public FinancialYearItem(long id, string code, string displayText)
            {
                FinancialYearId = id;
                Code = code;
                DisplayText = displayText;
            }

            public override string ToString() => DisplayText;
        }
    }
}
