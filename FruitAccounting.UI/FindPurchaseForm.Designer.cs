namespace FruitAccounting.UI
{
    partial class FindPurchaseForm
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
            chkMatchCase = new CheckBox();
            txtSearch = new TextBox();
            dgvGroups = new DataGridView();
            colInvNo = new DataGridViewTextBoxColumn();
            colInvDt = new DataGridViewTextBoxColumn();
            colCode = new DataGridViewTextBoxColumn();
            colParty = new DataGridViewTextBoxColumn();
            colTruck = new DataGridViewTextBoxColumn();
            colGross = new DataGridViewTextBoxColumn();
            colNetAmt = new DataGridViewTextBoxColumn();
            colMarko = new DataGridViewTextBoxColumn();
            colId = new DataGridViewTextBoxColumn();
            btnFind = new Button();
            btnSelect = new Button();
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvGroups).BeginInit();
            SuspendLayout();
            //
            // chkMatchCase
            //
            chkMatchCase.AutoSize = true;
            chkMatchCase.Location = new Point(12, 12);
            chkMatchCase.Name = "chkMatchCase";
            chkMatchCase.Size = new Size(88, 19);
            chkMatchCase.TabIndex = 0;
            chkMatchCase.Text = "Match Case";
            chkMatchCase.UseVisualStyleBackColor = true;
            //
            // txtSearch
            //
            txtSearch.BackColor = Color.FromArgb(224, 224, 224);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Location = new Point(12, 35);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(900, 23);
            txtSearch.TabIndex = 1;
            txtSearch.TextChanged += txtSearch_TextChanged;
            //
            // dgvGroups
            //
            dgvGroups.AllowUserToAddRows = false;
            dgvGroups.AllowUserToDeleteRows = false;
            dgvGroups.AllowUserToResizeRows = false;
            dgvGroups.BackgroundColor = Color.White;
            dgvGroups.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvGroups.Columns.AddRange(new DataGridViewColumn[] { colInvNo, colInvDt, colCode, colParty, colTruck, colGross, colNetAmt, colMarko, colId });
            dgvGroups.Location = new Point(12, 65);
            dgvGroups.MultiSelect = false;
            dgvGroups.Name = "dgvGroups";
            dgvGroups.ReadOnly = true;
            dgvGroups.RowHeadersVisible = false;
            dgvGroups.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGroups.Size = new Size(900, 420);
            dgvGroups.TabIndex = 2;
            dgvGroups.CellDoubleClick += dgvGroups_CellDoubleClick;
            dgvGroups.KeyDown += dgvGroups_KeyDown;
            //
            // colInvNo
            //
            colInvNo.HeaderText = "Invno";
            colInvNo.Name = "colInvNo";
            colInvNo.ReadOnly = true;
            colInvNo.Width = 60;
            //
            // colInvDt
            //
            colInvDt.HeaderText = "Invdt";
            colInvDt.Name = "colInvDt";
            colInvDt.ReadOnly = true;
            colInvDt.Width = 90;
            //
            // colCode
            //
            colCode.HeaderText = "Code";
            colCode.Name = "colCode";
            colCode.ReadOnly = true;
            colCode.Width = 70;
            //
            // colParty
            //
            colParty.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colParty.HeaderText = "Party";
            colParty.Name = "colParty";
            colParty.ReadOnly = true;
            //
            // colTruck
            //
            colTruck.HeaderText = "Truck";
            colTruck.Name = "colTruck";
            colTruck.ReadOnly = true;
            colTruck.Width = 110;
            //
            // colGross
            //
            colGross.HeaderText = "Gross";
            colGross.Name = "colGross";
            colGross.ReadOnly = true;
            colGross.Width = 90;
            //
            // colNetAmt
            //
            colNetAmt.HeaderText = "Netamt";
            colNetAmt.Name = "colNetAmt";
            colNetAmt.ReadOnly = true;
            colNetAmt.Width = 90;
            //
            // colMarko
            //
            colMarko.HeaderText = "Marko";
            colMarko.Name = "colMarko";
            colMarko.ReadOnly = true;
            colMarko.Width = 90;
            //
            // btnFind
            //
            btnFind.BackColor = Color.LightSteelBlue;
            btnFind.FlatStyle = FlatStyle.Popup;
            btnFind.Location = new Point(920, 34);
            btnFind.Name = "btnFind";
            btnFind.Size = new Size(85, 25);
            btnFind.TabIndex = 3;
            btnFind.Text = "Find";
            btnFind.UseVisualStyleBackColor = false;
            btnFind.Click += btnFind_Click;
            //
            // btnSelect
            //
            btnSelect.BackColor = Color.LightSteelBlue;
            btnSelect.FlatStyle = FlatStyle.Popup;
            btnSelect.Location = new Point(920, 65);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(85, 25);
            btnSelect.TabIndex = 4;
            btnSelect.Text = "Select";
            btnSelect.UseVisualStyleBackColor = false;
            btnSelect.Click += btnSelect_Click;
            //
            // btnCancel
            //
            btnCancel.BackColor = Color.LightSteelBlue;
            btnCancel.FlatStyle = FlatStyle.Popup;
            btnCancel.Location = new Point(920, 96);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(85, 25);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            //
            // FindPurchaseForm
            //
            AcceptButton = btnSelect;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(1020, 500);
            Controls.Add(btnCancel);
            Controls.Add(btnSelect);
            Controls.Add(btnFind);
            Controls.Add(dgvGroups);
            Controls.Add(txtSearch);
            Controls.Add(chkMatchCase);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FindPurchaseForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Purchase List";
            Load += FindPurchaseForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvGroups).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        protected CheckBox chkMatchCase;
        protected TextBox txtSearch;
        private DataGridView dgvGroups;
        private DataGridViewTextBoxColumn colInvNo;
        private DataGridViewTextBoxColumn colInvDt;
        private DataGridViewTextBoxColumn colCode;
        private DataGridViewTextBoxColumn colParty;
        private DataGridViewTextBoxColumn colTruck;
        private DataGridViewTextBoxColumn colGross;
        private DataGridViewTextBoxColumn colNetAmt;
        private DataGridViewTextBoxColumn colMarko;
        private DataGridViewTextBoxColumn colId;
        protected Button btnFind;
        protected Button btnSelect;
        protected Button btnCancel;
    }
}
