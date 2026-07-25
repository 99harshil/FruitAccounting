namespace FruitAccounting.UI
{
    partial class EditFinancialYearForm
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
            lblCode = new Label();
            txtCode = new TextBox();
            lblStartDate = new Label();
            dtStartDate = new DateTimePicker();
            lblEndDate = new Label();
            dtEndDate = new DateTimePicker();
            chkIsActive = new CheckBox();
            chkIsClosed = new CheckBox();
            btnOK = new Button();
            btnCancel = new Button();
            lblError = new Label();
            SuspendLayout();
            //
            // lblCode
            //
            lblCode.AutoSize = true;
            lblCode.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCode.Location = new Point(62, 22);
            lblCode.Name = "lblCode";
            lblCode.Size = new Size(35, 15);
            lblCode.TabIndex = 1;
            lblCode.Text = "Code";
            lblCode.TextAlign = ContentAlignment.MiddleRight;
            //
            // txtCode
            //
            txtCode.BackColor = Color.FromArgb(224, 224, 224);
            txtCode.BorderStyle = BorderStyle.FixedSingle;
            txtCode.Location = new Point(105, 19);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(160, 23);
            txtCode.TabIndex = 2;
            //
            // lblStartDate
            //
            lblStartDate.AutoSize = true;
            lblStartDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStartDate.Location = new Point(34, 51);
            lblStartDate.Name = "lblStartDate";
            lblStartDate.Size = new Size(63, 15);
            lblStartDate.TabIndex = 3;
            lblStartDate.Text = "Start Date";
            lblStartDate.TextAlign = ContentAlignment.MiddleRight;
            //
            // dtStartDate
            //
            dtStartDate.Format = DateTimePickerFormat.Short;
            dtStartDate.Location = new Point(105, 48);
            dtStartDate.Name = "dtStartDate";
            dtStartDate.Size = new Size(160, 23);
            dtStartDate.TabIndex = 4;
            //
            // lblEndDate
            //
            lblEndDate.AutoSize = true;
            lblEndDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEndDate.Location = new Point(41, 80);
            lblEndDate.Name = "lblEndDate";
            lblEndDate.Size = new Size(56, 15);
            lblEndDate.TabIndex = 5;
            lblEndDate.Text = "End Date";
            lblEndDate.TextAlign = ContentAlignment.MiddleRight;
            //
            // dtEndDate
            //
            dtEndDate.Format = DateTimePickerFormat.Short;
            dtEndDate.Location = new Point(105, 77);
            dtEndDate.Name = "dtEndDate";
            dtEndDate.Size = new Size(160, 23);
            dtEndDate.TabIndex = 6;
            //
            // chkIsActive
            //
            chkIsActive.AutoSize = true;
            chkIsActive.Location = new Point(105, 106);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(65, 19);
            chkIsActive.TabIndex = 7;
            chkIsActive.Text = "Is Active";
            chkIsActive.UseVisualStyleBackColor = true;
            //
            // chkIsClosed
            //
            chkIsClosed.AutoSize = true;
            chkIsClosed.Location = new Point(105, 131);
            chkIsClosed.Name = "chkIsClosed";
            chkIsClosed.Size = new Size(67, 19);
            chkIsClosed.TabIndex = 8;
            chkIsClosed.Text = "Is Closed";
            chkIsClosed.UseVisualStyleBackColor = true;
            //
            // btnOK
            //
            btnOK.BackColor = Color.CornflowerBlue;
            btnOK.FlatStyle = FlatStyle.Popup;
            btnOK.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnOK.Location = new Point(75, 165);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 26);
            btnOK.TabIndex = 9;
            btnOK.Text = "&Update";
            btnOK.UseVisualStyleBackColor = false;
            btnOK.Click += btnOK_Click;
            //
            // btnCancel
            //
            btnCancel.BackColor = Color.CornflowerBlue;
            btnCancel.FlatStyle = FlatStyle.Popup;
            btnCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnCancel.Location = new Point(165, 165);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 26);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "&Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            //
            // lblError
            //
            lblError.AutoSize = true;
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(105, 150);
            lblError.Name = "lblError";
            lblError.Size = new Size(0, 15);
            lblError.TabIndex = 11;
            //
            // EditFinancialYearForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(314, 211);
            Controls.Add(lblError);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(chkIsClosed);
            Controls.Add(chkIsActive);
            Controls.Add(dtEndDate);
            Controls.Add(lblEndDate);
            Controls.Add(dtStartDate);
            Controls.Add(lblStartDate);
            Controls.Add(txtCode);
            Controls.Add(lblCode);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditFinancialYearForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Edit Financial Year";
            Load += EditFinancialYearForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCode;
        private TextBox txtCode;
        private Label lblStartDate;
        private DateTimePicker dtStartDate;
        private Label lblEndDate;
        private DateTimePicker dtEndDate;
        private CheckBox chkIsActive;
        private CheckBox chkIsClosed;
        private Button btnOK;
        private Button btnCancel;
        private Label lblError;
    }
}
