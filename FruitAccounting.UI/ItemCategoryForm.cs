using System;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class ItemCategoryForm : BaseCrudForm<ItemCategory>
    {
        private readonly ItemCategoryService _itemCategoryService;
        private readonly long _companyId;

        public ItemCategoryForm(ItemCategoryService itemCategoryService, long companyId)
        {
            InitializeComponent();
            _itemCategoryService = itemCategoryService;
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

        private async void ItemCategoryForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        protected override async Task LoadDataAsync()
        {
            try
            {
                _dataList = await _itemCategoryService.GetAllItemCategoriesAsync(_companyId);
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
                MessageBox.Show($"Error loading Item Categories: {ex.Message}", "Error");
            }
        }

        protected override void DisplayCurrentRecord()
        {
            if (_currentIndex >= 0 && _currentIndex < _dataList.Count)
            {
                var category = _dataList[_currentIndex];
                txtName.Text = category.Name ?? "";
                UpdateNavigationButtons();
            }
        }

        protected override void ClearForm()
        {
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
            if (_isAddMode)
            {
                var (success, message) = await _itemCategoryService.CreateItemCategoryAsync(_companyId, txtName.Text.Trim());
                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
            else
            {
                var category = _dataList[_currentIndex];
                var (success, message) = await _itemCategoryService.UpdateItemCategoryAsync(category.ItemCategoryId, txtName.Text.Trim());
                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
        }

        protected override async Task<bool> DeleteRecordAsync()
        {
            var category = _dataList[_currentIndex];
            var (success, message) = await _itemCategoryService.DeleteItemCategoryAsync(category.ItemCategoryId);
            MessageBox.Show(message, success ? "Success" : "Error");
            return success;
        }

        protected override void ToggleEditMode(bool isEditing)
        {
            base.ToggleEditMode(isEditing);
            txtName.ReadOnly = !isEditing;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _isAddMode = true;
            OnAdd();
            txtName.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            OnUpdate();
            txtName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e) => OnSave();

        private void btnDelete_Click(object sender, EventArgs e) => OnDelete();

        private void btnPrevious_Click(object sender, EventArgs e) => OnPrevious();

        private void btnNext_Click(object sender, EventArgs e) => OnNext();

        private async void btnFind_Click(object sender, EventArgs e)
        {
            using var findForm = new FindItemCategoryForm(_itemCategoryService, _companyId);
            if (findForm.ShowDialog() == DialogResult.OK)
            {
                var selectedCategory = findForm.SelectedItemCategory;
                if (selectedCategory != null)
                {
                    _currentIndex = _dataList.FindIndex(c => c.ItemCategoryId == selectedCategory.ItemCategoryId);
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
