using System.Globalization;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class TrialBalanceRegionWiseForm : Form
    {
        private readonly TrialBalanceService _trialBalanceService;
        private readonly RegionService _regionService;
        private readonly AccountService _accountService;
        private readonly long _companyId;
        private readonly long _financialYearId;
        private readonly FinancialYear _financialYear;

        private List<FruitAccounting.Data.Entities.Region> _allRegions = new();
        private List<TrialBalanceService.TrialBalanceRow> _allRows = new();
        private List<long> _regionAccountIds = new();
        private FruitAccounting.Data.Entities.Region? _selectedRegion;

        public TrialBalanceRegionWiseForm(TrialBalanceService trialBalanceService, RegionService regionService,
            AccountService accountService, long companyId, long financialYearId, FinancialYear financialYear)
        {
            InitializeComponent();
            _trialBalanceService = trialBalanceService;
            _regionService = regionService;
            _accountService = accountService;
            _companyId = companyId;
            _financialYearId = financialYearId;
            _financialYear = financialYear;
        }

        private async void TrialBalanceRegionWiseForm_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = _financialYear.StartDate.ToDateTime(TimeOnly.MinValue);
            dtpToDate.Value = DateTime.Today;

            _allRegions = await _regionService.GetAllRegionsAsync(_companyId);
            cmbRegion.Items.Clear();
            foreach (var region in _allRegions.OrderBy(r => r.Name))
            {
                cmbRegion.Items.Add(region.Name);
            }

            if (cmbRegion.Items.Count > 0)
                cmbRegion.SelectedIndex = 0;
        }

        private async void cmbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRegion.SelectedIndex >= 0)
            {
                var selectedRegionName = cmbRegion.SelectedItem?.ToString();
                _selectedRegion = _allRegions.FirstOrDefault(r => r.Name == selectedRegionName);
            }
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            if (cmbRegion.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a Region", "Trial Balance");
                return;
            }

            var selectedRegionName = cmbRegion.SelectedItem?.ToString();
            _selectedRegion = _allRegions.FirstOrDefault(r => r.Name == selectedRegionName);

            if (_selectedRegion == null)
            {
                MessageBox.Show("Region not found", "Trial Balance");
                return;
            }

            // Get all accounts for this region
            var allAccounts = await _accountService.GetAllAccountsAsync(_companyId);
            _regionAccountIds = allAccounts
                .Where(a => a.RegionId == _selectedRegion.RegionId && !a.IsBlocked)
                .Select(a => a.AccountId)
                .ToList();

            await LoadTrialBalanceAsync();
        }

        private void rdbDetail_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDetail.Checked)
                RefreshDisplay();
        }

        private void rdbOnlyOp_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbOnlyOp.Checked)
                RefreshDisplay();
        }

        private void rdbCrClosing_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbCrClosing.Checked)
                RefreshDisplay();
        }

        private void rdbDbClosing_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbDbClosing.Checked)
                RefreshDisplay();
        }

        private void rdbOnlyCl_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbOnlyCl.Checked)
                RefreshDisplay();
        }

        private void rdbNoTransaction_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbNoTransaction.Checked)
                RefreshDisplay();
        }

        private async Task LoadTrialBalanceAsync()
        {
            var fromDate = DateOnly.FromDateTime(dtpFromDate.Value);
            var toDate = DateOnly.FromDateTime(dtpToDate.Value);

            if (fromDate > toDate)
            {
                MessageBox.Show("From date cannot be after To date", "Trial Balance");
                return;
            }

            _allRows = await _trialBalanceService.GetTrialBalanceAsync(_companyId, _financialYearId, fromDate, toDate);
            RefreshDisplay();
        }

        private void RefreshDisplay()
        {
            dgvTrialBalance.Rows.Clear();
            dgvTrialBalance.Columns.Clear();

            // Add Account column (always present)
            dgvTrialBalance.Columns.Add("colAccount", "ACCOUNT");
            dgvTrialBalance.Columns["colAccount"].Width = 350;

            var fromDate = DateOnly.FromDateTime(dtpFromDate.Value);
            var toDate = DateOnly.FromDateTime(dtpToDate.Value);
            bool isDifferentDates = fromDate != toDate;

            if (rdbOnlyOp.Checked)
            {
                dgvTrialBalance.Columns.Add("colOpCr", "OP.CR");
                dgvTrialBalance.Columns.Add("colOpDr", "OP.DR");
                dgvTrialBalance.Columns["colOpCr"].Width = 120;
                dgvTrialBalance.Columns["colOpDr"].Width = 120;
                PopulateRows(showOpening: true, showTransaction: false, showClosing: false);
            }
            else if (rdbCrClosing.Checked)
            {
                dgvTrialBalance.Columns.Add("colClCr", "CL.CR");
                dgvTrialBalance.Columns["colClCr"].Width = 120;
                PopulateRows(showOpening: false, showTransaction: false, showClosing: true, onlyCredit: true);
            }
            else if (rdbDbClosing.Checked)
            {
                dgvTrialBalance.Columns.Add("colClDr", "CL.DR");
                dgvTrialBalance.Columns["colClDr"].Width = 120;
                PopulateRows(showOpening: false, showTransaction: false, showClosing: true, onlyDebit: true);
            }
            else if (rdbOnlyCl.Checked)
            {
                dgvTrialBalance.Columns.Add("colClCr", "CL.CR");
                dgvTrialBalance.Columns.Add("colClDr", "CL.DR");
                dgvTrialBalance.Columns["colClCr"].Width = 120;
                dgvTrialBalance.Columns["colClDr"].Width = 120;
                PopulateRows(showOpening: false, showTransaction: false, showClosing: true);
            }
            else if (rdbNoTransaction.Checked)
            {
                dgvTrialBalance.Columns.Add("colOpCr", "OP.CR");
                dgvTrialBalance.Columns.Add("colOpDr", "OP.DR");
                dgvTrialBalance.Columns["colOpCr"].Width = 120;
                dgvTrialBalance.Columns["colOpDr"].Width = 120;
                PopulateRows(showOpening: true, showTransaction: false, showClosing: false);
            }
            else // Detail or default
            {
                if (isDifferentDates)
                {
                    dgvTrialBalance.Columns.Add("colOpCr", "OP.CR");
                    dgvTrialBalance.Columns.Add("colOpDr", "OP.DR");
                    dgvTrialBalance.Columns.Add("colTrCr", "TR.CR");
                    dgvTrialBalance.Columns.Add("colTrDr", "TR.DR");
                    dgvTrialBalance.Columns.Add("colClCr", "CL.CR");
                    dgvTrialBalance.Columns.Add("colClDr", "CL.DR");
                    foreach (var col in dgvTrialBalance.Columns.Cast<DataGridViewColumn>())
                        col.Width = 120;
                    PopulateRows(showOpening: true, showTransaction: true, showClosing: true);
                }
                else
                {
                    dgvTrialBalance.Columns.Add("colOpCr", "OP.CR");
                    dgvTrialBalance.Columns.Add("colOpDr", "OP.DR");
                    dgvTrialBalance.Columns.Add("colClCr", "CL.CR");
                    dgvTrialBalance.Columns.Add("colClDr", "CL.DR");
                    foreach (var col in dgvTrialBalance.Columns.Cast<DataGridViewColumn>())
                        col.Width = 120;
                    PopulateRows(showOpening: true, showTransaction: false, showClosing: true);
                }
            }

            dgvTrialBalance.Columns["colAccount"].Width = 350;
        }

        private void PopulateRows(bool showOpening = false, bool showTransaction = false, bool showClosing = false,
            bool onlyCredit = false, bool onlyDebit = false)
        {
            if (_selectedRegion == null || _regionAccountIds.Count == 0)
                return;

            // Filter rows for selected region's accounts only
            var filteredRows = _allRows.Where(r =>
                _regionAccountIds.Contains(r.AccountId) ||
                r.IsSubTotal ||
                r.IsGrandTotal).ToList();

            foreach (var row in filteredRows)
            {
                // Skip group headers and subtotals if no accounts from this region in that group
                if (row.IsGroupHeader || (row.IsSubTotal && !row.IsGrandTotal))
                {
                    var groupHasAccounts = filteredRows.Any(r =>
                        !r.IsGroupHeader && !r.IsSubTotal && !r.IsGrandTotal &&
                        r.GroupName == row.GroupName);

                    if (!groupHasAccounts)
                        continue;
                }

                if (row.IsGroupHeader)
                {
                    int idx = dgvTrialBalance.Rows.Add(row.GroupName);
                    dgvTrialBalance.Rows[idx].DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 124);
                    dgvTrialBalance.Rows[idx].DefaultCellStyle.Font = new Font(dgvTrialBalance.Font, FontStyle.Bold);
                    dgvTrialBalance.Rows[idx].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    for (int i = 1; i < dgvTrialBalance.Columns.Count; i++)
                        dgvTrialBalance.Rows[idx].Cells[i].Value = "";
                    continue;
                }

                if (row.IsSubTotal)
                {
                    int idx = dgvTrialBalance.Rows.Add(row.GroupName);
                    dgvTrialBalance.Rows[idx].DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 124);
                    dgvTrialBalance.Rows[idx].DefaultCellStyle.Font = new Font(dgvTrialBalance.Font, FontStyle.Bold);

                    int colIndex = 1;
                    if (showOpening && !onlyCredit && !onlyDebit)
                    {
                        dgvTrialBalance.Rows[idx].Cells[colIndex++].Value = row.OpeningCr == 0 ? "" : row.OpeningCr.ToString("N2", CultureInfo.InvariantCulture);
                        dgvTrialBalance.Rows[idx].Cells[colIndex++].Value = row.OpeningDr == 0 ? "" : row.OpeningDr.ToString("N2", CultureInfo.InvariantCulture);
                    }
                    else if (showOpening && onlyCredit)
                    {
                        dgvTrialBalance.Rows[idx].Cells[colIndex++].Value = row.OpeningCr == 0 ? "" : row.OpeningCr.ToString("N2", CultureInfo.InvariantCulture);
                    }
                    else if (showOpening && onlyDebit)
                    {
                        dgvTrialBalance.Rows[idx].Cells[colIndex++].Value = row.OpeningDr == 0 ? "" : row.OpeningDr.ToString("N2", CultureInfo.InvariantCulture);
                    }

                    if (showTransaction)
                    {
                        dgvTrialBalance.Rows[idx].Cells[colIndex++].Value = row.TransactionCr == 0 ? "" : row.TransactionCr.ToString("N2", CultureInfo.InvariantCulture);
                        dgvTrialBalance.Rows[idx].Cells[colIndex++].Value = row.TransactionDr == 0 ? "" : row.TransactionDr.ToString("N2", CultureInfo.InvariantCulture);
                    }

                    if (showClosing && !onlyCredit && !onlyDebit)
                    {
                        dgvTrialBalance.Rows[idx].Cells[colIndex++].Value = row.ClosingCr == 0 ? "" : row.ClosingCr.ToString("N2", CultureInfo.InvariantCulture);
                        dgvTrialBalance.Rows[idx].Cells[colIndex++].Value = row.ClosingDr == 0 ? "" : row.ClosingDr.ToString("N2", CultureInfo.InvariantCulture);
                    }
                    else if (showClosing && onlyCredit)
                    {
                        dgvTrialBalance.Rows[idx].Cells[colIndex++].Value = row.ClosingCr == 0 ? "" : row.ClosingCr.ToString("N2", CultureInfo.InvariantCulture);
                    }
                    else if (showClosing && onlyDebit)
                    {
                        dgvTrialBalance.Rows[idx].Cells[colIndex++].Value = row.ClosingDr == 0 ? "" : row.ClosingDr.ToString("N2", CultureInfo.InvariantCulture);
                    }
                    continue;
                }

                if (row.IsGrandTotal)
                    continue; // Don't show grand total in region-wise view

                // Regular account row
                int rowIdx = dgvTrialBalance.Rows.Add($"{row.AccountCode} {row.AccountName}");
                dgvTrialBalance.Rows[rowIdx].DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 180);

                int cellIndex = 1;
                if (showOpening && !onlyCredit && !onlyDebit)
                {
                    dgvTrialBalance.Rows[rowIdx].Cells[cellIndex++].Value = row.OpeningCr == 0 ? "" : row.OpeningCr.ToString("N2", CultureInfo.InvariantCulture);
                    dgvTrialBalance.Rows[rowIdx].Cells[cellIndex++].Value = row.OpeningDr == 0 ? "" : row.OpeningDr.ToString("N2", CultureInfo.InvariantCulture);
                }
                else if (showOpening && onlyCredit)
                {
                    dgvTrialBalance.Rows[rowIdx].Cells[cellIndex++].Value = row.OpeningCr == 0 ? "" : row.OpeningCr.ToString("N2", CultureInfo.InvariantCulture);
                }
                else if (showOpening && onlyDebit)
                {
                    dgvTrialBalance.Rows[rowIdx].Cells[cellIndex++].Value = row.OpeningDr == 0 ? "" : row.OpeningDr.ToString("N2", CultureInfo.InvariantCulture);
                }

                if (showTransaction)
                {
                    dgvTrialBalance.Rows[rowIdx].Cells[cellIndex++].Value = row.TransactionCr == 0 ? "" : row.TransactionCr.ToString("N2", CultureInfo.InvariantCulture);
                    dgvTrialBalance.Rows[rowIdx].Cells[cellIndex++].Value = row.TransactionDr == 0 ? "" : row.TransactionDr.ToString("N2", CultureInfo.InvariantCulture);
                }

                if (showClosing && !onlyCredit && !onlyDebit)
                {
                    dgvTrialBalance.Rows[rowIdx].Cells[cellIndex++].Value = row.ClosingCr == 0 ? "" : row.ClosingCr.ToString("N2", CultureInfo.InvariantCulture);
                    dgvTrialBalance.Rows[rowIdx].Cells[cellIndex++].Value = row.ClosingDr == 0 ? "" : row.ClosingDr.ToString("N2", CultureInfo.InvariantCulture);
                }
                else if (showClosing && onlyCredit)
                {
                    dgvTrialBalance.Rows[rowIdx].Cells[cellIndex++].Value = row.ClosingCr == 0 ? "" : row.ClosingCr.ToString("N2", CultureInfo.InvariantCulture);
                }
                else if (showClosing && onlyDebit)
                {
                    dgvTrialBalance.Rows[rowIdx].Cells[cellIndex++].Value = row.ClosingDr == 0 ? "" : row.ClosingDr.ToString("N2", CultureInfo.InvariantCulture);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void btnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Print / PDF export will be added in the next step (FastReport .NET).", "Trial Balance");
        }
    }
}
