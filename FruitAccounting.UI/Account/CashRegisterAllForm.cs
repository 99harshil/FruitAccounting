using System.Globalization;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class CashRegisterAllForm : Form
    {
        private readonly LedgerService _ledgerService;
        private readonly AccountService _accountService;
        private readonly DaybookService _daybookService;
        private readonly long _companyId;
        private readonly long _financialYearId;
        private readonly FinancialYear _financialYear;

        private List<Daybook> _cashDaybooks = new();
        private List<Account> _allAccounts = new();
        private long _loadSeq;

        public CashRegisterAllForm(LedgerService ledgerService, AccountService accountService, DaybookService daybookService,
            long companyId, long financialYearId, FinancialYear financialYear)
        {
            InitializeComponent();
            _ledgerService = ledgerService;
            _accountService = accountService;
            _daybookService = daybookService;
            _companyId = companyId;
            _financialYearId = financialYearId;
            _financialYear = financialYear;
        }

        private async void CashRegisterAllForm_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = _financialYear.StartDate.ToDateTime(TimeOnly.MinValue);
            dtpToDate.Value = DateTime.Today;

            _allAccounts = await _accountService.GetAllAccountsAsync(_companyId);
            _cashDaybooks = await _daybookService.GetDaybooksAsync(_companyId, 'C');

            cmbAccountCode.Items.Clear();
            cmbAccountName.Items.Clear();
            foreach (var daybook in _cashDaybooks.OrderBy(d => d.Name))
            {
                cmbAccountCode.Items.Add(daybook.Name);
                cmbAccountName.Items.Add(daybook.Name);
            }
        }

        private async void cmbAccountCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbAccountName.SelectedIndex = cmbAccountCode.SelectedIndex;
            UpdateMobileField();
            await LoadRegisterAsync();
        }

        private async void cmbAccountName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbAccountCode.SelectedIndex = cmbAccountName.SelectedIndex;
            UpdateMobileField();
            await LoadRegisterAsync();
        }

        private void UpdateMobileField()
        {
            if (cmbAccountName.SelectedIndex >= 0 && cmbAccountName.SelectedIndex < _cashDaybooks.Count)
            {
                var daybook = _cashDaybooks[cmbAccountName.SelectedIndex];
                var linkedAccount = _allAccounts.FirstOrDefault(a => a.AccountId == daybook.LinkedAccountId);
                txtMobile.Text = linkedAccount?.Mobile ?? "";
            }
            else
            {
                txtMobile.Text = "";
            }
        }

        private async void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            await LoadRegisterAsync();
        }

        private async void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            await LoadRegisterAsync();
        }

        private async Task LoadRegisterAsync()
        {
            var mySeq = ++_loadSeq;

            if (cmbAccountName.SelectedIndex < 0 || cmbAccountName.SelectedIndex >= _cashDaybooks.Count)
            {
                dgvRegister.Rows.Clear();
                return;
            }

            var daybook = _cashDaybooks[cmbAccountName.SelectedIndex];
            if (!daybook.LinkedAccountId.HasValue)
            {
                MessageBox.Show("This daybook has no linked account.", "Cash Register");
                return;
            }

            var fromDate = DateOnly.FromDateTime(dtpFromDate.Value);
            var toDate = DateOnly.FromDateTime(dtpToDate.Value);

            if (fromDate > toDate)
            {
                MessageBox.Show("From date cannot be after To date", "Cash Register");
                return;
            }

            var result = await _ledgerService.GetLedgerAsync(daybook.LinkedAccountId.Value, _financialYearId, fromDate, toDate);

            if (mySeq != _loadSeq)
                return;

            dgvRegister.Rows.Clear();

            AddBalanceRow("Opening Balance :", result.OpeningBalance);

            int sr = 1;
            foreach (var row in result.Rows)
            {
                int rowIndex = dgvRegister.Rows.Add(
                    sr++,
                    row.VoucherCode,
                    row.Date.ToString("dd/MM/yyyy"),
                    row.Detail,
                    row.ChequeOrUser,
                    row.Debit == 0 ? "" : row.Debit.ToString("N2", CultureInfo.InvariantCulture),
                    row.Credit == 0 ? "" : row.Credit.ToString("N2", CultureInfo.InvariantCulture),
                    FormatBalance(row.RunningBalance));
                dgvRegister.Rows[rowIndex].Tag = row;
            }

            dgvRegister.Rows.Add("", "", "", "Grand Total :-", "",
                result.TotalDebit.ToString("N2", CultureInfo.InvariantCulture),
                result.TotalCredit.ToString("N2", CultureInfo.InvariantCulture),
                "");
            AddBalanceRow("Balance :-", result.ClosingBalance);
        }

        private void AddBalanceRow(string label, decimal signedBalance)
        {
            var amount = Math.Abs(signedBalance).ToString("N2", CultureInfo.InvariantCulture);
            var balanceText = FormatBalance(signedBalance);
            if (signedBalance >= 0)
                dgvRegister.Rows.Add("", "", "", label, "", amount, "", balanceText);
            else
                dgvRegister.Rows.Add("", "", "", label, "", "", amount, balanceText);
        }

        private static string FormatBalance(decimal signedBalance)
        {
            var suffix = signedBalance < 0 ? "Cr" : "Db";
            return $"{Math.Abs(signedBalance):N2} {suffix}";
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void btnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Print / PDF export will be added in the next step (FastReport .NET).", "Cash Register");
        }
    }
}
