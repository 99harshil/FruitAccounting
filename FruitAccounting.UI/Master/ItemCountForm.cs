using System;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class ItemCountForm : BaseCrudForm<ItemCount>
    {
        private readonly ItemCountService _itemCountService;
        private readonly ItemGroupService _itemGroupService;
        private readonly long _companyId;

        private const string NoGroupOption = "(None)";

        private List<ItemGroup> _groups = new();

        public ItemCountForm(ItemCountService itemCountService, ItemGroupService itemGroupService, long companyId)
        {
            InitializeComponent();
            _itemCountService = itemCountService;
            _itemGroupService = itemGroupService;
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

        private async void ItemCountForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        protected override async Task LoadDataAsync()
        {
            try
            {
                await RefreshGroupsAsync();

                _dataList = await _itemCountService.GetAllItemCountsAsync(_companyId);
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
                MessageBox.Show($"Error loading Counts: {ex.Message}", "Error");
            }
        }

        private async Task RefreshGroupsAsync()
        {
            _groups = await _itemGroupService.GetAllItemGroupsAsync(_companyId);
            cmbGroup.Items.Clear();
            cmbGroup.Items.Add(NoGroupOption);
            foreach (var group in _groups)
                cmbGroup.Items.Add(group.Name);
        }

        protected override void DisplayCurrentRecord()
        {
            if (_currentIndex < 0 || _currentIndex >= _dataList.Count)
                return;

            var count = _dataList[_currentIndex];

            int groupIndex = count.ItemGroupId.HasValue ? _groups.FindIndex(g => g.ItemGroupId == count.ItemGroupId.Value) : -1;
            cmbGroup.SelectedIndex = groupIndex >= 0 ? groupIndex + 1 : 0; // +1 for "(None)" at index 0

            txtName.Text = count.Name ?? "";
            UpdateNavigationButtons();
        }

        protected override void ClearForm()
        {
            cmbGroup.SelectedIndex = cmbGroup.Items.Count > 0 ? 0 : -1;
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

            return true;
        }

        protected override async Task<bool> SaveRecordAsync()
        {
            long? groupId = cmbGroup.SelectedIndex > 0 ? _groups[cmbGroup.SelectedIndex - 1].ItemGroupId : null;

            if (_isAddMode)
            {
                var (success, message) = await _itemCountService.CreateItemCountAsync(_companyId, groupId, txtName.Text.Trim());
                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
            else
            {
                var count = _dataList[_currentIndex];
                var (success, message) = await _itemCountService.UpdateItemCountAsync(count.ItemCountId, groupId, txtName.Text.Trim());
                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
        }

        protected override async Task<bool> DeleteRecordAsync()
        {
            var count = _dataList[_currentIndex];
            var (success, message) = await _itemCountService.DeleteItemCountAsync(count.ItemCountId);
            MessageBox.Show(message, success ? "Success" : "Error");
            return success;
        }

        protected override void ToggleEditMode(bool isEditing)
        {
            base.ToggleEditMode(isEditing);
            cmbGroup.Enabled = isEditing;
            btnNewGroup.Enabled = isEditing;
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

        private async void btnNewGroup_Click(object sender, EventArgs e)
        {
            using var groupForm = new ItemGroupForm(_itemGroupService, _companyId);
            groupForm.ShowDialog();
            await RefreshGroupsAsync();
        }

        private async void btnFind_Click(object sender, EventArgs e)
        {
            using var findForm = new FindItemCountForm(_itemCountService, _companyId);
            if (findForm.ShowDialog() == DialogResult.OK)
            {
                var selectedCount = findForm.SelectedItemCount;
                if (selectedCount != null)
                {
                    _currentIndex = _dataList.FindIndex(c => c.ItemCountId == selectedCount.ItemCountId);
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
