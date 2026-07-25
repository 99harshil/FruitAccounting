using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class FindTdsPaymentForm : BaseSearchForm<TdsPayment>
    {
        private readonly TdsPaymentService _tdsPaymentService;
        private readonly long _financialYearId;

        public FindTdsPaymentForm(TdsPaymentService tdsPaymentService, long financialYearId)
        {
            InitializeComponent();
            _tdsPaymentService = tdsPaymentService;
            _financialYearId = financialYearId;

            base.txtSearch = this.txtSearch;
            base.chkMatchCase = this.chkMatchCase;
            base.btnFind = this.btnFind;
            base.btnSelect = this.btnSelect;
            base.btnCancel = this.btnCancel;

            dgvData = this.dgvGroups;
        }

        private async void FindTdsPaymentForm_Load(object sender, EventArgs e)
        {
            await LoadAllDataAsync();
        }

        protected override async Task LoadAllDataAsync()
        {
            try
            {
                _allData = await _tdsPaymentService.GetAllTdsPaymentsAsync(_financialYearId);
                DisplayData(_allData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading TDS Payments: {ex.Message}", "Error");
            }
        }

        protected override void DisplayData(List<TdsPayment> data)
        {
            dgvGroups.Rows.Clear();
            foreach (var t in data)
            {
                dgvGroups.Rows.Add(
                    t.TdsPaymentNo,
                    t.PaymentDate.ToString("dd/MM/yyyy"),
                    t.BsrCode,
                    t.ChallanSerialNo,
                    t.TotalAmount.ToString("N2"),
                    t.TdsPaymentId);
            }

            dgvGroups.EnableHeadersVisualStyles = false;
            dgvGroups.Columns[0].HeaderCell.Style.BackColor = Color.LimeGreen;
            dgvGroups.Columns[1].HeaderCell.Style.BackColor = Color.Cyan;
            dgvGroups.Columns[2].HeaderCell.Style.BackColor = Color.Cyan;
            dgvGroups.Columns[3].HeaderCell.Style.BackColor = Color.LimeGreen;
            dgvGroups.Columns[5].Visible = false; // Hide ID column
        }

        protected override List<TdsPayment> ApplySearchFilter(string searchPattern, bool matchCase)
        {
            RegexOptions options = matchCase ? RegexOptions.None : RegexOptions.IgnoreCase;
            var regex = new Regex(searchPattern, options);

            return _allData.Where(t =>
                regex.IsMatch(t.TdsPaymentNo.ToString()) ||
                regex.IsMatch(t.BsrCode ?? "") ||
                regex.IsMatch(t.ChallanSerialNo ?? "")
            ).ToList();
        }

        protected override TdsPayment? GetSelectedItemFromGrid()
        {
            if (dgvGroups.SelectedRows.Count <= 0)
                return null;

            var row = dgvGroups.SelectedRows[0];
            var tdsPaymentId = Convert.ToInt64(row.Cells[5].Value);
            return _allData.FirstOrDefault(t => t.TdsPaymentId == tdsPaymentId);
        }

        public TdsPayment? SelectedTdsPayment => SelectedItem;

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
