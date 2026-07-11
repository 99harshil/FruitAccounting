namespace FruitAccounting.UI
{
    partial class FindReceiptForm
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
            colRecNo = new DataGridViewTextBoxColumn();
            colRDate = new DataGridViewTextBoxColumn();
            colCode = new DataGridViewTextBoxColumn();
            colAccount = new DataGridViewTextBoxColumn();
            colDaybook = new DataGridViewTextBoxColumn();
            colChqNo = new DataGridViewTextBoxColumn();
            colBank = new DataGridViewTextBoxColumn();
            colRecAmt = new DataGridViewTextBoxColumn();
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
            dgvGroups.Columns.AddRange(new DataGridViewColumn[] { colRecNo, colRDate, colCode, colAccount, colDaybook, colChqNo, colBank, colRecAmt, colId });
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
            // colRecNo
            //
            colRecNo.HeaderText = "Recno";
            colRecNo.Name = "colRecNo";
            colRecNo.ReadOnly = true;
            colRecNo.Width = 60;
            //
            // colRDate
            //
            colRDate.HeaderText = "Rdate";
            colRDate.Name = "colRDate";
            colRDate.ReadOnly = true;
            colRDate.Width = 80;
            //
            // colCode
            //
            colCode.HeaderText = "Code";
            colCode.Name = "colCode";
            colCode.ReadOnly = true;
            colCode.Width = 70;
            //
            // colAccount
            //
            colAccount.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colAccount.HeaderText = "Account";
            colAccount.Name = "colAccount";
            colAccount.ReadOnly = true;
            //
            // colDaybook
            //
            colDaybook.HeaderText = "Daybook";
            colDaybook.Name = "colDaybook";
            colDaybook.ReadOnly = true;
            colDaybook.Width = 130;
            //
            // colChqNo
            //
            colChqNo.HeaderText = "Chqno";
            colChqNo.Name = "colChqNo";
            colChqNo.ReadOnly = true;
            colChqNo.Width = 70;
            //
            // colBank
            //
            colBank.HeaderText = "Bank";
            colBank.Name = "colBank";
            colBank.ReadOnly = true;
            colBank.Width = 100;
            //
            // colRecAmt
            //
            colRecAmt.HeaderText = "Recamt";
            colRecAmt.Name = "colRecAmt";
            colRecAmt.ReadOnly = true;
            colRecAmt.Width = 90;
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
            // FindReceiptForm
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
            Name = "FindReceiptForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Receipt List";
            Load += FindReceiptForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvGroups).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        protected CheckBox chkMatchCase;
        protected TextBox txtSearch;
        private DataGridView dgvGroups;
        private DataGridViewTextBoxColumn colRecNo;
        private DataGridViewTextBoxColumn colRDate;
        private DataGridViewTextBoxColumn colCode;
        private DataGridViewTextBoxColumn colAccount;
        private DataGridViewTextBoxColumn colDaybook;
        private DataGridViewTextBoxColumn colChqNo;
        private DataGridViewTextBoxColumn colBank;
        private DataGridViewTextBoxColumn colRecAmt;
        private DataGridViewTextBoxColumn colId;
        protected Button btnFind;
        protected Button btnSelect;
        protected Button btnCancel;
    }
}
