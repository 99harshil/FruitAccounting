using System;
using System.Globalization;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;
using FruitAccounting.Data.Enums;

namespace FruitAccounting.UI
{
    public partial class PaymentForm : BaseVoucherForm<Payment>
    {
        private readonly PaymentService _paymentService;
        private readonly AccountService _accountService;
        private readonly DaybookService _daybookService;
        private readonly AccountGroupService _accountGroupService;
        private readonly RegionService _regionService;
        private readonly long _companyId;
        private readonly long _financialYearId;
        private readonly char _bookTypeMode; // 'C' = Cash Payment, 'B' = Bank Payment
        private readonly long? _currentUserId;

        private List<Account> _accounts = new();
        private List<Daybook> _daybooks = new();
        private long _nextPaymentNo = 1;
        private bool _suppressAccountSync;

        public PaymentForm(PaymentService paymentService, AccountService accountService, DaybookService daybookService,
            AccountGroupService accountGroupService, RegionService regionService,
            long companyId, long financialYearId, char bookTypeMode, long? currentUserId)
        {
            InitializeComponent();
            _paymentService = paymentService;
            _accountService = accountService;
            _daybookService = daybookService;
            _accountGroupService = accountGroupService;
            _regionService = regionService;
            _companyId = companyId;
            _financialYearId = financialYearId;
            _bookTypeMode = bookTypeMode;
            _currentUserId = currentUserId;

            Text = bookTypeMode == 'B' ? "Bank Payment" : "Cash Payment";

            // Cheque/Bank/Branch/Return only apply to Bank Payments
            bool isBank = bookTypeMode == 'B';
            lblChequeNo.Visible = isBank;
            txtChequeNo.Visible = isBank;
            lblBank.Visible = isBank;
            txtBankName.Visible = isBank;
            lblBranch.Visible = isBank;
            txtBranch.Visible = isBank;
            chkReturn.Visible = isBank;
            dtpReturnDate.Visible = isBank;

            // Initialize base class fields from Designer-created controls
            // (Designer fields shadow base class fields, so we use 'base.' to access base class fields)
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

        private async void PaymentForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        protected override async Task LoadDataAsync()
        {
            try
            {
                await RefreshAccountsAsync();
                await RefreshDaybooksAsync();

                _dataList = await _paymentService.GetAllPaymentsAsync(_financialYearId, _bookTypeMode);
                _nextPaymentNo = await _paymentService.GetNextPaymentNoAsync(_financialYearId);
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
                MessageBox.Show($"Error loading Payments: {ex.Message}", "Error");
            }
        }

        private async Task RefreshAccountsAsync()
        {
            var all = await _accountService.GetAllAccountsAsync(_companyId);
            _accounts = all.Where(a => !a.IsBlocked).OrderBy(a => a.Name).ToList();
            cmbAccountCode.Items.Clear();
            cmbAccountName.Items.Clear();
            foreach (var acc in _accounts)
            {
                cmbAccountCode.Items.Add(acc.Code);
                cmbAccountName.Items.Add(acc.Name);
            }
        }

        // cmbAccountCode and cmbAccountName are populated from the same _accounts list in the
        // same order, so keeping them in sync is just mirroring the selected index - entering
        // either the code or the name resolves the other.
        private void cmbAccountCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressAccountSync) return;
            _suppressAccountSync = true;
            cmbAccountName.SelectedIndex = cmbAccountCode.SelectedIndex;
            _suppressAccountSync = false;
            _ = UpdateComputedFieldsAsync();
        }

        private void cmbAccountName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressAccountSync) return;
            _suppressAccountSync = true;
            cmbAccountCode.SelectedIndex = cmbAccountName.SelectedIndex;
            _suppressAccountSync = false;
            _ = UpdateComputedFieldsAsync();
        }

        private async Task RefreshDaybooksAsync()
        {
            var all = await _daybookService.GetAllDaybooksAsync(_companyId);
            _daybooks = all.Where(d => d.BookType == _bookTypeMode).OrderBy(d => d.Name).ToList();
            cmbDaybook.Items.Clear();
            foreach (var db in _daybooks)
                cmbDaybook.Items.Add(FormatDaybookDisplay(db));
        }

        private static string FormatDaybookDisplay(Daybook db)
        {
            var acctNo = db.LinkedAccount?.BankAccountNo;
            return string.IsNullOrWhiteSpace(acctNo) ? db.Name : $"{db.Name} :  {acctNo}";
        }

        protected override void DisplayCurrentRecord()
        {
            if (_currentIndex < 0 || _currentIndex >= _dataList.Count)
                return;

            var p = _dataList[_currentIndex];

            txtPaymentNo.Text = p.PaymentNo.ToString();
            dtpPaymentDate.Value = p.PaymentDate.ToDateTime(TimeOnly.MinValue);
            txtTime.Text = p.CreatedAt.ToLocalTime().ToString("HH:mm");

            int daybookIndex = _daybooks.FindIndex(d => d.DaybookId == p.DaybookId);
            cmbDaybook.SelectedIndex = daybookIndex;

            int accountIndex = _accounts.FindIndex(a => a.AccountId == p.AccountId);
            cmbAccountName.SelectedIndex = accountIndex;

            // txtPaidAmount holds the net amount actually paid; the gross Amount is redisplayed via Total Amount.
            txtPaidAmount.Text = p.TotalSettled.ToString(CultureInfo.InvariantCulture);
            txtVatav.Text = p.Vatav.ToString(CultureInfo.InvariantCulture);
            txtHamali.Text = p.Hamali.ToString(CultureInfo.InvariantCulture);
            txtChequeNo.Text = p.ChequeNo ?? "";
            txtBankName.Text = p.BankName ?? "";
            txtBranch.Text = p.BankBranch ?? "";
            txtRemarks.Text = p.Remarks ?? "";

            chkReturn.Checked = p.IsReturned;
            dtpReturnDate.Checked = p.ReturnedDate.HasValue;
            if (p.ReturnedDate.HasValue)
                dtpReturnDate.Value = p.ReturnedDate.Value.ToDateTime(TimeOnly.MinValue);

            _ = UpdateComputedFieldsAsync();
            UpdateNavigationButtons();
        }

        protected override void ClearForm()
        {
            txtPaymentNo.Text = _nextPaymentNo.ToString();
            dtpPaymentDate.Value = DateTime.Today;
            txtTime.Text = DateTime.Now.ToString("HH:mm");

            cmbDaybook.SelectedIndex = -1;
            cmbAccountName.SelectedIndex = -1;
            txtPaidAmount.Text = "0";
            txtVatav.Text = "0";
            txtHamali.Text = "0";
            txtTdsAmt.Text = "0";
            txtChequeNo.Clear();
            txtBankName.Clear();
            txtBranch.Clear();
            txtRemarks.Clear();
            chkReturn.Checked = false;
            dtpReturnDate.Checked = false;

            _ = UpdateComputedFieldsAsync();

            _currentIndex = -1;
            UpdateNavigationButtons();
        }

        protected override bool ValidateInput()
        {
            if (cmbDaybook.SelectedIndex < 0)
            {
                MessageBox.Show("Daybook is required", "Validation Error");
                return false;
            }

            if (cmbAccountName.SelectedIndex < 0)
            {
                MessageBox.Show("Account is required", "Validation Error");
                return false;
            }

            if (!decimal.TryParse(txtPaidAmount.Text, out var amount) || amount <= 0)
            {
                MessageBox.Show("Paid Amount must be a valid number greater than zero", "Validation Error");
                return false;
            }

            (string label, TextBox box)[] numericFields = { ("Vatav", txtVatav), ("Hamali", txtHamali) };
            foreach (var (label, box) in numericFields)
            {
                if (!decimal.TryParse(box.Text, out _))
                {
                    MessageBox.Show($"{label} must be a valid number", "Validation Error");
                    return false;
                }
            }

            var daybook = _daybooks[cmbDaybook.SelectedIndex];
            if (daybook.LinkedAccountId == null)
            {
                MessageBox.Show($"Daybook '{daybook.Name}' has no Linked Account set. Set one in the Daybook master before using it for a Payment.", "Validation Error");
                return false;
            }

            return true;
        }

        protected override async Task<bool> SaveRecordAsync()
        {
            // txtPaidAmount is the net amount actually paid out; PaymentService.Amount is the
            // gross bill amount debited to the party, so Vatav/Hamali are added back on.
            var netAmount = decimal.Parse(txtPaidAmount.Text);
            var vatav = decimal.Parse(txtVatav.Text);
            var hamali = decimal.Parse(txtHamali.Text);

            var input = new PaymentService.PaymentInput
            {
                AccountId = _accounts[cmbAccountName.SelectedIndex].AccountId,
                DaybookId = _daybooks[cmbDaybook.SelectedIndex].DaybookId,
                PaymentDate = DateOnly.FromDateTime(dtpPaymentDate.Value),
                Amount = netAmount + vatav + hamali,
                Vatav = vatav,
                Hamali = hamali,
                Mode = _bookTypeMode == 'B' ? PaymentMode.Bank : PaymentMode.Cash,
                ChequeNo = string.IsNullOrWhiteSpace(txtChequeNo.Text) ? null : txtChequeNo.Text.Trim(),
                BankName = string.IsNullOrWhiteSpace(txtBankName.Text) ? null : txtBankName.Text.Trim(),
                BankBranch = string.IsNullOrWhiteSpace(txtBranch.Text) ? null : txtBranch.Text.Trim(),
                Remarks = string.IsNullOrWhiteSpace(txtRemarks.Text) ? null : txtRemarks.Text.Trim(),
                IsReturned = chkReturn.Checked,
                ReturnedDate = dtpReturnDate.Checked ? DateOnly.FromDateTime(dtpReturnDate.Value) : null,
                FinancialYearId = _financialYearId,
                CreatedBy = _currentUserId
            };

            if (_isAddMode)
            {
                var (success, message) = await _paymentService.CreatePaymentAsync(input);
                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
            else
            {
                var payment = _dataList[_currentIndex];
                var (success, message) = await _paymentService.UpdatePaymentAsync(payment.PaymentId, input);
                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
        }

        protected override async Task<bool> DeleteRecordAsync()
        {
            var payment = _dataList[_currentIndex];
            var (success, message) = await _paymentService.DeletePaymentAsync(payment.PaymentId);
            MessageBox.Show(message, success ? "Success" : "Error");
            return success;
        }

        protected override void ToggleEditMode(bool isEditing)
        {
            base.ToggleEditMode(isEditing);

            Control[] controls =
            {
                dtpPaymentDate, cmbDaybook, btnNewDaybook, cmbAccountCode, cmbAccountName, btnNewAccount,
                txtPaidAmount, txtVatav, txtHamali,
                txtChequeNo, txtBankName, txtBranch, txtRemarks, chkReturn, dtpReturnDate
            };

            foreach (var control in controls)
                control.Enabled = isEditing;
        }

        // txtPaidAmount holds the net amount actually paid; Vatav/Hamali are
        // added on top to arrive at the gross bill total.
        // TDS (194Q) is a live preview only, computed from the combined Payment+Purchase-Bill
        // cumulative for this party/FY - it does NOT reduce txtPaidAmount/TotalSettled (the cash
        // handed over stays exactly what's typed); it's posted as a separate extra charge against
        // the party at save time (see PaymentService.BuildAndAddLedgerEntriesAsync).
        private async Task UpdateComputedFieldsAsync()
        {
            decimal.TryParse(txtPaidAmount.Text, out var amount);
            decimal.TryParse(txtVatav.Text, out var vatav);
            decimal.TryParse(txtHamali.Text, out var hamali);

            var grossTotal = amount + vatav + hamali;
            txtTotalAmount.Text = grossTotal.ToString("N2", CultureInfo.InvariantCulture);
            lblAmountWords.Text = AmountInWords.Convert(grossTotal) + " Only.";

            if (cmbAccountName.SelectedIndex >= 0 && grossTotal > 0)
            {
                var accountId = _accounts[cmbAccountName.SelectedIndex].AccountId;
                var excludingId = _isAddMode || _currentIndex < 0 ? (long?)null : _dataList[_currentIndex].PaymentId;
                var (_, tdsAmount) = await _paymentService.PreviewTdsAsync(accountId, _financialYearId, grossTotal, DateOnly.FromDateTime(dtpPaymentDate.Value), excludingId);
                txtTdsAmt.Text = tdsAmount.ToString("N2", CultureInfo.InvariantCulture);
            }
            else
            {
                txtTdsAmt.Text = "0";
            }
        }

        private void AmountField_Changed(object sender, EventArgs e) => _ = UpdateComputedFieldsAsync();

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            _nextPaymentNo = await _paymentService.GetNextPaymentNoAsync(_financialYearId);
            OnAdd();
            cmbDaybook.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            OnUpdate();
            cmbDaybook.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e) => OnSave();

        private void btnDelete_Click(object sender, EventArgs e) => OnDelete();

        private void btnPrevious_Click(object sender, EventArgs e) => OnPrevious();

        private void btnNext_Click(object sender, EventArgs e) => OnNext();

        private async void btnNewDaybook_Click(object sender, EventArgs e)
        {
            using var daybookForm = new DaybookForm(_daybookService, _accountService, _accountGroupService, _regionService, _companyId);
            daybookForm.ShowDialog();
            await RefreshDaybooksAsync();
        }

        private async void btnNewAccount_Click(object sender, EventArgs e)
        {
            using var accountForm = new AccountForm(_accountService, _accountGroupService, _regionService, _companyId);
            accountForm.ShowDialog();
            await RefreshAccountsAsync();
        }

        private async void btnFind_Click(object sender, EventArgs e)
        {
            using var findForm = new FindPaymentForm(_paymentService, _financialYearId, _bookTypeMode);
            if (findForm.ShowDialog() == DialogResult.OK)
            {
                var selected = findForm.SelectedPayment;
                if (selected != null)
                {
                    _currentIndex = _dataList.FindIndex(p => p.PaymentId == selected.PaymentId);
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
