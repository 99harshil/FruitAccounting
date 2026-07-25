namespace FruitAccounting.UI
{
    partial class UserRightsForm
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

        private void InitializeComponent()
        {
            lblUserIdRight = new Label();
            cmbUserIdRight = new ComboBox();
            tvMenuHierarchy = new TreeView();
            dgvPermissions = new DataGridView();
            colPermissionName = new DataGridViewTextBoxColumn();
            colView = new DataGridViewTextBoxColumn();
            colAdd = new DataGridViewTextBoxColumn();
            colModify = new DataGridViewTextBoxColumn();
            colDelete = new DataGridViewTextBoxColumn();
            btnSelectAll = new Button();
            btnClearAll = new Button();
            btnSave = new Button();
            btnClose = new Button();
            splitter = new SplitContainer();
            ((System.ComponentModel.ISupportInitialize)dgvPermissions).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitter).BeginInit();
            splitter.Panel1.SuspendLayout();
            splitter.Panel2.SuspendLayout();
            splitter.SuspendLayout();
            SuspendLayout();

            //
            // lblUserIdRight
            //
            lblUserIdRight.AutoSize = true;
            lblUserIdRight.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUserIdRight.Location = new Point(21, 17);
            lblUserIdRight.Name = "lblUserIdRight";
            lblUserIdRight.Size = new Size(49, 15);
            lblUserIdRight.TabIndex = 1;
            lblUserIdRight.Text = "User Id";

            //
            // cmbUserIdRight
            //
            cmbUserIdRight.BackColor = Color.FromArgb(224, 224, 224);
            cmbUserIdRight.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUserIdRight.Location = new Point(76, 14);
            cmbUserIdRight.Name = "cmbUserIdRight";
            cmbUserIdRight.Size = new Size(200, 23);
            cmbUserIdRight.TabIndex = 2;
            cmbUserIdRight.SelectedIndexChanged += cmbUserIdRight_SelectedIndexChanged;

            //
            // tvMenuHierarchy
            //
            tvMenuHierarchy.BackColor = Color.White;
            tvMenuHierarchy.Dock = DockStyle.Fill;
            tvMenuHierarchy.Location = new Point(0, 0);
            tvMenuHierarchy.Name = "tvMenuHierarchy";
            tvMenuHierarchy.Size = new Size(400, 350);
            tvMenuHierarchy.TabIndex = 3;

            //
            // dgvPermissions
            //
            dgvPermissions.AllowUserToAddRows = false;
            dgvPermissions.AllowUserToDeleteRows = false;
            dgvPermissions.AllowUserToResizeRows = false;
            dgvPermissions.BackgroundColor = Color.White;
            dgvPermissions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPermissions.Columns.AddRange(new DataGridViewColumn[] { colPermissionName, colView, colAdd, colModify, colDelete });
            dgvPermissions.Dock = DockStyle.Fill;
            dgvPermissions.Location = new Point(0, 0);
            dgvPermissions.Name = "dgvPermissions";
            dgvPermissions.RowHeadersVisible = false;
            dgvPermissions.Size = new Size(550, 350);
            dgvPermissions.TabIndex = 4;

            //
            // colPermissionName
            //
            colPermissionName.HeaderText = "Menu Item";
            colPermissionName.Name = "colPermissionName";
            colPermissionName.ReadOnly = true;
            colPermissionName.Width = 200;

            //
            // colView
            //
            colView.HeaderText = "View";
            colView.Name = "colView";
            colView.ReadOnly = true;
            colView.Width = 50;

            //
            // colAdd
            //
            colAdd.HeaderText = "Add";
            colAdd.Name = "colAdd";
            colAdd.ReadOnly = true;
            colAdd.Width = 50;

            //
            // colModify
            //
            colModify.HeaderText = "Modify";
            colModify.Name = "colModify";
            colModify.ReadOnly = true;
            colModify.Width = 60;

            //
            // colDelete
            //
            colDelete.HeaderText = "Delete";
            colDelete.Name = "colDelete";
            colDelete.ReadOnly = true;
            colDelete.Width = 60;

            //
            // splitter
            //
            splitter.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitter.Location = new Point(21, 50);
            splitter.Name = "splitter";
            splitter.Size = new Size(960, 350);
            splitter.SplitterDistance = 400;
            splitter.TabIndex = 5;

            splitter.Panel1.Controls.Add(tvMenuHierarchy);
            splitter.Panel2.Controls.Add(dgvPermissions);

            //
            // btnSelectAll
            //
            btnSelectAll.BackColor = Color.CornflowerBlue;
            btnSelectAll.FlatStyle = FlatStyle.Popup;
            btnSelectAll.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSelectAll.Location = new Point(21, 410);
            btnSelectAll.Name = "btnSelectAll";
            btnSelectAll.Size = new Size(110, 26);
            btnSelectAll.TabIndex = 6;
            btnSelectAll.Text = "Select &All";
            btnSelectAll.UseVisualStyleBackColor = false;
            btnSelectAll.Click += (s, e) => SelectAllPermissions();

            //
            // btnClearAll
            //
            btnClearAll.BackColor = Color.CornflowerBlue;
            btnClearAll.FlatStyle = FlatStyle.Popup;
            btnClearAll.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClearAll.Location = new Point(137, 410);
            btnClearAll.Name = "btnClearAll";
            btnClearAll.Size = new Size(110, 26);
            btnClearAll.TabIndex = 7;
            btnClearAll.Text = "Clear &All";
            btnClearAll.UseVisualStyleBackColor = false;
            btnClearAll.Click += (s, e) => ClearAllPermissions();

            //
            // btnSave
            //
            btnSave.BackColor = Color.CornflowerBlue;
            btnSave.FlatStyle = FlatStyle.Popup;
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.Location = new Point(825, 410);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 26);
            btnSave.TabIndex = 8;
            btnSave.Text = "&Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;

            //
            // btnClose
            //
            btnClose.BackColor = Color.CornflowerBlue;
            btnClose.FlatStyle = FlatStyle.Popup;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClose.Location = new Point(906, 410);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 26);
            btnClose.TabIndex = 9;
            btnClose.Text = "&Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;

            //
            // UserRightsForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1000, 450);
            Controls.Add(btnClose);
            Controls.Add(btnSave);
            Controls.Add(btnClearAll);
            Controls.Add(btnSelectAll);
            Controls.Add(splitter);
            Controls.Add(cmbUserIdRight);
            Controls.Add(lblUserIdRight);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = false;
            Name = "UserRightsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "User Rights";
            Load += UserRightsForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPermissions).EndInit();
            splitter.Panel1.ResumeLayout(false);
            splitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitter).EndInit();
            splitter.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblUserIdRight;
        private ComboBox cmbUserIdRight;
        private TreeView tvMenuHierarchy;
        private DataGridView dgvPermissions;
        private DataGridViewTextBoxColumn colPermissionName;
        private DataGridViewTextBoxColumn colView;
        private DataGridViewTextBoxColumn colAdd;
        private DataGridViewTextBoxColumn colModify;
        private DataGridViewTextBoxColumn colDelete;
        private Button btnSelectAll;
        private Button btnClearAll;
        private Button btnSave;
        private Button btnClose;
        private SplitContainer splitter;
    }
}
