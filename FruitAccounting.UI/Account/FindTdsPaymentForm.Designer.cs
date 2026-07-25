namespace FruitAccounting.UI
{
    partial class FindTdsPaymentForm
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
            colSrNo = new DataGridViewTextBoxColumn();
            colDate = new DataGridViewTextBoxColumn();
            colBsrCode = new DataGridViewTextBoxColumn();
            colChallanSerialNo = new DataGridViewTextBoxColumn();
            colTotalAmount = new DataGridViewTextBoxColumn();
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
            txtSearch.Size = new Size(760, 23);
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
            dgvGroups.Columns.AddRange(new DataGridViewColumn[] { colSrNo, colDate, colBsrCode, colChallanSerialNo, colTotalAmount, colId });
            dgvGroups.Location = new Point(12, 65);
            dgvGroups.MultiSelect = false;
            dgvGroups.Name = "dgvGroups";
            dgvGroups.ReadOnly = true;
            dgvGroups.RowHeadersVisible = false;
            dgvGroups.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGroups.Size = new Size(760, 380);
            dgvGroups.TabIndex = 2;
            dgvGroups.CellDoubleClick += dgvGroups_CellDoubleClick;
            dgvGroups.KeyDown += dgvGroups_KeyDown;
            //
            // colSrNo
            //
            colSrNo.HeaderText = "Sr.No";
            colSrNo.Name = "colSrNo";
            colSrNo.ReadOnly = true;
            colSrNo.Width = 60;
            //
            // colDate
            //
            colDate.HeaderText = "Date";
            colDate.Name = "colDate";
            colDate.ReadOnly = true;
            colDate.Width = 90;
            //
            // colBsrCode
            //
            colBsrCode.HeaderText = "BSR Code";
            colBsrCode.Name = "colBsrCode";
            colBsrCode.ReadOnly = true;
            colBsrCode.Width = 100;
            //
            // colChallanSerialNo
            //
            colChallanSerialNo.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colChallanSerialNo.HeaderText = "Ch.Sr.No";
            colChallanSerialNo.Name = "colChallanSerialNo";
            colChallanSerialNo.ReadOnly = true;
            //
            // colTotalAmount
            //
            colTotalAmount.HeaderText = "Total Amount";
            colTotalAmount.Name = "colTotalAmount";
            colTotalAmount.ReadOnly = true;
            colTotalAmount.Width = 110;
            //
            // btnFind
            //
            btnFind.BackColor = Color.LightSteelBlue;
            btnFind.FlatStyle = FlatStyle.Popup;
            btnFind.Location = new Point(780, 34);
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
            btnSelect.Location = new Point(780, 65);
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
            btnCancel.Location = new Point(780, 96);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(85, 25);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            //
            // FindTdsPaymentForm
            //
            AcceptButton = btnSelect;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(880, 461);
            Controls.Add(btnCancel);
            Controls.Add(btnSelect);
            Controls.Add(btnFind);
            Controls.Add(dgvGroups);
            Controls.Add(txtSearch);
            Controls.Add(chkMatchCase);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FindTdsPaymentForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "TDS Payment List";
            Load += FindTdsPaymentForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvGroups).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        protected CheckBox chkMatchCase;
        protected TextBox txtSearch;
        private DataGridView dgvGroups;
        private DataGridViewTextBoxColumn colSrNo;
        private DataGridViewTextBoxColumn colDate;
        private DataGridViewTextBoxColumn colBsrCode;
        private DataGridViewTextBoxColumn colChallanSerialNo;
        private DataGridViewTextBoxColumn colTotalAmount;
        private DataGridViewTextBoxColumn colId;
        protected Button btnFind;
        protected Button btnSelect;
        protected Button btnCancel;
    }
}
