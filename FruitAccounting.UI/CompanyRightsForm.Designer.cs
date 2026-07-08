namespace FruitAccounting.UI
{
    partial class CompanyRightsForm
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
            lblUserIdCompany = new Label();
            cmbUserIdCompany = new ComboBox();
            dgvCompanyRights = new DataGridView();
            colCompanyName = new DataGridViewTextBoxColumn();
            colYear = new DataGridViewTextBoxColumn();
            colRight = new DataGridViewCheckBoxColumn();
            btnSave = new Button();
            btnClose = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCompanyRights).BeginInit();
            SuspendLayout();

            //
            // lblUserIdCompany
            //
            lblUserIdCompany.AutoSize = true;
            lblUserIdCompany.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUserIdCompany.Location = new Point(21, 17);
            lblUserIdCompany.Name = "lblUserIdCompany";
            lblUserIdCompany.Size = new Size(49, 15);
            lblUserIdCompany.TabIndex = 1;
            lblUserIdCompany.Text = "User Id";

            //
            // cmbUserIdCompany
            //
            cmbUserIdCompany.BackColor = Color.FromArgb(224, 224, 224);
            cmbUserIdCompany.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUserIdCompany.Location = new Point(76, 14);
            cmbUserIdCompany.Name = "cmbUserIdCompany";
            cmbUserIdCompany.Size = new Size(200, 23);
            cmbUserIdCompany.TabIndex = 2;
            cmbUserIdCompany.SelectedIndexChanged += cmbUserIdCompany_SelectedIndexChanged;

            //
            // dgvCompanyRights
            //
            dgvCompanyRights.AllowUserToAddRows = false;
            dgvCompanyRights.AllowUserToDeleteRows = false;
            dgvCompanyRights.AllowUserToResizeRows = false;
            dgvCompanyRights.BackgroundColor = Color.White;
            dgvCompanyRights.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCompanyRights.Columns.AddRange(new DataGridViewColumn[] { colCompanyName, colYear, colRight });
            dgvCompanyRights.Location = new Point(21, 46);
            dgvCompanyRights.MultiSelect = false;
            dgvCompanyRights.Name = "dgvCompanyRights";
            dgvCompanyRights.RowHeadersVisible = false;
            dgvCompanyRights.Size = new Size(440, 300);
            dgvCompanyRights.TabIndex = 3;

            //
            // colCompanyName
            //
            colCompanyName.HeaderText = "Company Name";
            colCompanyName.Name = "colCompanyName";
            colCompanyName.ReadOnly = true;
            colCompanyName.Width = 200;

            //
            // colYear
            //
            colYear.HeaderText = "Year";
            colYear.Name = "colYear";
            colYear.ReadOnly = true;
            colYear.Width = 80;

            //
            // colRight
            //
            colRight.HeaderText = "Right";
            colRight.Name = "colRight";
            colRight.Width = 50;

            //
            // btnSave
            //
            btnSave.BackColor = Color.CornflowerBlue;
            btnSave.FlatStyle = FlatStyle.Popup;
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.Location = new Point(305, 354);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 26);
            btnSave.TabIndex = 4;
            btnSave.Text = "&Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;

            //
            // btnClose
            //
            btnClose.BackColor = Color.CornflowerBlue;
            btnClose.FlatStyle = FlatStyle.Popup;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClose.Location = new Point(386, 354);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 26);
            btnClose.TabIndex = 5;
            btnClose.Text = "&Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;

            //
            // CompanyRightsForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(482, 397);
            Controls.Add(btnClose);
            Controls.Add(btnSave);
            Controls.Add(dgvCompanyRights);
            Controls.Add(cmbUserIdCompany);
            Controls.Add(lblUserIdCompany);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CompanyRightsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Company Rights";
            Load += CompanyRightsForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCompanyRights).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblUserIdCompany;
        private ComboBox cmbUserIdCompany;
        private DataGridView dgvCompanyRights;
        private DataGridViewTextBoxColumn colCompanyName;
        private DataGridViewTextBoxColumn colYear;
        private DataGridViewCheckBoxColumn colRight;
        private Button btnSave;
        private Button btnClose;
    }
}
