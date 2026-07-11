using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class FindPaymentForm : BaseSearchForm<Payment>
    {
        private readonly PaymentService _paymentService;
        private readonly long _financialYearId;
        private readonly char _bookType;

        public FindPaymentForm(PaymentService paymentService, long financialYearId, char bookType)
        {
            InitializeComponent();
            _paymentService = paymentService;
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

        private async void FindPaymentForm_Load(object sender, EventArgs e)
        {
            await LoadAllDataAsync();
        }

        protected override async Task LoadAllDataAsync()
        {
            try
            {
                _allData = await _paymentService.GetAllPaymentsAsync(_financialYearId, _bookType);
                DisplayData(_allData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Payments: {ex.Message}", "Error");
            }
        }

        protected override void DisplayData(List<Payment> data)
        {
            dgvGroups.Rows.Clear();
            foreach (var p in data)
            {
                dgvGroups.Rows.Add(
                    p.PaymentNo,
                    p.PaymentDate.ToString("dd/MM/yyyy"),
                    p.Account?.Code,
                    p.Account?.Name,
                    p.Daybook?.Name,
                    p.ChequeNo,
                    p.BankName,
                    p.TotalSettled.ToString("N2"),
                    p.PaymentId);
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

        protected override List<Payment> ApplySearchFilter(string searchPattern, bool matchCase)
        {
            RegexOptions options = matchCase ? RegexOptions.None : RegexOptions.IgnoreCase;
            var regex = new Regex(searchPattern, options);

            return _allData.Where(p =>
                regex.IsMatch(p.PaymentNo.ToString()) ||
                regex.IsMatch(p.Account?.Code ?? "") ||
                regex.IsMatch(p.Account?.Name ?? "") ||
                regex.IsMatch(p.Daybook?.Name ?? "") ||
                regex.IsMatch(p.ChequeNo ?? "") ||
                regex.IsMatch(p.BankName ?? "")
            ).ToList();
        }

        protected override Payment? GetSelectedItemFromGrid()
        {
            if (dgvGroups.SelectedRows.Count <= 0)
                return null;

            var row = dgvGroups.SelectedRows[0];
            var paymentId = Convert.ToInt64(row.Cells[8].Value);
            return _allData.FirstOrDefault(p => p.PaymentId == paymentId);
        }

        public Payment? SelectedPayment => SelectedItem;

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
