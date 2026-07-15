using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FruitAccounting.Core.services;

namespace FruitAccounting.UI
{
    public partial class FindSalesForm : BaseSearchForm<SalesService.SalesVoucher>
    {
        private readonly SalesService _salesService;
        private readonly long _financialYearId;

        public FindSalesForm(SalesService salesService, long financialYearId)
        {
            InitializeComponent();
            _salesService = salesService;
            _financialYearId = financialYearId;

            base.txtSearch = this.txtSearch;
            base.chkMatchCase = this.chkMatchCase;
            base.btnFind = this.btnFind;
            base.btnSelect = this.btnSelect;
            base.btnCancel = this.btnCancel;

            dgvData = this.dgvGroups;
        }

        private async void FindSalesForm_Load(object sender, EventArgs e)
        {
            await LoadAllDataAsync();
        }

        protected override async Task LoadAllDataAsync()
        {
            try
            {
                _allData = await _salesService.GetAllSalesVouchersAsync(_financialYearId);
                DisplayData(_allData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Sales: {ex.Message}", "Error");
            }
        }

        protected override void DisplayData(List<SalesService.SalesVoucher> data)
        {
            dgvGroups.Rows.Clear();
            foreach (var voucher in data)
            {
                var lotNo = voucher.Lines.FirstOrDefault()?.Lot?.LotNo;
                dgvGroups.Rows.Add(lotNo, voucher.InvNo, voucher.SaleDate.ToString("yyyyMMdd"));
            }

            dgvGroups.EnableHeadersVisualStyles = false;
            dgvGroups.Columns[0].HeaderCell.Style.BackColor = Color.LimeGreen;
            dgvGroups.Columns[1].HeaderCell.Style.BackColor = Color.Cyan;
            dgvGroups.Columns[2].HeaderCell.Style.BackColor = Color.Cyan;
        }

        protected override List<SalesService.SalesVoucher> ApplySearchFilter(string searchPattern, bool matchCase)
        {
            RegexOptions options = matchCase ? RegexOptions.None : RegexOptions.IgnoreCase;
            var regex = new Regex(searchPattern, options);

            return _allData.Where(v =>
                regex.IsMatch(v.InvNo.ToString()) ||
                (v.Lines.FirstOrDefault()?.Lot?.LotNo.ToString() is string lotNo && regex.IsMatch(lotNo))
            ).ToList();
        }

        protected override SalesService.SalesVoucher? GetSelectedItemFromGrid()
        {
            if (dgvGroups.SelectedRows.Count <= 0)
                return null;

            var row = dgvGroups.SelectedRows[0];
            var invNo = Convert.ToInt32(row.Cells[1].Value);
            return _allData.FirstOrDefault(v => v.InvNo == invNo);
        }

        public SalesService.SalesVoucher? SelectedVoucher => SelectedItem;

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
