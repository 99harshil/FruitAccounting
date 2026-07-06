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
            lblTitle = new Label();
            label1 = new Label();
            txtUserId = new TextBox();
            label2 = new Label();
            txtPassword = new TextBox();
            btnOK = new Button();
            btnCancel = new Button();
            btnChangePassword = new Button();
            lblError = new Label();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(262, 60);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(230, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Fruit Accounting System";
            lblTitle.TextAlign = ContentAlignment.TopCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(363, 117);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 1;
            label1.Text = "UserID";
            // 
            // txtUserId
            // 
            txtUserId.Location = new Point(335, 135);
            txtUserId.Name = "txtUserId";
            txtUserId.Size = new Size(100, 23);
            txtUserId.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(354, 197);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 3;
            label2.Text = "Password";
            // 
            // txtPassword
            // 
            txtPassword.CharacterCasing = CharacterCasing.Lower;
            txtPassword.HideSelection = false;
            txtPassword.Location = new Point(335, 215);
            txtPassword.Name = "txtPassword";
            txtPassword.RightToLeft = RightToLeft.No;
            txtPassword.Size = new Size(100, 23);
            txtPassword.TabIndex = 4;
            txtPassword.UseSystemPasswordChar = true;
            //txtPassword.TextChanged += this.txtPassword_TextChanged;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(323, 260);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(40, 23);
            btnOK.TabIndex = 5;
            btnOK.Text = "Ok";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += this.btnOK_Click;
            //
            // btnCancel
            //
            btnCancel.Location = new Point(413, 260);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(64, 23);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += this.btnCancel_Click;
            //
            // btnChangePassword
            //
            btnChangePassword.ForeColor = SystemColors.InactiveCaptionText;
            btnChangePassword.Location = new Point(347, 304);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Size = new Size(88, 48);
            btnChangePassword.TabIndex = 7;
            btnChangePassword.Text = "Change Password";
            btnChangePassword.UseVisualStyleBackColor = true;
            btnChangePassword.Click += this.btnChangePassword_Click;
            //
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(370, 369);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 15);
            lblError.TabIndex = 8;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblError);
            Controls.Add(btnChangePassword);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(txtPassword);
            Controls.Add(label2);
            Controls.Add(txtUserId);
            Controls.Add(label1);
            Controls.Add(lblTitle);
            Name = "LoginForm";
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label label1;
        private TextBox txtUserId;
        private Label label2;
        private TextBox txtPassword;
        private Button btnOK;
        private Button btnCancel;
        private Button btnChangePassword;
        private Label lblError;
    }
}