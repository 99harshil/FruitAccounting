using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class FindReceiptForm : BaseSearchForm<Receipt>
    {
        private readonly ReceiptService _receiptService;
        private readonly long _financialYearId;
        private readonly char _bookType;

        public FindReceiptForm(ReceiptService receiptService, long financialYearId, char bookType)
        {
            InitializeComponent();
            _receiptService = receiptService;
            _financialYearId = financialYearId;
            _bookType = bookType;

            // Initialize base class fields from Designer-created controls
            base.txtSearch = this.txtSearch;
            base.chkMatchCase = this.chkMatchCase;
            base.btnFind = this.btnFind;
            base.btnSelect = this.btnSelect;
            base.btnCancel = this.btnCancel;

            // Wire up DataGridView reference
            dgvData = this.dgvGroups;
        }

        private async void FindReceiptForm_Load(object sender, EventArgs e)
        {
            await LoadAllDataAsync();
        }

        protected override async Task LoadAllDataAsync()
        {
            try
            {
                _allData = await _receiptService.GetAllReceiptsAsync(_financialYearId, _bookType);
                DisplayData(_allData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Receipts: {ex.Message}", "Error");
            }
        }

        protected override void DisplayData(List<Receipt> data)
        {
            dgvGroups.Rows.Clear();
            foreach (var r in data)
            {
                dgvGroups.Rows.Add(
                    r.ReceiptNo,
                    r.ReceiptDate.ToString("dd/MM/yyyy"),
                    r.Account?.Code,
                    r.Account?.Name,
                    r.Daybook?.Name,
                    r.ChequeNo,
                    r.BankName,
                    r.TotalSettled.ToString("N2"),
                    r.ReceiptId);
            }

            // Set header colors
            dgvGroups.EnableHeadersVisualStyles = false;
            dgvGroups.Columns[0].HeaderCell.Style.BackColor = Color.LimeGreen;
            dgvGroups.Columns[1].HeaderCell.Style.BackColor = Color.Cyan;
            dgvGroups.Columns[2].HeaderCell.Style.BackColor = Color.Cyan;
            dgvGroups.Columns[3].HeaderCell.Style.BackColor = Color.LimeGreen;
            dgvGroups.Columns[4].HeaderCell.Style.BackColor = Color.Cyan;
            dgvGroups.Columns[8].Visible = false; // Hide ID column
        }

        protected override List<Receipt> ApplySearchFilter(string searchPattern, bool matchCase)
        {
            RegexOptions options = matchCase ? RegexOptions.None : RegexOptions.IgnoreCase;
            var regex = new Regex(searchPattern, options);

            return _allData.Where(r =>
                regex.IsMatch(r.ReceiptNo.ToString()) ||
                regex.IsMatch(r.Account?.Code ?? "") ||
                regex.IsMatch(r.Account?.Name ?? "") ||
                regex.IsMatch(r.Daybook?.Name ?? "") ||
                regex.IsMatch(r.ChequeNo ?? "") ||
                regex.IsMatch(r.BankName ?? "")
            ).ToList();
        }

        protected override Receipt? GetSelectedItemFromGrid()
        {
            if (dgvGroups.SelectedRows.Count <= 0)
                return null;

            var row = dgvGroups.SelectedRows[0];
            var receiptId = Convert.ToInt64(row.Cells[8].Value);
            return _allData.FirstOrDefault(r => r.ReceiptId == receiptId);
        }

        public Receipt? SelectedReceipt => SelectedItem;

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
