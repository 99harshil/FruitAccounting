namespace FruitAccounting.UI
{
    partial class DeleteUserForm
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
            lblUserId = new Label();
            cmbUserId = new ComboBox();
            btnDelete = new Button();
            btnClose = new Button();
            lstUsers = new ListBox();
            SuspendLayout();

            //
            // lblUserId
            //
            lblUserId.AutoSize = true;
            lblUserId.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUserId.Location = new Point(21, 17);
            lblUserId.Name = "lblUserId";
            lblUserId.Size = new Size(49, 15);
            lblUserId.TabIndex = 1;
            lblUserId.Text = "User Id";

            //
            // cmbUserId
            //
            cmbUserId.BackColor = Color.FromArgb(224, 224, 224);
            cmbUserId.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUserId.Location = new Point(76, 14);
            cmbUserId.Name = "cmbUserId";
            cmbUserId.Size = new Size(200, 23);
            cmbUserId.TabIndex = 2;

            //
            // btnDelete
            //
            btnDelete.BackColor = Color.CornflowerBlue;
            btnDelete.FlatStyle = FlatStyle.Popup;
            btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDelete.Location = new Point(76, 46);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 26);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "&Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;

            //
            // btnClose
            //
            btnClose.BackColor = Color.CornflowerBlue;
            btnClose.FlatStyle = FlatStyle.Popup;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClose.Location = new Point(201, 46);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 26);
            btnClose.TabIndex = 4;
            btnClose.Text = "&Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;

            //
            // lstUsers
            //
            lstUsers.BackColor = Color.FromArgb(224, 224, 224);
            lstUsers.Location = new Point(21, 80);
            lstUsers.Name = "lstUsers";
            lstUsers.Size = new Size(255, 200);
            lstUsers.TabIndex = 5;

            //
            // DeleteUserForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(350, 350);
            Controls.Add(lstUsers);
            Controls.Add(btnClose);
            Controls.Add(btnDelete);
            Controls.Add(cmbUserId);
            Controls.Add(lblUserId);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DeleteUserForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Delete User";
            Load += DeleteUserForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblUserId;
        private ComboBox cmbUserId;
        private Button btnDelete;
        private Button btnClose;
        private ListBox lstUsers;
    }
}
