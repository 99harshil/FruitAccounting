using System.Globalization;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class TrialBalanceGroupWiseForm : Form
    {
        private readonly TrialBalanceService _trialBalanceService;
        private readonly AccountGroupService _accountGroupService;
        private readonly long _companyId;
        private readonly long _financialYearId;
        private readonly FinancialYear _financialYear;

        private List<AccountGroup> _allGroups = new();
        private List<TrialBalanceService.TrialBalanceRow> _allRows = new();
        private AccountGroup? _selectedGroup;

        public TrialBalanceGroupWiseForm(TrialBalanceService trialBalanceService, AccountGroupService accountGroupService,
            long companyId, long financialYearId, FinancialYear financialYear)
        {
            InitializeComponent();
            _trialBalanceService = trialBalanceService;
            _accountGroupService = accountGroupService;
            _companyId = companyId;
            _financialYearId = financialYearId;
            _financialYear = financialYear;
        }

        private async void TrialBalanceGroupWiseForm_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = _financialYear.StartDate.ToDateTime(TimeOnly.MinValue);
            dtpToDate.Value = DateTime.Today;

            var mainGroups = await _accountGroupService.GetAllMainGroupsAsync(_companyId);
            var subGroups = await _accountGroupService.GetAllSubGroupsAsync(_companyId);

            _allGroups = new List<AccountGroup>();
            _allGroups.AddRange(mainGroups);
            _allGroups.AddRange(subGroups.OrderBy(g => g.Name));

            cmbGroup.Items.Clear();
            foreach (var group in _allGroups)
            {
                // Check if it's a sub group (has a parent)
                if (group.ParentId.HasValue)
                    cmbGroup.Items.Add($"  └─ {group.Name}");
                else
                    cmbGroup.Items.Add(group.Name);
            }

            if (cmbGroup.Items.Count > 0)
                cmbGroup.SelectedIndex = 0;
        }

        private async void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGroup.SelectedIndex >= 0 && cmbGroup.SelectedIndex < _allGroups.Count)
            {
                _selectedGroup = _allGroups[cmbGroup.SelectedIndex];
                await LoadTrialBalanceAsync();
            }
        }

        private async void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            await LoadTrialBalanceAsync();
        }

        private async void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
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
            if (_selectedGroup == null)
                return;

            // Filter rows for selected group only
            var filteredRows = _allRows.Where(r => r.AccountGroupId == _selectedGroup.AccountGroupId || r.IsSubTotal || r.IsGrandTotal).ToList();

            foreach (var row in filteredRows)
            {
                // Skip group headers and subtotals that don't belong to this group
                if (row.IsGroupHeader && row.AccountGroupId != _selectedGroup.AccountGroupId)
                    continue;

                if (row.IsSubTotal && !row.GroupName.Contains(_selectedGroup.Name))
                    continue;

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
                    continue; // Don't show grand total in group-wise view

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
