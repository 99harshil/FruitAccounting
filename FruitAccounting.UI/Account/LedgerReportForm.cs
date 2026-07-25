using System.Globalization;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;
using FruitAccounting.Data.Enums;

namespace FruitAccounting.UI
{
    public partial class LedgerReportForm : Form
    {
        private readonly LedgerService _ledgerService;
        private readonly AccountService _accountService;
        private readonly long _companyId;
        private readonly long _financialYearId;
        private readonly FinancialYear _financialYear;

        private List<Account> _accounts = new();
        private bool _suppressAccountSync;
        private long _loadSeq;

        public LedgerReportForm(LedgerService ledgerService, AccountService accountService,
            long companyId, long financialYearId, FinancialYear financialYear)
        {
            InitializeComponent();
            _ledgerService = ledgerService;
            _accountService = accountService;
            _companyId = companyId;
            _financialYearId = financialYearId;
            _financialYear = financialYear;
        }

        private async void LedgerReportForm_Load(object sender, EventArgs e)
        {
            // Default period is FY Start -> Today, same as the legacy Ledger screen - not
            // clamped to the FY's nominal end date, since real entries can be dated past it.
            dtpFromDate.Value = _financialYear.StartDate.ToDateTime(TimeOnly.MinValue);
            dtpToDate.Value = DateTime.Today;

            var all = await _accountService.GetAllAccountsAsync(_companyId);
            _accounts = all.Where(a => !a.IsBlocked).OrderBy(a => a.Name).ToList();
            cmbAccountCode.Items.Clear();
            cmbAccountName.Items.Clear();
            foreach (var acc in _accounts)
            {
                cmbAccountCode.Items.Add(acc.Code);
                cmbAccountName.Items.Add(acc.Name);
            }
        }

        // cmbAccountCode and cmbAccountName are populated from the same _accounts list in the
        // same order, so keeping them in sync is just mirroring the selected index. Picking
        // either one (or changing the period) reloads the ledger immediately - there's no
        // separate OK button, matching the legacy Ledger screen.
        private async void cmbAccountCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressAccountSync) return;
            _suppressAccountSync = true;
            cmbAccountName.SelectedIndex = cmbAccountCode.SelectedIndex;
            _suppressAccountSync = false;
            await LoadLedgerAsync();
        }

        private async void cmbAccountName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressAccountSync) return;
            _suppressAccountSync = true;
            cmbAccountCode.SelectedIndex = cmbAccountName.SelectedIndex;
            _suppressAccountSync = false;
            await LoadLedgerAsync();
        }

        private async void dtpFromDate_ValueChanged(object sender, EventArgs e) => await LoadLedgerAsync();

        private async void dtpToDate_ValueChanged(object sender, EventArgs e) => await LoadLedgerAsync();

        // All seven radio buttons share this handler; RadioButton fires CheckedChanged for both the
        // button losing the check and the one gaining it, so only act on the one becoming checked.
        private async void rbViewMode_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton { Checked: true })
                await LoadLedgerAsync();
        }

        private async Task LoadLedgerAsync()
        {
            // DateTimePicker fires ValueChanged per keystroke while typing a date, so several
            // overlapping loads can be in flight at once. Each load claims a sequence number and
            // only the most recently started one is allowed to update the grid - otherwise a
            // slower, now-stale request (e.g. from a half-typed date) could finish last and show
            // the wrong period.
            var mySeq = ++_loadSeq;

            if (cmbAccountName.SelectedIndex < 0)
            {
                dgvLedger.Rows.Clear();
                return;
            }

            var account = _accounts[cmbAccountName.SelectedIndex];
            var fromDate = DateOnly.FromDateTime(dtpFromDate.Value);
            var toDate = DateOnly.FromDateTime(dtpToDate.Value);

            if (fromDate > toDate)
            {
                MessageBox.Show("From date cannot be after To date", "Ledger");
                return;
            }

            VoucherType? voucherTypeFilter = rbPurchase.Checked ? VoucherType.PurchaseBill
                : rbSales.Checked ? VoucherType.SalesBill
                : rbReceipt.Checked ? VoucherType.Receipt
                : rbPayment.Checked ? VoucherType.Payment
                : rbJV.Checked ? VoucherType.Journal
                : null;
            bool includeOpeningBalance = !rbWithoutOpening.Checked;
            bool summaryOnly = rbSummary.Checked;

            var result = await _ledgerService.GetLedgerAsync(account.AccountId, _financialYearId, fromDate, toDate,
                voucherTypeFilter, includeOpeningBalance);

            if (mySeq != _loadSeq)
                return; // a newer load has since started - this result is stale, discard it

            dgvLedger.Rows.Clear();

            if (includeOpeningBalance)
                dgvLedger.Rows.Add("", "", "Opening Balance :", "", "", "", FormatBalance(result.OpeningBalance));

            if (!summaryOnly)
            {
                int sr = 1;
                foreach (var row in result.Rows)
                {
                    dgvLedger.Rows.Add(
                        sr++,
                        row.Date.ToString("dd/MM/yyyy"),
                        row.Detail,
                        row.ChequeOrUser,
                        row.Debit == 0 ? "" : row.Debit.ToString("N2", CultureInfo.InvariantCulture),
                        row.Credit == 0 ? "" : row.Credit.ToString("N2", CultureInfo.InvariantCulture),
                        FormatBalance(row.RunningBalance));
                }
            }

            dgvLedger.Rows.Add("", "", "Grand Total :-", "",
                result.TotalDebit.ToString("N2", CultureInfo.InvariantCulture),
                result.TotalCredit.ToString("N2", CultureInfo.InvariantCulture),
                "");
            dgvLedger.Rows.Add("", "", "Balance :-", "", "", "", FormatBalance(result.ClosingBalance));
        }

        private static string FormatBalance(decimal signedBalance)
        {
            var suffix = signedBalance < 0 ? "Cr" : "Db";
            return $"{Math.Abs(signedBalance):N2} {suffix}";
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void btnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Print / PDF export will be added in the next step (FastReport .NET).", "Ledger");
        }

        private void btnWhatsapp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sending via WhatsApp is not implemented yet.", "WhatsApp", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            // Prints the ledgerdetail.rpt-style layout - same data as Print, with full
            // narration shown under each entry instead of the condensed grid line. Wired
            // up alongside Print once FastReport .NET is in (both are report outputs).
            MessageBox.Show("Print / PDF export will be added in the next step (FastReport .NET).", "Ledger");
        }
    }
}
