namespace FruitAccounting.UI
{
    partial class LedgerAllForm
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
            lblPeriod = new Label();
            dtpFromDate = new DateTimePicker();
            dtpToDate = new DateTimePicker();
            chkWeekTotal = new CheckBox();
            btnOk = new Button();
            btnClose = new Button();
            btnWhatsapp = new Button();
            SuspendLayout();
            //
            // lblPeriod
            //
            lblPeriod.AutoSize = true;
            lblPeriod.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPeriod.Location = new Point(20, 40);
            lblPeriod.Name = "lblPeriod";
            lblPeriod.Size = new Size(50, 15);
            lblPeriod.TabIndex = 0;
            lblPeriod.Text = "Period :";
            //
            // dtpFromDate
            //
            dtpFromDate.Format = DateTimePickerFormat.Custom;
            dtpFromDate.CustomFormat = "dd/MM/yyyy";
            dtpFromDate.Location = new Point(90, 37);
            dtpFromDate.Name = "dtpFromDate";
            dtpFromDate.Size = new Size(135, 23);
            dtpFromDate.TabIndex = 1;
            //
            // dtpToDate
            //
            dtpToDate.Format = DateTimePickerFormat.Custom;
            dtpToDate.CustomFormat = "dd/MM/yyyy";
            dtpToDate.Location = new Point(245, 37);
            dtpToDate.Name = "dtpToDate";
            dtpToDate.Size = new Size(135, 23);
            dtpToDate.TabIndex = 2;
            //
            // chkWeekTotal
            //
            chkWeekTotal.AutoSize = true;
            chkWeekTotal.Location = new Point(20, 75);
            chkWeekTotal.Name = "chkWeekTotal";
            chkWeekTotal.Size = new Size(90, 19);
            chkWeekTotal.TabIndex = 6;
            chkWeekTotal.Text = "Week Total";
            chkWeekTotal.UseVisualStyleBackColor = true;
            //
            // btnOk
            //
            btnOk.BackColor = Color.LightSteelBlue;
            btnOk.FlatStyle = FlatStyle.Popup;
            btnOk.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnOk.Location = new Point(427, 610);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(80, 28);
            btnOk.TabIndex = 3;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = false;
            btnOk.Click += btnOk_Click;
            //
            // btnClose
            //
            btnClose.BackColor = Color.LightSteelBlue;
            btnClose.FlatStyle = FlatStyle.Popup;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClose.Location = new Point(525, 610);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(80, 28);
            btnClose.TabIndex = 4;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            //
            // btnWhatsapp
            //
            btnWhatsapp.BackColor = Color.LightGreen;
            btnWhatsapp.FlatStyle = FlatStyle.Popup;
            btnWhatsapp.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnWhatsapp.Location = new Point(623, 610);
            btnWhatsapp.Name = "btnWhatsapp";
            btnWhatsapp.Size = new Size(38, 28);
            btnWhatsapp.TabIndex = 5;
            btnWhatsapp.Text = "\U0001F4AC";
            btnWhatsapp.UseVisualStyleBackColor = false;
            btnWhatsapp.Click += btnWhatsapp_Click;
            //
            // LedgerAllForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1050, 650);
            Controls.Add(btnWhatsapp);
            Controls.Add(btnClose);
            Controls.Add(btnOk);
            Controls.Add(chkWeekTotal);
            Controls.Add(dtpToDate);
            Controls.Add(dtpFromDate);
            Controls.Add(lblPeriod);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LedgerAllForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Reports";
            Load += LedgerAllForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblPeriod;
        private DateTimePicker dtpFromDate;
        private DateTimePicker dtpToDate;
        private CheckBox chkWeekTotal;
        private Button btnOk;
        private Button btnClose;
        private Button btnWhatsapp;
    }
}
