using System;
using System.Globalization;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;
using FruitAccounting.Data.Enums;

namespace FruitAccounting.UI
{
    public partial class PurchaseForm : BaseVoucherForm<PurchaseBill>
    {
        private readonly PurchaseService _purchaseService;
        private readonly AccountService _accountService;
        private readonly AccountGroupService _accountGroupService;
        private readonly RegionService _regionService;
        private readonly ItemService _itemService;
        private readonly LotService _lotService;
        private readonly long _companyId;
        private readonly long _financialYearId;
        private readonly long? _currentUserId;
        private readonly long? _preselectPurchaseBillId;

        private List<Account> _accounts = new();
        private List<Item> _items = new();
        private long _nextBillNo = 1;
        private int _nextLotNo = 1;
        private bool _suppressAccountSync;
        private bool _suppressGridEvents;

        public PurchaseForm(PurchaseService purchaseService, AccountService accountService,
            AccountGroupService accountGroupService, RegionService regionService,
            ItemService itemService, LotService lotService,
            long companyId, long financialYearId, long? currentUserId,
            long? preselectPurchaseBillId = null)
        {
            InitializeComponent();
            _purchaseService = purchaseService;
            _accountService = accountService;
            _accountGroupService = accountGroupService;
            _regionService = regionService;
            _itemService = itemService;
            _lotService = lotService;
            _companyId = companyId;
            _financialYearId = financialYearId;
            _currentUserId = currentUserId;
            _preselectPurchaseBillId = preselectPurchaseBillId;

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

        private async void PurchaseForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        protected override async Task LoadDataAsync()
        {
            try
            {
                await RefreshAccountsAsync();
                await RefreshItemsAsync();

                _dataList = await _purchaseService.GetAllPurchaseBillsAsync(_financialYearId);
                _nextBillNo = await _purchaseService.GetNextBillNoAsync(_financialYearId);
                _nextLotNo = await _lotService.GetNextLotNoAsync(_financialYearId);

                _currentIndex = _preselectPurchaseBillId.HasValue
                    ? _dataList.FindIndex(b => b.PurchaseBillId == _preselectPurchaseBillId.Value)
                    : (_dataList.Count > 0 ? 0 : -1);

                if (_currentIndex >= 0)
                    DisplayCurrentRecord();
                else
                    ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Purchase Bills: {ex.Message}", "Error");
            }
        }

        private async Task RefreshAccountsAsync()
        {
            var all = await _accountService.GetAllAccountsAsync(_companyId);
            _accounts = all.Where(a => !a.IsBlocked).OrderBy(a => a.Name).ToList();

            cmbSupplierCode.Items.Clear();
            cmbSupplierName.Items.Clear();
            cmbAmanatParty.Items.Clear();
            cmbCrateParty.Items.Clear();
            cmbAmanatParty.Items.Add("(None)");
            cmbCrateParty.Items.Add("(None)");
            foreach (var acc in _accounts)
            {
                cmbSupplierCode.Items.Add(acc.Code);
                cmbSupplierName.Items.Add(acc.Name);
                cmbAmanatParty.Items.Add(acc.Name);
                cmbCrateParty.Items.Add(acc.Name);
            }
        }

        private async Task RefreshItemsAsync()
        {
            var all = await _itemService.GetAllItemsAsync(_companyId);
            _items = all.Where(i => i.IsActive).OrderBy(i => i.Name).ToList();
            colItem.Items.Clear();
            foreach (var item in _items)
                colItem.Items.Add(item.Name);
        }

        // cmbSupplierCode and cmbSupplierName are populated from the same _accounts list in the
        // same order, so keeping them in sync is just mirroring the selected index.
        private void cmbSupplierCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressAccountSync) return;
            _suppressAccountSync = true;
            cmbSupplierName.SelectedIndex = cmbSupplierCode.SelectedIndex;
            _suppressAccountSync = false;
            _ = RecalculateFooterAsync();
        }

        private void cmbSupplierName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressAccountSync) return;
            _suppressAccountSync = true;
            cmbSupplierCode.SelectedIndex = cmbSupplierName.SelectedIndex;
            _suppressAccountSync = false;
            _ = RecalculateFooterAsync();
        }

        private async void ModeChanged(object sender, EventArgs e)
        {
            if (!((RadioButton)sender).Checked) return;

            bool isDirect = rbDirect.Checked;
            foreach (var box in new[]
            {
                txtCommissionPct, txtFreightRate, txtLabourRate, txtVatavPct, txtMarketFeePct,
                txtCommissionAmt, txtFreightAmt, txtLabourAmt, txtVatavAmt, txtMarketFeeAmt
            })
                box.Enabled = !isDirect && btnSave!.Enabled;

            await RecalculateFooterAsync();
        }

        private void FooterField_Changed(object sender, EventArgs e) => _ = RecalculateFooterAsync();

        protected override void DisplayCurrentRecord()
        {
            if (_currentIndex < 0 || _currentIndex >= _dataList.Count)
                return;

            var p = _dataList[_currentIndex];

            txtBillNo.Text = p.BillNo.ToString();
            dtpBillDate.Value = p.BillDate.ToDateTime(TimeOnly.MinValue);
            txtChallanNo.Text = p.ChallanNo ?? "";
            txtMark.Text = p.Mark ?? "";
            txtTruckNo.Text = p.TruckNo ?? "";
            txtDeliveryPerson.Text = p.DeliveryPerson ?? "";
            txtRemarks.Text = p.Remarks ?? "";

            (p.Mode switch
            {
                PurchaseMode.WithCommission => rbCommission,
                PurchaseMode.Trading => rbTrading,
                PurchaseMode.Direct => rbDirect,
                PurchaseMode.WithoutCommission => rbWithoutCommission,
                _ => rbCommission
            }).Checked = true;

            int supplierIndex = _accounts.FindIndex(a => a.AccountId == p.SupplierId);
            cmbSupplierName.SelectedIndex = supplierIndex;

            cmbAmanatParty.SelectedIndex = p.AmanatPartyId.HasValue
                ? _accounts.FindIndex(a => a.AccountId == p.AmanatPartyId.Value) + 1 : 0;
            cmbCrateParty.SelectedIndex = p.CratePartyId.HasValue
                ? _accounts.FindIndex(a => a.AccountId == p.CratePartyId.Value) + 1 : 0;

            txtCommissionPct.Text = p.CommissionPct.ToString(CultureInfo.InvariantCulture);
            txtFreightRate.Text = (p.FreightRate ?? 0).ToString(CultureInfo.InvariantCulture);
            txtLabourRate.Text = (p.LabourRate ?? 0).ToString(CultureInfo.InvariantCulture);
            txtVatavPct.Text = (p.VatavPct ?? 0).ToString(CultureInfo.InvariantCulture);
            txtMarketFeePct.Text = (p.MarketFeePct ?? 0).ToString(CultureInfo.InvariantCulture);

            // Seed the amount boxes from what was actually saved, so that if the %/rate was 0
            // (a manually-typed amount was used instead) RecalculateFooterAsync below has the
            // right manual value to pick up rather than a stale "0".
            txtCommissionAmt.Text = p.CommissionAmount.ToString("N2", CultureInfo.InvariantCulture);
            txtFreightAmt.Text = (p.Freight ?? 0).ToString("N2", CultureInfo.InvariantCulture);
            txtLabourAmt.Text = (p.Labour ?? 0).ToString("N2", CultureInfo.InvariantCulture);
            txtVatavAmt.Text = (p.Vatav ?? 0).ToString("N2", CultureInfo.InvariantCulture);
            txtMarketFeeAmt.Text = (p.MarketFee ?? 0).ToString("N2", CultureInfo.InvariantCulture);

            txtPostage.Text = (p.Postage ?? 0).ToString(CultureInfo.InvariantCulture);
            txtPackingMaterial.Text = (p.PackingMaterial ?? 0).ToString(CultureInfo.InvariantCulture);
            txtColdStore.Text = (p.ColdStore ?? 0).ToString(CultureInfo.InvariantCulture);
            txtDdCharge.Text = (p.DdCharge ?? 0).ToString(CultureInfo.InvariantCulture);
            txtInam.Text = (p.Inam ?? 0).ToString(CultureInfo.InvariantCulture);
            txtOtherDeduction.Text = (p.OtherDeduction ?? 0).ToString(CultureInfo.InvariantCulture);

            _suppressGridEvents = true;
            dgvItems.Rows.Clear();
            foreach (var line in p.PurchaseBillItems)
            {
                dgvItems.Rows.Add(
                    line.Item?.Name,
                    line.Description,
                    line.Quantity.ToString(CultureInfo.InvariantCulture),
                    line.Weight.HasValue && line.Weight.Value != 0 ? line.Weight.Value.ToString(CultureInfo.InvariantCulture) : "",
                    line.GrossRate.ToString(CultureInfo.InvariantCulture),
                    line.Amount.ToString("N2", CultureInfo.InvariantCulture),
                    line.Lot?.LotNo.ToString(),
                    line.CrateInfo);
            }
            _suppressGridEvents = false;

            _ = RecalculateFooterAsync();
            UpdateNavigationButtons();
        }

        protected override void ClearForm()
        {
            txtBillNo.Text = _nextBillNo.ToString();
            dtpBillDate.Value = DateTime.Today;
            txtChallanNo.Clear();
            txtMark.Clear();
            txtTruckNo.Clear();
            txtDeliveryPerson.Clear();
            txtRemarks.Clear();
            rbCommission.Checked = true;

            cmbSupplierName.SelectedIndex = -1;
            cmbAmanatParty.SelectedIndex = 0;
            cmbCrateParty.SelectedIndex = 0;

            txtCommissionPct.Text = "0";
            txtFreightRate.Text = "0";
            txtLabourRate.Text = "0";
            txtVatavPct.Text = "0";
            txtMarketFeePct.Text = "0";
            txtCommissionAmt.Text = "0";
            txtFreightAmt.Text = "0";
            txtLabourAmt.Text = "0";
            txtVatavAmt.Text = "0";
            txtMarketFeeAmt.Text = "0";
            txtPostage.Text = "10";
            txtPackingMaterial.Text = "0";
            txtColdStore.Text = "0";
            txtDdCharge.Text = "0";
            txtInam.Text = "0";
            txtOtherDeduction.Text = "0";

            _suppressGridEvents = true;
            dgvItems.Rows.Clear();
            _suppressGridEvents = false;

            _ = RecalculateFooterAsync();

            _currentIndex = -1;
            UpdateNavigationButtons();
        }

        private void dgvItems_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            // Lot No. is deliberately NOT assigned here - assigning it the instant a blank new row
            // appears (before the user has even touched Item/Qty/Rate) was too eager. It's assigned
            // instead in dgvItems_CellEnter, the moment focus actually reaches the Lot No. cell.
        }

        private void dgvItems_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != colLotNo.Index || e.RowIndex < 0)
                return;

            var cell = dgvItems.Rows[e.RowIndex].Cells[colLotNo.Index];
            if (cell.Value != null && !string.IsNullOrEmpty(cell.Value.ToString()))
                return; // already assigned (an existing/loaded row) - don't reassign

            // Lot No. is fully system-assigned, never typed - each row gets the next free number
            // and the counter advances so a second row in the same bill doesn't collide with the
            // first. Reusing an existing Lot No. by hand was the root cause of a real bug (Sales
            // couldn't tell which of two bills' item a shared Lot No. actually belonged to).
            cell.Value = _nextLotNo.ToString();
            _nextLotNo++;
        }

        private void dgvItems_KeyDown(object sender, KeyEventArgs e)
        {
            // Read-only columns (Lot No., Amount) should stay read-only even when a cell is merely
            // selected (not being edited) - DataGridView otherwise lets Delete/Backspace clear a
            // selected read-only cell's displayed value.
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

        private void dgvItems_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvItems.IsCurrentCellDirty)
                dgvItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_suppressGridEvents || e.RowIndex < 0)
                return;

            if (e.ColumnIndex == colQty.Index || e.ColumnIndex == colWeight.Index || e.ColumnIndex == colRate.Index)
                _ = RecalculateFooterAsync();
        }

        private void dgvItems_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e) => _ = RecalculateFooterAsync();

        // W/O Commission has no separate commission-income leg - the % is instead a straight
        // reduction on what's paid for the goods, so it's baked into each line's Amount (and
        // therefore into Gross Amount) directly rather than deducted once at the footer.
        private void RecalculateItemAmounts()
        {
            bool isWithoutCommission = rbWithoutCommission.Checked;
            decimal.TryParse(txtCommissionPct.Text, out var commissionPct);

            _suppressGridEvents = true;
            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                if (row.IsNewRow) continue;

                decimal.TryParse(row.Cells[colQty.Index].Value?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var qty);
                decimal.TryParse(row.Cells[colWeight.Index].Value?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var weight);
                decimal.TryParse(row.Cells[colRate.Index].Value?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var rate);

                // If weight is present, amount is weight x rate (qty is still required alongside it);
                // otherwise amount is qty x rate.
                var raw = (weight != 0 ? weight : qty) * rate;
                var amount = isWithoutCommission ? raw - Math.Round(raw * commissionPct / 100, 2) : raw;
                row.Cells[colAmount.Index].Value = amount == 0 ? "" : amount.ToString("N2", CultureInfo.InvariantCulture);
            }
            _suppressGridEvents = false;
        }

        private async Task RecalculateFooterAsync()
        {
            RecalculateItemAmounts();

            decimal gross = 0, totalQty = 0, totalWeight = 0;
            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                decimal.TryParse(row.Cells[colAmount.Index].Value?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var amt);
                decimal.TryParse(row.Cells[colQty.Index].Value?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var qty);
                decimal.TryParse(row.Cells[colWeight.Index].Value?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var wt);
                gross += amt;
                totalQty += qty;
                totalWeight += wt;
            }

            txtTotalQty.Text = totalQty.ToString("N2", CultureInfo.InvariantCulture);
            txtTotalWeight.Text = totalWeight.ToString("N2", CultureInfo.InvariantCulture);

            bool isDirect = rbDirect.Checked;
            bool isWithoutCommission = rbWithoutCommission.Checked;

            decimal commissionAmt = 0, freightAmt = 0, labourAmt = 0, vatavAmt = 0, marketFeeAmt = 0;
            if (!isDirect)
            {
                decimal.TryParse(txtCommissionPct.Text, out var commissionPct);
                decimal.TryParse(txtFreightRate.Text, out var freightRate);
                decimal.TryParse(txtLabourRate.Text, out var labourRate);
                decimal.TryParse(txtVatavPct.Text, out var vatavPct);
                decimal.TryParse(txtMarketFeePct.Text, out var marketFeePct);

                // W/O Commission already had commission deducted per item line by
                // RecalculateItemAmounts (baked into gross above) - don't deduct it again here.
                // For every other pair: if no %/rate is entered, take whatever the user typed
                // directly into the amount box instead of auto-computing it as zero.
                if (isWithoutCommission)
                    commissionAmt = 0;
                else if (commissionPct > 0)
                    commissionAmt = Math.Round(gross * commissionPct / 100, 2);
                else
                    commissionAmt = ParseManualAmount(txtCommissionAmt);

                freightAmt = freightRate > 0 ? Math.Round(totalQty * freightRate, 2) : ParseManualAmount(txtFreightAmt);
                labourAmt = labourRate > 0 ? Math.Round(totalQty * labourRate, 2) : ParseManualAmount(txtLabourAmt);
                vatavAmt = vatavPct > 0 ? Math.Round(gross * vatavPct / 100, 2) : ParseManualAmount(txtVatavAmt);
                marketFeeAmt = marketFeePct > 0 ? Math.Round(gross * marketFeePct / 100, 2) : ParseManualAmount(txtMarketFeeAmt);
            }

            // W/O Commission's deduction is already folded into each item's Amount (and so into
            // Gross above) - the Commission box itself stays blank since there's no separate
            // commission-income leg in this mode, just a lower effective purchase price.
            // Otherwise, only overwrite the amount box when it was auto-computed from a %/rate -
            // when the user is manually typing a value in (no %/rate entered), leave their input
            // alone rather than reformatting it out from under them on every keystroke.
            if (isWithoutCommission)
                txtCommissionAmt.Text = "";
            else if (decimal.TryParse(txtCommissionPct.Text, out var cp) && cp > 0)
                txtCommissionAmt.Text = commissionAmt.ToString("N2", CultureInfo.InvariantCulture);

            // Gross Total is always just the summed item Amounts - for W/O Commission those
            // Amounts are already net of commission, so Gross itself becomes "whatever total
            // the items add up to" per-item, not a separate footer-level subtraction.
            txtGrossTotal.Text = gross.ToString("N2", CultureInfo.InvariantCulture);

            if (decimal.TryParse(txtFreightRate.Text, out var fr) && fr > 0)
                txtFreightAmt.Text = freightAmt.ToString("N2", CultureInfo.InvariantCulture);
            if (decimal.TryParse(txtLabourRate.Text, out var lr) && lr > 0)
                txtLabourAmt.Text = labourAmt.ToString("N2", CultureInfo.InvariantCulture);
            if (decimal.TryParse(txtVatavPct.Text, out var vp) && vp > 0)
                txtVatavAmt.Text = vatavAmt.ToString("N2", CultureInfo.InvariantCulture);
            if (decimal.TryParse(txtMarketFeePct.Text, out var mp) && mp > 0)
                txtMarketFeeAmt.Text = marketFeeAmt.ToString("N2", CultureInfo.InvariantCulture);

            decimal.TryParse(txtPostage.Text, out var postage);
            decimal.TryParse(txtPackingMaterial.Text, out var packingMaterial);
            decimal.TryParse(txtColdStore.Text, out var coldStore);
            decimal.TryParse(txtDdCharge.Text, out var ddCharge);
            decimal.TryParse(txtInam.Text, out var inam);
            decimal.TryParse(txtOtherDeduction.Text, out var otherDeduction);

            decimal tdsAmt = 0;
            if (!isDirect && cmbSupplierName.SelectedIndex >= 0 && gross > 0)
            {
                var supplierId = _accounts[cmbSupplierName.SelectedIndex].AccountId;
                var excludingId = _isAddMode || _currentIndex < 0 ? (long?)null : _dataList[_currentIndex].PurchaseBillId;
                var (rate, amt) = await _purchaseService.PreviewTdsAsync(supplierId, _financialYearId, gross, DateOnly.FromDateTime(dtpBillDate.Value), excludingId);
                txtTdsPct.Text = rate.ToString(CultureInfo.InvariantCulture);
                tdsAmt = amt;
            }
            else
            {
                txtTdsPct.Text = "0";
            }
            txtTdsAmt.Text = tdsAmt.ToString("N2", CultureInfo.InvariantCulture);

            var totalExpense = isDirect ? 0 : freightAmt + labourAmt + postage + packingMaterial + coldStore + ddCharge + inam + otherDeduction + marketFeeAmt;
            txtTotalExpense.Text = totalExpense.ToString("N2", CultureInfo.InvariantCulture);

            var netAmount = isDirect
                ? gross
                : gross - commissionAmt - marketFeeAmt - freightAmt - labourAmt - postage - packingMaterial - coldStore - vatavAmt - ddCharge - inam - otherDeduction - tdsAmt;
            txtNetAmount.Text = netAmount.ToString("N2", CultureInfo.InvariantCulture);
        }

        protected override bool ValidateInput()
        {
            if (cmbSupplierName.SelectedIndex < 0)
            {
                MessageBox.Show("Party is required", "Validation Error");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtChallanNo.Text))
            {
                MessageBox.Show("Challan No. is required", "Validation Error");
                return false;
            }

            int validLines = 0;
            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                var itemName = row.Cells[colItem.Index].Value as string;
                if (string.IsNullOrWhiteSpace(itemName))
                    continue;

                if (!decimal.TryParse(row.Cells[colQty.Index].Value?.ToString(), out var qty) || qty <= 0)
                {
                    MessageBox.Show($"'{itemName}' needs a Qty greater than zero", "Validation Error");
                    return false;
                }
                if (!decimal.TryParse(row.Cells[colRate.Index].Value?.ToString(), out var rate) || rate <= 0)
                {
                    MessageBox.Show($"'{itemName}' needs a Rate greater than zero", "Validation Error");
                    return false;
                }
                if (!int.TryParse(row.Cells[colLotNo.Index].Value?.ToString(), out var lotNo) || lotNo <= 0)
                {
                    MessageBox.Show($"'{itemName}' needs a Lot No.", "Validation Error");
                    return false;
                }
                validLines++;
            }

            if (validLines == 0)
            {
                MessageBox.Show("At least one item line is required", "Validation Error");
                return false;
            }

            return true;
        }

        protected override async Task<bool> SaveRecordAsync()
        {
            var mode = rbTrading.Checked ? PurchaseMode.Trading
                : rbDirect.Checked ? PurchaseMode.Direct
                : rbWithoutCommission.Checked ? PurchaseMode.WithoutCommission
                : PurchaseMode.WithCommission;

            var input = new PurchaseService.PurchaseBillInput
            {
                BillDate = DateOnly.FromDateTime(dtpBillDate.Value),
                SupplierId = _accounts[cmbSupplierName.SelectedIndex].AccountId,
                Mode = mode,
                ChallanNo = txtChallanNo.Text.Trim(),
                TruckNo = string.IsNullOrWhiteSpace(txtTruckNo.Text) ? null : txtTruckNo.Text.Trim(),
                Mark = string.IsNullOrWhiteSpace(txtMark.Text) ? null : txtMark.Text.Trim(),
                DeliveryPerson = string.IsNullOrWhiteSpace(txtDeliveryPerson.Text) ? null : txtDeliveryPerson.Text.Trim(),
                AmanatPartyId = cmbAmanatParty.SelectedIndex > 0 ? _accounts[cmbAmanatParty.SelectedIndex - 1].AccountId : null,
                CratePartyId = cmbCrateParty.SelectedIndex > 0 ? _accounts[cmbCrateParty.SelectedIndex - 1].AccountId : null,
                CommissionPct = ParseDecimal(txtCommissionPct.Text),
                MarketFeePct = ParseDecimal(txtMarketFeePct.Text),
                FreightRate = ParseDecimal(txtFreightRate.Text),
                LabourRate = ParseDecimal(txtLabourRate.Text),
                CommissionAmt = ParseDecimal(txtCommissionAmt.Text),
                MarketFeeAmt = ParseDecimal(txtMarketFeeAmt.Text),
                FreightAmt = ParseDecimal(txtFreightAmt.Text),
                LabourAmt = ParseDecimal(txtLabourAmt.Text),
                VatavAmt = ParseDecimal(txtVatavAmt.Text),
                Postage = ParseDecimal(txtPostage.Text),
                PackingMaterial = ParseDecimal(txtPackingMaterial.Text),
                ColdStore = ParseDecimal(txtColdStore.Text),
                VatavPct = ParseDecimal(txtVatavPct.Text),
                DdCharge = ParseDecimal(txtDdCharge.Text),
                Inam = ParseDecimal(txtInam.Text),
                OtherDeduction = ParseDecimal(txtOtherDeduction.Text),
                Remarks = string.IsNullOrWhiteSpace(txtRemarks.Text) ? null : txtRemarks.Text.Trim(),
                FinancialYearId = _financialYearId,
                CreatedBy = _currentUserId
            };

            foreach (DataGridViewRow row in dgvItems.Rows)
            {
                var itemName = row.Cells[colItem.Index].Value as string;
                if (string.IsNullOrWhiteSpace(itemName))
                    continue;

                var item = _items.FirstOrDefault(i => i.Name == itemName);
                if (item == null)
                    continue;

                decimal.TryParse(row.Cells[colQty.Index].Value?.ToString(), out var qty);
                decimal.TryParse(row.Cells[colWeight.Index].Value?.ToString(), out var weight);
                decimal.TryParse(row.Cells[colRate.Index].Value?.ToString(), out var rate);
                decimal.TryParse(row.Cells[colAmount.Index].Value?.ToString(), out var amount);
                int.TryParse(row.Cells[colLotNo.Index].Value?.ToString(), out var lotNo);

                input.Items.Add(new PurchaseService.PurchaseBillItemInput
                {
                    ItemId = item.ItemId,
                    Description = row.Cells[colRemarks.Index].Value as string,
                    Quantity = qty,
                    Weight = weight == 0 ? null : weight,
                    Rate = rate,
                    Amount = amount,
                    LotNo = lotNo,
                    CrateInfo = row.Cells[colCrateInfo.Index].Value as string
                });
            }

            if (_isAddMode)
            {
                var (success, message) = await _purchaseService.CreatePurchaseBillAsync(input);
                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
            else
            {
                var bill = _dataList[_currentIndex];
                var (success, message) = await _purchaseService.UpdatePurchaseBillAsync(bill.PurchaseBillId, input);
                MessageBox.Show(message, success ? "Success" : "Error");
                return success;
            }
        }

        private static decimal ParseDecimal(string text) => decimal.TryParse(text, out var v) ? v : 0;

        private static decimal ParseManualAmount(TextBox box) =>
            decimal.TryParse(box.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out var v) ? v : 0;

        protected override async Task<bool> DeleteRecordAsync()
        {
            var bill = _dataList[_currentIndex];
            var (success, message) = await _purchaseService.DeletePurchaseBillAsync(bill.PurchaseBillId);
            MessageBox.Show(message, success ? "Success" : "Error");
            return success;
        }

        protected override void ToggleEditMode(bool isEditing)
        {
            base.ToggleEditMode(isEditing);

            Control[] controls =
            {
                dtpBillDate, rbCommission, rbTrading, rbDirect, rbWithoutCommission, txtChallanNo,
                cmbSupplierCode, cmbSupplierName, btnNewSupplier, txtMark, txtTruckNo, txtDeliveryPerson,
                cmbAmanatParty, cmbCrateParty,
                txtCommissionPct, txtFreightRate, txtLabourRate, txtVatavPct, txtMarketFeePct,
                txtCommissionAmt, txtFreightAmt, txtLabourAmt, txtVatavAmt, txtMarketFeeAmt,
                txtPostage, txtPackingMaterial, txtColdStore, txtDdCharge, txtInam, txtOtherDeduction, txtRemarks
            };

            foreach (var control in controls)
                control.Enabled = isEditing;

            dgvItems.ReadOnly = !isEditing;
            dgvItems.AllowUserToAddRows = isEditing;

            btnLotFind.Enabled = !isEditing;
            btnBill.Enabled = !isEditing;
            btnDocument.Enabled = !isEditing;
            btnSales.Enabled = !isEditing;

            if (isEditing)
            {
                bool isDirect = rbDirect.Checked;
                foreach (var box in new[]
                {
                    txtCommissionPct, txtFreightRate, txtLabourRate, txtVatavPct, txtMarketFeePct,
                    txtCommissionAmt, txtFreightAmt, txtLabourAmt, txtVatavAmt, txtMarketFeeAmt
                })
                    box.Enabled = !isDirect;
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            _nextBillNo = await _purchaseService.GetNextBillNoAsync(_financialYearId);
            _nextLotNo = await _lotService.GetNextLotNoAsync(_financialYearId);
            OnAdd();
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            // Existing rows keep their own already-saved Lot No.s (DefaultValuesNeeded only fires
            // for brand-new rows) - this just makes sure any NEW row added while editing this bill
            // starts from the true current max, not a possibly-stale value from an earlier Add.
            _nextLotNo = await _lotService.GetNextLotNoAsync(_financialYearId);
            OnUpdate();
        }

        private void btnSave_Click(object sender, EventArgs e) => OnSave();

        private void btnDelete_Click(object sender, EventArgs e) => OnDelete();

        private void btnPrevious_Click(object sender, EventArgs e) => OnPrevious();

        private void btnNext_Click(object sender, EventArgs e) => OnNext();

        private async void btnNewSupplier_Click(object sender, EventArgs e)
        {
            using var accountForm = new AccountForm(_accountService, _accountGroupService, _regionService, _companyId);
            accountForm.ShowDialog();
            await RefreshAccountsAsync();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            using var findForm = new FindPurchaseForm(_purchaseService, _financialYearId);
            if (findForm.ShowDialog() == DialogResult.OK)
            {
                var selected = findForm.SelectedPurchaseBill;
                if (selected != null)
                {
                    _currentIndex = _dataList.FindIndex(b => b.PurchaseBillId == selected.PurchaseBillId);
                    if (_currentIndex >= 0)
                    {
                        DisplayCurrentRecord();
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => OnClose();

        private void btnPrint_Click(object sender, EventArgs e) => OnPrint();

        private void btnWhatsapp_Click(object sender, EventArgs e) => OnWhatsapp();

        private void btnLotFind_Click(object sender, EventArgs e)
        {
            using var findForm = new FindLotForm(_lotService, _financialYearId);
            if (findForm.ShowDialog() == DialogResult.OK)
            {
                var selectedLot = findForm.SelectedLot;
                if (selectedLot == null)
                    return;

                var bill = _dataList.FirstOrDefault(b => b.PurchaseBillItems.Any(i => i.LotId == selectedLot.LotId));
                if (bill == null)
                {
                    MessageBox.Show($"Lot #{selectedLot.LotNo} isn't linked to any Purchase Bill in this financial year.", "Not Found");
                    return;
                }

                _currentIndex = _dataList.FindIndex(b => b.PurchaseBillId == bill.PurchaseBillId);
                if (_currentIndex >= 0)
                {
                    DisplayCurrentRecord();
                }
            }
        }

        // Bill (a distinct printed copy from the general Print button), Document (attaching/viewing
        // scanned paperwork), and Sales (jumping to what this lot was later sold for) all depend on
        // features that don't exist yet (the report engine, document storage, and the Sales module
        // respectively) - stubbed for now, same treatment as Print/WhatsApp before their real build.
        private void btnBill_Click(object sender, EventArgs e) =>
            MessageBox.Show("Bill printing is not implemented yet.", "Bill", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void btnDocument_Click(object sender, EventArgs e) =>
            MessageBox.Show("Document attachment is not implemented yet.", "Document", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void btnSales_Click(object sender, EventArgs e) =>
            MessageBox.Show("Sales cross-reference is not implemented yet - the Sales module doesn't exist.", "Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
