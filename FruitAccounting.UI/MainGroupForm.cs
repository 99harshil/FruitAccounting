using System;
using System.Drawing;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class MainGroupForm : BaseCrudForm<AccountGroup>
    {
        private readonly AccountGroupService _accountGroupService;
        private long _companyId;

        public MainGroupForm(AccountGroupService accountGroupService, long companyId)
        {
            InitializeComponent();
            _accountGroupService = accountGroupService;
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

        private async void MainGroupForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        protected override async Task LoadDataAsync()
        {
            try
            {
                _dataList = await _accountGroupService.GetAllMainGroupsAsync(_companyId);
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
                MessageBox.Show($"Error loading Main Groups: {ex.Message}", "Error");
            }
        }

        protected override void DisplayCurrentRecord()
        {
            if (_currentIndex >= 0 && _currentIndex < _dataList.Count)
            {
                var group = _dataList[_currentIndex];

                // Set combo box selection by finding the matching item
                int index = cmbGroup.Items.IndexOf(group.Nature);
                if (index >= 0)
                {
                    cmbGroup.SelectedIndex = index;
                }
                else
                {
                    cmbGroup.SelectedIndex = -1;
                }

                txtName.Text = group.Name ?? "";
                UpdateNavigationButtons();
            }
        }

        protected override void ClearForm()
        {
            cmbGroup.SelectedIndex = -1;
            txtName.Clear();
            _currentIndex = -1;
            UpdateNavigationButtons();
        }

        protected override bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name is required", "Validation Error");
                return false;
            }

            if (cmbGroup.SelectedIndex < 0)
            {
                MessageBox.Show("Group (Nature) is required", "Validation Error");
                return false;
            }

            return true;
        }

        protected override async Task<bool> SaveRecordAsync()
        {
            string nature = cmbGroup.SelectedItem?.ToString() ?? "";

            if (_isAddMode)
            {
                var (success, message) = await _accountGroupService.CreateMainGroupAsync(
                    _companyId, "", txtName.Text, nature);

                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
            else
            {
                var group = _dataList[_currentIndex];
                var (success, message) = await _accountGroupService.UpdateMainGroupAsync(
                    group.AccountGroupId, "", txtName.Text, nature);

                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
        }

        protected override async Task<bool> DeleteRecordAsync()
        {
            var group = _dataList[_currentIndex];
            var (success, message) = await _accountGroupService.DeleteMainGroupAsync(group.AccountGroupId);

            MessageBox.Show(message, success ? "Success" : "Error");
            return success;
        }

        protected override void ToggleEditMode(bool isEditing)
        {
            base.ToggleEditMode(isEditing);
            cmbGroup.Enabled = isEditing;
            txtName.ReadOnly = !isEditing;
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
            using var findForm = new FindMainGroupForm(_accountGroupService, _companyId);
            if (findForm.ShowDialog() == DialogResult.OK)
            {
                var selectedGroup = findForm.SelectedGroup;
                if (selectedGroup != null)
                {
                    _currentIndex = _dataList.FindIndex(g => g.AccountGroupId == selectedGroup.AccountGroupId);
                    if (_currentIndex >= 0)
                    {
                        DisplayCurrentRecord();
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => OnClose();
    }
}