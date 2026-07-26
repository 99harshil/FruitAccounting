using FruitAccounting.Core.services;

namespace FruitAccounting.UI
{
    // Same flow as LedgerAllForm - only the default period differs: defaults to the last fully
    // completed calendar month (not the current, still-partial one), e.g. opened on 26/07/2026
    // defaults to 01/06/2026 - 30/06/2026, the standard "last closed month" accounting convention.
    public partial class LedgerMonthlyForm : Form
    {
        private readonly LedgerService _ledgerService;
        private readonly AccountService _accountService;
        private readonly long _companyId;
        private readonly long _financialYearId;

        public LedgerMonthlyForm(LedgerService ledgerService, AccountService accountService,
            long companyId, long financialYearId)
        {
            InitializeComponent();
            _ledgerService = ledgerService;
            _accountService = accountService;
            _companyId = companyId;
            _financialYearId = financialYearId;
        }

        private void LedgerMonthlyForm_Load(object sender, EventArgs e)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var firstOfCurrentMonth = new DateOnly(today.Year, today.Month, 1);
            var lastMonthEnd = firstOfCurrentMonth.AddDays(-1);
            var lastMonthStart = new DateOnly(lastMonthEnd.Year, lastMonthEnd.Month, 1);

            dtpFromDate.Value = lastMonthStart.ToDateTime(TimeOnly.MinValue);
            dtpToDate.Value = lastMonthEnd.ToDateTime(TimeOnly.MinValue);
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

            new LedgerMultiAccountReportForm(_ledgerService, "Ledger - Monthly", accountIds, _financialYearId, fromDate, toDate)
                .ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void btnWhatsapp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sending via WhatsApp is not implemented yet.", "WhatsApp", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
