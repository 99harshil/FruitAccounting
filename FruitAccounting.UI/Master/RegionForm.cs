using System;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using Region = FruitAccounting.Data.Entities.Region;

namespace FruitAccounting.UI
{
    public partial class RegionForm : BaseCrudForm<Region>
    {
        private readonly RegionService _regionService;
        private readonly long _companyId;

        public RegionForm(RegionService regionService, long companyId)
        {
            InitializeComponent();
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

        private async void RegionForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        protected override async Task LoadDataAsync()
        {
            try
            {
                _dataList = await _regionService.GetAllRegionsAsync(_companyId);
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
                MessageBox.Show($"Error loading Regions: {ex.Message}", "Error");
            }
        }

        protected override void DisplayCurrentRecord()
        {
            if (_currentIndex >= 0 && _currentIndex < _dataList.Count)
            {
                var region = _dataList[_currentIndex];
                txtCode.Text = region.Code ?? "";
                txtName.Text = region.Name ?? "";
                UpdateNavigationButtons();
            }
        }

        protected override void ClearForm()
        {
            txtCode.Clear();
            txtName.Clear();
            _currentIndex = -1;
            UpdateNavigationButtons();
        }

        protected override bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text))
            {
                MessageBox.Show("Code is required", "Validation Error");
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
            if (_isAddMode)
            {
                var (success, message) = await _regionService.CreateRegionAsync(
                    _companyId, txtCode.Text.Trim(), txtName.Text.Trim());

                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
            else
            {
                var region = _dataList[_currentIndex];
                var (success, message) = await _regionService.UpdateRegionAsync(
                    region.RegionId, txtCode.Text.Trim(), txtName.Text.Trim());

                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
        }

        protected override async Task<bool> DeleteRecordAsync()
        {
            var region = _dataList[_currentIndex];
            var (success, message) = await _regionService.DeleteRegionAsync(region.RegionId);

            MessageBox.Show(message, success ? "Success" : "Error");
            return success;
        }

        protected override void ToggleEditMode(bool isEditing)
        {
            base.ToggleEditMode(isEditing);
            txtCode.ReadOnly = !isEditing;
            txtName.ReadOnly = !isEditing;
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

        private async void btnFind_Click(object sender, EventArgs e)
        {
            using var findForm = new FindRegionForm(_regionService, _companyId);
            if (findForm.ShowDialog() == DialogResult.OK)
            {
                var selectedRegion = findForm.SelectedRegion;
                if (selectedRegion != null)
                {
                    _currentIndex = _dataList.FindIndex(r => r.RegionId == selectedRegion.RegionId);
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
