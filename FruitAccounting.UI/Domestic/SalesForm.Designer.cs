namespace FruitAccounting.UI
{
    partial class SalesForm
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
            lblInvNo = new Label();
            txtInvNo = new TextBox();
            lblSaleDate = new Label();
            dtpSaleDate = new DateTimePicker();
            lblPurBillNo = new Label();
            cmbPurBillNo = new ComboBox();
            lblPurDate = new Label();
            txtPurDate = new TextBox();
            lblParty = new Label();
            txtParty = new TextBox();
            lblVehNo = new Label();
            txtVehNo = new TextBox();
            lblLotNo = new Label();
            cmbLotNo = new ComboBox();
            lblItem = new Label();
            txtItem = new TextBox();
            lblSplMarko = new Label();
            txtSplMarko = new TextBox();
            lblCarate = new Label();
            txtCarate = new TextBox();
            lblQty = new Label();
            txtQty = new TextBox();
            lblWeight = new Label();
            txtWeight = new TextBox();
            lblBalQty = new Label();
            txtBalQty = new TextBox();
            lblRate = new Label();
            txtRate = new TextBox();
            lblAvgRate = new Label();
            txtAvgRate = new TextBox();
            dgvItems = new DataGridView();
            colBuyerCode = new DataGridViewComboBoxColumn();
            colBuyerName = new DataGridViewComboBoxColumn();
            colQty = new DataGridViewTextBoxColumn();
            colWeight = new DataGridViewTextBoxColumn();
            colRate = new DataGridViewTextBoxColumn();
            colAmount = new DataGridViewTextBoxColumn();
            colLabourRate = new DataGridViewTextBoxColumn();
            colLabourAmt = new DataGridViewTextBoxColumn();
            colApmcAmt = new DataGridViewTextBoxColumn();
            colRemarks = new DataGridViewTextBoxColumn();
            panelTotals = new Panel();
            lblTotalQty = new Label();
            txtTotalQty = new TextBox();
            lblTotalWeight = new Label();
            txtTotalWeight = new TextBox();
            lblTotalAmount = new Label();
            txtTotalAmount = new TextBox();
            btnSearch = new Button();
            btnNewBuyer = new Button();
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
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).BeginInit();
            panelTotals.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();
            //
            // panelHeader
            //
            panelHeader.BorderStyle = BorderStyle.FixedSingle;
            panelHeader.Controls.Add(lblInvNo);
            panelHeader.Controls.Add(txtInvNo);
            panelHeader.Controls.Add(lblSaleDate);
            panelHeader.Controls.Add(dtpSaleDate);
            panelHeader.Controls.Add(lblPurBillNo);
            panelHeader.Controls.Add(cmbPurBillNo);
            panelHeader.Controls.Add(lblPurDate);
            panelHeader.Controls.Add(txtPurDate);
            panelHeader.Controls.Add(lblParty);
            panelHeader.Controls.Add(txtParty);
            panelHeader.Controls.Add(lblVehNo);
            panelHeader.Controls.Add(txtVehNo);
            panelHeader.Controls.Add(lblLotNo);
            panelHeader.Controls.Add(cmbLotNo);
            panelHeader.Controls.Add(lblItem);
            panelHeader.Controls.Add(txtItem);
            panelHeader.Controls.Add(lblSplMarko);
            panelHeader.Controls.Add(txtSplMarko);
            panelHeader.Controls.Add(lblCarate);
            panelHeader.Controls.Add(txtCarate);
            panelHeader.Controls.Add(lblQty);
            panelHeader.Controls.Add(txtQty);
            panelHeader.Controls.Add(lblWeight);
            panelHeader.Controls.Add(txtWeight);
            panelHeader.Controls.Add(lblBalQty);
            panelHeader.Controls.Add(txtBalQty);
            panelHeader.Controls.Add(lblRate);
            panelHeader.Controls.Add(txtRate);
            panelHeader.Controls.Add(lblAvgRate);
            panelHeader.Controls.Add(txtAvgRate);
            panelHeader.Location = new Point(10, 10);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1000, 130);
            panelHeader.TabIndex = 0;
            //
            // lblInvNo
            //
            lblInvNo.AutoSize = true;
            lblInvNo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblInvNo.Location = new Point(10, 12);
            lblInvNo.Name = "lblInvNo";
            lblInvNo.Size = new Size(45, 15);
            lblInvNo.TabIndex = 0;
            lblInvNo.Text = "Sr. No.";
            //
            // txtInvNo
            //
            txtInvNo.BackColor = Color.WhiteSmoke;
            txtInvNo.BorderStyle = BorderStyle.FixedSingle;
            txtInvNo.Location = new Point(90, 9);
            txtInvNo.Name = "txtInvNo";
            txtInvNo.ReadOnly = true;
            txtInvNo.Size = new Size(70, 23);
            txtInvNo.TabIndex = 1;
            //
            // lblSaleDate
            //
            lblSaleDate.AutoSize = true;
            lblSaleDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSaleDate.Location = new Point(180, 12);
            lblSaleDate.Name = "lblSaleDate";
            lblSaleDate.Size = new Size(34, 15);
            lblSaleDate.TabIndex = 2;
            lblSaleDate.Text = "Date";
            //
            // dtpSaleDate
            //
            dtpSaleDate.Format = DateTimePickerFormat.Short;
            dtpSaleDate.Location = new Point(260, 9);
            dtpSaleDate.Name = "dtpSaleDate";
            dtpSaleDate.Size = new Size(110, 23);
            dtpSaleDate.TabIndex = 3;
            //
            // lblPurBillNo
            //
            lblPurBillNo.AutoSize = true;
            lblPurBillNo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPurBillNo.Location = new Point(390, 12);
            lblPurBillNo.Name = "lblPurBillNo";
            lblPurBillNo.Size = new Size(80, 15);
            lblPurBillNo.TabIndex = 4;
            lblPurBillNo.Text = "Pur Bill No.";
            //
            // cmbPurBillNo
            //
            cmbPurBillNo.BackColor = Color.FromArgb(224, 224, 224);
            cmbPurBillNo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPurBillNo.FlatStyle = FlatStyle.Flat;
            cmbPurBillNo.Location = new Point(480, 9);
            cmbPurBillNo.Name = "cmbPurBillNo";
            cmbPurBillNo.Size = new Size(120, 23);
            cmbPurBillNo.TabIndex = 5;
            cmbPurBillNo.SelectedIndexChanged += cmbPurBillNo_SelectedIndexChanged;
            //
            // lblPurDate
            //
            lblPurDate.AutoSize = true;
            lblPurDate.Location = new Point(10, 43);
            lblPurDate.Name = "lblPurDate";
            lblPurDate.Size = new Size(56, 15);
            lblPurDate.TabIndex = 6;
            lblPurDate.Text = "Pur. Date";
            //
            // txtPurDate
            //
            txtPurDate.BackColor = Color.WhiteSmoke;
            txtPurDate.BorderStyle = BorderStyle.FixedSingle;
            txtPurDate.Location = new Point(90, 40);
            txtPurDate.Name = "txtPurDate";
            txtPurDate.ReadOnly = true;
            txtPurDate.Size = new Size(90, 23);
            txtPurDate.TabIndex = 7;
            //
            // lblParty
            //
            lblParty.AutoSize = true;
            lblParty.Location = new Point(190, 43);
            lblParty.Name = "lblParty";
            lblParty.Size = new Size(34, 15);
            lblParty.TabIndex = 8;
            lblParty.Text = "Party";
            //
            // txtParty
            //
            txtParty.BackColor = Color.WhiteSmoke;
            txtParty.BorderStyle = BorderStyle.FixedSingle;
            txtParty.Location = new Point(260, 40);
            txtParty.Name = "txtParty";
            txtParty.ReadOnly = true;
            txtParty.Size = new Size(340, 23);
            txtParty.TabIndex = 9;
            //
            // lblVehNo
            //
            lblVehNo.AutoSize = true;
            lblVehNo.Location = new Point(610, 43);
            lblVehNo.Name = "lblVehNo";
            lblVehNo.Size = new Size(50, 15);
            lblVehNo.TabIndex = 10;
            lblVehNo.Text = "Veh. No.";
            //
            // txtVehNo
            //
            txtVehNo.BackColor = Color.FromArgb(224, 224, 224);
            txtVehNo.BorderStyle = BorderStyle.FixedSingle;
            txtVehNo.Location = new Point(690, 40);
            txtVehNo.Name = "txtVehNo";
            txtVehNo.Size = new Size(120, 23);
            txtVehNo.TabIndex = 11;
            //
            // lblLotNo
            //
            lblLotNo.AutoSize = true;
            lblLotNo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLotNo.Location = new Point(10, 74);
            lblLotNo.Name = "lblLotNo";
            lblLotNo.Size = new Size(50, 15);
            lblLotNo.TabIndex = 12;
            lblLotNo.Text = "Lot No.";
            //
            // cmbLotNo
            //
            cmbLotNo.BackColor = Color.FromArgb(224, 224, 224);
            cmbLotNo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLotNo.FlatStyle = FlatStyle.Flat;
            cmbLotNo.Location = new Point(90, 71);
            cmbLotNo.Name = "cmbLotNo";
            cmbLotNo.Size = new Size(90, 23);
            cmbLotNo.TabIndex = 13;
            cmbLotNo.SelectedIndexChanged += cmbLotNo_SelectedIndexChanged;
            //
            // lblItem
            //
            lblItem.AutoSize = true;
            lblItem.Location = new Point(190, 74);
            lblItem.Name = "lblItem";
            lblItem.Size = new Size(28, 15);
            lblItem.TabIndex = 14;
            lblItem.Text = "Item";
            //
            // txtItem
            //
            txtItem.BackColor = Color.WhiteSmoke;
            txtItem.BorderStyle = BorderStyle.FixedSingle;
            txtItem.Location = new Point(260, 71);
            txtItem.Name = "txtItem";
            txtItem.ReadOnly = true;
            txtItem.Size = new Size(150, 23);
            txtItem.TabIndex = 15;
            //
            // lblSplMarko
            //
            lblSplMarko.AutoSize = true;
            lblSplMarko.Location = new Point(420, 74);
            lblSplMarko.Name = "lblSplMarko";
            lblSplMarko.Size = new Size(63, 15);
            lblSplMarko.TabIndex = 16;
            lblSplMarko.Text = "Spl. Marko";
            //
            // txtSplMarko
            //
            txtSplMarko.BackColor = Color.WhiteSmoke;
            txtSplMarko.BorderStyle = BorderStyle.FixedSingle;
            txtSplMarko.Location = new Point(490, 71);
            txtSplMarko.Name = "txtSplMarko";
            txtSplMarko.ReadOnly = true;
            txtSplMarko.Size = new Size(110, 23);
            txtSplMarko.TabIndex = 17;
            //
            // lblCarate
            //
            lblCarate.AutoSize = true;
            lblCarate.Location = new Point(610, 74);
            lblCarate.Name = "lblCarate";
            lblCarate.Size = new Size(41, 15);
            lblCarate.TabIndex = 18;
            lblCarate.Text = "Carate";
            //
            // txtCarate
            //
            txtCarate.BackColor = Color.WhiteSmoke;
            txtCarate.BorderStyle = BorderStyle.FixedSingle;
            txtCarate.Location = new Point(690, 71);
            txtCarate.Name = "txtCarate";
            txtCarate.ReadOnly = true;
            txtCarate.Size = new Size(120, 23);
            txtCarate.TabIndex = 19;
            //
            // lblQty
            //
            lblQty.AutoSize = true;
            lblQty.Location = new Point(10, 105);
            lblQty.Name = "lblQty";
            lblQty.Size = new Size(28, 15);
            lblQty.TabIndex = 20;
            lblQty.Text = "Qty.";
            //
            // txtQty
            //
            txtQty.BackColor = Color.WhiteSmoke;
            txtQty.BorderStyle = BorderStyle.FixedSingle;
            txtQty.Location = new Point(90, 102);
            txtQty.Name = "txtQty";
            txtQty.ReadOnly = true;
            txtQty.Size = new Size(90, 23);
            txtQty.TabIndex = 21;
            //
            // lblWeight
            //
            lblWeight.AutoSize = true;
            lblWeight.Location = new Point(190, 105);
            lblWeight.Name = "lblWeight";
            lblWeight.Size = new Size(45, 15);
            lblWeight.TabIndex = 22;
            lblWeight.Text = "Weight";
            //
            // txtWeight
            //
            txtWeight.BackColor = Color.WhiteSmoke;
            txtWeight.BorderStyle = BorderStyle.FixedSingle;
            txtWeight.Location = new Point(260, 102);
            txtWeight.Name = "txtWeight";
            txtWeight.ReadOnly = true;
            txtWeight.Size = new Size(90, 23);
            txtWeight.TabIndex = 23;
            //
            // lblBalQty
            //
            lblBalQty.AutoSize = true;
            lblBalQty.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBalQty.ForeColor = Color.DarkRed;
            lblBalQty.Location = new Point(360, 105);
            lblBalQty.Name = "lblBalQty";
            lblBalQty.Size = new Size(58, 15);
            lblBalQty.TabIndex = 24;
            lblBalQty.Text = "Bal. Qty.";
            //
            // txtBalQty
            //
            txtBalQty.BackColor = Color.WhiteSmoke;
            txtBalQty.BorderStyle = BorderStyle.FixedSingle;
            txtBalQty.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtBalQty.ForeColor = Color.DarkRed;
            txtBalQty.Location = new Point(430, 102);
            txtBalQty.Name = "txtBalQty";
            txtBalQty.ReadOnly = true;
            txtBalQty.Size = new Size(90, 23);
            txtBalQty.TabIndex = 25;
            //
            // lblRate
            //
            lblRate.AutoSize = true;
            lblRate.Location = new Point(540, 105);
            lblRate.Name = "lblRate";
            lblRate.Size = new Size(33, 15);
            lblRate.TabIndex = 26;
            lblRate.Text = "Rate";
            //
            // txtRate
            //
            txtRate.BackColor = Color.WhiteSmoke;
            txtRate.BorderStyle = BorderStyle.FixedSingle;
            txtRate.Location = new Point(610, 102);
            txtRate.Name = "txtRate";
            txtRate.ReadOnly = true;
            txtRate.Size = new Size(90, 23);
            txtRate.TabIndex = 27;
            //
            // lblAvgRate
            //
            lblAvgRate.AutoSize = true;
            lblAvgRate.Location = new Point(720, 105);
            lblAvgRate.Name = "lblAvgRate";
            lblAvgRate.Size = new Size(58, 15);
            lblAvgRate.TabIndex = 28;
            lblAvgRate.Text = "Avg. Rate";
            //
            // txtAvgRate
            //
            txtAvgRate.BackColor = Color.WhiteSmoke;
            txtAvgRate.BorderStyle = BorderStyle.FixedSingle;
            txtAvgRate.Location = new Point(790, 102);
            txtAvgRate.Name = "txtAvgRate";
            txtAvgRate.ReadOnly = true;
            txtAvgRate.Size = new Size(90, 23);
            txtAvgRate.TabIndex = 29;
            //
            // dgvItems
            //
            dgvItems.AllowUserToResizeRows = false;
            dgvItems.BackgroundColor = Color.White;
            dgvItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvItems.Columns.AddRange(new DataGridViewColumn[] { colBuyerCode, colBuyerName, colQty, colWeight, colRate, colAmount, colLabourRate, colLabourAmt, colApmcAmt, colRemarks });
            dgvItems.Location = new Point(10, 148);
            dgvItems.Name = "dgvItems";
            dgvItems.RowHeadersVisible = false;
            dgvItems.Size = new Size(1000, 300);
            dgvItems.TabIndex = 1;
            dgvItems.CellValueChanged += dgvItems_CellValueChanged;
            dgvItems.CurrentCellDirtyStateChanged += dgvItems_CurrentCellDirtyStateChanged;
            dgvItems.DefaultValuesNeeded += dgvItems_DefaultValuesNeeded;
            dgvItems.RowsRemoved += dgvItems_RowsRemoved;
            dgvItems.KeyDown += dgvItems_KeyDown;
            //
            // colBuyerCode
            //
            colBuyerCode.HeaderText = "Code";
            colBuyerCode.Name = "colBuyerCode";
            colBuyerCode.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            colBuyerCode.FlatStyle = FlatStyle.Flat;
            colBuyerCode.Width = 90;
            //
            // colBuyerName
            //
            colBuyerName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colBuyerName.HeaderText = "Name";
            colBuyerName.Name = "colBuyerName";
            colBuyerName.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            colBuyerName.FlatStyle = FlatStyle.Flat;
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
            colAmount.Width = 90;
            //
            // colLabourRate
            //
            colLabourRate.HeaderText = "Labour Rate";
            colLabourRate.Name = "colLabourRate";
            colLabourRate.Width = 80;
            //
            // colLabourAmt
            //
            colLabourAmt.HeaderText = "Labour Amt";
            colLabourAmt.Name = "colLabourAmt";
            colLabourAmt.ReadOnly = true;
            colLabourAmt.Width = 90;
            //
            // colApmcAmt
            //
            colApmcAmt.HeaderText = "APMC Amt";
            colApmcAmt.Name = "colApmcAmt";
            colApmcAmt.ReadOnly = true;
            colApmcAmt.Width = 90;
            //
            // colRemarks
            //
            colRemarks.HeaderText = "Remarks";
            colRemarks.Name = "colRemarks";
            colRemarks.Width = 150;
            //
            // panelTotals
            //
            panelTotals.Controls.Add(lblTotalQty);
            panelTotals.Controls.Add(txtTotalQty);
            panelTotals.Controls.Add(lblTotalWeight);
            panelTotals.Controls.Add(txtTotalWeight);
            panelTotals.Controls.Add(lblTotalAmount);
            panelTotals.Controls.Add(txtTotalAmount);
            panelTotals.Controls.Add(btnSearch);
            panelTotals.Controls.Add(btnNewBuyer);
            panelTotals.Location = new Point(10, 456);
            panelTotals.Name = "panelTotals";
            panelTotals.Size = new Size(1000, 40);
            panelTotals.TabIndex = 2;
            //
            // lblTotalQty
            //
            lblTotalQty.AutoSize = true;
            lblTotalQty.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalQty.Location = new Point(400, 10);
            lblTotalQty.Name = "lblTotalQty";
            lblTotalQty.Size = new Size(38, 15);
            lblTotalQty.TabIndex = 0;
            lblTotalQty.Text = "Total";
            //
            // txtTotalQty
            //
            txtTotalQty.BackColor = Color.WhiteSmoke;
            txtTotalQty.BorderStyle = BorderStyle.FixedSingle;
            txtTotalQty.Location = new Point(450, 7);
            txtTotalQty.Name = "txtTotalQty";
            txtTotalQty.ReadOnly = true;
            txtTotalQty.Size = new Size(90, 23);
            txtTotalQty.TabIndex = 1;
            //
            // lblTotalWeight
            //
            lblTotalWeight.AutoSize = true;
            lblTotalWeight.Location = new Point(546, 10);
            lblTotalWeight.Name = "lblTotalWeight";
            lblTotalWeight.Size = new Size(0, 15);
            lblTotalWeight.TabIndex = 2;
            //
            // txtTotalWeight
            //
            txtTotalWeight.BackColor = Color.WhiteSmoke;
            txtTotalWeight.BorderStyle = BorderStyle.FixedSingle;
            txtTotalWeight.Location = new Point(546, 7);
            txtTotalWeight.Name = "txtTotalWeight";
            txtTotalWeight.ReadOnly = true;
            txtTotalWeight.Size = new Size(90, 23);
            txtTotalWeight.TabIndex = 3;
            //
            // lblTotalAmount
            //
            lblTotalAmount.AutoSize = true;
            lblTotalAmount.Location = new Point(642, 10);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(0, 15);
            lblTotalAmount.TabIndex = 4;
            //
            // txtTotalAmount
            //
            txtTotalAmount.BackColor = Color.WhiteSmoke;
            txtTotalAmount.BorderStyle = BorderStyle.FixedSingle;
            txtTotalAmount.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtTotalAmount.Location = new Point(642, 7);
            txtTotalAmount.Name = "txtTotalAmount";
            txtTotalAmount.ReadOnly = true;
            txtTotalAmount.Size = new Size(110, 23);
            txtTotalAmount.TabIndex = 5;
            //
            // btnSearch
            //
            btnSearch.BackColor = Color.LightSteelBlue;
            btnSearch.FlatStyle = FlatStyle.Popup;
            btnSearch.Location = new Point(10, 6);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(80, 25);
            btnSearch.TabIndex = 6;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            //
            // btnNewBuyer
            //
            btnNewBuyer.BackColor = Color.LightSteelBlue;
            btnNewBuyer.FlatStyle = FlatStyle.Popup;
            btnNewBuyer.Location = new Point(100, 6);
            btnNewBuyer.Name = "btnNewBuyer";
            btnNewBuyer.Size = new Size(90, 25);
            btnNewBuyer.TabIndex = 7;
            btnNewBuyer.Text = "New Buyer";
            btnNewBuyer.UseVisualStyleBackColor = false;
            btnNewBuyer.Click += btnNewBuyer_Click;
            //
            // panelButtons
            //
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
            panelButtons.Location = new Point(10, 504);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(1000, 40);
            panelButtons.TabIndex = 3;
            //
            // btnAdd
            //
            btnAdd.BackColor = Color.LightSteelBlue;
            btnAdd.FlatStyle = FlatStyle.Popup;
            btnAdd.Location = new Point(0, 6);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(80, 28);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            //
            // btnUpdate
            //
            btnUpdate.BackColor = Color.LightSteelBlue;
            btnUpdate.FlatStyle = FlatStyle.Popup;
            btnUpdate.Location = new Point(90, 6);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(80, 28);
            btnUpdate.TabIndex = 1;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            //
            // btnDelete
            //
            btnDelete.BackColor = Color.LightSteelBlue;
            btnDelete.FlatStyle = FlatStyle.Popup;
            btnDelete.Location = new Point(180, 6);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 28);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            //
            // btnSave
            //
            btnSave.BackColor = Color.LightSteelBlue;
            btnSave.FlatStyle = FlatStyle.Popup;
            btnSave.Location = new Point(270, 6);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(80, 28);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            //
            // btnPrevious
            //
            btnPrevious.BackColor = Color.LightSteelBlue;
            btnPrevious.FlatStyle = FlatStyle.Popup;
            btnPrevious.Location = new Point(360, 6);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(80, 28);
            btnPrevious.TabIndex = 4;
            btnPrevious.Text = "Previous";
            btnPrevious.UseVisualStyleBackColor = false;
            btnPrevious.Click += btnPrevious_Click;
            //
            // btnNext
            //
            btnNext.BackColor = Color.LightSteelBlue;
            btnNext.FlatStyle = FlatStyle.Popup;
            btnNext.Location = new Point(450, 6);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(80, 28);
            btnNext.TabIndex = 5;
            btnNext.Text = "Next";
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += btnNext_Click;
            //
            // btnFind
            //
            btnFind.BackColor = Color.LightSteelBlue;
            btnFind.FlatStyle = FlatStyle.Popup;
            btnFind.Location = new Point(540, 6);
            btnFind.Name = "btnFind";
            btnFind.Size = new Size(80, 28);
            btnFind.TabIndex = 6;
            btnFind.Text = "Find";
            btnFind.UseVisualStyleBackColor = false;
            btnFind.Click += btnFind_Click;
            //
            // btnClose
            //
            btnClose.BackColor = Color.LightSteelBlue;
            btnClose.FlatStyle = FlatStyle.Popup;
            btnClose.Location = new Point(630, 6);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(80, 28);
            btnClose.TabIndex = 7;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            //
            // btnPrint
            //
            btnPrint.BackColor = Color.LightSteelBlue;
            btnPrint.FlatStyle = FlatStyle.Popup;
            btnPrint.Location = new Point(720, 6);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(80, 28);
            btnPrint.TabIndex = 8;
            btnPrint.Text = "Print";
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;
            //
            // btnWhatsapp
            //
            btnWhatsapp.BackColor = Color.LightSteelBlue;
            btnWhatsapp.FlatStyle = FlatStyle.Popup;
            btnWhatsapp.Location = new Point(810, 6);
            btnWhatsapp.Name = "btnWhatsapp";
            btnWhatsapp.Size = new Size(80, 28);
            btnWhatsapp.TabIndex = 9;
            btnWhatsapp.Text = "WhatsApp";
            btnWhatsapp.UseVisualStyleBackColor = false;
            btnWhatsapp.Click += btnWhatsapp_Click;
            //
            // SalesForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(1024, 556);
            Controls.Add(panelHeader);
            Controls.Add(dgvItems);
            Controls.Add(panelTotals);
            Controls.Add(panelButtons);
            Name = "SalesForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sales";
            Load += SalesForm_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvItems).EndInit();
            panelTotals.ResumeLayout(false);
            panelTotals.PerformLayout();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelHeader;
        private Label lblInvNo;
        private TextBox txtInvNo;
        private Label lblSaleDate;
        private DateTimePicker dtpSaleDate;
        private Label lblPurBillNo;
        private ComboBox cmbPurBillNo;
        private Label lblPurDate;
        private TextBox txtPurDate;
        private Label lblParty;
        private TextBox txtParty;
        private Label lblVehNo;
        private TextBox txtVehNo;
        private Label lblLotNo;
        private ComboBox cmbLotNo;
        private Label lblItem;
        private TextBox txtItem;
        private Label lblSplMarko;
        private TextBox txtSplMarko;
        private Label lblCarate;
        private TextBox txtCarate;
        private Label lblQty;
        private TextBox txtQty;
        private Label lblWeight;
        private TextBox txtWeight;
        private Label lblBalQty;
        private TextBox txtBalQty;
        private Label lblRate;
        private TextBox txtRate;
        private Label lblAvgRate;
        private TextBox txtAvgRate;
        private DataGridView dgvItems;
        private DataGridViewComboBoxColumn colBuyerCode;
        private DataGridViewComboBoxColumn colBuyerName;
        private DataGridViewTextBoxColumn colQty;
        private DataGridViewTextBoxColumn colWeight;
        private DataGridViewTextBoxColumn colRate;
        private DataGridViewTextBoxColumn colAmount;
        private DataGridViewTextBoxColumn colLabourRate;
        private DataGridViewTextBoxColumn colLabourAmt;
        private DataGridViewTextBoxColumn colApmcAmt;
        private DataGridViewTextBoxColumn colRemarks;
        private Panel panelTotals;
        private Label lblTotalQty;
        private TextBox txtTotalQty;
        private Label lblTotalWeight;
        private TextBox txtTotalWeight;
        private Label lblTotalAmount;
        private TextBox txtTotalAmount;
        private Button btnSearch;
        private Button btnNewBuyer;
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
    }
}
