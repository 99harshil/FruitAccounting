using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class LedgerAllForm : Form
    {
        private readonly LedgerService _ledgerService;
        private readonly AccountService _accountService;
        private readonly long _companyId;
        private readonly long _financialYearId;
        private readonly FinancialYear _financialYear;

        public LedgerAllForm(LedgerService ledgerService, AccountService accountService,
            long companyId, long financialYearId, FinancialYear financialYear)
        {
            InitializeComponent();
            _ledgerService = ledgerService;
            _accountService = accountService;
            _companyId = companyId;
            _financialYearId = financialYearId;
            _financialYear = financialYear;
        }

        private void LedgerAllForm_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = _financialYear.StartDate.ToDateTime(TimeOnly.MinValue);
            dtpToDate.Value = DateTime.Today;
        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            var fromDate = DateOnly.FromDateTime(dtpFromDate.Value);
            var toDate = DateOnly.FromDateTime(dtpToDate.Value);
            if (fromDate > toDate)
            {
                MessageBox.Show("From date cannot be after To date", "Ledger");
                return;
            }

            var all = await _accountService.GetAllAccountsAsync(_companyId);
            var accountIds = all.Where(a => !a.IsBlocked).OrderBy(a => a.Code).Select(a => a.AccountId).ToList();

            new LedgerMultiAccountReportForm(_ledgerService, "Ledger - All", accountIds, _financialYearId, fromDate, toDate)
                .ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void btnWhatsapp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sending via WhatsApp is not implemented yet.", "WhatsApp", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
