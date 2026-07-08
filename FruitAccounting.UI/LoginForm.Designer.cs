namespace FruitAccounting.UI
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            txtUserId = new TextBox();
            label2 = new Label();
            txtPassword = new TextBox();
            btnOK = new Button();
            btnCancel = new Button();
            btnChangePassword = new Button();
            lblError = new Label();
            pbKeyIcon = new PictureBox();
            panelInputBorder = new Panel();
            ((System.ComponentModel.ISupportInitialize)pbKeyIcon).BeginInit();
            panelInputBorder.SuspendLayout();
            SuspendLayout();
            //
            // label1
            //
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(57, 18);
            label1.Name = "label1";
            label1.Size = new Size(49, 15);
            label1.TabIndex = 1;
            label1.Text = "User Id";
            label1.TextAlign = ContentAlignment.MiddleRight;
            //
            // txtUserId
            //
            txtUserId.BackColor = Color.FromArgb(224, 224, 224);
            txtUserId.BorderStyle = BorderStyle.FixedSingle;
            txtUserId.Location = new Point(112, 15);
            txtUserId.Name = "txtUserId";
            txtUserId.Size = new Size(130, 23);
            txtUserId.TabIndex = 2;
            //
            // label2
            //
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.Location = new Point(47, 47);
            label2.Name = "label2";
            label2.Size = new Size(59, 15);
            label2.TabIndex = 3;
            label2.Text = "Password";
            label2.TextAlign = ContentAlignment.MiddleRight;
            //
            // txtPassword
            //
            txtPassword.BackColor = Color.FromArgb(224, 224, 224);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.CharacterCasing = CharacterCasing.Lower;
            txtPassword.HideSelection = false;
            txtPassword.Location = new Point(112, 44);
            txtPassword.Name = "txtPassword";
            txtPassword.RightToLeft = RightToLeft.No;
            txtPassword.Size = new Size(130, 23);
            txtPassword.TabIndex = 4;
            txtPassword.UseSystemPasswordChar = true;
            //
            // btnOK
            //
            btnOK.BackColor = Color.LightSteelBlue;
            btnOK.FlatStyle = FlatStyle.Popup;
            btnOK.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnOK.Location = new Point(275, 12);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(85, 26);
            btnOK.TabIndex = 5;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = false;
            btnOK.Click += btnOK_Click;
            //
            // btnCancel
            //
            btnCancel.BackColor = Color.LightSteelBlue;
            btnCancel.FlatStyle = FlatStyle.Popup;
            btnCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancel.Location = new Point(275, 42);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(85, 26);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            //
            // btnChangePassword
            //
            btnChangePassword.BackColor = Color.WhiteSmoke;
            btnChangePassword.FlatStyle = FlatStyle.Popup;
            btnChangePassword.ForeColor = SystemColors.ControlText;
            btnChangePassword.Location = new Point(275, 72);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Size = new Size(85, 40);
            btnChangePassword.TabIndex = 7;
            btnChangePassword.Text = "Change\r\nPassword";
            btnChangePassword.UseVisualStyleBackColor = false;
            btnChangePassword.Click += btnChangePassword_Click;
            //
            // lblError
            //
            lblError.AutoSize = true;
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(12, 100);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 15);
            lblError.TabIndex = 8;
            //
            // pbKeyIcon
            //
            pbKeyIcon.Location = new Point(8, 25);
            pbKeyIcon.Name = "pbKeyIcon";
            pbKeyIcon.Size = new Size(32, 32);
            pbKeyIcon.SizeMode = PictureBoxSizeMode.Zoom;
            pbKeyIcon.TabIndex = 9;
            pbKeyIcon.TabStop = false;
            // TODO: Assign your key image here in the properties window
            //
            // panelInputBorder
            //
            panelInputBorder.BorderStyle = BorderStyle.FixedSingle;
            panelInputBorder.Controls.Add(pbKeyIcon);
            panelInputBorder.Controls.Add(txtUserId);
            panelInputBorder.Controls.Add(label1);
            panelInputBorder.Controls.Add(txtPassword);
            panelInputBorder.Controls.Add(label2);
            panelInputBorder.Location = new Point(12, 12);
            panelInputBorder.Name = "panelInputBorder";
            panelInputBorder.Size = new Size(255, 85);
            panelInputBorder.TabIndex = 10;
            //
            // LoginForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(372, 122);
            Controls.Add(panelInputBorder);
            Controls.Add(lblError);
            Controls.Add(btnChangePassword);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login Screen";
            ((System.ComponentModel.ISupportInitialize)pbKeyIcon).EndInit();
            panelInputBorder.ResumeLayout(false);
            panelInputBorder.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtUserId;
        private Label label2;
        private TextBox txtPassword;
        private Button btnOK;
        private Button btnCancel;
        private Button btnChangePassword;
        private Label lblError;
        private PictureBox pbKeyIcon;
        private Panel panelInputBorder;
    }
}
