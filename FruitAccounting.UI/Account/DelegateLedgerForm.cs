using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;
using FruitAccounting.Data.Enums;

namespace FruitAccounting.UI
{
    public partial class DelegateLedgerForm : Form
    {
        private readonly LedgerService _ledgerService;
        private readonly long _delegateAccountId;
        private readonly long _amanatPartyAccountId;
        private readonly Account _delegateAccount;
        private readonly Account _amanatPartyAccount;
        private readonly long _financialYearId;

        private List<LedgerService.LedgerRow> _rows = new();

        public DelegateLedgerForm(LedgerService ledgerService, Account delegateAccount, Account amanatPartyAccount, long financialYearId)
        {
            InitializeComponent();
            _ledgerService = ledgerService;
            _delegateAccount = delegateAccount;
            _amanatPartyAccount = amanatPartyAccount;
            _delegateAccountId = delegateAccount.AccountId;
            _amanatPartyAccountId = amanatPartyAccount.AccountId;
            _financialYearId = financialYearId;

            // Set form title and banner
            this.Text = $"Delegate Ledger - {delegateAccount.Name}";
            lblBanner.Text = $"{delegateAccount.Name}'s Activity under {amanatPartyAccount.Name}";
            lblBanner.BackColor = Color.LightBlue;
            lblBanner.ForeColor = Color.DarkBlue;
            lblBanner.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            // Initialize date range to current financial year (approximation)
            dtFromDate.Value = new DateTime(DateTime.Now.Year, 4, 1); // April 1st (typical FY start in India)
            dtToDate.Value = DateTime.Now;
        }

        private async void DelegateLedgerForm_Load(object sender, EventArgs e)
        {
            await LoadLedgerAsync();
        }

        private async System.Threading.Tasks.Task LoadLedgerAsync()
        {
            try
            {
                var fromDate = DateOnly.FromDateTime(dtFromDate.Value);
                var toDate = DateOnly.FromDateTime(dtToDate.Value);

                var result = await _ledgerService.GetDelegateLedgerAsync(
                    _delegateAccountId, _amanatPartyAccountId, _financialYearId, fromDate, toDate);

                _rows = result.Rows;
                PopulateGrid(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading ledger: {ex.Message}", "Error");
            }
        }

        private void PopulateGrid(LedgerService.LedgerResult result)
        {
            dgvLedger.Rows.Clear();
            dgvLedger.Columns.Clear();

            // Set up columns
            dgvLedger.Columns.Add("colDate", "Date");
            dgvLedger.Columns.Add("colDetail", "Detail");
            dgvLedger.Columns.Add("colDebit", "Debit");
            dgvLedger.Columns.Add("colCredit", "Credit");
            dgvLedger.Columns.Add("colVoucher", "Voucher");

            dgvLedger.Columns["colDate"].Width = 80;
            dgvLedger.Columns["colDetail"].Width = 300;
            dgvLedger.Columns["colDebit"].Width = 100;
            dgvLedger.Columns["colCredit"].Width = 100;
            dgvLedger.Columns["colVoucher"].Width = 80;

            // Right-align numeric columns
            dgvLedger.Columns["colDebit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvLedger.Columns["colCredit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Populate data rows
            foreach (var row in result.Rows)
            {
                dgvLedger.Rows.Add(
                    row.Date.ToString("dd/MM/yyyy"),
                    row.Detail,
                    row.Debit > 0 ? row.Debit.ToString("N2") : "",
                    row.Credit > 0 ? row.Credit.ToString("N2") : "",
                    row.VoucherCode
                );
            }

            // Add subtotal row
            dgvLedger.Rows.Add();
            int lastRowIndex = dgvLedger.Rows.Count - 1;
            var subtotalRow = dgvLedger.Rows[lastRowIndex];
            subtotalRow.Cells["colDetail"].Value = "SUBTOTAL";
            subtotalRow.Cells["colDebit"].Value = result.TotalDebit > 0 ? result.TotalDebit.ToString("N2") : "";
            subtotalRow.Cells["colCredit"].Value = result.TotalCredit > 0 ? result.TotalCredit.ToString("N2") : "";

            // Format subtotal row
            subtotalRow.DefaultCellStyle.BackColor = Color.LightGray;
            subtotalRow.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            subtotalRow.Cells["colDebit"].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            subtotalRow.Cells["colCredit"].Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Update summary
            lblSummary.Text = $"Total Debit: {result.TotalDebit:N2}  |  Total Credit: {result.TotalCredit:N2}  |  Net: {result.ClosingBalance:N2}";
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadLedgerAsync();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitializeComponent()
        {
            this.lblBanner = new Label();
            this.dtFromDate = new DateTimePicker();
            this.dtToDate = new DateTimePicker();
            this.btnRefresh = new Button();
            this.btnClose = new Button();
            this.dgvLedger = new DataGridView();
            this.lblSummary = new Label();
            this.lblFromDate = new Label();
            this.lblToDate = new Label();
            this.pnlTop = new Panel();
            this.pnlBottom = new Panel();

            this.SuspendLayout();

            // Banner
            this.lblBanner.Dock = DockStyle.Top;
            this.lblBanner.Height = 40;
            this.lblBanner.TextAlign = ContentAlignment.MiddleCenter;

            // Top panel with date filters
            this.pnlTop.Dock = DockStyle.Top;
            this.pnlTop.Height = 60;
            this.pnlTop.Controls.Add(this.lblFromDate);
            this.pnlTop.Controls.Add(this.dtFromDate);
            this.pnlTop.Controls.Add(this.lblToDate);
            this.pnlTop.Controls.Add(this.dtToDate);
            this.pnlTop.Controls.Add(this.btnRefresh);

            this.lblFromDate.Text = "From:";
            this.lblFromDate.Location = new Point(10, 15);
            this.lblFromDate.Size = new Size(40, 20);

            this.dtFromDate.Location = new Point(55, 15);
            this.dtFromDate.Size = new Size(120, 24);
            this.dtFromDate.Format = DateTimePickerFormat.Short;

            this.lblToDate.Text = "To:";
            this.lblToDate.Location = new Point(190, 15);
            this.lblToDate.Size = new Size(25, 20);

            this.dtToDate.Location = new Point(220, 15);
            this.dtToDate.Size = new Size(120, 24);
            this.dtToDate.Format = DateTimePickerFormat.Short;

            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Location = new Point(360, 15);
            this.btnRefresh.Size = new Size(80, 24);
            this.btnRefresh.Click += btnRefresh_Click;

            // Data grid
            this.dgvLedger.Dock = DockStyle.Fill;
            this.dgvLedger.AllowUserToAddRows = false;
            this.dgvLedger.ReadOnly = true;
            this.dgvLedger.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Bottom panel
            this.pnlBottom.Dock = DockStyle.Bottom;
            this.pnlBottom.Height = 60;
            this.pnlBottom.Controls.Add(this.lblSummary);
            this.pnlBottom.Controls.Add(this.btnClose);

            this.lblSummary.Location = new Point(10, 10);
            this.lblSummary.Size = new Size(600, 20);
            this.lblSummary.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            this.btnClose.Text = "Close";
            this.btnClose.Location = new Point(720, 30);
            this.btnClose.Size = new Size(80, 24);
            this.btnClose.Click += btnClose_Click;

            // Form
            this.ClientSize = new Size(820, 600);
            this.Controls.Add(this.dgvLedger);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.lblBanner);
            this.Text = "Delegate Ledger";
            this.StartPosition = FormStartPosition.CenterParent;

            this.ResumeLayout(false);
        }

        private Label lblBanner;
        private DateTimePicker dtFromDate;
        private DateTimePicker dtToDate;
        private Button btnRefresh;
        private Button btnClose;
        private DataGridView dgvLedger;
        private Label lblSummary;
        private Label lblFromDate;
        private Label lblToDate;
        private Panel pnlTop;
        private Panel pnlBottom;
    }
}
