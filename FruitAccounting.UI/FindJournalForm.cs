using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class FindJournalForm : BaseSearchForm<JournalVoucher>
    {
        private readonly JournalService _journalService;
        private readonly long _financialYearId;

        public FindJournalForm(JournalService journalService, long financialYearId)
        {
            InitializeComponent();
            _journalService = journalService;
            _financialYearId = financialYearId;

            base.txtSearch = this.txtSearch;
            base.chkMatchCase = this.chkMatchCase;
            base.btnFind = this.btnFind;
            base.btnSelect = this.btnSelect;
            base.btnCancel = this.btnCancel;

            dgvData = this.dgvGroups;
        }

        private async void FindJournalForm_Load(object sender, EventArgs e)
        {
            await LoadAllDataAsync();
        }

        protected override async Task LoadAllDataAsync()
        {
            try
            {
                _allData = await _journalService.GetAllJournalVouchersAsync(_financialYearId);
                DisplayData(_allData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Journal Vouchers: {ex.Message}", "Error");
            }
        }

        protected override void DisplayData(List<JournalVoucher> data)
        {
            dgvGroups.Rows.Clear();
            foreach (var v in data)
            {
                // One row per line (Debit and Credit each get their own row), not one summed row
                // per voucher - Journal Vouchers can have any number of lines, and collapsing them
                // to a single total hides which account was debited vs credited.
                foreach (var line in v.JournalVoucherLines)
                {
                    dgvGroups.Rows.Add(
                        v.VoucherNo,
                        v.VoucherDate.ToString("dd/MM/yyyy"),
                        line.Account?.Name,
                        line.Debit == 0 ? "" : line.Debit.ToString("N2"),
                        line.Credit == 0 ? "" : line.Credit.ToString("N2"),
                        line.Narration,
                        v.JournalVoucherId);
                }
            }

            dgvGroups.EnableHeadersVisualStyles = false;
            dgvGroups.Columns[0].HeaderCell.Style.BackColor = Color.LimeGreen;
            dgvGroups.Columns[1].HeaderCell.Style.BackColor = Color.Cyan;
            dgvGroups.Columns[2].HeaderCell.Style.BackColor = Color.LimeGreen;
            dgvGroups.Columns[6].Visible = false; // Hide ID column
        }

        protected override List<JournalVoucher> ApplySearchFilter(string searchPattern, bool matchCase)
        {
            RegexOptions options = matchCase ? RegexOptions.None : RegexOptions.IgnoreCase;
            var regex = new Regex(searchPattern, options);

            return _allData.Where(v =>
                regex.IsMatch(v.VoucherNo.ToString()) ||
                regex.IsMatch(v.Narration ?? "") ||
                v.JournalVoucherLines.Any(l => regex.IsMatch(l.Account?.Name ?? "") || regex.IsMatch(l.Narration ?? ""))
            ).ToList();
        }

        protected override JournalVoucher? GetSelectedItemFromGrid()
        {
            if (dgvGroups.SelectedRows.Count <= 0)
                return null;

            var row = dgvGroups.SelectedRows[0];
            var journalVoucherId = Convert.ToInt64(row.Cells[6].Value);
            return _allData.FirstOrDefault(v => v.JournalVoucherId == journalVoucherId);
        }

        public JournalVoucher? SelectedJournalVoucher => SelectedItem;

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
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                ConfirmSelection();
            }
        }
    }
}
