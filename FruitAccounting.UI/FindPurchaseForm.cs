using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class FindPurchaseForm : BaseSearchForm<PurchaseBill>
    {
        private readonly PurchaseService _purchaseService;
        private readonly long _financialYearId;

        public FindPurchaseForm(PurchaseService purchaseService, long financialYearId)
        {
            InitializeComponent();
            _purchaseService = purchaseService;
            _financialYearId = financialYearId;

            base.txtSearch = this.txtSearch;
            base.chkMatchCase = this.chkMatchCase;
            base.btnFind = this.btnFind;
            base.btnSelect = this.btnSelect;
            base.btnCancel = this.btnCancel;

            dgvData = this.dgvGroups;
        }

        private async void FindPurchaseForm_Load(object sender, EventArgs e)
        {
            await LoadAllDataAsync();
        }

        protected override async Task LoadAllDataAsync()
        {
            try
            {
                _allData = await _purchaseService.GetAllPurchaseBillsAsync(_financialYearId);
                DisplayData(_allData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Purchase Bills: {ex.Message}", "Error");
            }
        }

        protected override void DisplayData(List<PurchaseBill> data)
        {
            dgvGroups.Rows.Clear();
            foreach (var b in data)
            {
                dgvGroups.Rows.Add(
                    b.BillNo,
                    b.BillDate.ToString("yyyyMMdd"),
                    b.Supplier?.Code,
                    b.Supplier?.Name,
                    b.TruckNo,
                    b.GrossAmount.ToString("N2"),
                    b.NetAmount.ToString("N2"),
                    b.Mark,
                    b.PurchaseBillId);
            }

            dgvGroups.EnableHeadersVisualStyles = false;
            dgvGroups.Columns[0].HeaderCell.Style.BackColor = Color.LimeGreen;
            dgvGroups.Columns[2].HeaderCell.Style.BackColor = Color.Cyan;
            dgvGroups.Columns[3].HeaderCell.Style.BackColor = Color.LimeGreen;
            dgvGroups.Columns[8].Visible = false; // Hide ID column
        }

        protected override List<PurchaseBill> ApplySearchFilter(string searchPattern, bool matchCase)
        {
            RegexOptions options = matchCase ? RegexOptions.None : RegexOptions.IgnoreCase;
            var regex = new Regex(searchPattern, options);

            return _allData.Where(b =>
                regex.IsMatch(b.BillNo.ToString()) ||
                regex.IsMatch(b.Supplier?.Code ?? "") ||
                regex.IsMatch(b.Supplier?.Name ?? "") ||
                regex.IsMatch(b.TruckNo ?? "") ||
                regex.IsMatch(b.Mark ?? "")
            ).ToList();
        }

        protected override PurchaseBill? GetSelectedItemFromGrid()
        {
            if (dgvGroups.SelectedRows.Count <= 0)
                return null;

            var row = dgvGroups.SelectedRows[0];
            var purchaseBillId = Convert.ToInt64(row.Cells[8].Value);
            return _allData.FirstOrDefault(b => b.PurchaseBillId == purchaseBillId);
        }

        public PurchaseBill? SelectedPurchaseBill => SelectedItem;

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
