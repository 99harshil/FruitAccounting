using FruitAccounting.Core.services;

namespace FruitAccounting.UI
{
    public partial class AddFinancialYearForm : Form
    {
        private readonly FinancialYearService _financialYearService;
        private readonly long _companyId;

        public AddFinancialYearForm(FinancialYearService financialYearService, long companyId)
        {
            InitializeComponent();
            _financialYearService = financialYearService;
            _companyId = companyId;
            lblError.Visible = false;
            dtStartDate.Value = new DateTime(DateTime.Now.Year, 4, 1);
            dtEndDate.Value = new DateTime(DateTime.Now.Year + 1, 3, 31);
            AcceptButton = btnOK;
            CancelButton = btnCancel;
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
                var (success, message) = await _financialYearService.AddFinancialYearAsync(
                    companyId: _companyId,
                    code: txtCode.Text.Trim(),
                    startDate: DateOnly.FromDateTime(dtStartDate.Value),
                    endDate: DateOnly.FromDateTime(dtEndDate.Value));

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
