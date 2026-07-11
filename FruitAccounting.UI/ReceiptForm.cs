using System;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;
using FruitAccounting.Data.Enums;

namespace FruitAccounting.UI
{
    public partial class ReceiptForm : BaseVoucherForm<Receipt>
    {
        private readonly ReceiptService _receiptService;
        private readonly AccountService _accountService;
        private readonly DaybookService _daybookService;
        private readonly AccountGroupService _accountGroupService;
        private readonly RegionService _regionService;
        private readonly long _companyId;
        private readonly long _financialYearId;
        private readonly char _bookTypeMode; // 'C' = Cash Receipt, 'B' = Bank Receipt
        private readonly long? _currentUserId;

        private List<Account> _accounts = new();
        private List<Daybook> _daybooks = new();
        private long _nextReceiptNo = 1;

        public ReceiptForm(ReceiptService receiptService, AccountService accountService, DaybookService daybookService,
            AccountGroupService accountGroupService, RegionService regionService,
            long companyId, long financialYearId, char bookTypeMode, long? currentUserId)
        {
            InitializeComponent();
            _receiptService = receiptService;
            _accountService = accountService;
            _daybookService = daybookService;
            _accountGroupService = accountGroupService;
            _regionService = regionService;
            _companyId = companyId;
            _financialYearId = financialYearId;
            _bookTypeMode = bookTypeMode;
            _currentUserId = currentUserId;

            Text = bookTypeMode == 'B' ? "Bank Receipt" : "Cash Receipt";

            // Cheque/Bank/Branch/Return only apply to Bank Receipts
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

        private async void ReceiptForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        protected override async Task LoadDataAsync()
        {
            try
            {
                await RefreshAccountsAsync();
                await RefreshDaybooksAsync();

                _dataList = await _receiptService.GetAllReceiptsAsync(_financialYearId);
                _nextReceiptNo = await _receiptService.GetNextReceiptNoAsync(_financialYearId);
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
                MessageBox.Show($"Error loading Receipts: {ex.Message}", "Error");
            }
        }

        private async Task RefreshAccountsAsync()
        {
            var all = await _accountService.GetAllAccountsAsync(_companyId);
            _accounts = all.Where(a => !a.IsBlocked).OrderBy(a => a.Name).ToList();
            cmbAccount.Items.Clear();
            foreach (var acc in _accounts)
                cmbAccount.Items.Add($"{acc.Code} - {acc.Name}");
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

            var r = _dataList[_currentIndex];

            txtReceiptNo.Text = r.ReceiptNo.ToString();
            dtpReceiptDate.Value = r.ReceiptDate.ToDateTime(TimeOnly.MinValue);
            txtTime.Text = r.CreatedAt.ToLocalTime().ToString("HH:mm");

            int daybookIndex = _daybooks.FindIndex(d => d.DaybookId == r.DaybookId);
            cmbDaybook.SelectedIndex = daybookIndex;

            int accountIndex = _accounts.FindIndex(a => a.AccountId == r.AccountId);
            cmbAccount.SelectedIndex = accountIndex;

            txtRecAmount.Text = r.TotalSettled.ToString(CultureInfo.InvariantCulture);
            txtVatav.Text = r.Vatav.ToString(CultureInfo.InvariantCulture);
            txtTds.Text = r.TdsAmount.ToString(CultureInfo.InvariantCulture);
            txtDifference.Text = r.RoundingDiff.ToString(CultureInfo.InvariantCulture);
            txtChequeNo.Text = r.ChequeNo ?? "";
            txtBankName.Text = r.BankName ?? "";
            txtBranch.Text = r.BankBranch ?? "";
            txtRemarks.Text = r.Remarks ?? "";

            chkReturn.Checked = r.IsReturned;
            dtpReturnDate.Checked = r.ReturnedDate.HasValue;
            if (r.ReturnedDate.HasValue)
                dtpReturnDate.Value = r.ReturnedDate.Value.ToDateTime(TimeOnly.MinValue);

            UpdateComputedFields();
            UpdateNavigationButtons();
        }

        protected override void ClearForm()
        {
            txtReceiptNo.Text = _nextReceiptNo.ToString();
            dtpReceiptDate.Value = DateTime.Today;
            txtTime.Text = DateTime.Now.ToString("HH:mm");

            cmbDaybook.SelectedIndex = -1;
            cmbAccount.SelectedIndex = -1;
            txtRecAmount.Text = "0";
            txtVatav.Text = "0";
            txtTds.Text = "0";
            txtDifference.Text = "0";
            txtChequeNo.Clear();
            txtBankName.Clear();
            txtBranch.Clear();
            txtRemarks.Clear();
            chkReturn.Checked = false;
            dtpReturnDate.Checked = false;

            UpdateComputedFields();

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

            if (cmbAccount.SelectedIndex < 0)
            {
                MessageBox.Show("Account is required", "Validation Error");
                return false;
            }

            if (!decimal.TryParse(txtRecAmount.Text, out var amount) || amount <= 0)
            {
                MessageBox.Show("Rec. Amount must be a valid number greater than zero", "Validation Error");
                return false;
            }

            (string label, TextBox box)[] numericFields = { ("Vatav", txtVatav), ("TDS", txtTds), ("Difference", txtDifference) };
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
                MessageBox.Show($"Daybook '{daybook.Name}' has no Linked Account set. Set one in the Daybook master before using it for a Receipt.", "Validation Error");
                return false;
            }

            return true;
        }

        protected override async Task<bool> SaveRecordAsync()
        {
            // txtRecAmount is the net amount actually paid/received; ReceiptService.Amount is the
            // gross bill amount credited to the party, so Vatav/TDS/Difference are added back on.
            var netAmount = decimal.Parse(txtRecAmount.Text);
            var vatav = decimal.Parse(txtVatav.Text);
            var tds = decimal.Parse(txtTds.Text);
            var diff = decimal.Parse(txtDifference.Text);

            var input = new ReceiptService.ReceiptInput
            {
                AccountId = _accounts[cmbAccount.SelectedIndex].AccountId,
                DaybookId = _daybooks[cmbDaybook.SelectedIndex].DaybookId,
                ReceiptDate = DateOnly.FromDateTime(dtpReceiptDate.Value),
                Amount = netAmount + vatav + tds + diff,
                Vatav = vatav,
                TdsAmount = tds,
                RoundingDiff = diff,
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
                var (success, message) = await _receiptService.CreateReceiptAsync(input);
                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
            else
            {
                var receipt = _dataList[_currentIndex];
                var (success, message) = await _receiptService.UpdateReceiptAsync(receipt.ReceiptId, input);
                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
        }

        protected override async Task<bool> DeleteRecordAsync()
        {
            var receipt = _dataList[_currentIndex];
            var (success, message) = await _receiptService.DeleteReceiptAsync(receipt.ReceiptId);
            MessageBox.Show(message, success ? "Success" : "Error");
            return success;
        }

        protected override void ToggleEditMode(bool isEditing)
        {
            base.ToggleEditMode(isEditing);

            Control[] controls =
            {
                dtpReceiptDate, cmbDaybook, btnNewDaybook, cmbAccount, btnNewAccount,
                txtRecAmount, txtVatav, txtTds, txtDifference,
                txtChequeNo, txtBankName, txtBranch, txtRemarks, chkReturn, dtpReturnDate
            };

            foreach (var control in controls)
                control.Enabled = isEditing;
        }

        // txtRecAmount holds the net amount actually paid/received; Vatav/TDS/Difference are
        // added on top to arrive at the gross bill total.
        private void UpdateComputedFields()
        {
            decimal.TryParse(txtRecAmount.Text, out var amount);
            decimal.TryParse(txtVatav.Text, out var vatav);
            decimal.TryParse(txtTds.Text, out var tds);
            decimal.TryParse(txtDifference.Text, out var diff);

            var total = amount + vatav + tds + diff;
            txtTotalAmount.Text = total.ToString("N2", CultureInfo.InvariantCulture);
            lblAmountWords.Text = AmountInWords.Convert(total) + " Only.";
        }

        private void AmountField_Changed(object sender, EventArgs e) => UpdateComputedFields();

        private void cmbDaybook_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Placeholder for a future live-balance display next to the Daybook picker
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            _nextReceiptNo = await _receiptService.GetNextReceiptNoAsync(_financialYearId);
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
            using var findForm = new FindReceiptForm(_receiptService, _financialYearId);
            if (findForm.ShowDialog() == DialogResult.OK)
            {
                var selected = findForm.SelectedReceipt;
                if (selected != null)
                {
                    _currentIndex = _dataList.FindIndex(r => r.ReceiptId == selected.ReceiptId);
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

    internal static class AmountInWords
    {
        private static readonly string[] Ones =
        {
            "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
            "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
        };
        private static readonly string[] Tens =
        {
            "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
        };

        public static string Convert(decimal amount)
        {
            bool negative = amount < 0;
            amount = Math.Abs(amount);
            long rupees = (long)Math.Floor(amount);
            int paise = (int)Math.Round((amount - rupees) * 100);

            var sb = new StringBuilder("Rupees ");
            if (negative) sb.Append("Minus ");
            sb.Append(rupees == 0 ? "Zero" : ConvertIndianGroups(rupees));
            if (paise > 0)
            {
                sb.Append(" and ");
                sb.Append(ConvertBelowThousand(paise));
                sb.Append(" Paise");
            }
            return sb.ToString();
        }

        private static string ConvertIndianGroups(long number)
        {
            var parts = new List<string>();

            long crore = number / 10000000;
            number %= 10000000;
            long lakh = number / 100000;
            number %= 100000;
            long thousand = number / 1000;
            number %= 1000;
            long remainder = number;

            if (crore > 0) parts.Add($"{ConvertBelowThousand((int)crore)} Crore");
            if (lakh > 0) parts.Add($"{ConvertBelowThousand((int)lakh)} Lakh");
            if (thousand > 0) parts.Add($"{ConvertBelowThousand((int)thousand)} Thousand");
            if (remainder > 0) parts.Add(ConvertBelowThousand((int)remainder));

            return string.Join(" ", parts);
        }

        private static string ConvertBelowThousand(int number)
        {
            if (number == 0) return "";
            if (number < 20) return Ones[number];
            if (number < 100)
                return Tens[number / 10] + (number % 10 != 0 ? " " + Ones[number % 10] : "");

            int hundreds = number / 100;
            int rest = number % 100;
            return Ones[hundreds] + " Hundred" + (rest != 0 ? " " + ConvertBelowThousand(rest) : "");
        }
    }
}
