# CRUD Implementation Guide

This guide shows how to create consistent CRUD forms using the base classes `BaseCrudForm<T>` and `BaseSearchForm<T>`.

## Overview

We have created two master base classes to handle common CRUD operations:

- **BaseCrudForm<T>**: Main form for Add, Update, Delete, Save, Navigate operations
- **BaseSearchForm<T>**: Search/Find dialog with regex support across all columns

## Benefits

✅ **Code Reuse**: Eliminate duplicate code across features  
✅ **Consistency**: All forms behave the same way  
✅ **Maintainability**: Bug fixes in base class benefit all features  
✅ **Extensibility**: Easy to add new features with consistent patterns  

## Quick Start: Creating a New CRUD Form

### 1. Create Service (if not exists)

```csharp
public class SubGroupService
{
    private readonly IDbContextFactory<FruitAccountingContext> _contextFactory;

    public async Task<List<SubGroup>> GetAllSubGroupsAsync(long mainGroupId)
    {
        using var context = _contextFactory.CreateDbContext();
        return await context.SubGroups
            .AsNoTracking()
            .Where(g => g.ParentId == mainGroupId)
            .OrderBy(g => g.Name)
            .ToListAsync();
    }

    public async Task<(bool success, string message)> CreateSubGroupAsync(
        long mainGroupId, string code, string name, string nature)
    {
        // Implementation...
    }

    public async Task<(bool success, string message)> UpdateSubGroupAsync(
        long subGroupId, string code, string name, string nature)
    {
        // Implementation...
    }

    public async Task<(bool success, string message)> DeleteSubGroupAsync(long subGroupId)
    {
        // Implementation...
    }
}
```

### 2. Create Main CRUD Form by Inheriting BaseCrudForm<T>

```csharp
public partial class SubGroupForm : BaseCrudForm<SubGroup>
{
    private readonly SubGroupService _service;
    private long _mainGroupId;

    public SubGroupForm(SubGroupService service, long mainGroupId)
    {
        InitializeComponent();
        _service = service;
        _mainGroupId = mainGroupId;

        // Wire up base class button references
        btnAdd = this.btnAdd;
        btnUpdate = this.btnUpdate;
        btnDelete = this.btnDelete;
        btnSave = this.btnSave;
        btnPrevious = this.btnPrevious;
        btnNext = this.btnNext;
        btnFind = this.btnFind;
        btnClose = this.btnClose;

        ToggleEditMode(false);
    }

    // Implement abstract methods:

    protected override async Task LoadDataAsync()
    {
        try
        {
            _dataList = await _service.GetAllSubGroupsAsync(_mainGroupId);
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
            MessageBox.Show($"Error: {ex.Message}", "Error");
        }
    }

    protected override void DisplayCurrentRecord()
    {
        if (_currentIndex >= 0 && _currentIndex < _dataList.Count)
        {
            var item = _dataList[_currentIndex];
            cmbNature.Text = item.Nature;
            txtName.Text = item.Name;
            UpdateNavigationButtons();
        }
    }

    protected override void ClearForm()
    {
        cmbNature.SelectedIndex = -1;
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
            var (success, message) = await _service.CreateSubGroupAsync(
                _mainGroupId, "", txtName.Text, cmbNature.SelectedItem?.ToString() ?? "");
            MessageBox.Show(message, success ? "Success" : "Error");
            return success;
        }
        else
        {
            var item = _dataList[_currentIndex];
            var (success, message) = await _service.UpdateSubGroupAsync(
                item.Id, "", txtName.Text, cmbNature.SelectedItem?.ToString() ?? "");
            MessageBox.Show(message, success ? "Success" : "Error");
            return success;
        }
    }

    protected override async Task<bool> DeleteRecordAsync()
    {
        var item = _dataList[_currentIndex];
        var (success, message) = await _service.DeleteSubGroupAsync(item.Id);
        MessageBox.Show(message, success ? "Success" : "Error");
        return success;
    }

    // Wire up button click handlers:

    private void btnAdd_Click(object sender, EventArgs e)
    {
        _isAddMode = true;
        OnAdd();
    }

    private void btnUpdate_Click(object sender, EventArgs e) => OnUpdate();
    private void btnSave_Click(object sender, EventArgs e) => OnSave();
    private void btnDelete_Click(object sender, EventArgs e) => OnDelete();
    private void btnPrevious_Click(object sender, EventArgs e) => OnPrevious();
    private void btnNext_Click(object sender, EventArgs e) => OnNext();
    private void btnClose_Click(object sender, EventArgs e) => OnClose();

    private async void btnFind_Click(object sender, EventArgs e)
    {
        using var findForm = new FindSubGroupForm(_service, _mainGroupId);
        if (findForm.ShowDialog() == DialogResult.OK && findForm.SelectedSubGroup != null)
        {
            var selectedItem = findForm.SelectedSubGroup;
            _currentIndex = _dataList.FindIndex(g => g.Id == selectedItem.Id);
            if (_currentIndex >= 0)
                DisplayCurrentRecord();
        }
    }
}
```

### 3. Create Search Form by Inheriting BaseSearchForm<T>

```csharp
public partial class FindSubGroupForm : BaseSearchForm<SubGroup>
{
    private readonly SubGroupService _service;
    private long _mainGroupId;

    public FindSubGroupForm(SubGroupService service, long mainGroupId)
    {
        InitializeComponent();
        _service = service;
        _mainGroupId = mainGroupId;

        // Wire up base class control references
        txtSearch = this.txtSearch;
        chkMatchCase = this.chkMatchCase;
        dgvData = this.dgvGroups;
        btnSelect = this.btnSelect;
        btnCancel = this.btnCancel;
        btnFind = this.btnFind;
    }

    public SubGroup? SelectedSubGroup => SelectedItem;

    private async void FindSubGroupForm_Load(object sender, EventArgs e)
    {
        await LoadAllDataAsync();
    }

    protected override async Task LoadAllDataAsync()
    {
        try
        {
            _allData = await _service.GetAllSubGroupsAsync(_mainGroupId);
            DisplayData(_allData);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}", "Error");
        }
    }

    protected override void DisplayData(List<SubGroup> data)
    {
        dgvGroups.Rows.Clear();
        foreach (var item in data)
        {
            dgvGroups.Rows.Add(item.Name, item.Nature, item.Id);
        }
        dgvGroups.Columns[2].Visible = false; // Hide ID column
    }

    protected override List<SubGroup> ApplySearchFilter(string searchPattern, bool matchCase)
    {
        RegexOptions options = matchCase ? RegexOptions.None : RegexOptions.IgnoreCase;
        var regex = new Regex(searchPattern, options);

        return _allData.Where(item =>
            regex.IsMatch(item.Name ?? "") ||
            regex.IsMatch(item.Nature ?? "")
        ).ToList();
    }

    protected override SubGroup? GetSelectedItemFromGrid()
    {
        if (dgvGroups.SelectedRows.Count <= 0)
            return null;

        var row = dgvGroups.SelectedRows[0];
        var id = Convert.ToInt64(row.Cells[2].Value);
        return _allData.FirstOrDefault(g => g.Id == id);
    }

    // Wire up event handlers:
    private void txtSearch_TextChanged(object sender, EventArgs e) => PerformSearch();
    private void btnFind_Click(object sender, EventArgs e) => PerformSearch();
    private void btnSelect_Click(object sender, EventArgs e) => ConfirmSelection();
    private void btnCancel_Click(object sender, EventArgs e) => Close();
    private void dgvGroups_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0) ConfirmSelection();
    }
}
```

## Key Features Provided by Base Classes

### BaseCrudForm<T>

| Feature | Method | What You Need to Do |
|---------|--------|-------------------|
| **Add** | OnAdd() | Override SaveRecordAsync() |
| **Update** | OnUpdate() | Override SaveRecordAsync() with logic for update vs add |
| **Delete** | OnDelete() | Override DeleteRecordAsync() |
| **Save** | OnSave() | Override ValidateInput() and SaveRecordAsync() |
| **Previous** | OnPrevious() | Automatic navigation |
| **Next** | OnNext() | Automatic navigation |
| **Edit Mode** | ToggleEditMode() | Override to enable/disable form controls |
| **Validation** | ValidateInput() | Implement your validation |
| **Display** | DisplayCurrentRecord() | Display loaded data in controls |
| **Clear** | ClearForm() | Clear all controls |

### BaseSearchForm<T>

| Feature | Method | What You Need to Do |
|---------|--------|-------------------|
| **Regex Search** | PerformSearch() | Automatic - searches all matching columns |
| **Case Sensitivity** | chkMatchCase | Automatic checkbox toggle |
| **Filter** | ApplySearchFilter() | Override to specify searchable columns |
| **Display Results** | DisplayData() | Populate grid with results |
| **Get Selected** | GetSelectedItemFromGrid() | Override to extract selected row data |

## Search Patterns Supported

The search box supports regex patterns:

- `^Account` - Starts with "Account"
- `Group$` - Ends with "Group"  
- `Main|Sub` - Contains either "Main" or "Sub"
- `[A-Z]{3}` - Contains 3 uppercase letters
- `.*Group.*` - Contains "Group" anywhere
- `^[^A]*$` - Doesn't contain the letter "A"

## Wiring in MainShell

Add to MainShell.cs ShowForm() method:

```csharp
case "SubGroup":
    var accountGroupService = Program.ServiceProvider?.GetService(typeof(AccountGroupService)) as AccountGroupService;
    if (accountGroupService != null)
    {
        var mainGroupId = 1; // Get from selected main group
        newForm = new SubGroupForm(accountGroupService, mainGroupId);
    }
    break;
```

## Example: Implemented MainGroupForm

See `MainGroupForm.cs` and `FindMainGroupForm.cs` for complete working examples.

## Best Practices

1. **Always validate input** in ValidateInput()
2. **Show user feedback** with MessageBox for success/error
3. **Reload data after save** using LoadDataAsync()
4. **Handle exceptions** in try-catch blocks
5. **Wire button handlers** in Designer or code-behind
6. **Test search patterns** with various regex combinations

## Common Customizations

### Custom Edit Mode Logic

```csharp
protected override void ToggleEditMode(bool isEditing)
{
    base.ToggleEditMode(isEditing);
    
    // Your custom logic
    txtCode.ReadOnly = !isEditing;
    cmbCategory.Enabled = isEditing;
}
```

### Custom Validation

```csharp
protected override bool ValidateInput()
{
    if (!base.ValidateInput()) return false;
    
    // Your custom validation
    if (txtCode.Text.Length < 3)
    {
        MessageBox.Show("Code must be at least 3 characters", "Validation");
        return false;
    }
    
    return true;
}
```

### Filter by Multiple Conditions

```csharp
protected override List<MyEntity> ApplySearchFilter(string searchPattern, bool matchCase)
{
    RegexOptions options = matchCase ? RegexOptions.None : RegexOptions.IgnoreCase;
    var regex = new Regex(searchPattern, options);

    return _allData.Where(item =>
        regex.IsMatch(item.Name ?? "") ||
        regex.IsMatch(item.Code ?? "") ||
        regex.IsMatch(item.Description ?? "")
    ).ToList();
}
```

---

**Now you can create consistent CRUD interfaces across all features quickly and efficiently!** 🚀
