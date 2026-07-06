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

        private void InitializeComponent()
        {
            lblTitle = new Label();
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

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(110, 20);
            lblTitle.Size = new Size(217, 25);
            lblTitle.Text = "Edit Financial Year";

            lblCode.AutoSize = true;
            lblCode.Location = new Point(20, 70);
            lblCode.Size = new Size(35, 15);
            lblCode.Text = "Code";

            txtCode.Location = new Point(20, 90);
            txtCode.Size = new Size(200, 23);

            lblStartDate.AutoSize = true;
            lblStartDate.Location = new Point(20, 125);
            lblStartDate.Size = new Size(65, 15);
            lblStartDate.Text = "Start Date";

            dtStartDate.Location = new Point(20, 145);
            dtStartDate.Size = new Size(200, 23);
            dtStartDate.Format = DateTimePickerFormat.Short;

            lblEndDate.AutoSize = true;
            lblEndDate.Location = new Point(20, 180);
            lblEndDate.Size = new Size(59, 15);
            lblEndDate.Text = "End Date";

            dtEndDate.Location = new Point(20, 200);
            dtEndDate.Size = new Size(200, 23);
            dtEndDate.Format = DateTimePickerFormat.Short;

            chkIsActive.AutoSize = true;
            chkIsActive.Location = new Point(20, 240);
            chkIsActive.Size = new Size(65, 19);
            chkIsActive.Text = "Is Active";

            chkIsClosed.AutoSize = true;
            chkIsClosed.Location = new Point(20, 265);
            chkIsClosed.Size = new Size(67, 19);
            chkIsClosed.Text = "Is Closed";

            btnOK.Location = new Point(50, 310);
            btnOK.Size = new Size(75, 23);
            btnOK.Text = "Update";
            btnOK.Click += this.btnOK_Click;

            btnCancel.Location = new Point(150, 310);
            btnCancel.Size = new Size(75, 23);
            btnCancel.Text = "Cancel";
            btnCancel.Click += this.btnCancel_Click;

            lblError.AutoSize = true;
            lblError.ForeColor = Color.Red;
            lblError.Location = new Point(20, 350);

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(350, 390);
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
            Controls.Add(lblTitle);
            Name = "EditFinancialYearForm";
            Text = "Edit Financial Year";
            Load += this.EditFinancialYearForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
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
