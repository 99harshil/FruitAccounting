using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class FindMainGroupForm : BaseSearchForm<AccountGroup>
    {
        private readonly AccountGroupService _accountGroupService;
        private readonly long _companyId;

        public FindMainGroupForm(AccountGroupService accountGroupService, long companyId)
        {
            InitializeComponent();
            _accountGroupService = accountGroupService;
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

        private async void FindMainGroupForm_Load(object sender, EventArgs e)
        {
            await LoadAllDataAsync();
        }

        protected override async Task LoadAllDataAsync()
        {
            try
            {
                _allData = await _accountGroupService.GetAllMainGroupsAsync(_companyId);
                DisplayData(_allData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Main Groups: {ex.Message}", "Error");
            }
        }

        protected override void DisplayData(List<AccountGroup> data)
        {
            dgvGroups.Rows.Clear();
            foreach (var group in data)
            {
                dgvGroups.Rows.Add(group.Name, group.Nature, group.AccountGroupId);
            }

            // Set header colors
            dgvGroups.EnableHeadersVisualStyles = false;
            dgvGroups.Columns[0].HeaderCell.Style.BackColor = Color.LimeGreen;
            dgvGroups.Columns[1].HeaderCell.Style.BackColor = Color.Cyan;
            dgvGroups.Columns[2].Visible = false; // Hide ID column
        }

        protected override List<AccountGroup> ApplySearchFilter(string searchPattern, bool matchCase)
        {
            RegexOptions options = matchCase ? RegexOptions.None : RegexOptions.IgnoreCase;
            var regex = new Regex(searchPattern, options);

            return _allData.Where(g =>
                regex.IsMatch(g.Name ?? "") ||
                regex.IsMatch(g.Nature ?? "")
            ).ToList();
        }

        protected override AccountGroup? GetSelectedItemFromGrid()
        {
            if (dgvGroups.SelectedRows.Count <= 0)
                return null;

            var row = dgvGroups.SelectedRows[0];
            var groupId = Convert.ToInt64(row.Cells[2].Value);
            return _allData.FirstOrDefault(g => g.AccountGroupId == groupId);
        }

        public AccountGroup? SelectedGroup => SelectedItem;

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