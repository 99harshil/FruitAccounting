using System.Globalization;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class CashRegisterUserWiseForm : Form
    {
        private readonly LedgerService _ledgerService;
        private readonly AccountService _accountService;
        private readonly DaybookService _daybookService;
        private readonly long _companyId;
        private readonly long _financialYearId;
        private readonly FinancialYear _financialYear;
        private readonly long? _currentUserId;

        private List<Account> _allAccounts = new();
        private List<Account> _userAccounts = new();
        private List<User> _users = new();
        private long _loadSeq;

        public CashRegisterUserWiseForm(LedgerService ledgerService, AccountService accountService, DaybookService daybookService,
            long companyId, long financialYearId, FinancialYear financialYear, long? currentUserId = null)
        {
            InitializeComponent();
            _ledgerService = ledgerService;
            _accountService = accountService;
            _daybookService = daybookService;
            _companyId = companyId;
            _financialYearId = financialYearId;
            _financialYear = financialYear;
            _currentUserId = currentUserId;
        }

        private async void CashRegisterUserWiseForm_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = _financialYear.StartDate.ToDateTime(TimeOnly.MinValue);
            dtpToDate.Value = DateTime.Today;

            _allAccounts = await _accountService.GetAllAccountsAsync(_companyId);

            // Load users - for now, show a default user
            cmbUser.Items.Clear();
            cmbUser.Items.Add("ADMIN");
            if (cmbUser.Items.Count > 0)
                cmbUser.SelectedIndex = 0;
        }

        private async void cmbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Load cash daybooks and get their linked accounts
            var cashDaybooks = await _daybookService.GetDaybooksAsync(_companyId, 'C');
            var cashAccountIds = cashDaybooks
                .Where(d => d.LinkedAccountId.HasValue)
                .Select(d => d.LinkedAccountId.Value)
                .Distinct()
                .ToList();

            // Load accounts for selected user - only those linked to cash daybooks
            _userAccounts = _allAccounts
                .Where(a => !a.IsBlocked && cashAccountIds.Contains(a.AccountId))
                .OrderBy(a => a.Code)
                .ToList();

            cmbAccountCode.Items.Clear();
            cmbAccountName.Items.Clear();
            foreach (var acc in _userAccounts)
            {
                cmbAccountCode.Items.Add(acc.Code);
                cmbAccountName.Items.Add(acc.Name);
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
            if (cmbAccountName.SelectedIndex >= 0 && cmbAccountName.SelectedIndex < _userAccounts.Count)
            {
                txtMobile.Text = _userAccounts[cmbAccountName.SelectedIndex].Mobile ?? "";
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

            if (cmbAccountName.SelectedIndex < 0 || cmbAccountName.SelectedIndex >= _userAccounts.Count)
            {
                dgvRegister.Rows.Clear();
                return;
            }

            var account = _userAccounts[cmbAccountName.SelectedIndex];
            var fromDate = DateOnly.FromDateTime(dtpFromDate.Value);
            var toDate = DateOnly.FromDateTime(dtpToDate.Value);

            if (fromDate > toDate)
            {
                MessageBox.Show("From date cannot be after To date", "Cash Register");
                return;
            }

            var result = await _ledgerService.GetLedgerAsync(account.AccountId, _financialYearId, fromDate, toDate);

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
