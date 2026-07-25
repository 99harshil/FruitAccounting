using System;
using System.Globalization;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class BankReconciliationForm : Form
    {
        private readonly BankReconciliationService _bankReconciliationService;
        private readonly long _companyId;
        private readonly long _financialYearId;

        private List<Daybook> _bankDaybooks = new();
        private List<BankReconciliationService.ReconciliationLine> _lines = new();

        public BankReconciliationForm(BankReconciliationService bankReconciliationService, long companyId, long financialYearId)
        {
            InitializeComponent();
            _bankReconciliationService = bankReconciliationService;
            _companyId = companyId;
            _financialYearId = financialYearId;
        }

        private async void BankReconciliationForm_Load(object sender, EventArgs e)
        {
            _bankDaybooks = await _bankReconciliationService.GetBankDaybooksAsync(_companyId);
            cmbBankName.Items.Clear();
            foreach (var db in _bankDaybooks)
                cmbBankName.Items.Add(FormatDaybookDisplay(db));
        }

        private static string FormatDaybookDisplay(Daybook db)
        {
            var acctNo = db.LinkedAccount?.BankAccountNo;
            return string.IsNullOrWhiteSpace(acctNo) ? db.Name : $"{db.Name} :  {acctNo}";
        }

        private async void cmbBankName_SelectedIndexChanged(object sender, EventArgs e) => await LoadLinesAsync();

        private async void chkPending_CheckedChanged(object sender, EventArgs e) => await LoadLinesAsync();

        private async Task LoadLinesAsync()
        {
            dgvLines.Rows.Clear();
            if (cmbBankName.SelectedIndex < 0)
                return;

            var daybook = _bankDaybooks[cmbBankName.SelectedIndex];
            _lines = await _bankReconciliationService.GetReconciliationLinesAsync(daybook.DaybookId, _financialYearId, chkPending.Checked);

            foreach (var line in _lines)
            {
                int rowIndex = dgvLines.Rows.Add(
                    line.VNo,
                    line.ChequeNo,
                    line.Date.ToString("dd/MM/yyyy"),
                    line.AccountName,
                    line.ReceiveAmount == 0 ? "" : line.ReceiveAmount.ToString("N2"),
                    line.PaymentAmount == 0 ? "" : line.PaymentAmount.ToString("N2"),
                    line.ClearanceDate.HasValue ? line.ClearanceDate.Value.ToString("dd/MM/yyyy") : "");
                dgvLines.Rows[rowIndex].Tag = line;
            }
        }

        private void dgvLines_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Only the CL. Date column is editable - VNo/Chq No/Date/Account/Receive/Payment reflect
            // the underlying Receipt or Payment voucher and aren't meant to be changed from here.
            if (e.ColumnIndex != colClDate.Index)
                e.Cancel = true;
        }

        private async void dgvLines_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != colClDate.Index || e.RowIndex < 0)
                return;

            var row = dgvLines.Rows[e.RowIndex];
            if (row.Tag is not BankReconciliationService.ReconciliationLine line)
                return;

            var text = row.Cells[colClDate.Index].Value?.ToString()?.Trim();
            DateOnly? clearanceDate = null;
            if (!string.IsNullOrEmpty(text))
            {
                if (!DateOnly.TryParseExact(text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed))
                {
                    MessageBox.Show("Enter a valid date (dd/MM/yyyy) or leave blank for pending", "Invalid Date");
                    row.Cells[colClDate.Index].Value = line.ClearanceDate.HasValue ? line.ClearanceDate.Value.ToString("dd/MM/yyyy") : "";
                    return;
                }
                clearanceDate = parsed;
            }

            var (success, message) = await _bankReconciliationService.SetClearanceDateAsync(
                line.Type == BankReconciliationService.LineType.Receipt ? BankReconciliationService.LineType.Receipt : BankReconciliationService.LineType.Payment,
                line.VoucherId, clearanceDate);

            if (!success)
            {
                MessageBox.Show(message, "Error");
                row.Cells[colClDate.Index].Value = line.ClearanceDate.HasValue ? line.ClearanceDate.Value.ToString("dd/MM/yyyy") : "";
                return;
            }

            line.ClearanceDate = clearanceDate;

            // Re-filter immediately if showing pending-only and this row just got a clearance date
            if (chkPending.Checked && clearanceDate.HasValue)
                await LoadLinesAsync();
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();
    }
}
