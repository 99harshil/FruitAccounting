using System;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class CountryForm : BaseCrudForm<Country>
    {
        private readonly CountryService _countryService;
        private readonly long _companyId;

        public CountryForm(CountryService countryService, long companyId)
        {
            InitializeComponent();
            _countryService = countryService;
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

        private async void CountryForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        protected override async Task LoadDataAsync()
        {
            try
            {
                _dataList = await _countryService.GetAllCountriesAsync(_companyId);
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
                MessageBox.Show($"Error loading Countries: {ex.Message}", "Error");
            }
        }

        protected override void DisplayCurrentRecord()
        {
            if (_currentIndex >= 0 && _currentIndex < _dataList.Count)
            {
                var country = _dataList[_currentIndex];
                txtName.Text = country.Name ?? "";
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
                var (success, message) = await _countryService.CreateCountryAsync(_companyId, txtName.Text.Trim());
                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
            else
            {
                var country = _dataList[_currentIndex];
                var (success, message) = await _countryService.UpdateCountryAsync(country.CountryId, txtName.Text.Trim());
                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
        }

        protected override async Task<bool> DeleteRecordAsync()
        {
            var country = _dataList[_currentIndex];
            var (success, message) = await _countryService.DeleteCountryAsync(country.CountryId);
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
            using var findForm = new FindCountryForm(_countryService, _companyId);
            if (findForm.ShowDialog() == DialogResult.OK)
            {
                var selectedCountry = findForm.SelectedCountry;
                if (selectedCountry != null)
                {
                    _currentIndex = _dataList.FindIndex(c => c.CountryId == selectedCountry.CountryId);
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
