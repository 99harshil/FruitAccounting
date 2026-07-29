using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class LedgerAmanatPartyWiseForm : Form
    {
        private readonly LedgerService _ledgerService;
        private readonly AccountService _accountService;
        private readonly long _companyId;
        private readonly long _financialYearId;
        private readonly FinancialYear _financialYear;

        // Parallel to cmbAmanatParty.Items - list of amanat party accounts.
        private List<Account> _amanatParties = new();

        public LedgerAmanatPartyWiseForm(LedgerService ledgerService, AccountService accountService,
            long companyId, long financialYearId, FinancialYear financialYear)
        {
            InitializeComponent();
            _ledgerService = ledgerService;
            _accountService = accountService;
            _companyId = companyId;
            _financialYearId = financialYearId;
            _financialYear = financialYear;
        }

        private async void LedgerAmanatPartyWiseForm_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = _financialYear.StartDate.ToDateTime(TimeOnly.MinValue);
            dtpToDate.Value = DateTime.Today;

            var all = await _accountService.GetAllAccountsAsync(_companyId);

            // Get all unique AmanatPartyIds that have at least one account pointing to them
            var amanatPartyIds = all
                .Where(a => a.AmanatPartyId.HasValue && a.AmanatPartyId != a.AccountId)
                .Select(a => a.AmanatPartyId.Value)
                .Distinct()
                .ToList();

            // Get the accounts that are Amanat Parties (have delegates)
            _amanatParties = all.Where(a => !a.IsBlocked && amanatPartyIds.Contains(a.AccountId))
                .OrderBy(a => a.Code)
                .ToList();

            cmbAmanatParty.Items.Clear();
            foreach (var ap in _amanatParties)
            {
                cmbAmanatParty.Items.Add($"{ap.Code} - {ap.Name}");
            }
        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            if (cmbAmanatParty.SelectedIndex < 0)
            {
                MessageBox.Show("Select an Amanat Party first", "Ledger");
                return;
            }

            var fromDate = DateOnly.FromDateTime(dtpFromDate.Value);
            var toDate = DateOnly.FromDateTime(dtpToDate.Value);
            if (fromDate > toDate)
            {
                MessageBox.Show("From date cannot be after To date", "Ledger");
                return;
            }

            var selectedAmanatParty = _amanatParties[cmbAmanatParty.SelectedIndex];

            var all = await _accountService.GetAllAccountsAsync(_companyId);
            // Get all accounts that point to this Amanat Party (excluding the Amanat Party itself)
            var accountIds = all.Where(a => !a.IsBlocked && a.AmanatPartyId == selectedAmanatParty.AccountId && a.AccountId != selectedAmanatParty.AccountId)
                .OrderBy(a => a.Code).Select(a => a.AccountId).ToList();

            new LedgerMultiAccountReportForm(_ledgerService, $"Ledger - Amanat Party Wise ({selectedAmanatParty.Name})",
                accountIds, _financialYearId, fromDate, toDate, chkWeekTotal.Checked, prependAccountName: true, amanatPartyId: selectedAmanatParty.AccountId).ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void btnWhatsapp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sending via WhatsApp is not implemented yet.", "WhatsApp", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
