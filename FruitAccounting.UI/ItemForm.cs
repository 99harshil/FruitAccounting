using System;
using System.Globalization;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class ItemForm : BaseCrudForm<Item>
    {
        private readonly ItemService _itemService;
        private readonly ItemGroupService _itemGroupService;
        private readonly ItemCategoryService _itemCategoryService;
        private readonly long _companyId;

        private const string NoGroupOption = "(None)";
        private const string NoCategoryOption = "(None)";

        private List<ItemGroup> _groups = new();
        private List<ItemCategory> _categories = new();

        public ItemForm(ItemService itemService, ItemGroupService itemGroupService,
            ItemCategoryService itemCategoryService, long companyId)
        {
            InitializeComponent();
            _itemService = itemService;
            _itemGroupService = itemGroupService;
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

        private async void ItemForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        protected override async Task LoadDataAsync()
        {
            try
            {
                await RefreshGroupsAsync();
                await RefreshCategoriesAsync();

                _dataList = await _itemService.GetAllItemsAsync(_companyId);
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
                MessageBox.Show($"Error loading Items: {ex.Message}", "Error");
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

        private async Task RefreshCategoriesAsync()
        {
            _categories = await _itemCategoryService.GetAllItemCategoriesAsync(_companyId);
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add(NoCategoryOption);
            foreach (var category in _categories)
                cmbCategory.Items.Add(category.Name);
        }

        protected override void DisplayCurrentRecord()
        {
            if (_currentIndex < 0 || _currentIndex >= _dataList.Count)
                return;

            var item = _dataList[_currentIndex];

            txtCode.Text = item.Code ?? "";
            txtUnit.Text = item.Unit ?? "";
            txtName.Text = item.Name ?? "";

            int groupIndex = item.ItemGroupId.HasValue ? _groups.FindIndex(g => g.ItemGroupId == item.ItemGroupId.Value) : -1;
            cmbGroup.SelectedIndex = groupIndex >= 0 ? groupIndex + 1 : 0; // +1 for "(None)" at index 0

            int categoryIndex = item.ItemCategoryId.HasValue ? _categories.FindIndex(c => c.ItemCategoryId == item.ItemCategoryId.Value) : -1;
            cmbCategory.SelectedIndex = categoryIndex >= 0 ? categoryIndex + 1 : 0;

            SetDecimalText(txtLabour, item.LabourRate);
            SetDecimalText(txtPackingRate, item.PackingRate);
            chkUsesCrate.Checked = item.UsesCrate;

            UpdateNavigationButtons();
        }

        protected override void ClearForm()
        {
            txtCode.Clear();
            txtUnit.Text = "Box";
            txtName.Clear();
            cmbGroup.SelectedIndex = cmbGroup.Items.Count > 0 ? 0 : -1;
            cmbCategory.SelectedIndex = cmbCategory.Items.Count > 0 ? 0 : -1;
            txtLabour.Clear();
            txtPackingRate.Clear();
            chkUsesCrate.Checked = false;

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

            if (string.IsNullOrWhiteSpace(txtUnit.Text))
            {
                MessageBox.Show("Unit is required", "Validation Error");
                return false;
            }

            (string label, TextBox box)[] numericFields = { ("Labour", txtLabour), ("Crate Exp.", txtPackingRate) };
            foreach (var (label, box) in numericFields)
            {
                if (!string.IsNullOrWhiteSpace(box.Text) && !decimal.TryParse(box.Text, out _))
                {
                    MessageBox.Show($"{label} must be a valid number", "Validation Error");
                    return false;
                }
            }

            return true;
        }

        protected override async Task<bool> SaveRecordAsync()
        {
            var input = new ItemService.ItemInput
            {
                Code = string.IsNullOrWhiteSpace(txtCode.Text) ? null : txtCode.Text.Trim(),
                Name = txtName.Text.Trim(),
                ItemGroupId = cmbGroup.SelectedIndex > 0 ? _groups[cmbGroup.SelectedIndex - 1].ItemGroupId : null,
                ItemCategoryId = cmbCategory.SelectedIndex > 0 ? _categories[cmbCategory.SelectedIndex - 1].ItemCategoryId : null,
                Unit = txtUnit.Text.Trim(),
                LabourRate = ParseDecimalOrNull(txtLabour.Text),
                PackingRate = ParseDecimalOrNull(txtPackingRate.Text),
                UsesCrate = chkUsesCrate.Checked
            };

            if (_isAddMode)
            {
                var (success, message) = await _itemService.CreateItemAsync(_companyId, input);
                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
            else
            {
                var item = _dataList[_currentIndex];
                var (success, message) = await _itemService.UpdateItemAsync(item.ItemId, input);
                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
        }

        protected override async Task<bool> DeleteRecordAsync()
        {
            var item = _dataList[_currentIndex];
            var (success, message) = await _itemService.DeleteItemAsync(item.ItemId);
            MessageBox.Show(message, success ? "Success" : "Error");
            return success;
        }

        protected override void ToggleEditMode(bool isEditing)
        {
            base.ToggleEditMode(isEditing);

            Control[] controls =
            {
                txtCode, txtUnit, txtName, cmbGroup, btnNewGroup, cmbCategory, btnNewCategory,
                txtLabour, txtPackingRate, chkUsesCrate
            };

            foreach (var control in controls)
                control.Enabled = isEditing;
        }

        private static void SetDecimalText(TextBox box, decimal? value)
        {
            box.Text = value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : "";
        }

        private static decimal? ParseDecimalOrNull(string text)
        {
            return decimal.TryParse(text, out var value) ? value : null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _isAddMode = true;
            OnAdd();
            txtCode.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            OnUpdate();
            txtCode.Focus();
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

        private async void btnNewCategory_Click(object sender, EventArgs e)
        {
            using var categoryForm = new ItemCategoryForm(_itemCategoryService, _companyId);
            categoryForm.ShowDialog();
            await RefreshCategoriesAsync();
        }

        private async void btnFind_Click(object sender, EventArgs e)
        {
            using var findForm = new FindItemForm(_itemService, _companyId);
            if (findForm.ShowDialog() == DialogResult.OK)
            {
                var selectedItem = findForm.SelectedItemEntity;
                if (selectedItem != null)
                {
                    _currentIndex = _dataList.FindIndex(i => i.ItemId == selectedItem.ItemId);
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
