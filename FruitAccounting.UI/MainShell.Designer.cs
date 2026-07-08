namespace FruitAccounting.UI
{
    partial class MainShell
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
            components = new System.ComponentModel.Container();
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 700);
            Name = "MainShell";
            Text = "Fruit Accounting System";
            WindowState = FormWindowState.Maximized;
            Load += this.MainShell_Load;
            ResumeLayout(false);
        }
    }
}
