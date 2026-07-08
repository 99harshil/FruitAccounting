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
            await LoadFinancialYears();

            // Wire up events for selection
            dgvFinancialYears.DoubleClick += (s, e) => SelectFinancialYear();
            dgvFinancialYears.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Return)
                {
                    SelectFinancialYear();
                    e.Handled = true;
                }
            };
        }

        private async Task LoadFinancialYears()
        {
            try
            {
                var financialYears = await _financialYearService.GetFinancialYearsByCompanyAsync(_company.CompanyId);
                dgvFinancialYears.Rows.Clear();

                foreach (var fy in financialYears)
                {
                    var rowIndex = dgvFinancialYears.Rows.Add(
                        fy.Code,
                        $"{fy.StartDate:dd-MMM-yyyy} to {fy.EndDate:dd-MMM-yyyy}"
                    );
                    dgvFinancialYears.Rows[rowIndex].Tag = fy.FinancialYearId;
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
            if (dgvFinancialYears.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a financial year to edit.", "Info");
                return;
            }

            var row = dgvFinancialYears.SelectedRows[0];
            var fyId = (long)row.Tag;
            var fy = await _financialYearService.GetFinancialYearAsync(fyId);

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
            if (dgvFinancialYears.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a financial year to delete.", "Info");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this financial year?", "Confirm",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var row = dgvFinancialYears.SelectedRows[0];
            var fyId = (long)row.Tag;
            var (success, message) = await _financialYearService.DeleteFinancialYearAsync(fyId);

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SelectFinancialYear()
        {
            if (dgvFinancialYears.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a financial year.", "Info");
                return;
            }

            var row = dgvFinancialYears.SelectedRows[0];
            SelectedFinancialYearId = (long)row.Tag;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
