using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class EditFinancialYearForm : Form
    {
        private readonly FinancialYearService _financialYearService;
        private readonly FinancialYear _financialYear;

        public EditFinancialYearForm(FinancialYearService financialYearService, FinancialYear financialYear)
        {
            InitializeComponent();
            _financialYearService = financialYearService;
            _financialYear = financialYear;
            lblError.Visible = false;
            AcceptButton = btnOK;
            CancelButton = btnCancel;
        }

        private void EditFinancialYearForm_Load(object sender, EventArgs e)
        {
            txtCode.Text = _financialYear.Code;
            dtStartDate.Value = _financialYear.StartDate.ToDateTime(TimeOnly.MinValue);
            dtEndDate.Value = _financialYear.EndDate.ToDateTime(TimeOnly.MinValue);
            chkIsActive.Checked = _financialYear.IsActive;
            chkIsClosed.Checked = _financialYear.IsClosed;
            chkIsClosed.Enabled = false; // Don't allow changing closed status from UI
        }

        private async void btnOK_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            btnOK.Enabled = false;

            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                lblError.Text = "Financial year code is required.";
                lblError.Visible = true;
                btnOK.Enabled = true;
                return;
            }

            try
            {
                var (success, message) = await _financialYearService.EditFinancialYearAsync(
                    financialYearId: _financialYear.FinancialYearId,
                    code: txtCode.Text.Trim(),
                    startDate: DateOnly.FromDateTime(dtStartDate.Value),
                    endDate: DateOnly.FromDateTime(dtEndDate.Value),
                    isActive: chkIsActive.Checked);

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
