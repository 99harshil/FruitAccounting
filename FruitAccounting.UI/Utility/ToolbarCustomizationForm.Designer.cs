namespace FruitAccounting.UI
{
    partial class ToolbarCustomizationForm
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
            lblAvailable = new Label();
            lstAvailable = new ListBox();
            lblSelected = new Label();
            lstSelected = new ListBox();
            btnAdd = new Button();
            btnRemove = new Button();
            btnUp = new Button();
            btnDown = new Button();
            btnReset = new Button();
            btnOK = new Button();
            btnCancel = new Button();
            SuspendLayout();

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(20, 15);
            lblTitle.Size = new Size(230, 21);
            lblTitle.Text = "Customize Quick Access Toolbar";

            lblAvailable.AutoSize = true;
            lblAvailable.Location = new Point(20, 50);
            lblAvailable.Size = new Size(125, 15);
            lblAvailable.Text = "Available Buttons:";

            lstAvailable.FormattingEnabled = true;
            lstAvailable.ItemHeight = 15;
            lstAvailable.Location = new Point(20, 70);
            lstAvailable.Size = new Size(200, 200);

            lblSelected.AutoSize = true;
            lblSelected.Location = new Point(350, 50);
            lblSelected.Size = new Size(120, 15);
            lblSelected.Text = "Toolbar Buttons:";

            lstSelected.FormattingEnabled = true;
            lstSelected.ItemHeight = 15;
            lstSelected.Location = new Point(350, 70);
            lstSelected.Size = new Size(200, 200);

            btnAdd.Location = new Point(230, 120);
            btnAdd.Size = new Size(110, 25);
            btnAdd.Text = "Add →";
            btnAdd.Click += this.btnAdd_Click;

            btnRemove.Location = new Point(230, 155);
            btnRemove.Size = new Size(110, 25);
            btnRemove.Text = "← Remove";
            btnRemove.Click += this.btnRemove_Click;

            btnUp.Location = new Point(560, 120);
            btnUp.Size = new Size(75, 25);
            btnUp.Text = "Move Up";
            btnUp.Click += this.btnUp_Click;

            btnDown.Location = new Point(560, 155);
            btnDown.Size = new Size(75, 25);
            btnDown.Text = "Move Down";
            btnDown.Click += this.btnDown_Click;

            btnReset.Location = new Point(20, 290);
            btnReset.Size = new Size(120, 25);
            btnReset.Text = "Reset to Default";
            btnReset.Click += this.btnReset_Click;

            btnOK.Location = new Point(350, 290);
            btnOK.Size = new Size(75, 25);
            btnOK.Text = "OK";
            btnOK.Click += this.btnOK_Click;

            btnCancel.Location = new Point(435, 290);
            btnCancel.Size = new Size(75, 25);
            btnCancel.Text = "Cancel";
            btnCancel.Click += this.btnCancel_Click;

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(650, 330);
            Controls.Add(lblTitle);
            Controls.Add(lblAvailable);
            Controls.Add(lstAvailable);
            Controls.Add(lblSelected);
            Controls.Add(lstSelected);
            Controls.Add(btnAdd);
            Controls.Add(btnRemove);
            Controls.Add(btnUp);
            Controls.Add(btnDown);
            Controls.Add(btnReset);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Name = "ToolbarCustomizationForm";
            Text = "Customize Toolbar";
            Load += this.ToolbarCustomizationForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private Label lblAvailable;
        private ListBox lstAvailable;
        private Label lblSelected;
        private ListBox lstSelected;
        private Button btnAdd;
        private Button btnRemove;
        private Button btnUp;
        private Button btnDown;
        private Button btnReset;
        private Button btnOK;
        private Button btnCancel;
    }
}
