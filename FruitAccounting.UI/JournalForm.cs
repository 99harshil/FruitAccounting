using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class JournalForm : BaseVoucherForm<JournalVoucher>
    {
        private readonly JournalService _journalService;
        private readonly AccountService _accountService;
        private readonly long _companyId;
        private readonly long _financialYearId;
        private readonly long? _currentUserId;

        private List<Account> _accounts = new();
        private long _nextVoucherNo = 1;
        private bool _suppressGridEvents;

        public JournalForm(JournalService journalService, AccountService accountService,
            long companyId, long financialYearId, long? currentUserId)
        {
            InitializeComponent();
            _journalService = journalService;
            _accountService = accountService;
            _companyId = companyId;
            _financialYearId = financialYearId;
            _currentUserId = currentUserId;

            base.btnAdd = this.btnAdd;
            base.btnUpdate = this.btnUpdate;
            base.btnDelete = this.btnDelete;
            base.btnSave = this.btnSave;
            base.btnPrevious = this.btnPrevious;
            base.btnNext = this.btnNext;
            base.btnFind = this.btnFind;
            base.btnClose = this.btnClose;
            base.btnPrint = this.btnPrint;
            base.btnWhatsapp = this.btnWhatsapp;

            ToggleEditMode(false);
        }

        private async void JournalForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        protected override async Task LoadDataAsync()
        {
            try
            {
                await RefreshAccountsAsync();

                _dataList = await _journalService.GetAllJournalVouchersAsync(_financialYearId);
                _nextVoucherNo = await _journalService.GetNextVoucherNoAsync(_financialYearId);
                if (_dataList.Count > 0)
                {
                    _currentIndex = 0;
                    DisplayCurrentRecord();
                }
                else
                {
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Journal Vouchers: {ex.Message}", "Error");
            }
        }

        private async Task RefreshAccountsAsync()
        {
            var all = await _accountService.GetAllAccountsAsync(_companyId);
            _accounts = all.Where(a => !a.IsBlocked).OrderBy(a => a.Name).ToList();

            colAccount.Items.Clear();
            foreach (var acc in _accounts)
                colAccount.Items.Add(acc.Name);
        }

        private Account? FindAccountByName(string? name) =>
            string.IsNullOrEmpty(name) ? null : _accounts.FirstOrDefault(a => a.Name == name);

        private Account? FindAccountByCode(string? code) =>
            string.IsNullOrEmpty(code) ? null : _accounts.FirstOrDefault(a => string.Equals(a.Code, code, StringComparison.OrdinalIgnoreCase));

        protected override void DisplayCurrentRecord()
        {
            if (_currentIndex < 0 || _currentIndex >= _dataList.Count)
                return;

            var v = _dataList[_currentIndex];

            txtVoucherNo.Text = v.VoucherNo.ToString();
            dtpVoucherDate.Value = v.VoucherDate.ToDateTime(TimeOnly.MinValue);

            _suppressGridEvents = true;
            dgvLines.Rows.Clear();
            foreach (var line in v.JournalVoucherLines)
            {
                dgvLines.Rows.Add(
                    line.Account?.Code,
                    line.Account?.Name,
                    line.Debit == 0 ? "" : line.Debit.ToString("N2"),
                    line.Credit == 0 ? "" : line.Credit.ToString("N2"),
                    line.Narration);
            }
            _suppressGridEvents = false;

            RecalculateTotals();
            UpdateNavigationButtons();
        }

        protected override void ClearForm()
        {
            txtVoucherNo.Text = _nextVoucherNo.ToString();
            dtpVoucherDate.Value = DateTime.Today;

            _suppressGridEvents = true;
            dgvLines.Rows.Clear();
            _suppressGridEvents = false;

            RecalculateTotals();

            _currentIndex = -1;
            UpdateNavigationButtons();
        }

        private void dgvLines_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvLines.IsCurrentCellDirty)
                dgvLines.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvLines_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_suppressGridEvents || e.RowIndex < 0)
                return;

            var row = dgvLines.Rows[e.RowIndex];

            // Account and Code cross-populate each other - entering either one resolves the other.
            if (e.ColumnIndex == colAccount.Index)
            {
                var account = FindAccountByName(row.Cells[colAccount.Index].Value as string);
                _suppressGridEvents = true;
                row.Cells[colCode.Index].Value = account?.Code;
                _suppressGridEvents = false;
            }
            else if (e.ColumnIndex == colCode.Index)
            {
                var account = FindAccountByCode(row.Cells[colCode.Index].Value as string);
                if (account != null)
                {
                    _suppressGridEvents = true;
                    row.Cells[colAccount.Index].Value = account.Name;
                    row.Cells[colCode.Index].Value = account.Code;
                    _suppressGridEvents = false;
                }
            }

            if (e.ColumnIndex == colDebit.Index || e.ColumnIndex == colCredit.Index)
                RecalculateTotals();
        }

        private void dgvLines_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) => RecalculateTotals();

        private void RecalculateTotals()
        {
            decimal totalDebit = 0, totalCredit = 0;
            foreach (DataGridViewRow row in dgvLines.Rows)
            {
                if (decimal.TryParse(row.Cells[colDebit.Index].Value?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
                    totalDebit += d;
                if (decimal.TryParse(row.Cells[colCredit.Index].Value?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var c))
                    totalCredit += c;
            }
            txtTotalDebit.Text = totalDebit.ToString("N2", CultureInfo.InvariantCulture);
            txtTotalCredit.Text = totalCredit.ToString("N2", CultureInfo.InvariantCulture);
            txtTotalCredit.ForeColor = totalDebit == totalCredit ? Color.Black : Color.Red;
            txtTotalDebit.ForeColor = totalDebit == totalCredit ? Color.Black : Color.Red;
        }

        protected override bool ValidateInput()
        {
            int validLines = 0;
            foreach (DataGridViewRow row in dgvLines.Rows)
            {
                var accountName = row.Cells[colAccount.Index].Value as string;
                if (string.IsNullOrWhiteSpace(accountName))
                    continue;

                if (FindAccountByName(accountName) == null)
                {
                    MessageBox.Show($"'{accountName}' is not a recognized account", "Validation Error");
                    return false;
                }

                var debitText = row.Cells[colDebit.Index].Value?.ToString();
                var creditText = row.Cells[colCredit.Index].Value?.ToString();
                decimal.TryParse(debitText, out var debit);
                decimal.TryParse(creditText, out var credit);

                if (!string.IsNullOrWhiteSpace(debitText) && !decimal.TryParse(debitText, out _))
                {
                    MessageBox.Show($"Invalid Debit amount for '{accountName}'", "Validation Error");
                    return false;
                }
                if (!string.IsNullOrWhiteSpace(creditText) && !decimal.TryParse(creditText, out _))
                {
                    MessageBox.Show($"Invalid Credit amount for '{accountName}'", "Validation Error");
                    return false;
                }
                if (debit != 0 && credit != 0)
                {
                    MessageBox.Show($"'{accountName}' has both Debit and Credit - use separate lines", "Validation Error");
                    return false;
                }
                if (debit == 0 && credit == 0)
                {
                    MessageBox.Show($"'{accountName}' needs either a Debit or a Credit amount", "Validation Error");
                    return false;
                }

                validLines++;
            }

            if (validLines < 2)
            {
                MessageBox.Show("A Journal Voucher needs at least two lines", "Validation Error");
                return false;
            }

            if (decimal.Parse(txtTotalDebit.Text, NumberStyles.Any, CultureInfo.InvariantCulture) !=
                decimal.Parse(txtTotalCredit.Text, NumberStyles.Any, CultureInfo.InvariantCulture))
            {
                MessageBox.Show("Total Debit must equal Total Credit before saving", "Validation Error");
                return false;
            }

            return true;
        }

        protected override async Task<bool> SaveRecordAsync()
        {
            var input = new JournalService.JournalVoucherInput
            {
                VoucherDate = DateOnly.FromDateTime(dtpVoucherDate.Value),
                FinancialYearId = _financialYearId,
                CreatedBy = _currentUserId
            };

            foreach (DataGridViewRow row in dgvLines.Rows)
            {
                var accountName = row.Cells[colAccount.Index].Value as string;
                var account = FindAccountByName(accountName);
                if (account == null)
                    continue;

                decimal.TryParse(row.Cells[colDebit.Index].Value?.ToString(), out var debit);
                decimal.TryParse(row.Cells[colCredit.Index].Value?.ToString(), out var credit);

                input.Lines.Add(new JournalService.JournalLineInput
                {
                    AccountId = account.AccountId,
                    Debit = debit,
                    Credit = credit,
                    Narration = row.Cells[colRemarks.Index].Value as string
                });
            }

            if (_isAddMode)
            {
                var (success, message) = await _journalService.CreateJournalVoucherAsync(input);
                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
            else
            {
                var voucher = _dataList[_currentIndex];
                var (success, message) = await _journalService.UpdateJournalVoucherAsync(voucher.JournalVoucherId, input);
                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
        }

        protected override async Task<bool> DeleteRecordAsync()
        {
            var voucher = _dataList[_currentIndex];
            var (success, message) = await _journalService.DeleteJournalVoucherAsync(voucher.JournalVoucherId);
            MessageBox.Show(message, success ? "Success" : "Error");
            return success;
        }

        protected override void ToggleEditMode(bool isEditing)
        {
            base.ToggleEditMode(isEditing);
            dtpVoucherDate.Enabled = isEditing;
            dgvLines.ReadOnly = !isEditing;
            dgvLines.AllowUserToAddRows = isEditing;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            _nextVoucherNo = await _journalService.GetNextVoucherNoAsync(_financialYearId);
            OnAdd();
        }

        private void btnUpdate_Click(object sender, EventArgs e) => OnUpdate();

        private void btnSave_Click(object sender, EventArgs e) => OnSave();

        private void btnDelete_Click(object sender, EventArgs e) => OnDelete();

        private void btnPrevious_Click(object sender, EventArgs e) => OnPrevious();

        private void btnNext_Click(object sender, EventArgs e) => OnNext();

        private void btnFind_Click(object sender, EventArgs e)
        {
            using var findForm = new FindJournalForm(_journalService, _financialYearId);
            if (findForm.ShowDialog() == DialogResult.OK)
            {
                var selected = findForm.SelectedJournalVoucher;
                if (selected != null)
                {
                    _currentIndex = _dataList.FindIndex(v => v.JournalVoucherId == selected.JournalVoucherId);
                    if (_currentIndex >= 0)
                    {
                        DisplayCurrentRecord();
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => OnClose();

        private void btnPrint_Click(object sender, EventArgs e) => OnPrint();

        private void btnWhatsapp_Click(object sender, EventArgs e) => OnWhatsapp();
    }
}
