using FruitAccounting.Core.services;

namespace FruitAccounting.UI
{
    // Same flow as LedgerAllForm (walk every non-blocked account, skip ones with no activity) -
    // only the default period differs: both From and To default to today, so opening the screen
    // shows "everyone's transactions today" without having to set a range first.
    public partial class LedgerDailyForm : Form
    {
        private readonly LedgerService _ledgerService;
        private readonly AccountService _accountService;
        private readonly long _companyId;
        private readonly long _financialYearId;

        public LedgerDailyForm(LedgerService ledgerService, AccountService accountService,
            long companyId, long financialYearId)
        {
            InitializeComponent();
            _ledgerService = ledgerService;
            _accountService = accountService;
            _companyId = companyId;
            _financialYearId = financialYearId;
        }

        private void LedgerDailyForm_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = DateTime.Today;
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

            new LedgerMultiAccountReportForm(_ledgerService, "Ledger - Daily", accountIds, _financialYearId, fromDate, toDate)
                .ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void btnWhatsapp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sending via WhatsApp is not implemented yet.", "WhatsApp", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
