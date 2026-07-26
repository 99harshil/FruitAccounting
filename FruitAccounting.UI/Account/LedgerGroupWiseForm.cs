using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class LedgerGroupWiseForm : Form
    {
        private readonly LedgerService _ledgerService;
        private readonly AccountService _accountService;
        private readonly AccountGroupService _accountGroupService;
        private readonly long _companyId;
        private readonly long _financialYearId;
        private readonly FinancialYear _financialYear;

        // Parallel to cmbGroup.Items - main groups followed by their sub groups (indented in the
        // dropdown text). Index into this list resolves what cmbGroup.SelectedIndex actually means.
        private List<AccountGroup> _groups = new();

        public LedgerGroupWiseForm(LedgerService ledgerService, AccountService accountService, AccountGroupService accountGroupService,
            long companyId, long financialYearId, FinancialYear financialYear)
        {
            InitializeComponent();
            _ledgerService = ledgerService;
            _accountService = accountService;
            _accountGroupService = accountGroupService;
            _companyId = companyId;
            _financialYearId = financialYearId;
            _financialYear = financialYear;
        }

        private async void LedgerGroupWiseForm_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = _financialYear.StartDate.ToDateTime(TimeOnly.MinValue);
            dtpToDate.Value = DateTime.Today;

            var mainGroups = await _accountGroupService.GetAllMainGroupsAsync(_companyId);
            var subGroups = await _accountGroupService.GetAllSubGroupsAsync(_companyId);

            _groups = new List<AccountGroup>();
            cmbGroup.Items.Clear();
            foreach (var mg in mainGroups)
            {
                _groups.Add(mg);
                cmbGroup.Items.Add(mg.Name);

                foreach (var sg in subGroups.Where(s => s.ParentId == mg.AccountGroupId).OrderBy(s => s.Name))
                {
                    _groups.Add(sg);
                    cmbGroup.Items.Add($"    {sg.Name}");
                }
            }
        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            if (cmbGroup.SelectedIndex < 0)
            {
                MessageBox.Show("Select a Group first", "Ledger");
                return;
            }

            var fromDate = DateOnly.FromDateTime(dtpFromDate.Value);
            var toDate = DateOnly.FromDateTime(dtpToDate.Value);
            if (fromDate > toDate)
            {
                MessageBox.Show("From date cannot be after To date", "Ledger");
                return;
            }

            var selectedGroup = _groups[cmbGroup.SelectedIndex];

            // Picking a Main Group rolls up its Sub Groups too; picking a Sub Group is exact.
            var matchingGroupIds = new HashSet<long> { selectedGroup.AccountGroupId };
            if (selectedGroup.ParentId == null)
            {
                foreach (var g in _groups.Where(g => g.ParentId == selectedGroup.AccountGroupId))
                    matchingGroupIds.Add(g.AccountGroupId);
            }

            var all = await _accountService.GetAllAccountsAsync(_companyId);
            var accountIds = all.Where(a => !a.IsBlocked && matchingGroupIds.Contains(a.AccountGroupId))
                .OrderBy(a => a.Code).Select(a => a.AccountId).ToList();

            new LedgerMultiAccountReportForm(_ledgerService, $"Ledger - Group Wise ({selectedGroup.Name})",
                accountIds, _financialYearId, fromDate, toDate, chkWeekTotal.Checked).ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void btnWhatsapp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sending via WhatsApp is not implemented yet.", "WhatsApp", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
