using System;
using System.Globalization;
using System.Windows.Forms;
using FruitAccounting.Core.services;

namespace FruitAccounting.UI
{
    public partial class CompanyInfoForm : Form
    {
        private readonly CompanyService _companyService;
        private readonly long _companyId;

        public CompanyInfoForm(CompanyService companyService, long companyId)
        {
            InitializeComponent();
            _companyService = companyService;
            _companyId = companyId;
        }

        private async void CompanyInfoForm_Load(object sender, EventArgs e)
        {
            var company = await _companyService.GetCompanyAsync(_companyId);
            if (company == null)
            {
                MessageBox.Show("Company not found", "Error");
                Close();
                return;
            }

            txtName.Text = company.Name;
            txtAddress1.Text = company.Address1 ?? "";
            txtAddress2.Text = company.Address2 ?? "";
            txtCity.Text = company.City ?? "";
            txtPhone.Text = company.Phone ?? "";
            txtEmail.Text = company.Email ?? "";
            txtPanNo.Text = company.PanNo ?? "";
            txtGstin.Text = company.Gstin ?? "";
            txtApmcLicenceNo.Text = company.ApmcLicenceNo ?? "";
            txtApmcPct.Text = (company.ApmcPct ?? 0).ToString(CultureInfo.InvariantCulture);
            txtBankName.Text = company.BankName ?? "";
            txtBankAccountNo.Text = company.BankAccountNo ?? "";
            txtBankIfsc.Text = company.BankIfsc ?? "";
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtApmcPct.Text, out var apmcPct))
            {
                MessageBox.Show("APMC % must be a valid number", "Validation Error");
                return;
            }

            var input = new CompanyService.CompanyInfoInput
            {
                Address1 = string.IsNullOrWhiteSpace(txtAddress1.Text) ? null : txtAddress1.Text.Trim(),
                Address2 = string.IsNullOrWhiteSpace(txtAddress2.Text) ? null : txtAddress2.Text.Trim(),
                City = string.IsNullOrWhiteSpace(txtCity.Text) ? null : txtCity.Text.Trim(),
                Phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                PanNo = string.IsNullOrWhiteSpace(txtPanNo.Text) ? null : txtPanNo.Text.Trim(),
                Gstin = string.IsNullOrWhiteSpace(txtGstin.Text) ? null : txtGstin.Text.Trim(),
                ApmcLicenceNo = string.IsNullOrWhiteSpace(txtApmcLicenceNo.Text) ? null : txtApmcLicenceNo.Text.Trim(),
                ApmcPct = apmcPct,
                BankName = string.IsNullOrWhiteSpace(txtBankName.Text) ? null : txtBankName.Text.Trim(),
                BankAccountNo = string.IsNullOrWhiteSpace(txtBankAccountNo.Text) ? null : txtBankAccountNo.Text.Trim(),
                BankIfsc = string.IsNullOrWhiteSpace(txtBankIfsc.Text) ? null : txtBankIfsc.Text.Trim()
            };

            var (success, message) = await _companyService.UpdateCompanyInfoAsync(_companyId, input);
            MessageBox.Show(message, success ? "Success" : "Error");
            if (success)
                Close();
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();
    }
}
