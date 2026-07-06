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

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblCompanyName = new Label();
            lstFinancialYears = new ListBox();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnSelect = new Button();
            btnCancel = new Button();
            lblError = new Label();
            SuspendLayout();

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(150, 20);
            lblTitle.Size = new Size(231, 25);
            lblTitle.Text = "Financial Year Management";

            lblCompanyName.AutoSize = true;
            lblCompanyName.Location = new Point(20, 70);
            lblCompanyName.Size = new Size(94, 15);
            lblCompanyName.Text = "Company: ";

            lstFinancialYears.FormattingEnabled = true;
            lstFinancialYears.ItemHeight = 15;
            lstFinancialYears.Location = new Point(20, 100);
            lstFinancialYears.Name = "lstFinancialYears";
            lstFinancialYears.Size = new Size(420, 184);
            lstFinancialYears.TabIndex = 2;

            btnAdd.Location = new Point(20, 300);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 23);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += this.btnAdd_Click;

            btnEdit.Location = new Point(105, 300);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(75, 23);
            btnEdit.TabIndex = 4;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += this.btnEdit_Click;

            btnDelete.Location = new Point(190, 300);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += this.btnDelete_Click;

            btnSelect.Location = new Point(120, 350);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(75, 23);
            btnSelect.TabIndex = 6;
            btnSelect.Text = "Select";
            btnSelect.UseVisualStyleBackColor = true;
            btnSelect.Click += this.btnSelect_Click;

            btnCancel.Location = new Point(220, 350);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += this.btnCancel_Click;

            lblError.AutoSize = true;
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(20, 390);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 15);
            lblError.TabIndex = 8;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 430);
            Controls.Add(lblError);
            Controls.Add(btnCancel);
            Controls.Add(btnSelect);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(lstFinancialYears);
            Controls.Add(lblCompanyName);
            Controls.Add(lblTitle);
            Name = "FinancialYearManagementForm";
            Text = "Financial Year Management";
            Load += this.FinancialYearManagementForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private Label lblCompanyName;
        private ListBox lstFinancialYears;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnSelect;
        private Button btnCancel;
        private Label lblError;
    }
}
