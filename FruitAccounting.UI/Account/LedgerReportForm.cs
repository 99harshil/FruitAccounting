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
        private readonly long? _currentUserId;

        private List<Account> _accounts = new();
        private bool _suppressAccountSync;
        private bool _suppressReload;
        private long _loadSeq;

        public LedgerReportForm(LedgerService ledgerService, AccountService accountService,
            long companyId, long financialYearId, FinancialYear financialYear, long? currentUserId)
        {
            InitializeComponent();
            _ledgerService = ledgerService;
            _accountService = accountService;
            _companyId = companyId;
            _financialYearId = financialYearId;
            _financialYear = financialYear;
            _currentUserId = currentUserId;
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
            UpdateMobileField();
            await LoadLedgerAsync();
        }

        private async void cmbAccountName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressAccountSync) return;
            _suppressAccountSync = true;
            cmbAccountCode.SelectedIndex = cmbAccountName.SelectedIndex;
            _suppressAccountSync = false;
            UpdateMobileField();
            await LoadLedgerAsync();
        }

        private void UpdateMobileField()
        {
            txtMobile.Text = cmbAccountName.SelectedIndex >= 0
                ? _accounts[cmbAccountName.SelectedIndex].Mobile ?? ""
                : "";
        }

        private async void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressReload) return;
            await LoadLedgerAsync();
        }

        private async void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressReload) return;
            await LoadLedgerAsync();
        }

        // All eight radio buttons share this handler; RadioButton fires CheckedChanged for both the
        // button losing the check and the one gaining it, so only act on the one becoming checked.
        private async void rbViewMode_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressReload) return;
            if (sender is RadioButton { Checked: true })
                await LoadLedgerAsync();
        }

        private async void chkWeekTotal_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressReload) return;
            await LoadLedgerAsync();
        }

        // Gross Amount (show gross instead of net settled figures) is still a UI stub - deciding
        // which alternate figure applies per voucher type is separate work from Week Total.
        private async void chkGrossAmount_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressReload) return;
            MessageBox.Show("Gross Amount is not implemented yet.", "Ledger");
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

            // Auto-detect if this is a delegate account (has Amanat Party set and it's not self)
            bool isDelegateAccount = account.AmanatPartyId.HasValue
                && account.AmanatPartyId.Value != account.AccountId;

            LedgerService.LedgerResult result;

            if (isDelegateAccount)
            {
                // This is a delegate account - show its activity from the Amanat Party's ledger
                var amanatParty = _accounts.FirstOrDefault(a => a.AccountId == account.AmanatPartyId.Value);
                if (amanatParty == null)
                {
                    MessageBox.Show("Amanat Party not found", "Ledger");
                    return;
                }

                result = await _ledgerService.GetDelegateLedgerAsync(
                    account.AccountId, amanatParty.AccountId, _financialYearId, fromDate, toDate);
            }
            else
            {
                // Normal account - show regular ledger
                result = await _ledgerService.GetLedgerAsync(account.AccountId, _financialYearId, fromDate, toDate,
                    voucherTypeFilter, includeOpeningBalance);
            }

            if (mySeq != _loadSeq)
                return; // a newer load has since started - this result is stale, discard it

            dgvLedger.Rows.Clear();

            // If this is a delegate account, show a note in the first row
            if (isDelegateAccount && account.AmanatPartyId.HasValue)
            {
                var amanatParty = _accounts.FirstOrDefault(a => a.AccountId == account.AmanatPartyId.Value);
                if (amanatParty != null)
                {
                    var bannerRowIndex = dgvLedger.Rows.Add("", "", "", $"[{account.Name}'s Activity under {amanatParty.Name}]", "", "", "", "");
                    var bannerRow = dgvLedger.Rows[bannerRowIndex];
                    bannerRow.DefaultCellStyle.BackColor = Color.LightBlue;
                    bannerRow.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                }
            }

            if (includeOpeningBalance)
                AddBalanceRow("Opening Balance :", result.OpeningBalance);

            if (!summaryOnly)
            {
                var displayRows = chkWeekTotal.Checked ? LedgerService.AggregateByWeek(result.Rows) : result.Rows;
                int sr = 1;
                foreach (var row in displayRows)
                {
                    int rowIndex = dgvLedger.Rows.Add(
                        sr++,
                        row.VoucherCode,
                        row.Date.ToString("dd/MM/yyyy"),
                        row.Detail,
                        row.ChequeOrUser,
                        row.Debit == 0 ? "" : row.Debit.ToString("N2", CultureInfo.InvariantCulture),
                        row.Credit == 0 ? "" : row.Credit.ToString("N2", CultureInfo.InvariantCulture),
                        FormatBalance(row.RunningBalance));
                    dgvLedger.Rows[rowIndex].Tag = row;
                }
            }

            dgvLedger.Rows.Add("", "", "", "Grand Total :-", "",
                result.TotalDebit.ToString("N2", CultureInfo.InvariantCulture),
                result.TotalCredit.ToString("N2", CultureInfo.InvariantCulture),
                "");
            AddBalanceRow("Balance :-", result.ClosingBalance);
        }

        // Opening/Closing balance rows show the amount in the Debit or Credit column matching its
        // side, like a real transaction line, plus the same figure restated in Balance.
        private void AddBalanceRow(string label, decimal signedBalance)
        {
            var amount = Math.Abs(signedBalance).ToString("N2", CultureInfo.InvariantCulture);
            var balanceText = FormatBalance(signedBalance);
            if (signedBalance >= 0)
                dgvLedger.Rows.Add("", "", "", label, "", amount, "", balanceText);
            else
                dgvLedger.Rows.Add("", "", "", label, "", "", amount, balanceText);
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

        private void btnChithi_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chithi export is not implemented yet.", "Ledger");
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sending via Email is not implemented yet.", "Ledger");
        }

        // Double-clicking a transaction row opens the originating voucher screen, preselected to
        // that exact record - not just opened on whatever the newest entry happens to be. Balance/
        // summary rows (Opening/Grand Total/Balance) have no Tag, so they no-op here. Double-
        // clicking a Week Total row instead "drills in": switches off aggregation and narrows the
        // period to that week, showing the same daily entries a regular (non-aggregated) ledger would.
        private async void dgvLedger_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvLedger.Rows[e.RowIndex].Tag is not LedgerService.LedgerRow row) return;

            if (row.IsWeekAggregate && row.WeekStart.HasValue && row.WeekEnd.HasValue)
            {
                _suppressReload = true;
                chkWeekTotal.Checked = false;
                dtpFromDate.Value = row.WeekStart.Value.ToDateTime(TimeOnly.MinValue);
                dtpToDate.Value = row.WeekEnd.Value.ToDateTime(TimeOnly.MinValue);
                _suppressReload = false;
                await LoadLedgerAsync();
                return;
            }

            switch (row.VoucherType)
            {
                case VoucherType.PurchaseBill:
                    OpenPurchase(row.VoucherId);
                    break;
                case VoucherType.SalesBill:
                    if (row.SalesInvNo.HasValue) OpenSales(row.SalesInvNo.Value);
                    break;
                case VoucherType.Receipt:
                    if (row.BookType.HasValue) OpenReceipt(row.VoucherId, row.BookType.Value);
                    break;
                case VoucherType.Payment:
                    if (row.BookType.HasValue) OpenPayment(row.VoucherId, row.BookType.Value);
                    break;
                case VoucherType.Journal:
                    OpenJournal(row.VoucherId);
                    break;
            }
        }

        private static T? Resolve<T>() where T : class => Program.ServiceProvider?.GetService(typeof(T)) as T;

        private void OpenPurchase(long purchaseBillId)
        {
            var purchaseService = Resolve<PurchaseService>();
            var accountService = Resolve<AccountService>();
            var accountGroupService = Resolve<AccountGroupService>();
            var regionService = Resolve<RegionService>();
            var itemService = Resolve<ItemService>();
            var lotService = Resolve<LotService>();
            if (purchaseService == null || accountService == null || accountGroupService == null
                || regionService == null || itemService == null || lotService == null) return;

            new PurchaseForm(purchaseService, accountService, accountGroupService, regionService,
                itemService, lotService, _companyId, _financialYearId, _currentUserId, purchaseBillId).ShowDialog();
        }

        private void OpenSales(int invNo)
        {
            var salesService = Resolve<SalesService>();
            var accountService = Resolve<AccountService>();
            var accountGroupService = Resolve<AccountGroupService>();
            var regionService = Resolve<RegionService>();
            var companyService = Resolve<CompanyService>();
            if (salesService == null || accountService == null || accountGroupService == null
                || regionService == null || companyService == null) return;

            new SalesForm(salesService, accountService, accountGroupService, regionService, companyService,
                _companyId, _financialYearId, _currentUserId, preselectInvNo: invNo).ShowDialog();
        }

        private void OpenReceipt(long receiptId, char bookType)
        {
            var receiptService = Resolve<ReceiptService>();
            var accountService = Resolve<AccountService>();
            var daybookService = Resolve<DaybookService>();
            var accountGroupService = Resolve<AccountGroupService>();
            var regionService = Resolve<RegionService>();
            if (receiptService == null || accountService == null || daybookService == null
                || accountGroupService == null || regionService == null) return;

            new ReceiptForm(receiptService, accountService, daybookService, accountGroupService, regionService,
                _companyId, _financialYearId, bookType, _currentUserId, preselectReceiptId: receiptId).ShowDialog();
        }

        private void OpenPayment(long paymentId, char bookType)
        {
            var paymentService = Resolve<PaymentService>();
            var accountService = Resolve<AccountService>();
            var daybookService = Resolve<DaybookService>();
            var accountGroupService = Resolve<AccountGroupService>();
            var regionService = Resolve<RegionService>();
            if (paymentService == null || accountService == null || daybookService == null
                || accountGroupService == null || regionService == null) return;

            new PaymentForm(paymentService, accountService, daybookService, accountGroupService, regionService,
                _companyId, _financialYearId, bookType, _currentUserId, preselectPaymentId: paymentId).ShowDialog();
        }

        private void OpenJournal(long journalVoucherId)
        {
            var journalService = Resolve<JournalService>();
            var accountService = Resolve<AccountService>();
            if (journalService == null || accountService == null) return;

            new JournalForm(journalService, accountService, _companyId, _financialYearId, _currentUserId,
                preselectJournalVoucherId: journalVoucherId).ShowDialog();
        }
    }
}
