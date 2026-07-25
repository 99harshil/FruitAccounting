namespace FruitAccounting.UI
{
    partial class ItemForm
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
            panelInput = new Panel();
            chkUsesCrate = new CheckBox();
            txtPackingRate = new TextBox();
            lblCrateExp = new Label();
            txtLabour = new TextBox();
            lblLabour = new Label();
            btnNewCategory = new Button();
            cmbCategory = new ComboBox();
            lblCategory = new Label();
            btnNewGroup = new Button();
            cmbGroup = new ComboBox();
            lblGroup = new Label();
            txtName = new TextBox();
            lblName = new Label();
            txtUnit = new TextBox();
            lblUnit = new Label();
            txtCode = new TextBox();
            lblCode = new Label();
            panelButtons = new Panel();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnSave = new Button();
            btnNext = new Button();
            btnPrevious = new Button();
            btnClose = new Button();
            btnFind = new Button();
            panelInput.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();
            //
            // panelInput
            //
            panelInput.BorderStyle = BorderStyle.FixedSingle;
            panelInput.Controls.Add(chkUsesCrate);
            panelInput.Controls.Add(txtPackingRate);
            panelInput.Controls.Add(lblCrateExp);
            panelInput.Controls.Add(txtLabour);
            panelInput.Controls.Add(lblLabour);
            panelInput.Controls.Add(btnNewCategory);
            panelInput.Controls.Add(cmbCategory);
            panelInput.Controls.Add(lblCategory);
            panelInput.Controls.Add(btnNewGroup);
            panelInput.Controls.Add(cmbGroup);
            panelInput.Controls.Add(lblGroup);
            panelInput.Controls.Add(txtName);
            panelInput.Controls.Add(lblName);
            panelInput.Controls.Add(txtUnit);
            panelInput.Controls.Add(lblUnit);
            panelInput.Controls.Add(txtCode);
            panelInput.Controls.Add(lblCode);
            panelInput.Location = new Point(12, 12);
            panelInput.Name = "panelInput";
            panelInput.Size = new Size(350, 195);
            panelInput.TabIndex = 0;
            //
            // chkUsesCrate
            //
            chkUsesCrate.AutoSize = true;
            chkUsesCrate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            chkUsesCrate.Location = new Point(65, 164);
            chkUsesCrate.Name = "chkUsesCrate";
            chkUsesCrate.Size = new Size(90, 19);
            chkUsesCrate.TabIndex = 16;
            chkUsesCrate.Text = "Uses Crate";
            chkUsesCrate.UseVisualStyleBackColor = true;
            //
            // txtPackingRate
            //
            txtPackingRate.BackColor = Color.FromArgb(224, 224, 224);
            txtPackingRate.BorderStyle = BorderStyle.FixedSingle;
            txtPackingRate.Location = new Point(230, 134);
            txtPackingRate.Name = "txtPackingRate";
            txtPackingRate.Size = new Size(80, 23);
            txtPackingRate.TabIndex = 15;
            //
            // lblCrateExp
            //
            lblCrateExp.AutoSize = true;
            lblCrateExp.Location = new Point(160, 138);
            lblCrateExp.Name = "lblCrateExp";
            lblCrateExp.Size = new Size(64, 15);
            lblCrateExp.TabIndex = 14;
            lblCrateExp.Text = "Crate Exp.";
            //
            // txtLabour
            //
            txtLabour.BackColor = Color.FromArgb(224, 224, 224);
            txtLabour.BorderStyle = BorderStyle.FixedSingle;
            txtLabour.Location = new Point(65, 134);
            txtLabour.Name = "txtLabour";
            txtLabour.Size = new Size(80, 23);
            txtLabour.TabIndex = 13;
            //
            // lblLabour
            //
            lblLabour.AutoSize = true;
            lblLabour.Location = new Point(10, 138);
            lblLabour.Name = "lblLabour";
            lblLabour.Size = new Size(44, 15);
            lblLabour.TabIndex = 12;
            lblLabour.Text = "Labour";
            //
            // btnNewCategory
            //
            btnNewCategory.BackColor = Color.LightSteelBlue;
            btnNewCategory.FlatStyle = FlatStyle.Popup;
            btnNewCategory.Location = new Point(250, 103);
            btnNewCategory.Name = "btnNewCategory";
            btnNewCategory.Size = new Size(55, 23);
            btnNewCategory.TabIndex = 11;
            btnNewCategory.Text = "New";
            btnNewCategory.UseVisualStyleBackColor = false;
            btnNewCategory.Click += btnNewCategory_Click;
            //
            // cmbCategory
            //
            cmbCategory.BackColor = Color.FromArgb(255, 255, 192);
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.FlatStyle = FlatStyle.Flat;
            cmbCategory.FormattingEnabled = true;
            cmbCategory.Location = new Point(65, 103);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(180, 23);
            cmbCategory.TabIndex = 10;
            //
            // lblCategory
            //
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(10, 107);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(53, 15);
            lblCategory.TabIndex = 9;
            lblCategory.Text = "Category";
            //
            // btnNewGroup
            //
            btnNewGroup.BackColor = Color.LightSteelBlue;
            btnNewGroup.FlatStyle = FlatStyle.Popup;
            btnNewGroup.Location = new Point(250, 73);
            btnNewGroup.Name = "btnNewGroup";
            btnNewGroup.Size = new Size(55, 23);
            btnNewGroup.TabIndex = 8;
            btnNewGroup.Text = "New";
            btnNewGroup.UseVisualStyleBackColor = false;
            btnNewGroup.Click += btnNewGroup_Click;
            //
            // cmbGroup
            //
            cmbGroup.BackColor = Color.FromArgb(255, 255, 192);
            cmbGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGroup.FlatStyle = FlatStyle.Flat;
            cmbGroup.FormattingEnabled = true;
            cmbGroup.Location = new Point(65, 73);
            cmbGroup.Name = "cmbGroup";
            cmbGroup.Size = new Size(180, 23);
            cmbGroup.TabIndex = 7;
            //
            // lblGroup
            //
            lblGroup.AutoSize = true;
            lblGroup.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblGroup.ForeColor = Color.Red;
            lblGroup.Location = new Point(10, 77);
            lblGroup.Name = "lblGroup";
            lblGroup.Size = new Size(42, 15);
            lblGroup.TabIndex = 6;
            lblGroup.Text = "Group";
            //
            // txtName
            //
            txtName.BackColor = Color.FromArgb(224, 224, 224);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Location = new Point(65, 44);
            txtName.Name = "txtName";
            txtName.Size = new Size(270, 23);
            txtName.TabIndex = 5;
            //
            // lblName
            //
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblName.ForeColor = Color.Red;
            lblName.Location = new Point(10, 47);
            lblName.Name = "lblName";
            lblName.Size = new Size(40, 15);
            lblName.TabIndex = 4;
            lblName.Text = "Name";
            //
            // txtUnit
            //
            txtUnit.BackColor = Color.FromArgb(224, 224, 224);
            txtUnit.BorderStyle = BorderStyle.FixedSingle;
            txtUnit.Location = new Point(225, 14);
            txtUnit.Name = "txtUnit";
            txtUnit.Size = new Size(80, 23);
            txtUnit.TabIndex = 3;
            //
            // lblUnit
            //
            lblUnit.AutoSize = true;
            lblUnit.Location = new Point(185, 17);
            lblUnit.Name = "lblUnit";
            lblUnit.Size = new Size(29, 15);
            lblUnit.TabIndex = 2;
            lblUnit.Text = "Unit";
            //
            // txtCode
            //
            txtCode.BackColor = Color.FromArgb(224, 224, 224);
            txtCode.BorderStyle = BorderStyle.FixedSingle;
            txtCode.Location = new Point(65, 14);
            txtCode.MaxLength = 10;
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(100, 23);
            txtCode.TabIndex = 1;
            //
            // lblCode
            //
            lblCode.AutoSize = true;
            lblCode.Location = new Point(10, 17);
            lblCode.Name = "lblCode";
            lblCode.Size = new Size(34, 15);
            lblCode.TabIndex = 0;
            lblCode.Text = "Code";
            //
            // panelButtons
            //
            panelButtons.BorderStyle = BorderStyle.FixedSingle;
            panelButtons.Controls.Add(btnAdd);
            panelButtons.Controls.Add(btnUpdate);
            panelButtons.Controls.Add(btnDelete);
            panelButtons.Controls.Add(btnSave);
            panelButtons.Controls.Add(btnNext);
            panelButtons.Controls.Add(btnPrevious);
            panelButtons.Controls.Add(btnClose);
            panelButtons.Controls.Add(btnFind);
            panelButtons.Location = new Point(37, 217);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(300, 70);
            panelButtons.TabIndex = 1;
            //
            // btnAdd
            //
            btnAdd.BackColor = Color.LightSteelBlue;
            btnAdd.FlatStyle = FlatStyle.Popup;
            btnAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAdd.Location = new Point(15, 8);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(65, 25);
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
            btnUpdate.Location = new Point(85, 8);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(65, 25);
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
            btnDelete.Location = new Point(155, 8);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(65, 25);
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
            btnSave.Location = new Point(225, 8);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(65, 25);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            //
            // btnNext
            //
            btnNext.BackColor = Color.LightSteelBlue;
            btnNext.FlatStyle = FlatStyle.Popup;
            btnNext.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnNext.Location = new Point(15, 38);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(65, 25);
            btnNext.TabIndex = 4;
            btnNext.Text = "Next";
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += btnNext_Click;
            //
            // btnPrevious
            //
            btnPrevious.BackColor = Color.LightSteelBlue;
            btnPrevious.FlatStyle = FlatStyle.Popup;
            btnPrevious.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPrevious.Location = new Point(85, 38);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(65, 25);
            btnPrevious.TabIndex = 5;
            btnPrevious.Text = "Previous";
            btnPrevious.UseVisualStyleBackColor = false;
            btnPrevious.Click += btnPrevious_Click;
            //
            // btnClose
            //
            btnClose.BackColor = Color.LightSteelBlue;
            btnClose.FlatStyle = FlatStyle.Popup;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClose.Location = new Point(155, 38);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(65, 25);
            btnClose.TabIndex = 6;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            //
            // btnFind
            //
            btnFind.BackColor = Color.LightSteelBlue;
            btnFind.FlatStyle = FlatStyle.Popup;
            btnFind.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnFind.Location = new Point(225, 38);
            btnFind.Name = "btnFind";
            btnFind.Size = new Size(65, 25);
            btnFind.TabIndex = 7;
            btnFind.Text = "Find";
            btnFind.UseVisualStyleBackColor = false;
            btnFind.Click += btnFind_Click;
            //
            // ItemForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(374, 299);
            Controls.Add(panelButtons);
            Controls.Add(panelInput);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ItemForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Item Details";
            Load += ItemForm_Load;
            panelInput.ResumeLayout(false);
            panelInput.PerformLayout();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelInput;
        private Label lblCode;
        private TextBox txtCode;
        private Label lblUnit;
        private TextBox txtUnit;
        private Label lblName;
        private TextBox txtName;
        private Label lblGroup;
        private ComboBox cmbGroup;
        private Button btnNewGroup;
        private Label lblCategory;
        private ComboBox cmbCategory;
        private Button btnNewCategory;
        private Label lblLabour;
        private TextBox txtLabour;
        private Label lblCrateExp;
        private TextBox txtPackingRate;
        private CheckBox chkUsesCrate;
        private Panel panelButtons;
        protected Button btnAdd;
        protected Button btnUpdate;
        protected Button btnDelete;
        protected Button btnSave;
        protected Button btnNext;
        protected Button btnPrevious;
        protected Button btnClose;
        protected Button btnFind;
    }
}
