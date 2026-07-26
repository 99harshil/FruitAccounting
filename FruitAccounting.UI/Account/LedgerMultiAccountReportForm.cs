using System.Globalization;
using FruitAccounting.Core.services;

namespace FruitAccounting.UI
{
    // Shared preview grid for the multi-account Ledger variants (Account -> Reports -> Ledger ->
    // All / Group Wise) - one full section per account (header, opening balance, transactions,
    // grand total, closing balance), same row shape as LedgerReportForm's single-account grid.
    // Print is stubbed for now (in-app preview only, real output waits on FastReport .NET).
    public partial class LedgerMultiAccountReportForm : Form
    {
        private readonly LedgerService _ledgerService;
        private readonly List<long> _accountIds;
        private readonly long _financialYearId;
        private readonly DateOnly _fromDate;
        private readonly DateOnly _toDate;

        public LedgerMultiAccountReportForm(LedgerService ledgerService, string heading,
            List<long> accountIds, long financialYearId, DateOnly fromDate, DateOnly toDate)
        {
            InitializeComponent();
            _ledgerService = ledgerService;
            _accountIds = accountIds;
            _financialYearId = financialYearId;
            _fromDate = fromDate;
            _toDate = toDate;

            lblHeading.Text = $"{heading}   ({fromDate:dd/MM/yyyy} to {toDate:dd/MM/yyyy})";
            Text = heading;
        }

        private async void LedgerMultiAccountReportForm_Load(object sender, EventArgs e)
        {
            dgvLedger.Rows.Clear();

            if (_accountIds.Count == 0)
            {
                MessageBox.Show("No accounts to show for this selection.", "Ledger");
                return;
            }

            var results = await _ledgerService.GetLedgerForAccountsAsync(_accountIds, _financialYearId, _fromDate, _toDate);

            if (results.Count == 0)
            {
                MessageBox.Show("No activity found for any account in this period.", "Ledger");
                return;
            }

            foreach (var result in results)
            {
                int headerRowIndex = dgvLedger.Rows.Add("", $"{result.Account.Code}   {result.Account.Name}", "", "", "");
                dgvLedger.Rows[headerRowIndex].DefaultCellStyle.Font = new Font(dgvLedger.Font, FontStyle.Bold);

                AddBalanceRow("Opening Balance :", result.OpeningBalance);

                foreach (var row in result.Rows)
                {
                    dgvLedger.Rows.Add(
                        row.Date.ToString("dd/MM/yyyy"),
                        row.Detail,
                        row.Debit == 0 ? "" : row.Debit.ToString("N2", CultureInfo.InvariantCulture),
                        row.Credit == 0 ? "" : row.Credit.ToString("N2", CultureInfo.InvariantCulture),
                        FormatBalance(row.RunningBalance));
                }

                dgvLedger.Rows.Add("", "Grand Total :-",
                    result.TotalDebit.ToString("N2", CultureInfo.InvariantCulture),
                    result.TotalCredit.ToString("N2", CultureInfo.InvariantCulture),
                    "");
                AddBalanceRow("Balance :-", result.ClosingBalance);

                dgvLedger.Rows.Add("", "", "", "", ""); // blank separator before the next account
            }
        }

        private void AddBalanceRow(string label, decimal signedBalance)
        {
            var amount = Math.Abs(signedBalance).ToString("N2", CultureInfo.InvariantCulture);
            var balanceText = FormatBalance(signedBalance);
            if (signedBalance >= 0)
                dgvLedger.Rows.Add("", label, amount, "", balanceText);
            else
                dgvLedger.Rows.Add("", label, "", amount, balanceText);
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
    }
}
