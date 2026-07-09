using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;
using FruitAccounting.Data.Enums;
using Region = FruitAccounting.Data.Entities.Region;

namespace FruitAccounting.UI
{
    public partial class AccountForm : BaseCrudForm<Account>
    {
        private readonly AccountService _accountService;
        private readonly AccountGroupService _accountGroupService;
        private readonly RegionService _regionService;
        private readonly long _companyId;

        private List<Region> _regions = new();
        private List<AccountGroup> _groups = new(); // Main + Sub Groups combined
        private List<Account> _amanatCandidates = new();

        private const string NoRegionOption = "(None)";

        public AccountForm(AccountService accountService, AccountGroupService accountGroupService,
            RegionService regionService, long companyId)
        {
            InitializeComponent();
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

        private async void AccountForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        protected override async Task LoadDataAsync()
        {
            try
            {
                _regions = await _regionService.GetAllRegionsAsync(_companyId);
                cmbRegion.Items.Clear();
                cmbRegion.Items.Add(NoRegionOption);
                foreach (var region in _regions)
                    cmbRegion.Items.Add($"{region.Code} - {region.Name}");

                var mainGroups = await _accountGroupService.GetAllMainGroupsAsync(_companyId);
                var subGroups = await _accountGroupService.GetAllSubGroupsAsync(_companyId);
                _groups = mainGroups.Concat(subGroups).OrderBy(g => g.Name).ToList();
                cmbBsGroup.Items.Clear();
                foreach (var group in _groups)
                    cmbBsGroup.Items.Add(group.Name);

                _dataList = await _accountService.GetAllAccountsAsync(_companyId);

                // Blocked accounts must not be selectable as another account's Amanat Party
                _amanatCandidates = _dataList.Where(a => !a.IsBlocked).ToList();
                cmbAmanatParty.Items.Clear();
                foreach (var acc in _amanatCandidates)
                    cmbAmanatParty.Items.Add($"{acc.Code} - {acc.Name}");

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
                MessageBox.Show($"Error loading Accounts: {ex.Message}", "Error");
            }
        }

        protected override void DisplayCurrentRecord()
        {
            if (_currentIndex < 0 || _currentIndex >= _dataList.Count)
                return;

            var a = _dataList[_currentIndex];

            SetComboByCode(cmbRegion, a.RegionId.HasValue ? _regions.FindIndex(r => r.RegionId == a.RegionId.Value) : -1, offsetForNone: true);
            txtCode.Text = a.Code ?? "";
            txtName.Text = a.Name ?? "";
            txtAddress1.Text = a.Address1 ?? "";
            txtAddress2.Text = a.Address2 ?? "";
            txtCity.Text = a.City ?? "";
            txtPinCode.Text = a.PinCode ?? "";
            txtCountry.Text = a.Country ?? "";
            txtContactPerson.Text = a.ContactPerson ?? "";
            txtPhone.Text = a.Phone ?? "";
            txtMobile.Text = a.Mobile ?? "";
            txtFax.Text = a.Fax ?? "";
            txtEmail.Text = a.Email ?? "";
            chkBlockParty.Checked = a.IsBlocked;

            SetDecimalText(txtCommission, a.CommissionPct);
            SetDecimalText(txtVatav, a.VatavPct);
            SetDecimalText(txtAamanat, a.AamanatPct);
            SetDecimalText(txtCrateDeposit, a.CrateDeposit);
            SetDecimalText(txtLabour, a.Labour);
            chkApmc.Checked = a.IsApmc;
            chkTdsApplicable.Checked = a.IsTdsApplicable;
            txtTdsHead.Text = a.TdsHead ?? "";

            int amanatIndex = a.AmanatPartyId.HasValue ? _amanatCandidates.FindIndex(x => x.AccountId == a.AmanatPartyId.Value) : -1;
            cmbAmanatParty.SelectedIndex = amanatIndex;
            UpdatePartyGrpDisplay();

            txtNameInBank.Text = a.NameInBank ?? "";
            SetDecimalText(txtCreditLimit, a.CreditLimit);
            txtBankName.Text = a.BankName ?? "";
            txtBranch.Text = a.BankBranch ?? "";
            txtBankAccountNo.Text = a.BankAccountNo ?? "";
            txtIfsc.Text = a.BankIfsc ?? "";

            int groupIndex = _groups.FindIndex(g => g.AccountGroupId == a.AccountGroupId);
            cmbBsGroup.SelectedIndex = groupIndex;
            txtPan.Text = a.PanNo ?? "";
            txtTin.Text = a.TinNo ?? "";
            txtCst.Text = a.CstNo ?? "";
            txtEditPin.Text = a.EditPin ?? "";

            UpdateNavigationButtons();
        }

        protected override void ClearForm()
        {
            cmbRegion.SelectedIndex = cmbRegion.Items.Count > 0 ? 0 : -1;
            txtCode.Clear();
            txtName.Clear();
            txtAddress1.Clear();
            txtAddress2.Clear();
            txtCity.Clear();
            txtPinCode.Clear();
            txtCountry.Clear();
            txtContactPerson.Clear();
            txtPhone.Clear();
            txtMobile.Clear();
            txtFax.Clear();
            txtEmail.Clear();
            chkBlockParty.Checked = false;

            txtCommission.Clear();
            txtVatav.Clear();
            txtAamanat.Clear();
            txtCrateDeposit.Clear();
            txtLabour.Clear();
            chkApmc.Checked = false;
            chkTdsApplicable.Checked = true;
            txtTdsHead.Clear();

            cmbAmanatParty.SelectedIndex = -1;
            lblPartyGrpValue.Text = "";

            txtNameInBank.Clear();
            txtCreditLimit.Clear();
            txtBankName.Clear();
            txtBranch.Clear();
            txtBankAccountNo.Clear();
            txtIfsc.Clear();

            cmbBsGroup.SelectedIndex = -1;
            txtPan.Clear();
            txtTin.Clear();
            txtCst.Clear();
            txtEditPin.Clear();

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

            if (cmbBsGroup.SelectedIndex < 0)
            {
                MessageBox.Show("B/s Group is required", "Validation Error");
                return false;
            }

            (string label, TextBox box)[] numericFields =
            {
                ("Comm.%", txtCommission), ("Vatav%", txtVatav), ("Aamanat%", txtAamanat),
                ("Crate Dep.", txtCrateDeposit), ("Labour", txtLabour), ("Cr. Limit", txtCreditLimit)
            };

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
            var input = new AccountService.AccountInput
            {
                Code = txtCode.Text.Trim(),
                Name = txtName.Text.Trim(),
                AccountGroupId = _groups[cmbBsGroup.SelectedIndex].AccountGroupId,
                RegionId = cmbRegion.SelectedIndex > 0 ? _regions[cmbRegion.SelectedIndex - 1].RegionId : null,
                Address1 = EmptyToNull(txtAddress1.Text),
                Address2 = EmptyToNull(txtAddress2.Text),
                City = EmptyToNull(txtCity.Text),
                PinCode = EmptyToNull(txtPinCode.Text),
                Country = EmptyToNull(txtCountry.Text),
                ContactPerson = EmptyToNull(txtContactPerson.Text),
                Phone = EmptyToNull(txtPhone.Text),
                Mobile = EmptyToNull(txtMobile.Text),
                Fax = EmptyToNull(txtFax.Text),
                Email = EmptyToNull(txtEmail.Text),
                CommissionPct = ParseDecimalOrNull(txtCommission.Text),
                VatavPct = ParseDecimalOrNull(txtVatav.Text),
                AamanatPct = ParseDecimalOrNull(txtAamanat.Text),
                CrateDeposit = ParseDecimalOrNull(txtCrateDeposit.Text),
                Labour = ParseDecimalOrNull(txtLabour.Text),
                IsApmc = chkApmc.Checked,
                IsTdsApplicable = chkTdsApplicable.Checked,
                TdsHead = EmptyToNull(txtTdsHead.Text),
                AmanatPartyId = cmbAmanatParty.SelectedIndex >= 0 ? _amanatCandidates[cmbAmanatParty.SelectedIndex].AccountId : null,
                NameInBank = EmptyToNull(txtNameInBank.Text),
                CreditLimit = ParseDecimalOrNull(txtCreditLimit.Text),
                BankName = EmptyToNull(txtBankName.Text),
                BankBranch = EmptyToNull(txtBranch.Text),
                BankAccountNo = EmptyToNull(txtBankAccountNo.Text),
                BankIfsc = EmptyToNull(txtIfsc.Text),
                PanNo = EmptyToNull(txtPan.Text),
                TinNo = EmptyToNull(txtTin.Text),
                CstNo = EmptyToNull(txtCst.Text),
                EditPin = EmptyToNull(txtEditPin.Text),
                IsBlocked = chkBlockParty.Checked
            };

            if (_isAddMode)
            {
                var (success, message) = await _accountService.CreateAccountAsync(_companyId, input);
                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
            else
            {
                var account = _dataList[_currentIndex];
                var (success, message) = await _accountService.UpdateAccountAsync(account.AccountId, input);
                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
        }

        protected override async Task<bool> DeleteRecordAsync()
        {
            var account = _dataList[_currentIndex];
            var (success, message) = await _accountService.DeleteAccountAsync(account.AccountId);
            MessageBox.Show(message, success ? "Success" : "Error");
            return success;
        }

        protected override void ToggleEditMode(bool isEditing)
        {
            base.ToggleEditMode(isEditing);

            Control[] controls =
            {
                cmbRegion, btnNewRegion, txtName, txtAddress1, txtAddress2, txtCity, txtPinCode,
                txtCountry, txtContactPerson, txtPhone, txtMobile, txtFax, txtEmail, chkBlockParty,
                txtCommission, txtVatav, txtAamanat, txtCrateDeposit, txtLabour, chkApmc, chkTdsApplicable, txtTdsHead,
                cmbAmanatParty, txtNameInBank, txtCreditLimit, txtBankName, txtBranch, txtBankAccountNo, txtIfsc,
                cmbBsGroup, btnNewGroup, txtPan, txtTin, txtEditPin, txtCst
            };

            foreach (var control in controls)
                control.Enabled = isEditing;

            // Code is only editable while adding a new account - it's the identity key
            // once records/history exist against it, so Update must not be able to change it
            txtCode.Enabled = isEditing && _isAddMode;
        }

        private void UpdatePartyGrpDisplay()
        {
            if (cmbAmanatParty.SelectedIndex >= 0)
            {
                var selected = _amanatCandidates[cmbAmanatParty.SelectedIndex];
                lblPartyGrpValue.Text = $"{selected.Code} - {selected.Name}";
            }
            else
            {
                lblPartyGrpValue.Text = "";
            }
        }

        private static void SetDecimalText(TextBox box, decimal? value)
        {
            box.Text = value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : "";
        }

        private static decimal? ParseDecimalOrNull(string text)
        {
            return decimal.TryParse(text, out var value) ? value : null;
        }

        private static string? EmptyToNull(string text)
        {
            return string.IsNullOrWhiteSpace(text) ? null : text.Trim();
        }

        private static void SetComboByCode(ComboBox combo, int rawIndex, bool offsetForNone)
        {
            if (offsetForNone)
                combo.SelectedIndex = rawIndex >= 0 ? rawIndex + 1 : 0;
            else
                combo.SelectedIndex = rawIndex;
        }

        private void cmbAmanatParty_SelectedIndexChanged(object sender, EventArgs e) => UpdatePartyGrpDisplay();

        private async void btnNewRegion_Click(object sender, EventArgs e)
        {
            using var regionForm = new RegionForm(_regionService, _companyId);
            regionForm.ShowDialog();

            // Refresh the dropdown so the newly added Region shows up immediately
            _regions = await _regionService.GetAllRegionsAsync(_companyId);
            cmbRegion.Items.Clear();
            cmbRegion.Items.Add(NoRegionOption);
            foreach (var region in _regions)
                cmbRegion.Items.Add($"{region.Code} - {region.Name}");
        }

        private async void btnNewGroup_Click(object sender, EventArgs e)
        {
            var menu = new ContextMenuStrip();
            menu.Items.Add("New Main Group", null, async (s, ev) =>
            {
                using var mainGroupForm = new MainGroupForm(_accountGroupService, _companyId);
                mainGroupForm.ShowDialog();
                await RefreshGroupsAsync();
            });
            menu.Items.Add("New Sub Group", null, async (s, ev) =>
            {
                using var subGroupForm = new SubGroupForm(_accountGroupService, _companyId);
                subGroupForm.ShowDialog();
                await RefreshGroupsAsync();
            });
            menu.Show(btnNewGroup, new Point(0, btnNewGroup.Height));
        }

        private async Task RefreshGroupsAsync()
        {
            var mainGroups = await _accountGroupService.GetAllMainGroupsAsync(_companyId);
            var subGroups = await _accountGroupService.GetAllSubGroupsAsync(_companyId);
            _groups = mainGroups.Concat(subGroups).OrderBy(g => g.Name).ToList();
            cmbBsGroup.Items.Clear();
            foreach (var group in _groups)
                cmbBsGroup.Items.Add(group.Name);
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
            using var findForm = new FindAccountForm(_accountService, _companyId);
            if (findForm.ShowDialog() == DialogResult.OK)
            {
                var selectedAccount = findForm.SelectedAccount;
                if (selectedAccount != null)
                {
                    _currentIndex = _dataList.FindIndex(a => a.AccountId == selectedAccount.AccountId);
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
