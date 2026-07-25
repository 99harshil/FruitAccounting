namespace FruitAccounting.UI
{
    partial class MainGroupForm
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
            cmbGroup = new ComboBox();
            lblGroup = new Label();
            txtName = new TextBox();
            lblName = new Label();
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
            panelInput.Controls.Add(cmbGroup);
            panelInput.Controls.Add(lblGroup);
            panelInput.Controls.Add(txtName);
            panelInput.Controls.Add(lblName);
            panelInput.Location = new Point(12, 12);
            panelInput.Name = "panelInput";
            panelInput.Size = new Size(330, 80);
            panelInput.TabIndex = 0;
            // 
            // cmbGroup
            // 
            cmbGroup.BackColor = Color.FromArgb(255, 255, 192);
            cmbGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGroup.FlatStyle = FlatStyle.Flat;
            cmbGroup.FormattingEnabled = true;
            cmbGroup.Items.AddRange(new object[] { "ASSETS", "EXPENSE", "INCOME", "LIABILITY", "TRADING EXPENSE", "TRADING INCOME" });
            cmbGroup.Location = new Point(75, 14);
            cmbGroup.Name = "cmbGroup";
            cmbGroup.Size = new Size(235, 23);
            cmbGroup.TabIndex = 2;
            // 
            // lblGroup
            // 
            lblGroup.AutoSize = true;
            lblGroup.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblGroup.ForeColor = Color.Red;
            lblGroup.Location = new Point(23, 17);
            lblGroup.Name = "lblGroup";
            lblGroup.Size = new Size(42, 15);
            lblGroup.TabIndex = 1;
            lblGroup.Text = "Group";
            lblGroup.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            txtName.BackColor = Color.FromArgb(224, 224, 224);
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Location = new Point(75, 44);
            txtName.Name = "txtName";
            txtName.Size = new Size(235, 23);
            txtName.TabIndex = 4;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblName.ForeColor = Color.Red;
            lblName.Location = new Point(25, 47);
            lblName.Name = "lblName";
            lblName.Size = new Size(40, 15);
            lblName.TabIndex = 3;
            lblName.Text = "Name";
            lblName.TextAlign = ContentAlignment.MiddleRight;
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
            panelButtons.Location = new Point(25, 105);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(300, 70);
            panelButtons.TabIndex = 5;
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
            // MainGroupForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(354, 186);
            Controls.Add(panelButtons);
            Controls.Add(panelInput);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainGroupForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Main Group";
            Load += MainGroupForm_Load;
            panelInput.ResumeLayout(false);
            panelInput.PerformLayout();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelInput;
        private ComboBox cmbGroup;
        private Label lblGroup;
        private TextBox txtName;
        private Label lblName;
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