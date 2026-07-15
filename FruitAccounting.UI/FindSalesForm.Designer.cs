namespace FruitAccounting.UI
{
    partial class FindSalesForm
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
            colLotNo = new DataGridViewTextBoxColumn();
            colInvNo = new DataGridViewTextBoxColumn();
            colInvDt = new DataGridViewTextBoxColumn();
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
            txtSearch.Size = new Size(360, 23);
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
            dgvGroups.Columns.AddRange(new DataGridViewColumn[] { colLotNo, colInvNo, colInvDt });
            dgvGroups.Location = new Point(12, 65);
            dgvGroups.MultiSelect = false;
            dgvGroups.Name = "dgvGroups";
            dgvGroups.ReadOnly = true;
            dgvGroups.RowHeadersVisible = false;
            dgvGroups.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvGroups.Size = new Size(360, 380);
            dgvGroups.TabIndex = 2;
            dgvGroups.CellDoubleClick += dgvGroups_CellDoubleClick;
            dgvGroups.KeyDown += dgvGroups_KeyDown;
            //
            // colLotNo
            //
            colLotNo.HeaderText = "Lotno";
            colLotNo.Name = "colLotNo";
            colLotNo.ReadOnly = true;
            colLotNo.Width = 100;
            //
            // colInvNo
            //
            colInvNo.HeaderText = "Invno";
            colInvNo.Name = "colInvNo";
            colInvNo.ReadOnly = true;
            colInvNo.Width = 100;
            //
            // colInvDt
            //
            colInvDt.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colInvDt.HeaderText = "Invdt";
            colInvDt.Name = "colInvDt";
            colInvDt.ReadOnly = true;
            //
            // btnFind
            //
            btnFind.BackColor = Color.LightSteelBlue;
            btnFind.FlatStyle = FlatStyle.Popup;
            btnFind.Location = new Point(382, 34);
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
            btnSelect.Location = new Point(382, 65);
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
            btnCancel.Location = new Point(382, 96);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(85, 25);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            //
            // FindSalesForm
            //
            AcceptButton = btnSelect;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            ClientSize = new Size(482, 461);
            Controls.Add(btnCancel);
            Controls.Add(btnSelect);
            Controls.Add(btnFind);
            Controls.Add(dgvGroups);
            Controls.Add(txtSearch);
            Controls.Add(chkMatchCase);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FindSalesForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Sales List";
            Load += FindSalesForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvGroups).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        protected CheckBox chkMatchCase;
        protected TextBox txtSearch;
        private DataGridView dgvGroups;
        private DataGridViewTextBoxColumn colLotNo;
        private DataGridViewTextBoxColumn colInvNo;
        private DataGridViewTextBoxColumn colInvDt;
        protected Button btnFind;
        protected Button btnSelect;
        protected Button btnCancel;
    }
}
