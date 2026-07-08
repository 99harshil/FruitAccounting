namespace FruitAccounting.UI
{
    partial class FinancialYearManagementForm
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
            dgvFinancialYears = new DataGridView();
            colYearCode = new DataGridViewTextBoxColumn();
            colYearName = new DataGridViewTextBoxColumn();
            panelButtonDock = new Panel();
            btnAdd = new Button();
            btnCancel = new Button();
            btnDelete = new Button();
            btnEdit = new Button();
            lblError = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvFinancialYears).BeginInit();
            panelButtonDock.SuspendLayout();
            SuspendLayout();
            //
            // dgvFinancialYears
            //
            dgvFinancialYears.AllowUserToAddRows = false;
            dgvFinancialYears.AllowUserToDeleteRows = false;
            dgvFinancialYears.AllowUserToResizeRows = false;
            dgvFinancialYears.BackgroundColor = Color.Gray;
            dgvFinancialYears.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFinancialYears.Columns.AddRange(new DataGridViewColumn[] { colYearCode, colYearName });
            dgvFinancialYears.Location = new Point(24, 20);
            dgvFinancialYears.MultiSelect = false;
            dgvFinancialYears.Name = "dgvFinancialYears";
            dgvFinancialYears.ReadOnly = true;
            dgvFinancialYears.RowHeadersVisible = false;
            dgvFinancialYears.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFinancialYears.Size = new Size(468, 240);
            dgvFinancialYears.TabIndex = 0;
            //
            // colYearCode
            //
            colYearCode.HeaderText = "Year Code";
            colYearCode.Name = "colYearCode";
            colYearCode.ReadOnly = true;
            colYearCode.Width = 120;
            //
            // colYearName
            //
            colYearName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colYearName.HeaderText = "Financial Year Period";
            colYearName.Name = "colYearName";
            colYearName.ReadOnly = true;
            //
            // panelButtonDock
            //
            panelButtonDock.BorderStyle = BorderStyle.FixedSingle;
            panelButtonDock.Controls.Add(btnAdd);
            panelButtonDock.Controls.Add(btnCancel);
            panelButtonDock.Controls.Add(btnDelete);
            panelButtonDock.Controls.Add(btnEdit);
            panelButtonDock.Location = new Point(83, 285);
            panelButtonDock.Name = "panelButtonDock";
            panelButtonDock.Size = new Size(350, 42);
            panelButtonDock.TabIndex = 1;
            //
            // btnAdd
            //
            btnAdd.BackColor = Color.LightSteelBlue;
            btnAdd.FlatStyle = FlatStyle.Popup;
            btnAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAdd.Location = new Point(15, 7);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 26);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "&Add";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            //
            // btnCancel
            //
            btnCancel.BackColor = Color.LightSteelBlue;
            btnCancel.FlatStyle = FlatStyle.Popup;
            btnCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancel.Location = new Point(96, 7);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 26);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "&Exit";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            //
            // btnDelete
            //
            btnDelete.BackColor = Color.LightSteelBlue;
            btnDelete.FlatStyle = FlatStyle.Popup;
            btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDelete.Location = new Point(177, 7);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 26);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "&Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            //
            // btnEdit
            //
            btnEdit.BackColor = Color.LightSteelBlue;
            btnEdit.FlatStyle = FlatStyle.Popup;
            btnEdit.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEdit.Location = new Point(258, 7);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(75, 26);
            btnEdit.TabIndex = 5;
            btnEdit.Text = "&Update";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            //
            // lblError
            //
            lblError.AutoSize = true;
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(24, 342);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 15);
            lblError.TabIndex = 6;
            //
            // FinancialYearManagementForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(516, 352);
            Controls.Add(lblError);
            Controls.Add(panelButtonDock);
            Controls.Add(dgvFinancialYears);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FinancialYearManagementForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Financial Year Information";
            Load += FinancialYearManagementForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvFinancialYears).EndInit();
            panelButtonDock.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvFinancialYears;
        private DataGridViewTextBoxColumn colYearCode;
        private DataGridViewTextBoxColumn colYearName;
        private Panel panelButtonDock;
        private Button btnAdd;
        private Button btnCancel;
        private Button btnDelete;
        private Button btnEdit;
        private Label lblError;
    }
}
