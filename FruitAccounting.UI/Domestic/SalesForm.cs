using System;
using System.Globalization;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class SalesForm : BaseVoucherForm<SalesService.SalesVoucher>
    {
        private readonly SalesService _salesService;
        private readonly AccountService _accountService;
        private readonly AccountGroupService _accountGroupService;
        private readonly RegionService _regionService;
        private readonly CompanyService _companyService;
        private readonly long _companyId;
        private readonly long _financialYearId;
        private readonly long? _currentUserId;

        private List<Account> _accounts = new();

        // Guards the Activated refresh - Activated fires before the first load has populated
        // _accounts, and refreshing an empty list would just clear the buyer columns.
        private bool _accountsLoaded;
        private List<SalesService.AvailablePurchaseBillInfo> _availableBills = new();
        private List<SalesService.LotSaleInfo> _availableLots = new();
        private SalesService.LotSaleInfo? _currentLot;
        private decimal _apmcPct;
        private int _nextInvNo = 1;
        private bool _suppressGridEvents;
        private bool _suppressHeaderEvents;
        private readonly long? _preselectPurchaseBillId;
        private readonly long? _preselectLotId;
        private readonly int? _preselectInvNo;

        public SalesForm(SalesService salesService, AccountService accountService, AccountGroupService accountGroupService,
            RegionService regionService, CompanyService companyService,
            long companyId, long financialYearId, long? currentUserId,
            long? preselectPurchaseBillId = null, long? preselectLotId = null, int? preselectInvNo = null)
        {
            InitializeComponent();
            _salesService = salesService;
            _accountService = accountService;
            _accountGroupService = accountGroupService;
            _regionService = regionService;
            _companyService = companyService;
            _companyId = companyId;
            _financialYearId = financialYearId;
            _currentUserId = currentUserId;
            _preselectPurchaseBillId = preselectPurchaseBillId;
            _preselectLotId = preselectLotId;
            _preselectInvNo = preselectInvNo;

            base.btnAdd = this.btnAdd;
            base.btnUpdate = this.btnUpdate;
            base.btnDelete = this.btnDelete;
            base.btnSave = this.btnSave;
            base.btnPrevious = this.btnPrevious;
            base.btnNext = this.btnNext;
            base.btnFind = this.btnFind;
            base.btnClose = this.btnClose;
            base.btnPrint = this.btnPrint;
            base.btnWhatsapp = this.btnWhatsapp;

            ToggleEditMode(false);
        }

        private async void SalesForm_Load(object sender, EventArgs e)
        {
            Activated += SalesForm_Activated;
            await LoadDataAsync();

            if (_preselectPurchaseBillId.HasValue && _preselectLotId.HasValue)
                await PreselectLotAsync(_preselectPurchaseBillId.Value, _preselectLotId.Value);
        }

        // Entered from the Stock screen's "Sell" action - skips the Pur Bill No./Lot No. dropdown
        // steps by going straight to Add mode with both already resolved. Bypasses the normal
        // SelectedIndexChanged event chain (which is async void and can't be reliably awaited)
        // and drives the same underlying state directly instead.
        private async Task PreselectLotAsync(long purchaseBillId, long lotId)
        {
            _isAddMode = true;
            await ClearFormAsync();
            ToggleEditMode(true);

            var billIndex = _availableBills.FindIndex(b => b.PurchaseBillId == purchaseBillId);
            _availableLots = await _salesService.GetAvailableLotsForPurchaseBillAsync(purchaseBillId, _financialYearId);
            var lotIndex = _availableLots.FindIndex(l => l.LotId == lotId);

            _suppressHeaderEvents = true;
            if (billIndex >= 0) cmbPurBillNo.SelectedIndex = billIndex;
            cmbLotNo.Items.Clear();
            foreach (var lot in _availableLots)
                cmbLotNo.Items.Add(lot.LotNo.ToString());
            if (lotIndex >= 0) cmbLotNo.SelectedIndex = lotIndex;
            _suppressHeaderEvents = false;

            if (lotIndex >= 0)
            {
                _currentLot = _availableLots[lotIndex];
                DisplayLotHeader();
                RecalculateFooter();
            }
            else
            {
                MessageBox.Show("This Lot no longer has any balance available to sell.", "Not Available");
            }
        }

        protected override async Task LoadDataAsync()
        {
            try
            {
                await RefreshAccountsAsync();

                var company = await _companyService.GetCompanyAsync(_companyId);
                _apmcPct = company?.ApmcPct ?? 0;

                _nextInvNo = await _salesService.GetNextInvNoAsync(_financialYearId);
                _dataList = await _salesService.GetAllSalesVouchersAsync(_financialYearId);

                _currentIndex = _preselectInvNo.HasValue
                    ? _dataList.FindIndex(v => v.InvNo == _preselectInvNo.Value)
                    : (_dataList.Count > 0 ? 0 : -1);

                if (_currentIndex >= 0)
                    await DisplayCurrentRecordAsync();
                else
                    await ClearFormAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Sales: {ex.Message}", "Error");
            }
        }

        private async Task RefreshAccountsAsync()
        {
            _accounts = await _accountService.GetPartyAccountsAsync(_companyId);

            colBuyerCode.Items.Clear();
            colBuyerName.Items.Clear();
            foreach (var acc in _accounts)
            {
                colBuyerCode.Items.Add(acc.Code);
                colBuyerName.Items.Add(acc.Name);
            }

            // A DataGridViewComboBoxCell throws if its value is not among the column's items. This
            // also runs on Activated, so a buyer that was blocked or deleted meanwhile would blow
            // up the grid on the next repaint - keep those values selectable instead.
            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.IsNewRow) continue;
                KeepExistingValue(colBuyerCode, row);
                KeepExistingValue(colBuyerName, row);
            }

            _accountsLoaded = true;
        }

        private static void KeepExistingValue(DataGridViewComboBoxColumn column, DataGridViewRow row)
        {
            if (row.Cells[column.Index].Value is string v && v.Length > 0 && !column.Items.Contains(v))
                column.Items.Add(v);
        }

        // Picks up accounts added or removed in Account Master while this form stayed open.
        private async void SalesForm_Activated(object sender, EventArgs e)
        {
            if (!_accountsLoaded) return;
            await RefreshAccountsAsync();
        }

        private async Task RefreshAvailableBillsAsync(long? excludingLotId = null)
        {
            _availableBills = await _salesService.GetAvailablePurchaseBillsAsync(_financialYearId);

            _suppressHeaderEvents = true;
            cmbPurBillNo.Items.Clear();
            foreach (var b in _availableBills)
                cmbPurBillNo.Items.Add($"{b.BillNo} ({b.BillDate:dd/MM/yyyy})");
            _suppressHeaderEvents = false;
        }

        private async void cmbPurBillNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressHeaderEvents || cmbPurBillNo.SelectedIndex < 0) return;

            var bill = _availableBills[cmbPurBillNo.SelectedIndex];
            _availableLots = await _salesService.GetAvailableLotsForPurchaseBillAsync(bill.PurchaseBillId, _financialYearId);

            // Whatever Lot/header/grid was showing belonged to the PREVIOUS bill - clear it now
            // rather than leaving stale details on screen until a Lot No. is (re-)picked.
            _currentLot = null;
            ClearLotHeader();
            _suppressGridEvents = true;
            dgvItems.Rows.Clear();
            _suppressGridEvents = false;
            RecalculateFooter();

            var previousLotIndex = cmbLotNo.SelectedIndex;
            _suppressHeaderEvents = true;
            cmbLotNo.Items.Clear();
            foreach (var lot in _availableLots)
                cmbLotNo.Items.Add(lot.LotNo.ToString());
            _suppressHeaderEvents = false;

            if (_availableLots.Count == 1)
            {
                if (previousLotIndex == 0)
                    // Items.Clear() should already reset SelectedIndex to -1, but guard anyway:
                    // setting SelectedIndex to a value it's already at doesn't raise the change
                    // event, so call the handler directly instead of relying on the assignment.
                    cmbLotNo_SelectedIndexChanged(cmbLotNo, EventArgs.Empty);
                else
                    cmbLotNo.SelectedIndex = 0;
            }
        }

        private void cmbLotNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressHeaderEvents || cmbLotNo.SelectedIndex < 0) return;

            _currentLot = _availableLots[cmbLotNo.SelectedIndex];
            DisplayLotHeader();

            _suppressGridEvents = true;
            dgvItems.Rows.Clear();
            _suppressGridEvents = false;

            RecalculateFooter();
        }

        private void DisplayLotHeader()
        {
            if (_currentLot == null) return;

            txtPurDate.Text = _currentLot.PurchaseDate.ToString("dd/MM/yyyy");
            txtParty.Text = _currentLot.SupplierName;
            txtItem.Text = _currentLot.ItemName;
            txtSplMarko.Text = _currentLot.Mark ?? "";
            txtCarate.Text = _currentLot.CrateInfo ?? "";
            txtQty.Text = _currentLot.PurchasedQty.ToString(CultureInfo.InvariantCulture);
            txtRate.Text = _currentLot.PurchaseRate.ToString(CultureInfo.InvariantCulture);
        }

        private void ClearLotHeader()
        {
            txtPurDate.Clear();
            txtParty.Clear();
            txtItem.Clear();
            txtSplMarko.Clear();
            txtCarate.Clear();
            txtQty.Clear();
            txtRate.Clear();
        }

        protected override void DisplayCurrentRecord() => _ = DisplayCurrentRecordAsync();

        private async Task DisplayCurrentRecordAsync()
        {
            if (_currentIndex < 0 || _currentIndex >= _dataList.Count)
                return;

            var voucher = _dataList[_currentIndex];

            txtInvNo.Text = voucher.InvNo.ToString();
            dtpSaleDate.Value = voucher.SaleDate.ToDateTime(TimeOnly.MinValue);
            txtVehNo.Text = voucher.VehNo ?? "";

            await RefreshAvailableBillsAsync();

            _currentLot = await _salesService.GetLotSaleInfoAsync(voucher.LotId, _financialYearId, excludingInvNo: voucher.InvNo);
            if (_currentLot != null)
                DisplayLotHeader();

            _suppressGridEvents = true;
            dgvItems.Rows.Clear();
            foreach (var line in voucher.Lines)
            {
                var buyerIndex = _accounts.FindIndex(a => a.AccountId == line.BuyerId);
                dgvItems.Rows.Add(
                    buyerIndex >= 0 ? _accounts[buyerIndex].Code : "",
                    buyerIndex >= 0 ? _accounts[buyerIndex].Name : "",
                    line.Quantity.ToString(CultureInfo.InvariantCulture),
                    line.Weight.HasValue && line.Weight.Value != 0 ? line.Weight.Value.ToString(CultureInfo.InvariantCulture) : "",
                    line.Rate.ToString(CultureInfo.InvariantCulture),
                    line.Amount.ToString("N2", CultureInfo.InvariantCulture),
                    (line.LabourRate ?? 0).ToString(CultureInfo.InvariantCulture),
                    (line.Labour ?? 0).ToString("N2", CultureInfo.InvariantCulture),
                    (line.MarketFee ?? 0).ToString("N2", CultureInfo.InvariantCulture),
                    line.Remarks);
            }
            _suppressGridEvents = false;

            RecalculateFooter();
            UpdateNavigationButtons();
        }

        protected override void ClearForm() => _ = ClearFormAsync();

        private async Task ClearFormAsync()
        {
            txtInvNo.Text = _nextInvNo.ToString();
            dtpSaleDate.Value = DateTime.Today;
            txtVehNo.Clear();

            await RefreshAvailableBillsAsync();
            cmbLotNo.Items.Clear();
            _currentLot = null;

            txtPurDate.Clear();
            txtParty.Clear();
            txtItem.Clear();
            txtSplMarko.Clear();
            txtCarate.Clear();
            txtQty.Clear();
            txtWeight.Clear();
            txtRate.Clear();

            _suppressGridEvents = true;
            dgvItems.Rows.Clear();
            _suppressGridEvents = false;

            RecalculateFooter();

            _currentIndex = -1;
            UpdateNavigationButtons();
        }

        private void dgvItems_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            // Default the next buyer's Qty to whatever's left in the Lot right now - lets the user
            // just accept it (the rest goes to this buyer) or type a smaller split and keep going.
            decimal.TryParse(txtBalQty.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var bal);
            e.Row.Cells[colQty.Index].Value = bal > 0 ? bal.ToString(CultureInfo.InvariantCulture) : "";
        }

        private void dgvItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvItems.IsCurrentCellDirty)
                dgvItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_suppressGridEvents || e.RowIndex < 0)
                return;

            var row = dgvItems.Rows[e.RowIndex];

            if (e.ColumnIndex == colBuyerCode.Index)
            {
                var code = row.Cells[colBuyerCode.Index].Value as string;
                var idx = _accounts.FindIndex(a => a.Code == code);
                if (idx >= 0)
                {
                    _suppressGridEvents = true;
                    row.Cells[colBuyerName.Index].Value = _accounts[idx].Name;
                    _suppressGridEvents = false;
                }
            }
            else if (e.ColumnIndex == colBuyerName.Index)
            {
                var name = row.Cells[colBuyerName.Index].Value as string;
                var idx = _accounts.FindIndex(a => a.Name == name);
                if (idx >= 0)
                {
                    _suppressGridEvents = true;
                    row.Cells[colBuyerCode.Index].Value = _accounts[idx].Code;
                    _suppressGridEvents = false;
                }
            }

            if (e.ColumnIndex == colQty.Index || e.ColumnIndex == colWeight.Index || e.ColumnIndex == colRate.Index || e.ColumnIndex == colLabourRate.Index)
            {
                decimal.TryParse(row.Cells[colQty.Index].Value?.ToString(), out var qty);
                decimal.TryParse(row.Cells[colWeight.Index].Value?.ToString(), out var weight);
                decimal.TryParse(row.Cells[colRate.Index].Value?.ToString(), out var rate);
                decimal.TryParse(row.Cells[colLabourRate.Index].Value?.ToString(), out var labourRate);

                // If weight is present, amount is weight x rate (qty is still required alongside it);
                // otherwise amount is qty x rate - same convention as Purchase Bill.
                var amount = (weight != 0 ? weight : qty) * rate;
                var labourAmt = labourRate * qty;
                _suppressGridEvents = true;
                row.Cells[colAmount.Index].Value = amount == 0 ? "" : amount.ToString("N2", CultureInfo.InvariantCulture);
                row.Cells[colLabourAmt.Index].Value = labourAmt == 0 ? "" : Math.Round(labourAmt, 2).ToString("N2", CultureInfo.InvariantCulture);
                // APMC is rounded to whole rupees (matches real historical data - every APMC value
                // observed in the legacy `sales` table was a whole number, never paise-precision).
                row.Cells[colApmcAmt.Index].Value = amount == 0 ? "" : Math.Round(amount * _apmcPct / 100, 0).ToString("N2", CultureInfo.InvariantCulture);
                _suppressGridEvents = false;
            }

            if (e.ColumnIndex == colQty.Index)
                RecalculateFooter(focusSaveWhenComplete: true);
        }

        private void dgvItems_KeyDown(object sender, KeyEventArgs e)
        {
            // Read-only columns (Amount, Labour Amt, APMC Amt) should stay read-only even when a
            // cell is merely selected (not being edited) - DataGridView otherwise lets Delete/
            // Backspace clear a selected read-only cell's displayed value.
            if ((e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back) &&
                dgvItems.CurrentCell != null && dgvItems.CurrentCell.ReadOnly)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            // Escape while adding lines - once there's nothing more to enter, jump straight to
            // Save instead of having to reach for the mouse.
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                btnSave.Focus();
            }
        }

        private void dgvItems_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) => RecalculateFooter();

        private void RecalculateFooter(bool focusSaveWhenComplete = false)
        {
            decimal totalQty = 0, totalWeight = 0, totalAmount = 0, rateSum = 0;
            int rateCount = 0;
            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.IsNewRow) continue;
                decimal.TryParse(row.Cells[colQty.Index].Value?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var qty);
                decimal.TryParse(row.Cells[colWeight.Index].Value?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var wt);
                decimal.TryParse(row.Cells[colAmount.Index].Value?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var amt);
                decimal.TryParse(row.Cells[colRate.Index].Value?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var rate);
                totalQty += qty;
                totalWeight += wt;
                totalAmount += amt;
                if (rate > 0)
                {
                    rateSum += rate;
                    rateCount++;
                }
            }

            txtTotalQty.Text = totalQty.ToString("N2", CultureInfo.InvariantCulture);
            txtTotalWeight.Text = totalWeight.ToString("N2", CultureInfo.InvariantCulture);
            txtTotalAmount.Text = totalAmount.ToString("N2", CultureInfo.InvariantCulture);

            // Avg Rate is the plain average of the Rate entered on each buyer line, not a
            // quantity-weighted average of Amount/Qty - and rounded off to a whole number, per the
            // user's own worked example.
            var avgRate = rateCount > 0 ? Math.Round(rateSum / rateCount, 0) : 0;
            txtAvgRate.Text = avgRate.ToString("N2", CultureInfo.InvariantCulture);

            var purchasedQty = _currentLot?.PurchasedQty ?? 0;
            var soldElsewhere = _currentLot?.SoldQty ?? 0;
            var bal = purchasedQty - soldElsewhere - totalQty;
            txtBalQty.Text = bal.ToString(CultureInfo.InvariantCulture);

            if (focusSaveWhenComplete && bal <= 0)
                btnSave.Focus();
        }

        protected override bool ValidateInput()
        {
            if (_currentLot == null)
            {
                MessageBox.Show("Select a Purchase Bill and Lot No. first", "Validation Error");
                return false;
            }

            int validLines = 0;
            decimal totalQty = 0;
            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.IsNewRow) continue;
                var code = row.Cells[colBuyerCode.Index].Value as string;
                if (string.IsNullOrWhiteSpace(code))
                    continue;

                if (!_accounts.Any(a => a.Code == code))
                {
                    MessageBox.Show($"'{code}' is not a recognized buyer account", "Validation Error");
                    return false;
                }
                if (!decimal.TryParse(row.Cells[colQty.Index].Value?.ToString(), out var qty) || qty <= 0)
                {
                    MessageBox.Show($"'{code}' needs a Qty greater than zero", "Validation Error");
                    return false;
                }
                if (!decimal.TryParse(row.Cells[colRate.Index].Value?.ToString(), out var rate) || rate <= 0)
                {
                    MessageBox.Show($"'{code}' needs a Rate greater than zero", "Validation Error");
                    return false;
                }
                totalQty += qty;
                validLines++;
            }

            if (validLines == 0)
            {
                MessageBox.Show("At least one buyer line is required", "Validation Error");
                return false;
            }

            var available = _currentLot!.PurchasedQty - _currentLot.SoldQty;
            if (totalQty > available)
            {
                MessageBox.Show($"Total Qty ({totalQty}) exceeds this Lot's remaining balance ({available})", "Validation Error");
                return false;
            }

            return true;
        }

        protected override async Task<bool> SaveRecordAsync()
        {
            var input = new SalesService.SalesVoucherInput
            {
                CompanyId = _companyId,
                FinancialYearId = _financialYearId,
                SaleDate = DateOnly.FromDateTime(dtpSaleDate.Value),
                LotId = _currentLot!.LotId,
                VehNo = string.IsNullOrWhiteSpace(txtVehNo.Text) ? null : txtVehNo.Text.Trim(),
                CreatedBy = _currentUserId
            };

            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.IsNewRow) continue;
                var code = row.Cells[colBuyerCode.Index].Value as string;
                if (string.IsNullOrWhiteSpace(code)) continue;

                var buyer = _accounts.FirstOrDefault(a => a.Code == code);
                if (buyer == null) continue;

                decimal.TryParse(row.Cells[colQty.Index].Value?.ToString(), out var qty);
                decimal.TryParse(row.Cells[colWeight.Index].Value?.ToString(), out var weight);
                decimal.TryParse(row.Cells[colRate.Index].Value?.ToString(), out var rate);
                decimal.TryParse(row.Cells[colAmount.Index].Value?.ToString(), out var amount);
                decimal.TryParse(row.Cells[colLabourRate.Index].Value?.ToString(), out var labourRate);

                input.Lines.Add(new SalesService.SaleLineInput
                {
                    BuyerId = buyer.AccountId,
                    Quantity = qty,
                    Weight = weight == 0 ? null : weight,
                    Rate = rate,
                    Amount = amount,
                    LabourRate = labourRate,
                    Remarks = row.Cells[colRemarks.Index].Value as string
                });
            }

            if (_isAddMode)
            {
                var (success, message) = await _salesService.CreateSalesVoucherAsync(input);
                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
            else
            {
                var voucher = _dataList[_currentIndex];
                var (success, message) = await _salesService.UpdateSalesVoucherAsync(_financialYearId, voucher.InvNo, input);
                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
        }

        protected override async Task<bool> DeleteRecordAsync()
        {
            var voucher = _dataList[_currentIndex];
            var (success, message) = await _salesService.DeleteSalesVoucherAsync(_financialYearId, voucher.InvNo);
            MessageBox.Show(message, success ? "Success" : "Error");
            return success;
        }

        protected override void ToggleEditMode(bool isEditing)
        {
            base.ToggleEditMode(isEditing);

            Control[] controls = { dtpSaleDate, cmbPurBillNo, cmbLotNo, txtVehNo };
            foreach (var control in controls)
                control.Enabled = isEditing;

            dgvItems.ReadOnly = !isEditing;
            dgvItems.AllowUserToAddRows = isEditing;
            btnNewBuyer.Enabled = isEditing;
            btnSearch.Enabled = isEditing;
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            _nextInvNo = await _salesService.GetNextInvNoAsync(_financialYearId);
            OnAdd();
        }

        private void btnUpdate_Click(object sender, EventArgs e) => OnUpdate();

        private void btnSave_Click(object sender, EventArgs e) => OnSave();

        private void btnDelete_Click(object sender, EventArgs e) => OnDelete();

        private void btnPrevious_Click(object sender, EventArgs e) => OnPrevious();

        private void btnNext_Click(object sender, EventArgs e) => OnNext();

        private async void btnNewBuyer_Click(object sender, EventArgs e)
        {
            using var accountForm = new AccountForm(_accountService, _accountGroupService, _regionService, _companyId);
            accountForm.ShowDialog();
            await RefreshAccountsAsync();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using var findForm = new FindAccountForm(_accountService, _companyId);
            if (findForm.ShowDialog() != DialogResult.OK || findForm.SelectedAccount == null)
                return;

            var account = findForm.SelectedAccount;
            var targetRow = dgvItems.CurrentRow != null && !dgvItems.CurrentRow.IsNewRow
                ? dgvItems.CurrentRow
                : null;

            if (targetRow == null)
            {
                var rowIndex = dgvItems.Rows.Add();
                targetRow = dgvItems.Rows[rowIndex];
            }

            targetRow.Cells[colBuyerCode.Index].Value = account.Code;
            targetRow.Cells[colBuyerName.Index].Value = account.Name;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            using var findForm = new FindSalesForm(_salesService, _financialYearId);
            if (findForm.ShowDialog() == DialogResult.OK)
            {
                var selected = findForm.SelectedVoucher;
                if (selected != null)
                {
                    _currentIndex = _dataList.FindIndex(v => v.InvNo == selected.InvNo);
                    if (_currentIndex >= 0)
                        _ = DisplayCurrentRecordAsync();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => OnClose();

        private void btnPrint_Click(object sender, EventArgs e) => OnPrint();

        private void btnWhatsapp_Click(object sender, EventArgs e) => OnWhatsapp();
    }
}
