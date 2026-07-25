using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class FindAccountForm : BaseSearchForm<Account>
    {
        private readonly AccountService _accountService;
        private readonly long _companyId;

        public FindAccountForm(AccountService accountService, long companyId)
        {
            InitializeComponent();
            _accountService = accountService;
            _companyId = companyId;

            // Initialize base class fields from Designer-created controls
            base.txtSearch = this.txtSearch;
            base.chkMatchCase = this.chkMatchCase;
            base.btnFind = this.btnFind;
            base.btnSelect = this.btnSelect;
            base.btnCancel = this.btnCancel;

            // Wire up DataGridView reference
            dgvData = this.dgvGroups;
        }

        private async void FindAccountForm_Load(object sender, EventArgs e)
        {
            await LoadAllDataAsync();
        }

        protected override async Task LoadAllDataAsync()
        {
            try
            {
                _allData = await _accountService.GetAllAccountsAsync(_companyId);
                DisplayData(_allData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Accounts: {ex.Message}", "Error");
            }
        }

        protected override void DisplayData(List<Account> data)
        {
            dgvGroups.Rows.Clear();
            foreach (var account in data)
            {
                dgvGroups.Rows.Add(account.Code, account.Name, account.AccountGroup?.Name, account.AccountId);
            }

            // Set header colors
            dgvGroups.EnableHeadersVisualStyles = false;
            dgvGroups.Columns[0].HeaderCell.Style.BackColor = Color.Cyan;
            dgvGroups.Columns[1].HeaderCell.Style.BackColor = Color.LimeGreen;
            dgvGroups.Columns[2].HeaderCell.Style.BackColor = Color.Cyan;
            dgvGroups.Columns[3].Visible = false; // Hide ID column
        }

        protected override List<Account> ApplySearchFilter(string searchPattern, bool matchCase)
        {
            RegexOptions options = matchCase ? RegexOptions.None : RegexOptions.IgnoreCase;
            var regex = new Regex(searchPattern, options);

            return _allData.Where(a =>
                regex.IsMatch(a.Name ?? "") ||
                regex.IsMatch(a.Code ?? "") ||
                regex.IsMatch(a.AccountGroup?.Name ?? "")
            ).ToList();
        }

        protected override Account? GetSelectedItemFromGrid()
        {
            if (dgvGroups.SelectedRows.Count <= 0)
                return null;

            var row = dgvGroups.SelectedRows[0];
            var accountId = Convert.ToInt64(row.Cells[3].Value);
            return _allData.FirstOrDefault(a => a.AccountId == accountId);
        }

        public Account? SelectedAccount => SelectedItem;

        private void txtSearch_TextChanged(object sender, EventArgs e) => PerformSearch();

        private void btnFind_Click(object sender, EventArgs e) => PerformSearch();

        private void btnSelect_Click(object sender, EventArgs e) => ConfirmSelection();

        private void btnCancel_Click(object sender, EventArgs e) => Close();

        private void dgvGroups_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                ConfirmSelection();
        }

        private void dgvGroups_KeyDown(object sender, KeyEventArgs e)
        {
            // Enter on the grid selects the highlighted row (grid normally swallows Enter to move down a row)
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                ConfirmSelection();
            }
        }
    }
}
