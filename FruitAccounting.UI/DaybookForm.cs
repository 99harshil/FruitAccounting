using System;
using System.Drawing;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class DaybookForm : BaseCrudForm<Daybook>
    {
        private readonly DaybookService _daybookService;
        private readonly AccountService _accountService;
        private readonly AccountGroupService _accountGroupService;
        private readonly RegionService _regionService;
        private readonly long _companyId;
        private List<Account> _accounts = new();

        private const string BookTypeCash = "Cash";
        private const string BookTypeBank = "Bank";
        private const string NoAccountOption = "(None)";

        public DaybookForm(DaybookService daybookService, AccountService accountService,
            AccountGroupService accountGroupService, RegionService regionService, long companyId)
        {
            InitializeComponent();
            _daybookService = daybookService;
            _accountService = accountService;
            _accountGroupService = accountGroupService;
            _regionService = regionService;
            _companyId = companyId;

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

            ToggleEditMode(false);
        }

        private async void DaybookForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        protected override async Task LoadDataAsync()
        {
            try
            {
                await RefreshAccountsAsync();

                _dataList = await _daybookService.GetAllDaybooksAsync(_companyId);
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
                MessageBox.Show($"Error loading Daybooks: {ex.Message}", "Error");
            }
        }

        private async Task RefreshAccountsAsync()
        {
            _accounts = await _daybookService.GetAccountsForLinkingAsync(_companyId);
            cmbAccount.Items.Clear();
            cmbAccount.Items.Add(NoAccountOption);
            foreach (var account in _accounts)
            {
                cmbAccount.Items.Add(account.Name);
            }
        }

        protected override void DisplayCurrentRecord()
        {
            if (_currentIndex >= 0 && _currentIndex < _dataList.Count)
            {
                var daybook = _dataList[_currentIndex];

                cmbGroup.SelectedItem = daybook.BookType == 'B' ? BookTypeBank : BookTypeCash;

                txtName.Text = daybook.Name ?? "";

                if (daybook.LinkedAccountId.HasValue)
                {
                    int index = _accounts.FindIndex(a => a.AccountId == daybook.LinkedAccountId.Value);
                    cmbAccount.SelectedIndex = index >= 0 ? index + 1 : 0; // +1 to account for "(None)" at index 0
                }
                else
                {
                    cmbAccount.SelectedIndex = 0; // "(None)"
                }

                UpdateNavigationButtons();
            }
        }

        protected override void ClearForm()
        {
            cmbGroup.SelectedIndex = -1;
            txtName.Clear();
            cmbAccount.SelectedIndex = cmbAccount.Items.Count > 0 ? 0 : -1;
            _currentIndex = -1;
            UpdateNavigationButtons();
        }

        protected override bool ValidateInput()
        {
            if (cmbGroup.SelectedIndex < 0)
            {
                MessageBox.Show("Type is required", "Validation Error");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name is required", "Validation Error");
                return false;
            }

            return true;
        }

        protected override async Task<bool> SaveRecordAsync()
        {
            char bookType = cmbGroup.SelectedItem?.ToString() == BookTypeBank ? 'B' : 'C';

            long? linkedAccountId = null;
            if (cmbAccount.SelectedIndex > 0) // index 0 is "(None)"
            {
                linkedAccountId = _accounts[cmbAccount.SelectedIndex - 1].AccountId;
            }

            if (_isAddMode)
            {
                var (success, message) = await _daybookService.CreateDaybookAsync(
                    _companyId, txtName.Text.Trim(), bookType, linkedAccountId);

                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
            else
            {
                var daybook = _dataList[_currentIndex];
                var (success, message) = await _daybookService.UpdateDaybookAsync(
                    daybook.DaybookId, txtName.Text.Trim(), bookType, linkedAccountId);

                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
        }

        protected override async Task<bool> DeleteRecordAsync()
        {
            var daybook = _dataList[_currentIndex];
            var (success, message) = await _daybookService.DeleteDaybookAsync(daybook.DaybookId);

            MessageBox.Show(message, success ? "Success" : "Error");
            return success;
        }

        protected override void ToggleEditMode(bool isEditing)
        {
            base.ToggleEditMode(isEditing);
            cmbGroup.Enabled = isEditing;
            txtName.ReadOnly = !isEditing;
            cmbAccount.Enabled = isEditing;
            btnNewAccount.Enabled = isEditing;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _isAddMode = true;
            OnAdd();
            cmbGroup.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            OnUpdate();
            cmbGroup.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e) => OnSave();

        private void btnDelete_Click(object sender, EventArgs e) => OnDelete();

        private void btnPrevious_Click(object sender, EventArgs e) => OnPrevious();

        private void btnNext_Click(object sender, EventArgs e) => OnNext();

        private async void btnFind_Click(object sender, EventArgs e)
        {
            using var findForm = new FindDaybookForm(_daybookService, _companyId);
            if (findForm.ShowDialog() == DialogResult.OK)
            {
                var selectedDaybook = findForm.SelectedDaybook;
                if (selectedDaybook != null)
                {
                    _currentIndex = _dataList.FindIndex(d => d.DaybookId == selectedDaybook.DaybookId);
                    if (_currentIndex >= 0)
                    {
                        DisplayCurrentRecord();
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => OnClose();

        private async void btnNewAccount_Click(object sender, EventArgs e)
        {
            using var accountForm = new AccountForm(_accountService, _accountGroupService, _regionService, _companyId);
            accountForm.ShowDialog();
            await RefreshAccountsAsync();
        }
    }
}
