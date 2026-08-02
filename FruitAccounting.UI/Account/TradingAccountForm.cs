using System.Globalization;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class TradingAccountForm : Form
    {
        private readonly TradingAccountService _tradingAccountService;
        private readonly long _companyId;
        private readonly long _financialYearId;
        private readonly FinancialYear _financialYear;

        private bool _loaded;

        private static readonly Color GroupColor = Color.FromArgb(255, 200, 124);
        private static readonly Color RowColor = Color.FromArgb(255, 230, 180);
        private static readonly Color TotalColor = Color.FromArgb(255, 190, 110);

        public TradingAccountForm(TradingAccountService tradingAccountService,
            long companyId, long financialYearId, FinancialYear financialYear)
        {
            InitializeComponent();
            _tradingAccountService = tradingAccountService;
            _companyId = companyId;
            _financialYearId = financialYearId;
            _financialYear = financialYear;
        }

        private async void TradingAccountForm_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = _financialYear.StartDate.ToDateTime(TimeOnly.MinValue);
            dtpToDate.Value = DateTime.Today;
            _loaded = true;
            await LoadAsync();
        }

        // Only the To date reloads. Changing the From date first would fire a load against a
        // half-set period, so the report waits for the To date to be confirmed.
        private async void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            if (!_loaded) return;
            await LoadAsync();
        }

        private async Task LoadAsync()
        {
            var fromDate = DateOnly.FromDateTime(dtpFromDate.Value);
            var toDate = DateOnly.FromDateTime(dtpToDate.Value);

            if (fromDate > toDate)
            {
                MessageBox.Show("From date cannot be after To date", "Trading A/c");
                return;
            }

            var result = await _tradingAccountService.GetTradingAccountAsync(
                _companyId, _financialYearId, fromDate, toDate);

            dgvTrading.Rows.Clear();

            int rowCount = Math.Max(result.Expense.Count, result.Income.Count);
            for (int i = 0; i < rowCount; i++)
            {
                var left = i < result.Expense.Count ? result.Expense[i] : null;
                var right = i < result.Income.Count ? result.Income[i] : null;

                int idx = dgvTrading.Rows.Add(
                    left?.Text ?? "", FormatAmount(left),
                    right?.Text ?? "", FormatAmount(right));

                StyleHalf(idx, left, colExpenseName.Index, colExpenseAmount.Index);
                StyleHalf(idx, right, colIncomeName.Index, colIncomeAmount.Index);
            }
        }

        private void StyleHalf(int rowIndex, TradingAccountService.TradingLine? line, int nameCol, int amountCol)
        {
            var row = dgvTrading.Rows[rowIndex];
            var bold = new Font(dgvTrading.Font, FontStyle.Bold);

            var back = RowColor;
            var font = dgvTrading.Font;

            if (line == null)
            {
                back = Color.White;
            }
            else if (line.IsGroupHeader)
            {
                back = GroupColor;
                font = bold;
            }
            else if (line.IsSubTotal || line.IsGrossResult)
            {
                font = bold;
            }
            else if (line.IsGrandTotal)
            {
                back = TotalColor;
                font = bold;
            }

            foreach (var c in new[] { nameCol, amountCol })
            {
                row.Cells[c].Style.BackColor = back;
                row.Cells[c].Style.Font = font;
            }

            // account and total lines are indented; group headings sit flush left
            row.Cells[nameCol].Style.Padding =
                line != null && !line.IsGroupHeader ? new Padding(14, 0, 0, 0) : new Padding(0);
        }

        private static string FormatAmount(TradingAccountService.TradingLine? line)
            => line?.Amount == null ? "" : line.Amount.Value.ToString("N2", CultureInfo.InvariantCulture);

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void btnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Print / PDF export will be added in the next step (FastReport .NET).", "Trading A/c");
        }
    }
}
