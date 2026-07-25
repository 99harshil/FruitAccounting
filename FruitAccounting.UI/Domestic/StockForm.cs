using System;
using System.Globalization;
using System.Windows.Forms;
using FruitAccounting.Core.services;

namespace FruitAccounting.UI
{
    public partial class StockForm : Form
    {
        private readonly LotService _lotService;
        private readonly SalesService _salesService;
        private readonly AccountService _accountService;
        private readonly AccountGroupService _accountGroupService;
        private readonly RegionService _regionService;
        private readonly CompanyService _companyService;
        private readonly long _companyId;
        private readonly long _financialYearId;
        private readonly long? _currentUserId;

        private List<LotService.StockInfo> _allStock = new();

        public StockForm(LotService lotService, SalesService salesService, AccountService accountService,
            AccountGroupService accountGroupService, RegionService regionService, CompanyService companyService,
            long companyId, long financialYearId, long? currentUserId)
        {
            InitializeComponent();
            _lotService = lotService;
            _salesService = salesService;
            _accountService = accountService;
            _accountGroupService = accountGroupService;
            _regionService = regionService;
            _companyService = companyService;
            _companyId = companyId;
            _financialYearId = financialYearId;
            _currentUserId = currentUserId;
        }

        private async void StockForm_Load(object sender, EventArgs e)
        {
            await LoadStockAsync();
        }

        private async Task LoadStockAsync()
        {
            try
            {
                _allStock = await _lotService.GetStockAsync(_financialYearId);
                DisplayStock();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Stock: {ex.Message}", "Error");
            }
        }

        private void DisplayStock()
        {
            var filtered = _allStock.AsEnumerable();

            if (chkAvailableOnly.Checked)
                filtered = filtered.Where(s => s.BalanceQty > 0);

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                var term = txtSearch.Text.Trim();
                filtered = filtered.Where(s =>
                    s.LotNo.ToString().Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    s.ItemName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    s.SupplierName.Contains(term, StringComparison.OrdinalIgnoreCase));
            }

            dgvStock.Rows.Clear();
            foreach (var s in filtered)
            {
                var rowIndex = dgvStock.Rows.Add(
                    s.LotNo,
                    s.ItemName,
                    s.SupplierName,
                    s.PurchaseDate.ToString("dd/MM/yyyy"),
                    s.PurchasedQty.ToString(CultureInfo.InvariantCulture),
                    s.SoldQty.ToString(CultureInfo.InvariantCulture),
                    s.BalanceQty.ToString(CultureInfo.InvariantCulture));
                dgvStock.Rows[rowIndex].Tag = s;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) => DisplayStock();

        private void chkAvailableOnly_CheckedChanged(object sender, EventArgs e) => DisplayStock();

        private void dgvStock_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                OpenSalesForSelectedLot();
        }

        private void btnSell_Click(object sender, EventArgs e) => OpenSalesForSelectedLot();

        private void OpenSalesForSelectedLot()
        {
            if (dgvStock.CurrentRow?.Tag is not LotService.StockInfo stock)
            {
                MessageBox.Show("Select a Lot first", "Info");
                return;
            }

            if (stock.BalanceQty <= 0)
            {
                MessageBox.Show("This Lot has no remaining balance to sell.", "Not Available");
                return;
            }

            using var salesForm = new SalesForm(_salesService, _accountService, _accountGroupService, _regionService,
                _companyService, _companyId, _financialYearId, _currentUserId,
                preselectPurchaseBillId: stock.PurchaseBillId, preselectLotId: stock.LotId);
            salesForm.ShowDialog();

            _ = LoadStockAsync();
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();
    }
}
