using System;
using System.Globalization;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class TdsPaymentForm : BaseVoucherForm<TdsPayment>
    {
        private readonly TdsPaymentService _tdsPaymentService;
        private readonly AccountService _accountService;
        private readonly DaybookService _daybookService;
        private readonly AccountGroupService _accountGroupService;
        private readonly RegionService _regionService;
        private readonly long _companyId;
        private readonly long _financialYearId;
        private readonly long? _currentUserId;

        private List<Account> _accounts = new();
        private List<Daybook> _daybooks = new();
        private List<TdsPurchaseDeduction> _availableDeductions = new();
        private long _nextTdsPaymentNo = 1;

        public TdsPaymentForm(TdsPaymentService tdsPaymentService, AccountService accountService, DaybookService daybookService,
            AccountGroupService accountGroupService, RegionService regionService,
            long companyId, long financialYearId, long? currentUserId)
        {
            InitializeComponent();
            _tdsPaymentService = tdsPaymentService;
            _accountService = accountService;
            _daybookService = daybookService;
            _accountGroupService = accountGroupService;
            _regionService = regionService;
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

        private async void TdsPaymentForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        protected override async Task LoadDataAsync()
        {
            try
            {
                await RefreshAccountsAsync();
                await RefreshDaybooksAsync();

                _dataList = await _tdsPaymentService.GetAllTdsPaymentsAsync(_financialYearId);
                _nextTdsPaymentNo = await _tdsPaymentService.GetNextTdsPaymentNoAsync(_financialYearId);
                if (_dataList.Count > 0)
                {
                    _currentIndex = 0;
                    await DisplayCurrentRecordAsync();
                }
                else
                {
                    await ClearFormAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading TDS Payments: {ex.Message}", "Error");
            }
        }

        private async Task RefreshAccountsAsync()
        {
            var all = await _accountService.GetAllAccountsAsync(_companyId);
            _accounts = all.Where(a => !a.IsBlocked).OrderBy(a => a.Name).ToList();

            foreach (var combo in new[] { cmbTdsAccount, cmbInterestAccount, cmbFeesAccount, cmbPenaltyAccount })
            {
                combo.Items.Clear();
                foreach (var acc in _accounts)
                    combo.Items.Add($"{acc.Code} - {acc.Name}");
            }
        }

        private async Task RefreshDaybooksAsync()
        {
            _daybooks = await _daybookService.GetAllDaybooksAsync(_companyId);
            cmbDaybook.Items.Clear();
            foreach (var db in _daybooks)
                cmbDaybook.Items.Add(db.LinkedAccount?.BankAccountNo is string acct && !string.IsNullOrWhiteSpace(acct)
                    ? $"{db.Name} :  {acct}" : db.Name);
        }

        private int FindAccountIndexByCode(string code) =>
            _accounts.FindIndex(a => string.Equals(a.Code, code, StringComparison.OrdinalIgnoreCase));

        // DisplayCurrentRecord/ClearForm are synchronous overrides required by BaseCrudForm, but
        // loading the deduction grid needs to hit the database - these async wrappers do the real
        // work and the sync overrides just delegate for the calls that come from the base class.
        protected override void DisplayCurrentRecord() => _ = DisplayCurrentRecordAsync();
        protected override void ClearForm() => _ = ClearFormAsync();

        private async Task DisplayCurrentRecordAsync()
        {
            if (_currentIndex < 0 || _currentIndex >= _dataList.Count)
                return;

            var t = _dataList[_currentIndex];

            txtTdsPaymentNo.Text = t.TdsPaymentNo.ToString();
            dtpPaymentDate.Value = t.PaymentDate.ToDateTime(TimeOnly.MinValue);
            txtBsrCode.Text = t.BsrCode ?? "";
            txtChallanSerialNo.Text = t.ChallanSerialNo ?? "";
            txtInterestRatePct.Text = t.InterestRatePct.ToString(CultureInfo.InvariantCulture);

            cmbTdsAccount.SelectedIndex = _accounts.FindIndex(a => a.AccountId == t.TdsAccountId);
            cmbDaybook.SelectedIndex = _daybooks.FindIndex(d => d.DaybookId == t.DaybookId);
            cmbInterestAccount.SelectedIndex = t.InterestAccountId.HasValue ? _accounts.FindIndex(a => a.AccountId == t.InterestAccountId.Value) : -1;
            cmbFeesAccount.SelectedIndex = t.FeesAccountId.HasValue ? _accounts.FindIndex(a => a.AccountId == t.FeesAccountId.Value) : -1;
            cmbPenaltyAccount.SelectedIndex = t.PenaltyAccountId.HasValue ? _accounts.FindIndex(a => a.AccountId == t.PenaltyAccountId.Value) : -1;

            txtInterestAmount.Text = t.InterestAmount.ToString(CultureInfo.InvariantCulture);
            txtFeesAmount.Text = t.FeesAmount.ToString(CultureInfo.InvariantCulture);
            txtPenaltyAmount.Text = t.PenaltyAmount.ToString(CultureInfo.InvariantCulture);
            txtRemarks.Text = t.Remarks ?? "";

            await LoadDeductionGridAsync(t.TdsPaymentId);
            UpdateComputedFields();
            UpdateNavigationButtons();
        }

        private async Task ClearFormAsync()
        {
            txtTdsPaymentNo.Text = _nextTdsPaymentNo.ToString();
            dtpPaymentDate.Value = DateTime.Today;
            txtBsrCode.Clear();
            txtChallanSerialNo.Clear();
            txtInterestRatePct.Text = "0";

            cmbTdsAccount.SelectedIndex = FindAccountIndexByCode("TDSP");
            cmbDaybook.SelectedIndex = -1;
            cmbInterestAccount.SelectedIndex = FindAccountIndexByCode("TDSINT");
            cmbFeesAccount.SelectedIndex = FindAccountIndexByCode("TDSFEE");
            cmbPenaltyAccount.SelectedIndex = FindAccountIndexByCode("TDSPEN");

            txtInterestAmount.Text = "0";
            txtFeesAmount.Text = "0";
            txtPenaltyAmount.Text = "0";
            txtRemarks.Clear();

            await LoadDeductionGridAsync(null);
            UpdateComputedFields();

            _currentIndex = -1;
            UpdateNavigationButtons();
        }

        private async Task LoadDeductionGridAsync(long? forTdsPaymentId)
        {
            _availableDeductions = await _tdsPaymentService.GetAvailableDeductionsAsync(_financialYearId, forTdsPaymentId);

            dgvDeductions.Rows.Clear();
            foreach (var d in _availableDeductions)
            {
                bool included = forTdsPaymentId.HasValue && d.TdsPaymentId == forTdsPaymentId.Value;
                dgvDeductions.Rows.Add(included, d.Payment?.PaymentNo, d.DeductedAt.ToString("dd/MM/yyyy"),
                    d.Supplier?.Name, d.TdsAmount.ToString("N2"), d.TdsDeductionId);
            }
            RecalculateTaxFromGrid();
        }

        private void RecalculateTaxFromGrid()
        {
            decimal tax = 0;
            foreach (DataGridViewRow row in dgvDeductions.Rows)
            {
                if (row.Cells[0].Value is bool included && included
                    && decimal.TryParse(row.Cells[4].Value?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var amount))
                {
                    tax += amount;
                }
            }
            txtTax.Text = tax.ToString("N2", CultureInfo.InvariantCulture);
            UpdateComputedFields();
        }

        private void dgvDeductions_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // Commit the checkbox edit immediately so CellValueChanged fires without needing focus to leave the cell
            if (dgvDeductions.IsCurrentCellDirty)
                dgvDeductions.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvDeductions_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == 0 || e.ColumnIndex == 4))
                RecalculateTaxFromGrid();
        }

        // Rows pulled from a real TdsPurchaseDeduction record (colDeductionId set) are read-only for
        // Bill No/Date/Party/Amount - only the Y/N checkbox can be toggled. Manually-added rows (no
        // linked deduction, since the Purchase module doesn't generate these yet) are fully editable.
        private void dgvDeductions_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0)
                return;

            var row = dgvDeductions.Rows[e.RowIndex];
            if (row.Cells[5].Value != null)
                e.Cancel = true;
        }

        private void dgvDeductions_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[0].Value = true;
        }

        private async void btnNewTdsAccount_Click(object sender, EventArgs e)
        {
            using var accountForm = new AccountForm(_accountService, _accountGroupService, _regionService, _companyId);
            accountForm.ShowDialog();
            await RefreshAccountsAsync();
        }

        protected override bool ValidateInput()
        {
            if (cmbTdsAccount.SelectedIndex < 0)
            {
                MessageBox.Show("TDS A/c is required", "Validation Error");
                return false;
            }

            if (cmbDaybook.SelectedIndex < 0)
            {
                MessageBox.Show("Daybook is required", "Validation Error");
                return false;
            }

            if (!decimal.TryParse(txtTax.Text, out var tax) || tax <= 0)
            {
                MessageBox.Show("Select at least one deduction line, or check the Tax amount", "Validation Error");
                return false;
            }

            (string label, TextBox box)[] numericFields =
            {
                ("Interest %", txtInterestRatePct), ("Interest", txtInterestAmount),
                ("Fees", txtFeesAmount), ("Other Penalty", txtPenaltyAmount)
            };
            foreach (var (label, box) in numericFields)
            {
                if (!decimal.TryParse(box.Text, out _))
                {
                    MessageBox.Show($"{label} must be a valid number", "Validation Error");
                    return false;
                }
            }

            if (decimal.Parse(txtInterestAmount.Text) != 0 && cmbInterestAccount.SelectedIndex < 0)
            {
                MessageBox.Show("Interest amount entered - select an Interest A/c", "Validation Error");
                return false;
            }
            if (decimal.Parse(txtFeesAmount.Text) != 0 && cmbFeesAccount.SelectedIndex < 0)
            {
                MessageBox.Show("Fees amount entered - select a Fees A/c", "Validation Error");
                return false;
            }
            if (decimal.Parse(txtPenaltyAmount.Text) != 0 && cmbPenaltyAccount.SelectedIndex < 0)
            {
                MessageBox.Show("Other Penalty amount entered - select a Penalty A/c", "Validation Error");
                return false;
            }

            var daybook = _daybooks[cmbDaybook.SelectedIndex];
            if (daybook.LinkedAccountId == null)
            {
                MessageBox.Show($"Daybook '{daybook.Name}' has no Linked Account set. Set one in the Daybook master before using it for a TDS Payment.", "Validation Error");
                return false;
            }

            return true;
        }

        protected override async Task<bool> SaveRecordAsync()
        {
            var deductionIds = new List<long>();
            foreach (DataGridViewRow row in dgvDeductions.Rows)
            {
                if (row.Cells[0].Value is bool included && included && row.Cells[5].Value != null)
                    deductionIds.Add(Convert.ToInt64(row.Cells[5].Value));
            }

            var input = new TdsPaymentService.TdsPaymentInput
            {
                PaymentDate = DateOnly.FromDateTime(dtpPaymentDate.Value),
                TdsAccountId = _accounts[cmbTdsAccount.SelectedIndex].AccountId,
                DaybookId = _daybooks[cmbDaybook.SelectedIndex].DaybookId,
                BsrCode = string.IsNullOrWhiteSpace(txtBsrCode.Text) ? null : txtBsrCode.Text.Trim(),
                ChallanSerialNo = string.IsNullOrWhiteSpace(txtChallanSerialNo.Text) ? null : txtChallanSerialNo.Text.Trim(),
                InterestRatePct = decimal.Parse(txtInterestRatePct.Text),
                TaxAmount = decimal.Parse(txtTax.Text),
                InterestAmount = decimal.Parse(txtInterestAmount.Text),
                FeesAmount = decimal.Parse(txtFeesAmount.Text),
                PenaltyAmount = decimal.Parse(txtPenaltyAmount.Text),
                InterestAccountId = cmbInterestAccount.SelectedIndex >= 0 ? _accounts[cmbInterestAccount.SelectedIndex].AccountId : null,
                FeesAccountId = cmbFeesAccount.SelectedIndex >= 0 ? _accounts[cmbFeesAccount.SelectedIndex].AccountId : null,
                PenaltyAccountId = cmbPenaltyAccount.SelectedIndex >= 0 ? _accounts[cmbPenaltyAccount.SelectedIndex].AccountId : null,
                Remarks = string.IsNullOrWhiteSpace(txtRemarks.Text) ? null : txtRemarks.Text.Trim(),
                FinancialYearId = _financialYearId,
                CreatedBy = _currentUserId,
                DeductionIds = deductionIds
            };

            if (_isAddMode)
            {
                var (success, message) = await _tdsPaymentService.CreateTdsPaymentAsync(input);
                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
            else
            {
                var tdsPayment = _dataList[_currentIndex];
                var (success, message) = await _tdsPaymentService.UpdateTdsPaymentAsync(tdsPayment.TdsPaymentId, input);
                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
        }

        protected override async Task<bool> DeleteRecordAsync()
        {
            var tdsPayment = _dataList[_currentIndex];
            var (success, message) = await _tdsPaymentService.DeleteTdsPaymentAsync(tdsPayment.TdsPaymentId);
            MessageBox.Show(message, success ? "Success" : "Error");
            return success;
        }

        protected override void ToggleEditMode(bool isEditing)
        {
            base.ToggleEditMode(isEditing);

            Control[] controls =
            {
                dtpPaymentDate, txtBsrCode, txtChallanSerialNo, cmbTdsAccount, btnNewTdsAccount, txtInterestRatePct, cmbDaybook,
                cmbInterestAccount, txtInterestAmount, cmbFeesAccount, txtFeesAmount,
                cmbPenaltyAccount, txtPenaltyAmount, txtRemarks
            };

            foreach (var control in controls)
                control.Enabled = isEditing;

            dgvDeductions.ReadOnly = !isEditing;
        }

        private void UpdateComputedFields()
        {
            decimal.TryParse(txtTax.Text, out var tax);
            decimal.TryParse(txtInterestAmount.Text, out var interest);
            decimal.TryParse(txtFeesAmount.Text, out var fees);
            decimal.TryParse(txtPenaltyAmount.Text, out var penalty);

            var total = tax + interest + fees + penalty;
            txtTotalAmount.Text = total.ToString("N2", CultureInfo.InvariantCulture);
        }

        private void AmountField_Changed(object sender, EventArgs e) => UpdateComputedFields();

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            _nextTdsPaymentNo = await _tdsPaymentService.GetNextTdsPaymentNoAsync(_financialYearId);
            OnAdd();
        }

        private void btnUpdate_Click(object sender, EventArgs e) => OnUpdate();

        private void btnSave_Click(object sender, EventArgs e) => OnSave();

        private void btnDelete_Click(object sender, EventArgs e) => OnDelete();

        private void btnPrevious_Click(object sender, EventArgs e) => OnPrevious();

        private void btnNext_Click(object sender, EventArgs e) => OnNext();

        private async void btnFind_Click(object sender, EventArgs e)
        {
            using var findForm = new FindTdsPaymentForm(_tdsPaymentService, _financialYearId);
            if (findForm.ShowDialog() == DialogResult.OK)
            {
                var selected = findForm.SelectedTdsPayment;
                if (selected != null)
                {
                    _currentIndex = _dataList.FindIndex(t => t.TdsPaymentId == selected.TdsPaymentId);
                    if (_currentIndex >= 0)
                    {
                        await DisplayCurrentRecordAsync();
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => OnClose();

        private void btnPrint_Click(object sender, EventArgs e) => OnPrint();

        private void btnWhatsapp_Click(object sender, EventArgs e) => OnWhatsapp();
    }
}
