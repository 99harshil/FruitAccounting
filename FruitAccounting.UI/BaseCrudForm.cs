using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FruitAccounting.UI
{
    /// <summary>
    /// Base class for CRUD forms with common navigation and state management
    /// Provides reusable functionality for Add, Update, Delete, Save, Previous, Next operations
    /// </summary>
    [DesignerCategory("")]
    public abstract class BaseCrudForm<T> : Form where T : class
    {
        protected List<T> _dataList = new();
        protected int _currentIndex = -1;
        protected bool _isAddMode = false;

        protected Button? btnAdd;
        protected Button? btnUpdate;
        protected Button? btnDelete;
        protected Button? btnSave;
        protected Button? btnPrevious;
        protected Button? btnNext;
        protected Button? btnFind;
        protected Button? btnClose;

        public BaseCrudForm()
        {
            // Constructor for derived classes
        }

        /// <summary>
        /// Override this to load data from service
        /// </summary>
        protected abstract Task LoadDataAsync();

        /// <summary>
        /// Override this to display the current record
        /// </summary>
        protected abstract void DisplayCurrentRecord();

        /// <summary>
        /// Override this to clear the form
        /// </summary>
        protected abstract void ClearForm();

        /// <summary>
        /// Override this to validate form input
        /// </summary>
        protected abstract bool ValidateInput();

        /// <summary>
        /// Override this to save record (Add or Update)
        /// </summary>
        protected abstract Task<bool> SaveRecordAsync();

        /// <summary>
        /// Override this to delete the current record
        /// </summary>
        protected abstract Task<bool> DeleteRecordAsync();

        /// <summary>
        /// Toggle between edit and view modes
        /// </summary>
        protected virtual void ToggleEditMode(bool isEditing)
        {
            if (btnSave != null)
            {
                btnSave.Enabled = isEditing;
                btnSave.BackColor = isEditing ? Color.LightSteelBlue : Color.WhiteSmoke;
            }

            if (btnAdd != null) btnAdd.Enabled = !isEditing;
            if (btnUpdate != null) btnUpdate.Enabled = !isEditing && _currentIndex >= 0;
            if (btnDelete != null) btnDelete.Enabled = !isEditing && _currentIndex >= 0;
            if (btnFind != null) btnFind.Enabled = !isEditing;
            if (btnNext != null) btnNext.Enabled = !isEditing && _currentIndex < _dataList.Count - 1;
            if (btnPrevious != null) btnPrevious.Enabled = !isEditing && _currentIndex > 0;
        }

        /// <summary>
        /// Update navigation button states
        /// </summary>
        protected virtual void UpdateNavigationButtons()
        {
            if (btnPrevious != null) btnPrevious.Enabled = _currentIndex > 0;
            if (btnNext != null) btnNext.Enabled = _currentIndex < _dataList.Count - 1;
            if (btnUpdate != null) btnUpdate.Enabled = _currentIndex >= 0;
            if (btnDelete != null) btnDelete.Enabled = _currentIndex >= 0;
        }

        /// <summary>
        /// Add button click handler
        /// </summary>
        protected virtual void OnAdd()
        {
            _isAddMode = true;
            ClearForm();
            ToggleEditMode(true);
        }

        /// <summary>
        /// Update button click handler
        /// </summary>
        protected virtual void OnUpdate()
        {
            if (_currentIndex < 0)
            {
                MessageBox.Show("Please select a record to update", "Info");
                return;
            }
            _isAddMode = false;
            ToggleEditMode(true);
        }

        /// <summary>
        /// Save button click handler
        /// </summary>
        protected async virtual void OnSave()
        {
            if (!ValidateInput())
                return;

            try
            {
                bool success = await SaveRecordAsync();
                if (success)
                {
                    ToggleEditMode(false);
                    await LoadDataAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving record: {ex.Message}", "Error");
            }
        }

        /// <summary>
        /// Delete button click handler
        /// </summary>
        protected async virtual void OnDelete()
        {
            if (_currentIndex < 0)
            {
                MessageBox.Show("Please select a record to delete", "Info");
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this record?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                bool success = await DeleteRecordAsync();
                if (success)
                {
                    await LoadDataAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting record: {ex.Message}", "Error");
            }
        }

        /// <summary>
        /// Previous button click handler
        /// </summary>
        protected virtual void OnPrevious()
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                DisplayCurrentRecord();
            }
        }

        /// <summary>
        /// Next button click handler
        /// </summary>
        protected virtual void OnNext()
        {
            if (_currentIndex < _dataList.Count - 1)
            {
                _currentIndex++;
                DisplayCurrentRecord();
            }
        }

        /// <summary>
        /// Close button click handler
        /// </summary>
        protected virtual void OnClose()
        {
            Close();
        }
    }

    /// <summary>
    /// Base class for search/find forms with regex support
    /// </summary>
    [DesignerCategory("")]
    public abstract class BaseSearchForm<T> : Form where T : class
    {
        protected List<T> _allData = new();
        public T? SelectedItem { get; protected set; }

        protected TextBox? txtSearch;
        protected CheckBox? chkMatchCase;
        protected DataGridView? dgvData;
        protected Button? btnSelect;
        protected Button? btnCancel;
        protected Button? btnFind;

        public BaseSearchForm()
        {
            // Constructor for derived classes
        }

        /// <summary>
        /// Override this to load all data
        /// </summary>
        protected abstract Task LoadAllDataAsync();

        /// <summary>
        /// Override this to display data in grid
        /// </summary>
        protected abstract void DisplayData(List<T> data);

        /// <summary>
        /// Override this to apply search filter
        /// </summary>
        protected abstract List<T> ApplySearchFilter(string searchPattern, bool matchCase);

        /// <summary>
        /// Override this to get selected item from grid
        /// </summary>
        protected abstract T? GetSelectedItemFromGrid();

        /// <summary>
        /// Perform regex-based search across all columns
        /// </summary>
        protected virtual void PerformSearch()
        {
            if (txtSearch == null || string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                DisplayData(_allData);
                return;
            }

            bool matchCase = chkMatchCase?.Checked ?? false;
            List<T> filteredData;
            try
            {
                filteredData = ApplySearchFilter(txtSearch.Text, matchCase);
            }
            catch (ArgumentException)
            {
                // Text is not a valid regex (e.g. user typed "(") - search it as literal text instead
                filteredData = ApplySearchFilter(Regex.Escape(txtSearch.Text), matchCase);
            }
            DisplayData(filteredData);
        }

        /// <summary>
        /// Confirm selection and close dialog
        /// </summary>
        protected virtual async void ConfirmSelection()
        {
            SelectedItem = GetSelectedItemFromGrid();
            if (SelectedItem != null)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Please select an item from the list.", "Selection Required");
            }
        }
    }
}
