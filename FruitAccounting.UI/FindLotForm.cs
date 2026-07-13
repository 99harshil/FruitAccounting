using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class FindLotForm : BaseSearchForm<Lot>
    {
        private readonly LotService _lotService;
        private readonly long _financialYearId;

        public FindLotForm(LotService lotService, long financialYearId)
        {
            InitializeComponent();
            _lotService = lotService;
            _financialYearId = financialYearId;

            base.txtSearch = this.txtSearch;
            base.chkMatchCase = this.chkMatchCase;
            base.btnFind = this.btnFind;
            base.btnSelect = this.btnSelect;
            base.btnCancel = this.btnCancel;

            dgvData = this.dgvGroups;
        }

        private async void FindLotForm_Load(object sender, EventArgs e)
        {
            await LoadAllDataAsync();
        }

        protected override async Task LoadAllDataAsync()
        {
            try
            {
                _allData = await _lotService.GetAllLotsAsync(_financialYearId);
                DisplayData(_allData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Lots: {ex.Message}", "Error");
            }
        }

        protected override void DisplayData(List<Lot> data)
        {
            dgvGroups.Rows.Clear();
            foreach (var lot in data)
            {
                dgvGroups.Rows.Add(
                    lot.LotNo,
                    lot.Item?.Name,
                    lot.Supplier?.Name,
                    lot.ReceivedDate.ToString("dd/MM/yyyy"),
                    lot.IsClosed ? "Closed" : "Open",
                    lot.LotId);
            }

            dgvGroups.EnableHeadersVisualStyles = false;
            dgvGroups.Columns[0].HeaderCell.Style.BackColor = Color.LimeGreen;
            dgvGroups.Columns[1].HeaderCell.Style.BackColor = Color.Cyan;
            dgvGroups.Columns[2].HeaderCell.Style.BackColor = Color.LimeGreen;
            dgvGroups.Columns[5].Visible = false; // Hide ID column
        }

        protected override List<Lot> ApplySearchFilter(string searchPattern, bool matchCase)
        {
            RegexOptions options = matchCase ? RegexOptions.None : RegexOptions.IgnoreCase;
            var regex = new Regex(searchPattern, options);

            return _allData.Where(l =>
                regex.IsMatch(l.LotNo.ToString()) ||
                regex.IsMatch(l.Item?.Name ?? "") ||
                regex.IsMatch(l.Supplier?.Name ?? "")
            ).ToList();
        }

        protected override Lot? GetSelectedItemFromGrid()
        {
            if (dgvGroups.SelectedRows.Count <= 0)
                return null;

            var row = dgvGroups.SelectedRows[0];
            var lotId = Convert.ToInt64(row.Cells[5].Value);
            return _allData.FirstOrDefault(l => l.LotId == lotId);
        }

        public Lot? SelectedLot => SelectedItem;

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
