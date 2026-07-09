using System;
using System.Drawing;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class SubGroupForm : BaseCrudForm<AccountGroup>
    {
        private readonly AccountGroupService _accountGroupService;
        private readonly long _companyId;
        private List<AccountGroup> _mainGroups = new();

        public SubGroupForm(AccountGroupService accountGroupService, long companyId)
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

        private async void SubGroupForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        protected override async Task LoadDataAsync()
        {
            try
            {
                // The Group dropdown lists all active Main Groups
                _mainGroups = await _accountGroupService.GetAllMainGroupsAsync(_companyId);
                cmbGroup.Items.Clear();
                foreach (var mainGroup in _mainGroups)
                {
                    cmbGroup.Items.Add(mainGroup.Name);
                }

                _dataList = await _accountGroupService.GetAllSubGroupsAsync(_companyId);
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
                MessageBox.Show($"Error loading Sub Groups: {ex.Message}", "Error");
            }
        }

        protected override void DisplayCurrentRecord()
        {
            if (_currentIndex >= 0 && _currentIndex < _dataList.Count)
            {
                var subGroup = _dataList[_currentIndex];

                // Select the parent Main Group in the dropdown
                int index = _mainGroups.FindIndex(g => g.AccountGroupId == subGroup.ParentId);
                cmbGroup.SelectedIndex = index; // -1 clears selection if parent not found

                txtName.Text = subGroup.Name ?? "";
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
            if (cmbGroup.SelectedIndex < 0)
            {
                MessageBox.Show("Main Group is required", "Validation Error");
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
            long parentId = _mainGroups[cmbGroup.SelectedIndex].AccountGroupId;

            if (_isAddMode)
            {
                var (success, message) = await _accountGroupService.CreateSubGroupAsync(
                    _companyId, parentId, txtName.Text.Trim());

                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
            else
            {
                var subGroup = _dataList[_currentIndex];
                var (success, message) = await _accountGroupService.UpdateSubGroupAsync(
                    subGroup.AccountGroupId, parentId, txtName.Text.Trim());

                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
        }

        protected override async Task<bool> DeleteRecordAsync()
        {
            var subGroup = _dataList[_currentIndex];
            var (success, message) = await _accountGroupService.DeleteSubGroupAsync(subGroup.AccountGroupId);

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
            using var findForm = new FindSubGroupForm(_accountGroupService, _companyId);
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
