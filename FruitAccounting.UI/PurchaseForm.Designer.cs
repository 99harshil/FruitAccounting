namespace FruitAccounting.UI
{
    partial class PurchaseForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelHeader = new Panel();
            lblBillNo = new Label();
            txtBillNo = new TextBox();
            lblBillDate = new Label();
            dtpBillDate = new DateTimePicker();
            rbCommission = new RadioButton();
            rbTrading = new RadioButton();
            rbDirect = new RadioButton();
            rbWithoutCommission = new RadioButton();
            lblChallanNo = new Label();
            txtChallanNo = new TextBox();
            lblSupplier = new Label();
            cmbSupplierCode = new ComboBox();
            cmbSupplierName = new ComboBox();
            btnNewSupplier = new Button();
            lblMark = new Label();
            txtMark = new TextBox();
            lblTruckNo = new Label();
            txtTruckNo = new TextBox();
            lblDeliveryPerson = new Label();
            txtDeliveryPerson = new TextBox();
            lblAmanatParty = new Label();
            cmbAmanatParty = new ComboBox();
            lblCrateParty = new Label();
            cmbCrateParty = new ComboBox();
            dgvItems = new DataGridView();
            colItem = new DataGridViewComboBoxColumn();
            colRemarks = new DataGridViewTextBoxColumn();
            colQty = new DataGridViewTextBoxColumn();
            colWeight = new DataGridViewTextBoxColumn();
            colRate = new DataGridViewTextBoxColumn();
            colAmount = new DataGridViewTextBoxColumn();
            colLotNo = new DataGridViewTextBoxColumn();
            colCrateInfo = new DataGridViewTextBoxColumn();
            panelFooter = new Panel();
            lblCommission = new Label();
            txtCommissionPct = new TextBox();
            txtCommissionAmt = new TextBox();
            lblFreight = new Label();
            txtFreightRate = new TextBox();
            txtFreightAmt = new TextBox();
            lblLabour = new Label();
            txtLabourRate = new TextBox();
            txtLabourAmt = new TextBox();
            lblVatav = new Label();
            txtVatavPct = new TextBox();
            txtVatavAmt = new TextBox();
            lblMarketFee = new Label();
            txtMarketFeePct = new TextBox();
            txtMarketFeeAmt = new TextBox();
            lblPostage = new Label();
            txtPostage = new TextBox();
            lblPackingMaterial = new Label();
            txtPackingMaterial = new TextBox();
            lblColdStore = new Label();
            txtColdStore = new TextBox();
            lblDdCharge = new Label();
            txtDdCharge = new TextBox();
            lblInam = new Label();
            txtInam = new TextBox();
            lblOtherDeduction = new Label();
            txtOtherDeduction = new TextBox();
            panelItemTotals = new Panel();
            lblTotalQty = new Label();
            txtTotalQty = new TextBox();
            lblTotalWeight = new Label();
            txtTotalWeight = new TextBox();
            lblGrossTotal = new Label();
            txtGrossTotal = new TextBox();
            lblTds = new Label();
            txtTdsPct = new TextBox();
            txtTdsAmt = new TextBox();
            lblTotalExpense = new Label();
            txtTotalExpense = new TextBox();
            lblNetAmount = new Label();
            txtNetAmount = new TextBox();
            lblRemarks = new Label();
            txtRemarks = new TextBox();
            panelButtons = new Panel();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnSave = new Button();
            btnPrevious = new Button();
            btnNext = new Button();
            btnFind = new Button();
            btnClose = new Button();
            btnPrint = new Button();
            btnWhatsapp = new Button();
            btnLotFind = new Button();
            btnBill = new Button();
            btnDocument = new Button();
            btnSales = new Button();
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            panelItemTotals.SuspendLayout();
            panelFooter.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();
            //
            // panelHeader
            //
            panelHeader.BorderStyle = BorderStyle.FixedSingle;
            panelHeader.Controls.Add(lblBillNo);
            panelHeader.Controls.Add(txtBillNo);
            panelHeader.Controls.Add(lblBillDate);
            panelHeader.Controls.Add(dtpBillDate);
            panelHeader.Controls.Add(rbCommission);
            panelHeader.Controls.Add(rbTrading);
            panelHeader.Controls.Add(rbDirect);
            panelHeader.Controls.Add(rbWithoutCommission);
            panelHeader.Controls.Add(lblChallanNo);
            panelHeader.Controls.Add(txtChallanNo);
            panelHeader.Controls.Add(lblSupplier);
            panelHeader.Controls.Add(cmbSupplierCode);
            panelHeader.Controls.Add(cmbSupplierName);
            panelHeader.Controls.Add(btnNewSupplier);
            panelHeader.Controls.Add(lblMark);
            panelHeader.Controls.Add(txtMark);
            panelHeader.Controls.Add(lblTruckNo);
            panelHeader.Controls.Add(txtTruckNo);
            panelHeader.Controls.Add(lblDeliveryPerson);
            panelHeader.Controls.Add(txtDeliveryPerson);
            panelHeader.Controls.Add(lblAmanatParty);
            panelHeader.Controls.Add(cmbAmanatParty);
            panelHeader.Controls.Add(lblCrateParty);
            panelHeader.Controls.Add(cmbCrateParty);
            panelHeader.Location = new Point(12, 12);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1000, 130);
            panelHeader.TabIndex = 0;
            //
            // lblBillNo
            //
            lblBillNo.AutoSize = true;
            lblBillNo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBillNo.ForeColor = Color.Red;
            lblBillNo.Location = new Point(10, 15);
            lblBillNo.Name = "lblBillNo";
            lblBillNo.Size = new Size(45, 15);
            lblBillNo.TabIndex = 0;
            lblBillNo.Text = "Sr. No.";
            //
            // txtBillNo
            //
            txtBillNo.BackColor = Color.FromArgb(224, 224, 224);
            txtBillNo.BorderStyle = BorderStyle.FixedSingle;
            txtBillNo.Location = new Point(70, 12);
            txtBillNo.Name = "txtBillNo";
            txtBillNo.ReadOnly = true;
            txtBillNo.Size = new Size(70, 23);
            txtBillNo.TabIndex = 1;
            //
            // lblBillDate
            //
            lblBillDate.AutoSize = true;
            lblBillDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBillDate.ForeColor = Color.Red;
            lblBillDate.Location = new Point(150, 15);
            lblBillDate.Name = "lblBillDate";
            lblBillDate.Size = new Size(35, 15);
            lblBillDate.TabIndex = 2;
            lblBillDate.Text = "Date";
            //
            // dtpBillDate
            //
            dtpBillDate.Format = DateTimePickerFormat.Short;
            dtpBillDate.Location = new Point(190, 12);
            dtpBillDate.Name = "dtpBillDate";
            dtpBillDate.Size = new Size(110, 23);
            dtpBillDate.TabIndex = 3;
            //
            // rbCommission
            //
            rbCommission.AutoSize = true;
            rbCommission.Location = new Point(320, 14);
            rbCommission.Name = "rbCommission";
            rbCommission.Size = new Size(90, 19);
            rbCommission.TabIndex = 4;
            rbCommission.Text = "Commission";
            rbCommission.UseVisualStyleBackColor = true;
            rbCommission.CheckedChanged += ModeChanged;
            //
            // rbTrading
            //
            rbTrading.AutoSize = true;
            rbTrading.Location = new Point(420, 14);
            rbTrading.Name = "rbTrading";
            rbTrading.Size = new Size(65, 19);
            rbTrading.TabIndex = 5;
            rbTrading.Text = "Trading";
            rbTrading.UseVisualStyleBackColor = true;
            rbTrading.CheckedChanged += ModeChanged;
            //
            // rbDirect
            //
            rbDirect.AutoSize = true;
            rbDirect.Location = new Point(495, 14);
            rbDirect.Name = "rbDirect";
            rbDirect.Size = new Size(58, 19);
            rbDirect.TabIndex = 6;
            rbDirect.Text = "Direct";
            rbDirect.UseVisualStyleBackColor = true;
            rbDirect.CheckedChanged += ModeChanged;
            //
            // rbWithoutCommission
            //
            rbWithoutCommission.AutoSize = true;
            rbWithoutCommission.Location = new Point(565, 14);
            rbWithoutCommission.Name = "rbWithoutCommission";
            rbWithoutCommission.Size = new Size(80, 19);
            rbWithoutCommission.TabIndex = 7;
            rbWithoutCommission.Text = "W/O Comm.";
            rbWithoutCommission.UseVisualStyleBackColor = true;
            rbWithoutCommission.CheckedChanged += ModeChanged;
            //
            // lblChallanNo
            //
            lblChallanNo.AutoSize = true;
            lblChallanNo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblChallanNo.ForeColor = Color.Red;
            lblChallanNo.Location = new Point(700, 15);
            lblChallanNo.Name = "lblChallanNo";
            lblChallanNo.Size = new Size(75, 15);
            lblChallanNo.TabIndex = 8;
            lblChallanNo.Text = "Challan No.";
            //
            // txtChallanNo
            //
            txtChallanNo.BackColor = Color.FromArgb(224, 224, 224);
            txtChallanNo.BorderStyle = BorderStyle.FixedSingle;
            txtChallanNo.Location = new Point(790, 12);
            txtChallanNo.Name = "txtChallanNo";
            txtChallanNo.Size = new Size(190, 23);
            txtChallanNo.TabIndex = 9;
            //
            // lblSupplier
            //
            lblSupplier.AutoSize = true;
            lblSupplier.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSupplier.ForeColor = Color.Red;
            lblSupplier.Location = new Point(10, 47);
            lblSupplier.Name = "lblSupplier";
            lblSupplier.Size = new Size(40, 15);
            lblSupplier.TabIndex = 10;
            lblSupplier.Text = "Party";
            //
            // cmbSupplierCode
            //
            cmbSupplierCode.BackColor = Color.FromArgb(255, 255, 192);
            cmbSupplierCode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSupplierCode.FlatStyle = FlatStyle.Flat;
            cmbSupplierCode.FormattingEnabled = true;
            cmbSupplierCode.Location = new Point(70, 44);
            cmbSupplierCode.Name = "cmbSupplierCode";
            cmbSupplierCode.Size = new Size(90, 23);
            cmbSupplierCode.TabIndex = 11;
            cmbSupplierCode.SelectedIndexChanged += cmbSupplierCode_SelectedIndexChanged;
            //
            // cmbSupplierName
            //
            cmbSupplierName.BackColor = Color.FromArgb(255, 255, 192);
            cmbSupplierName.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSupplierName.FlatStyle = FlatStyle.Flat;
            cmbSupplierName.FormattingEnabled = true;
            cmbSupplierName.Location = new Point(165, 44);
            cmbSupplierName.Name = "cmbSupplierName";
            cmbSupplierName.Size = new Size(300, 23);
            cmbSupplierName.TabIndex = 12;
            cmbSupplierName.SelectedIndexChanged += cmbSupplierName_SelectedIndexChanged;
            //
            // btnNewSupplier
            //
            btnNewSupplier.BackColor = Color.LightSteelBlue;
            btnNewSupplier.FlatStyle = FlatStyle.Popup;
            btnNewSupplier.Location = new Point(470, 43);
            btnNewSupplier.Name = "btnNewSupplier";
            btnNewSupplier.Size = new Size(55, 23);
            btnNewSupplier.TabIndex = 13;
            btnNewSupplier.Text = "New";
            btnNewSupplier.UseVisualStyleBackColor = false;
            btnNewSupplier.Click += btnNewSupplier_Click;
            //
            // lblMark
            //
            lblMark.AutoSize = true;
            lblMark.Location = new Point(540, 47);
            lblMark.Name = "lblMark";
            lblMark.Size = new Size(45, 15);
            lblMark.TabIndex = 14;
            lblMark.Text = "Marko";
            //
            // txtMark
            //
            txtMark.BackColor = Color.FromArgb(224, 224, 224);
            txtMark.BorderStyle = BorderStyle.FixedSingle;
            txtMark.Location = new Point(590, 44);
            txtMark.MaxLength = 30;
            txtMark.Name = "txtMark";
            txtMark.Size = new Size(120, 23);
            txtMark.TabIndex = 15;
            //
            // lblTruckNo
            //
            lblTruckNo.AutoSize = true;
            lblTruckNo.Location = new Point(720, 47);
            lblTruckNo.Name = "lblTruckNo";
            lblTruckNo.Size = new Size(55, 15);
            lblTruckNo.TabIndex = 16;
            lblTruckNo.Text = "Veh. No.";
            //
            // txtTruckNo
            //
            txtTruckNo.BackColor = Color.FromArgb(224, 224, 224);
            txtTruckNo.BorderStyle = BorderStyle.FixedSingle;
            txtTruckNo.Location = new Point(790, 44);
            txtTruckNo.Name = "txtTruckNo";
            txtTruckNo.Size = new Size(190, 23);
            txtTruckNo.TabIndex = 17;
            //
            // lblDeliveryPerson
            //
            lblDeliveryPerson.AutoSize = true;
            lblDeliveryPerson.Location = new Point(700, 79);
            lblDeliveryPerson.Name = "lblDeliveryPerson";
            lblDeliveryPerson.Size = new Size(50, 15);
            lblDeliveryPerson.TabIndex = 18;
            lblDeliveryPerson.Text = "Rec. By";
            //
            // txtDeliveryPerson
            //
            txtDeliveryPerson.BackColor = Color.FromArgb(224, 224, 224);
            txtDeliveryPerson.BorderStyle = BorderStyle.FixedSingle;
            txtDeliveryPerson.Location = new Point(790, 76);
            txtDeliveryPerson.Name = "txtDeliveryPerson";
            txtDeliveryPerson.Size = new Size(190, 23);
            txtDeliveryPerson.TabIndex = 19;
            //
            // lblAmanatParty
            //
            lblAmanatParty.AutoSize = true;
            lblAmanatParty.Location = new Point(10, 79);
            lblAmanatParty.Name = "lblAmanatParty";
            lblAmanatParty.Size = new Size(75, 15);
            lblAmanatParty.TabIndex = 20;
            lblAmanatParty.Text = "Amanat Party";
            //
            // cmbAmanatParty
            //
            cmbAmanatParty.BackColor = Color.FromArgb(255, 255, 192);
            cmbAmanatParty.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAmanatParty.FlatStyle = FlatStyle.Flat;
            cmbAmanatParty.FormattingEnabled = true;
            cmbAmanatParty.Location = new Point(165, 76);
            cmbAmanatParty.Name = "cmbAmanatParty";
            cmbAmanatParty.Size = new Size(300, 23);
            cmbAmanatParty.TabIndex = 21;
            //
            // lblCrateParty
            //
            lblCrateParty.AutoSize = true;
            lblCrateParty.Location = new Point(10, 108);
            lblCrateParty.Name = "lblCrateParty";
            lblCrateParty.Size = new Size(65, 15);
            lblCrateParty.TabIndex = 22;
            lblCrateParty.Text = "Crate Party";
            //
            // cmbCrateParty
            //
            cmbCrateParty.BackColor = Color.FromArgb(255, 255, 192);
            cmbCrateParty.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCrateParty.FlatStyle = FlatStyle.Flat;
            cmbCrateParty.FormattingEnabled = true;
            cmbCrateParty.Location = new Point(165, 105);
            cmbCrateParty.Name = "cmbCrateParty";
            cmbCrateParty.Size = new Size(300, 23);
            cmbCrateParty.TabIndex = 23;
            //
            // dgvItems
            //
            dgvItems.AllowUserToDeleteRows = false;
            dgvItems.AllowUserToResizeRows = false;
            dgvItems.BackgroundColor = Color.White;
            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.Columns.AddRange(new DataGridViewColumn[] { colItem, colRemarks, colQty, colWeight, colRate, colAmount, colLotNo, colCrateInfo });
            dgvItems.EnableHeadersVisualStyles = false;
            dgvItems.Location = new Point(12, 148);
            dgvItems.Name = "dgvItems";
            dgvItems.RowHeadersVisible = false;
            dgvItems.Size = new Size(1000, 220);
            dgvItems.TabIndex = 1;
            dgvItems.CellValueChanged += dgvItems_CellValueChanged;
            dgvItems.CurrentCellDirtyStateChanged += dgvItems_CurrentCellDirtyStateChanged;
            dgvItems.RowsRemoved += dgvItems_RowsRemoved;
            dgvItems.DefaultValuesNeeded += dgvItems_DefaultValuesNeeded;
            //
            // colItem
            //
            colItem.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colItem.HeaderText = "Item";
            colItem.Name = "colItem";
            colItem.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            colItem.FlatStyle = FlatStyle.Flat;
            //
            // colRemarks
            //
            colRemarks.HeaderText = "Spl. Remarks";
            colRemarks.Name = "colRemarks";
            colRemarks.Width = 140;
            //
            // colQty
            //
            colQty.HeaderText = "Qty";
            colQty.Name = "colQty";
            colQty.Width = 70;
            //
            // colWeight
            //
            colWeight.HeaderText = "Wt";
            colWeight.Name = "colWeight";
            colWeight.Width = 70;
            //
            // colRate
            //
            colRate.HeaderText = "Rate";
            colRate.Name = "colRate";
            colRate.Width = 80;
            //
            // colAmount
            //
            colAmount.HeaderText = "Amount";
            colAmount.Name = "colAmount";
            colAmount.ReadOnly = true;
            colAmount.Width = 100;
            //
            // colLotNo
            //
            colLotNo.HeaderText = "Lot No.";
            colLotNo.Name = "colLotNo";
            colLotNo.Width = 70;
            //
            // colCrateInfo
            //
            colCrateInfo.HeaderText = "Carate";
            colCrateInfo.Name = "colCrateInfo";
            colCrateInfo.Width = 100;
            //
            // panelFooter
            //
            panelFooter.BorderStyle = BorderStyle.FixedSingle;
            panelFooter.Controls.Add(lblCommission);
            panelFooter.Controls.Add(txtCommissionPct);
            panelFooter.Controls.Add(txtCommissionAmt);
            panelFooter.Controls.Add(lblFreight);
            panelFooter.Controls.Add(txtFreightRate);
            panelFooter.Controls.Add(txtFreightAmt);
            panelFooter.Controls.Add(lblLabour);
            panelFooter.Controls.Add(txtLabourRate);
            panelFooter.Controls.Add(txtLabourAmt);
            panelFooter.Controls.Add(lblVatav);
            panelFooter.Controls.Add(txtVatavPct);
            panelFooter.Controls.Add(txtVatavAmt);
            panelFooter.Controls.Add(lblMarketFee);
            panelFooter.Controls.Add(txtMarketFeePct);
            panelFooter.Controls.Add(txtMarketFeeAmt);
            panelFooter.Controls.Add(lblPostage);
            panelFooter.Controls.Add(txtPostage);
            panelFooter.Controls.Add(lblPackingMaterial);
            panelFooter.Controls.Add(txtPackingMaterial);
            panelFooter.Controls.Add(lblColdStore);
            panelFooter.Controls.Add(txtColdStore);
            panelFooter.Controls.Add(lblDdCharge);
            panelFooter.Controls.Add(txtDdCharge);
            panelFooter.Controls.Add(lblInam);
            panelFooter.Controls.Add(txtInam);
            panelFooter.Controls.Add(lblOtherDeduction);
            panelFooter.Controls.Add(txtOtherDeduction);
            panelFooter.Controls.Add(lblTotalExpense);
            panelFooter.Controls.Add(txtTotalExpense);
            panelFooter.Controls.Add(lblNetAmount);
            panelFooter.Controls.Add(txtNetAmount);
            panelFooter.Controls.Add(lblRemarks);
            panelFooter.Controls.Add(txtRemarks);
            panelFooter.Location = new Point(12, 412);
            panelFooter.Name = "panelFooter";
            panelFooter.Size = new Size(1000, 160);
            panelFooter.TabIndex = 2;
            //
            // lblCommission
            //
            lblCommission.AutoSize = true;
            lblCommission.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCommission.Location = new Point(10, 15);
            lblCommission.Name = "lblCommission";
            lblCommission.Size = new Size(75, 15);
            lblCommission.TabIndex = 0;
            lblCommission.Text = "Commission";
            //
            // txtCommissionPct
            //
            txtCommissionPct.BackColor = Color.FromArgb(224, 224, 224);
            txtCommissionPct.BorderStyle = BorderStyle.FixedSingle;
            txtCommissionPct.Location = new Point(120, 12);
            txtCommissionPct.Name = "txtCommissionPct";
            txtCommissionPct.Size = new Size(70, 23);
            txtCommissionPct.TabIndex = 1;
            txtCommissionPct.Text = "0";
            txtCommissionPct.TextChanged += FooterField_Changed;
            //
            // txtCommissionAmt
            //
            txtCommissionAmt.BackColor = Color.WhiteSmoke;
            txtCommissionAmt.BorderStyle = BorderStyle.FixedSingle;
            txtCommissionAmt.Location = new Point(195, 12);
            txtCommissionAmt.Name = "txtCommissionAmt";
            txtCommissionAmt.ReadOnly = true;
            txtCommissionAmt.Size = new Size(90, 23);
            txtCommissionAmt.TabIndex = 2;
            txtCommissionAmt.Text = "0";
            //
            // lblFreight
            //
            lblFreight.AutoSize = true;
            lblFreight.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFreight.Location = new Point(300, 15);
            lblFreight.Name = "lblFreight";
            lblFreight.Size = new Size(45, 15);
            lblFreight.TabIndex = 3;
            lblFreight.Text = "Freight";
            //
            // txtFreightRate
            //
            txtFreightRate.BackColor = Color.FromArgb(224, 224, 224);
            txtFreightRate.BorderStyle = BorderStyle.FixedSingle;
            txtFreightRate.Location = new Point(410, 12);
            txtFreightRate.Name = "txtFreightRate";
            txtFreightRate.Size = new Size(70, 23);
            txtFreightRate.TabIndex = 4;
            txtFreightRate.Text = "0";
            txtFreightRate.TextChanged += FooterField_Changed;
            //
            // txtFreightAmt
            //
            txtFreightAmt.BackColor = Color.WhiteSmoke;
            txtFreightAmt.BorderStyle = BorderStyle.FixedSingle;
            txtFreightAmt.Location = new Point(485, 12);
            txtFreightAmt.Name = "txtFreightAmt";
            txtFreightAmt.ReadOnly = true;
            txtFreightAmt.Size = new Size(90, 23);
            txtFreightAmt.TabIndex = 5;
            txtFreightAmt.Text = "0";
            //
            // lblLabour
            //
            lblLabour.AutoSize = true;
            lblLabour.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLabour.Location = new Point(600, 15);
            lblLabour.Name = "lblLabour";
            lblLabour.Size = new Size(45, 15);
            lblLabour.TabIndex = 6;
            lblLabour.Text = "Labour";
            //
            // txtLabourRate
            //
            txtLabourRate.BackColor = Color.FromArgb(224, 224, 224);
            txtLabourRate.BorderStyle = BorderStyle.FixedSingle;
            txtLabourRate.Location = new Point(710, 12);
            txtLabourRate.Name = "txtLabourRate";
            txtLabourRate.Size = new Size(70, 23);
            txtLabourRate.TabIndex = 7;
            txtLabourRate.Text = "0";
            txtLabourRate.TextChanged += FooterField_Changed;
            //
            // txtLabourAmt
            //
            txtLabourAmt.BackColor = Color.WhiteSmoke;
            txtLabourAmt.BorderStyle = BorderStyle.FixedSingle;
            txtLabourAmt.Location = new Point(785, 12);
            txtLabourAmt.Name = "txtLabourAmt";
            txtLabourAmt.ReadOnly = true;
            txtLabourAmt.Size = new Size(90, 23);
            txtLabourAmt.TabIndex = 8;
            txtLabourAmt.Text = "0";
            //
            // lblVatav
            //
            lblVatav.AutoSize = true;
            lblVatav.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblVatav.Location = new Point(10, 45);
            lblVatav.Name = "lblVatav";
            lblVatav.Size = new Size(38, 15);
            lblVatav.TabIndex = 9;
            lblVatav.Text = "Vatav";
            //
            // txtVatavPct
            //
            txtVatavPct.BackColor = Color.FromArgb(224, 224, 224);
            txtVatavPct.BorderStyle = BorderStyle.FixedSingle;
            txtVatavPct.Location = new Point(120, 42);
            txtVatavPct.Name = "txtVatavPct";
            txtVatavPct.Size = new Size(70, 23);
            txtVatavPct.TabIndex = 10;
            txtVatavPct.Text = "0";
            txtVatavPct.TextChanged += FooterField_Changed;
            //
            // txtVatavAmt
            //
            txtVatavAmt.BackColor = Color.WhiteSmoke;
            txtVatavAmt.BorderStyle = BorderStyle.FixedSingle;
            txtVatavAmt.Location = new Point(195, 42);
            txtVatavAmt.Name = "txtVatavAmt";
            txtVatavAmt.ReadOnly = true;
            txtVatavAmt.Size = new Size(90, 23);
            txtVatavAmt.TabIndex = 11;
            txtVatavAmt.Text = "0";
            //
            // lblMarketFee
            //
            lblMarketFee.AutoSize = true;
            lblMarketFee.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMarketFee.Location = new Point(300, 45);
            lblMarketFee.Name = "lblMarketFee";
            lblMarketFee.Size = new Size(85, 15);
            lblMarketFee.TabIndex = 12;
            lblMarketFee.Text = "APMC/Market";
            //
            // txtMarketFeePct
            //
            txtMarketFeePct.BackColor = Color.FromArgb(224, 224, 224);
            txtMarketFeePct.BorderStyle = BorderStyle.FixedSingle;
            txtMarketFeePct.Location = new Point(410, 42);
            txtMarketFeePct.Name = "txtMarketFeePct";
            txtMarketFeePct.Size = new Size(70, 23);
            txtMarketFeePct.TabIndex = 13;
            txtMarketFeePct.Text = "0";
            txtMarketFeePct.TextChanged += FooterField_Changed;
            //
            // txtMarketFeeAmt
            //
            txtMarketFeeAmt.BackColor = Color.WhiteSmoke;
            txtMarketFeeAmt.BorderStyle = BorderStyle.FixedSingle;
            txtMarketFeeAmt.Location = new Point(485, 42);
            txtMarketFeeAmt.Name = "txtMarketFeeAmt";
            txtMarketFeeAmt.ReadOnly = true;
            txtMarketFeeAmt.Size = new Size(90, 23);
            txtMarketFeeAmt.TabIndex = 14;
            txtMarketFeeAmt.Text = "0";
            //
            // lblPostage
            //
            lblPostage.AutoSize = true;
            lblPostage.Location = new Point(600, 45);
            lblPostage.Name = "lblPostage";
            lblPostage.Size = new Size(50, 15);
            lblPostage.TabIndex = 15;
            lblPostage.Text = "Postage";
            //
            // txtPostage
            //
            txtPostage.BackColor = Color.FromArgb(224, 224, 224);
            txtPostage.BorderStyle = BorderStyle.FixedSingle;
            txtPostage.Location = new Point(710, 42);
            txtPostage.Name = "txtPostage";
            txtPostage.Size = new Size(90, 23);
            txtPostage.TabIndex = 16;
            txtPostage.Text = "10";
            txtPostage.TextChanged += FooterField_Changed;
            //
            // lblPackingMaterial
            //
            lblPackingMaterial.AutoSize = true;
            lblPackingMaterial.Location = new Point(10, 75);
            lblPackingMaterial.Name = "lblPackingMaterial";
            lblPackingMaterial.Size = new Size(90, 15);
            lblPackingMaterial.TabIndex = 17;
            lblPackingMaterial.Text = "Pkg. Material";
            //
            // txtPackingMaterial
            //
            txtPackingMaterial.BackColor = Color.FromArgb(224, 224, 224);
            txtPackingMaterial.BorderStyle = BorderStyle.FixedSingle;
            txtPackingMaterial.Location = new Point(120, 72);
            txtPackingMaterial.Name = "txtPackingMaterial";
            txtPackingMaterial.Size = new Size(90, 23);
            txtPackingMaterial.TabIndex = 18;
            txtPackingMaterial.Text = "0";
            txtPackingMaterial.TextChanged += FooterField_Changed;
            //
            // lblColdStore
            //
            lblColdStore.AutoSize = true;
            lblColdStore.Location = new Point(300, 75);
            lblColdStore.Name = "lblColdStore";
            lblColdStore.Size = new Size(65, 15);
            lblColdStore.TabIndex = 19;
            lblColdStore.Text = "Store Exp.";
            //
            // txtColdStore
            //
            txtColdStore.BackColor = Color.FromArgb(224, 224, 224);
            txtColdStore.BorderStyle = BorderStyle.FixedSingle;
            txtColdStore.Location = new Point(410, 72);
            txtColdStore.Name = "txtColdStore";
            txtColdStore.Size = new Size(90, 23);
            txtColdStore.TabIndex = 20;
            txtColdStore.Text = "0";
            txtColdStore.TextChanged += FooterField_Changed;
            //
            // lblDdCharge
            //
            lblDdCharge.AutoSize = true;
            lblDdCharge.Location = new Point(600, 75);
            lblDdCharge.Name = "lblDdCharge";
            lblDdCharge.Size = new Size(70, 15);
            lblDdCharge.TabIndex = 21;
            lblDdCharge.Text = "Bank Chgs.";
            //
            // txtDdCharge
            //
            txtDdCharge.BackColor = Color.FromArgb(224, 224, 224);
            txtDdCharge.BorderStyle = BorderStyle.FixedSingle;
            txtDdCharge.Location = new Point(710, 72);
            txtDdCharge.Name = "txtDdCharge";
            txtDdCharge.Size = new Size(90, 23);
            txtDdCharge.TabIndex = 22;
            txtDdCharge.Text = "0";
            txtDdCharge.TextChanged += FooterField_Changed;
            //
            // lblInam
            //
            lblInam.AutoSize = true;
            lblInam.Location = new Point(10, 105);
            lblInam.Name = "lblInam";
            lblInam.Size = new Size(35, 15);
            lblInam.TabIndex = 23;
            lblInam.Text = "Inam";
            //
            // txtInam
            //
            txtInam.BackColor = Color.FromArgb(224, 224, 224);
            txtInam.BorderStyle = BorderStyle.FixedSingle;
            txtInam.Location = new Point(120, 102);
            txtInam.Name = "txtInam";
            txtInam.Size = new Size(90, 23);
            txtInam.TabIndex = 24;
            txtInam.Text = "0";
            txtInam.TextChanged += FooterField_Changed;
            //
            // lblOtherDeduction
            //
            lblOtherDeduction.AutoSize = true;
            lblOtherDeduction.Location = new Point(300, 105);
            lblOtherDeduction.Name = "lblOtherDeduction";
            lblOtherDeduction.Size = new Size(85, 15);
            lblOtherDeduction.TabIndex = 25;
            lblOtherDeduction.Text = "Other Deduct.";
            //
            // txtOtherDeduction
            //
            txtOtherDeduction.BackColor = Color.FromArgb(224, 224, 224);
            txtOtherDeduction.BorderStyle = BorderStyle.FixedSingle;
            txtOtherDeduction.Location = new Point(410, 102);
            txtOtherDeduction.Name = "txtOtherDeduction";
            txtOtherDeduction.Size = new Size(90, 23);
            txtOtherDeduction.TabIndex = 26;
            txtOtherDeduction.Text = "0";
            txtOtherDeduction.TextChanged += FooterField_Changed;
            //
            // panelItemTotals
            //
            panelItemTotals.BorderStyle = BorderStyle.FixedSingle;
            panelItemTotals.Controls.Add(lblTotalQty);
            panelItemTotals.Controls.Add(txtTotalQty);
            panelItemTotals.Controls.Add(lblTotalWeight);
            panelItemTotals.Controls.Add(txtTotalWeight);
            panelItemTotals.Controls.Add(lblGrossTotal);
            panelItemTotals.Controls.Add(txtGrossTotal);
            panelItemTotals.Controls.Add(lblTds);
            panelItemTotals.Controls.Add(txtTdsPct);
            panelItemTotals.Controls.Add(txtTdsAmt);
            panelItemTotals.Location = new Point(12, 374);
            panelItemTotals.Name = "panelItemTotals";
            panelItemTotals.Size = new Size(1000, 34);
            panelItemTotals.TabIndex = 4;
            //
            // lblTotalQty
            //
            lblTotalQty.AutoSize = true;
            lblTotalQty.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalQty.Location = new Point(10, 10);
            lblTotalQty.Name = "lblTotalQty";
            lblTotalQty.Size = new Size(60, 15);
            lblTotalQty.TabIndex = 0;
            lblTotalQty.Text = "Total";
            //
            // txtTotalQty
            //
            txtTotalQty.BackColor = Color.WhiteSmoke;
            txtTotalQty.BorderStyle = BorderStyle.FixedSingle;
            txtTotalQty.Location = new Point(90, 7);
            txtTotalQty.Name = "txtTotalQty";
            txtTotalQty.ReadOnly = true;
            txtTotalQty.Size = new Size(90, 23);
            txtTotalQty.TabIndex = 1;
            txtTotalQty.Text = "0.00";
            //
            // lblTotalWeight
            //
            lblTotalWeight.AutoSize = true;
            lblTotalWeight.Location = new Point(185, 10);
            lblTotalWeight.Name = "lblTotalWeight";
            lblTotalWeight.Size = new Size(15, 15);
            lblTotalWeight.TabIndex = 2;
            lblTotalWeight.Text = "/";
            //
            // txtTotalWeight
            //
            txtTotalWeight.BackColor = Color.WhiteSmoke;
            txtTotalWeight.BorderStyle = BorderStyle.FixedSingle;
            txtTotalWeight.Location = new Point(200, 7);
            txtTotalWeight.Name = "txtTotalWeight";
            txtTotalWeight.ReadOnly = true;
            txtTotalWeight.Size = new Size(90, 23);
            txtTotalWeight.TabIndex = 3;
            txtTotalWeight.Text = "0.00";
            //
            // lblGrossTotal
            //
            lblGrossTotal.AutoSize = true;
            lblGrossTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblGrossTotal.Location = new Point(320, 10);
            lblGrossTotal.Name = "lblGrossTotal";
            lblGrossTotal.Size = new Size(45, 15);
            lblGrossTotal.TabIndex = 4;
            lblGrossTotal.Text = "Gross";
            //
            // txtGrossTotal
            //
            txtGrossTotal.BackColor = Color.WhiteSmoke;
            txtGrossTotal.BorderStyle = BorderStyle.FixedSingle;
            txtGrossTotal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtGrossTotal.Location = new Point(410, 7);
            txtGrossTotal.Name = "txtGrossTotal";
            txtGrossTotal.ReadOnly = true;
            txtGrossTotal.Size = new Size(110, 23);
            txtGrossTotal.TabIndex = 5;
            txtGrossTotal.Text = "0.00";
            //
            // lblTds
            //
            lblTds.AutoSize = true;
            lblTds.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTds.ForeColor = Color.Red;
            lblTds.Location = new Point(600, 10);
            lblTds.Name = "lblTds";
            lblTds.Size = new Size(30, 15);
            lblTds.TabIndex = 6;
            lblTds.Text = "TDS";
            //
            // txtTdsPct
            //
            txtTdsPct.BackColor = Color.WhiteSmoke;
            txtTdsPct.BorderStyle = BorderStyle.FixedSingle;
            txtTdsPct.Location = new Point(660, 7);
            txtTdsPct.Name = "txtTdsPct";
            txtTdsPct.ReadOnly = true;
            txtTdsPct.Size = new Size(70, 23);
            txtTdsPct.TabIndex = 7;
            txtTdsPct.Text = "0";
            //
            // txtTdsAmt
            //
            txtTdsAmt.BackColor = Color.WhiteSmoke;
            txtTdsAmt.BorderStyle = BorderStyle.FixedSingle;
            txtTdsAmt.Location = new Point(735, 7);
            txtTdsAmt.Name = "txtTdsAmt";
            txtTdsAmt.ReadOnly = true;
            txtTdsAmt.Size = new Size(90, 23);
            txtTdsAmt.TabIndex = 8;
            txtTdsAmt.Text = "0";
            //
            // lblTotalExpense
            //
            lblTotalExpense.AutoSize = true;
            lblTotalExpense.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalExpense.Location = new Point(10, 135);
            lblTotalExpense.Name = "lblTotalExpense";
            lblTotalExpense.Size = new Size(80, 15);
            lblTotalExpense.TabIndex = 30;
            lblTotalExpense.Text = "Total Exp.";
            //
            // txtTotalExpense
            //
            txtTotalExpense.BackColor = Color.WhiteSmoke;
            txtTotalExpense.BorderStyle = BorderStyle.FixedSingle;
            txtTotalExpense.Location = new Point(120, 132);
            txtTotalExpense.Name = "txtTotalExpense";
            txtTotalExpense.ReadOnly = true;
            txtTotalExpense.Size = new Size(90, 23);
            txtTotalExpense.TabIndex = 31;
            txtTotalExpense.Text = "0";
            //
            // lblNetAmount
            //
            lblNetAmount.AutoSize = true;
            lblNetAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNetAmount.ForeColor = Color.Red;
            lblNetAmount.Location = new Point(300, 133);
            lblNetAmount.Name = "lblNetAmount";
            lblNetAmount.Size = new Size(90, 19);
            lblNetAmount.TabIndex = 32;
            lblNetAmount.Text = "Net Amount";
            //
            // txtNetAmount
            //
            txtNetAmount.BackColor = Color.White;
            txtNetAmount.BorderStyle = BorderStyle.FixedSingle;
            txtNetAmount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            txtNetAmount.ForeColor = Color.Red;
            txtNetAmount.Location = new Point(410, 130);
            txtNetAmount.Name = "txtNetAmount";
            txtNetAmount.ReadOnly = true;
            txtNetAmount.Size = new Size(150, 25);
            txtNetAmount.TabIndex = 33;
            txtNetAmount.Text = "0";
            //
            // lblRemarks
            //
            lblRemarks.AutoSize = true;
            lblRemarks.Location = new Point(600, 135);
            lblRemarks.Name = "lblRemarks";
            lblRemarks.Size = new Size(53, 15);
            lblRemarks.TabIndex = 34;
            lblRemarks.Text = "Remarks";
            //
            // txtRemarks
            //
            txtRemarks.BackColor = Color.FromArgb(224, 224, 224);
            txtRemarks.BorderStyle = BorderStyle.FixedSingle;
            txtRemarks.Location = new Point(710, 132);
            txtRemarks.Name = "txtRemarks";
            txtRemarks.Size = new Size(270, 23);
            txtRemarks.TabIndex = 35;
            //
            // panelButtons
            //
            panelButtons.BorderStyle = BorderStyle.FixedSingle;
            panelButtons.Controls.Add(btnAdd);
            panelButtons.Controls.Add(btnUpdate);
            panelButtons.Controls.Add(btnDelete);
            panelButtons.Controls.Add(btnSave);
            panelButtons.Controls.Add(btnPrevious);
            panelButtons.Controls.Add(btnNext);
            panelButtons.Controls.Add(btnFind);
            panelButtons.Controls.Add(btnClose);
            panelButtons.Controls.Add(btnPrint);
            panelButtons.Controls.Add(btnWhatsapp);
            panelButtons.Controls.Add(btnLotFind);
            panelButtons.Controls.Add(btnBill);
            panelButtons.Controls.Add(btnDocument);
            panelButtons.Controls.Add(btnSales);
            panelButtons.Location = new Point(12, 578);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(1000, 40);
            panelButtons.TabIndex = 3;
            //
            // btnAdd
            //
            btnAdd.BackColor = Color.LightSteelBlue;
            btnAdd.FlatStyle = FlatStyle.Popup;
            btnAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAdd.Location = new Point(8, 6);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(60, 27);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            //
            // btnUpdate
            //
            btnUpdate.BackColor = Color.LightSteelBlue;
            btnUpdate.FlatStyle = FlatStyle.Popup;
            btnUpdate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnUpdate.Location = new Point(72, 6);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(60, 27);
            btnUpdate.TabIndex = 1;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            //
            // btnDelete
            //
            btnDelete.BackColor = Color.LightSteelBlue;
            btnDelete.FlatStyle = FlatStyle.Popup;
            btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDelete.Location = new Point(136, 6);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(60, 27);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            //
            // btnSave
            //
            btnSave.BackColor = Color.WhiteSmoke;
            btnSave.Enabled = false;
            btnSave.FlatStyle = FlatStyle.Popup;
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.Location = new Point(200, 6);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(60, 27);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            //
            // btnPrevious
            //
            btnPrevious.BackColor = Color.LightSteelBlue;
            btnPrevious.FlatStyle = FlatStyle.Popup;
            btnPrevious.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPrevious.Location = new Point(264, 6);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(70, 27);
            btnPrevious.TabIndex = 4;
            btnPrevious.Text = "Previous";
            btnPrevious.UseVisualStyleBackColor = false;
            btnPrevious.Click += btnPrevious_Click;
            //
            // btnNext
            //
            btnNext.BackColor = Color.LightSteelBlue;
            btnNext.FlatStyle = FlatStyle.Popup;
            btnNext.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnNext.Location = new Point(338, 6);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(60, 27);
            btnNext.TabIndex = 5;
            btnNext.Text = "Next";
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += btnNext_Click;
            //
            // btnFind
            //
            btnFind.BackColor = Color.LightSteelBlue;
            btnFind.FlatStyle = FlatStyle.Popup;
            btnFind.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnFind.Location = new Point(402, 6);
            btnFind.Name = "btnFind";
            btnFind.Size = new Size(55, 27);
            btnFind.TabIndex = 6;
            btnFind.Text = "Find";
            btnFind.UseVisualStyleBackColor = false;
            btnFind.Click += btnFind_Click;
            //
            // btnClose
            //
            btnClose.BackColor = Color.LightSteelBlue;
            btnClose.FlatStyle = FlatStyle.Popup;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClose.Location = new Point(461, 6);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(55, 27);
            btnClose.TabIndex = 7;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            //
            // btnPrint
            //
            btnPrint.BackColor = Color.LightSteelBlue;
            btnPrint.FlatStyle = FlatStyle.Popup;
            btnPrint.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPrint.Location = new Point(524, 6);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(55, 27);
            btnPrint.TabIndex = 8;
            btnPrint.Text = "Print";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;
            //
            // btnWhatsapp
            //
            btnWhatsapp.BackColor = Color.LightSteelBlue;
            btnWhatsapp.FlatStyle = FlatStyle.Popup;
            btnWhatsapp.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnWhatsapp.Location = new Point(587, 6);
            btnWhatsapp.Name = "btnWhatsapp";
            btnWhatsapp.Size = new Size(90, 27);
            btnWhatsapp.TabIndex = 9;
            btnWhatsapp.Text = "WhatsApp";
            btnWhatsapp.UseVisualStyleBackColor = false;
            btnWhatsapp.Click += btnWhatsapp_Click;
            //
            // btnLotFind
            //
            btnLotFind.BackColor = Color.LightSteelBlue;
            btnLotFind.FlatStyle = FlatStyle.Popup;
            btnLotFind.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLotFind.Location = new Point(685, 6);
            btnLotFind.Name = "btnLotFind";
            btnLotFind.Size = new Size(70, 27);
            btnLotFind.TabIndex = 10;
            btnLotFind.Text = "Lot Find";
            btnLotFind.UseVisualStyleBackColor = false;
            btnLotFind.Click += btnLotFind_Click;
            //
            // btnBill
            //
            btnBill.BackColor = Color.LightSteelBlue;
            btnBill.FlatStyle = FlatStyle.Popup;
            btnBill.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnBill.Location = new Point(760, 6);
            btnBill.Name = "btnBill";
            btnBill.Size = new Size(55, 27);
            btnBill.TabIndex = 11;
            btnBill.Text = "Bill";
            btnBill.UseVisualStyleBackColor = false;
            btnBill.Click += btnBill_Click;
            //
            // btnDocument
            //
            btnDocument.BackColor = Color.LightSteelBlue;
            btnDocument.FlatStyle = FlatStyle.Popup;
            btnDocument.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDocument.Location = new Point(820, 6);
            btnDocument.Name = "btnDocument";
            btnDocument.Size = new Size(80, 27);
            btnDocument.TabIndex = 12;
            btnDocument.Text = "Document";
            btnDocument.UseVisualStyleBackColor = false;
            btnDocument.Click += btnDocument_Click;
            //
            // btnSales
            //
            btnSales.BackColor = Color.LightSteelBlue;
            btnSales.FlatStyle = FlatStyle.Popup;
            btnSales.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSales.Location = new Point(905, 6);
            btnSales.Name = "btnSales";
            btnSales.Size = new Size(65, 27);
            btnSales.TabIndex = 13;
            btnSales.Text = "Sales";
            btnSales.UseVisualStyleBackColor = false;
            btnSales.Click += btnSales_Click;
            //
            // PurchaseForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1024, 630);
            Controls.Add(panelButtons);
            Controls.Add(panelFooter);
            Controls.Add(panelItemTotals);
            Controls.Add(dgvItems);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PurchaseForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Purchase";
            Load += PurchaseForm_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            panelItemTotals.ResumeLayout(false);
            panelItemTotals.PerformLayout();
            panelFooter.ResumeLayout(false);
            panelFooter.PerformLayout();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private Label lblBillNo;
        private TextBox txtBillNo;
        private Label lblBillDate;
        private DateTimePicker dtpBillDate;
        private RadioButton rbCommission;
        private RadioButton rbTrading;
        private RadioButton rbDirect;
        private RadioButton rbWithoutCommission;
        private Label lblChallanNo;
        private TextBox txtChallanNo;
        private Label lblSupplier;
        private ComboBox cmbSupplierCode;
        private ComboBox cmbSupplierName;
        private Button btnNewSupplier;
        private Label lblMark;
        private TextBox txtMark;
        private Label lblTruckNo;
        private TextBox txtTruckNo;
        private Label lblDeliveryPerson;
        private TextBox txtDeliveryPerson;
        private Label lblAmanatParty;
        private ComboBox cmbAmanatParty;
        private Label lblCrateParty;
        private ComboBox cmbCrateParty;
        private DataGridView dgvItems;
        private DataGridViewComboBoxColumn colItem;
        private DataGridViewTextBoxColumn colRemarks;
        private DataGridViewTextBoxColumn colQty;
        private DataGridViewTextBoxColumn colWeight;
        private DataGridViewTextBoxColumn colRate;
        private DataGridViewTextBoxColumn colAmount;
        private DataGridViewTextBoxColumn colLotNo;
        private DataGridViewTextBoxColumn colCrateInfo;
        private Panel panelFooter;
        private Label lblCommission;
        private TextBox txtCommissionPct;
        private TextBox txtCommissionAmt;
        private Label lblFreight;
        private TextBox txtFreightRate;
        private TextBox txtFreightAmt;
        private Label lblLabour;
        private TextBox txtLabourRate;
        private TextBox txtLabourAmt;
        private Label lblVatav;
        private TextBox txtVatavPct;
        private TextBox txtVatavAmt;
        private Label lblMarketFee;
        private TextBox txtMarketFeePct;
        private TextBox txtMarketFeeAmt;
        private Label lblPostage;
        private TextBox txtPostage;
        private Label lblPackingMaterial;
        private TextBox txtPackingMaterial;
        private Label lblColdStore;
        private TextBox txtColdStore;
        private Label lblDdCharge;
        private TextBox txtDdCharge;
        private Label lblInam;
        private TextBox txtInam;
        private Label lblOtherDeduction;
        private TextBox txtOtherDeduction;
        private Panel panelItemTotals;
        private Label lblTotalQty;
        private TextBox txtTotalQty;
        private Label lblTotalWeight;
        private TextBox txtTotalWeight;
        private Label lblGrossTotal;
        private TextBox txtGrossTotal;
        private Label lblTds;
        private TextBox txtTdsPct;
        private TextBox txtTdsAmt;
        private Label lblTotalExpense;
        private TextBox txtTotalExpense;
        private Label lblNetAmount;
        private TextBox txtNetAmount;
        private Label lblRemarks;
        private TextBox txtRemarks;
        private Panel panelButtons;
        protected Button btnAdd;
        protected Button btnUpdate;
        protected Button btnDelete;
        protected Button btnSave;
        protected Button btnPrevious;
        protected Button btnNext;
        protected Button btnFind;
        protected Button btnClose;
        protected Button btnPrint;
        protected Button btnWhatsapp;
        private Button btnLotFind;
        private Button btnBill;
        private Button btnDocument;
        private Button btnSales;
    }
}
