using System.Globalization;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class AmanatGroupWiseForm : Form
    {
        private readonly LedgerService _ledgerService;
        private readonly AccountService _accountService;
        private readonly long _companyId;
        private readonly long _financialYearId;
        private readonly FinancialYear _financialYear;
        private readonly long? _currentUserId;

        private List<Account> _amanatParties = new(); // Accounts that have delegates
        private long _loadSeq;

        public AmanatGroupWiseForm(LedgerService ledgerService, AccountService accountService,
            long companyId, long financialYearId, FinancialYear financialYear, long? currentUserId = null)
        {
            InitializeComponent();
            _ledgerService = ledgerService;
            _accountService = accountService;
            _companyId = companyId;
            _financialYearId = financialYearId;
            _financialYear = financialYear;
            _currentUserId = currentUserId;
        }

        private async void AmanatGroupWiseForm_Load(object sender, EventArgs e)
        {
            dtpFromDate.Value = _financialYear.StartDate.ToDateTime(TimeOnly.MinValue);
            dtpToDate.Value = DateTime.Today;

            var allAccounts = await _accountService.GetAllAccountsAsync(_companyId);
            // Filter to Amanat Party accounts (accounts that have delegates - where InverseAmanatParty.Count > 0)
            _amanatParties = allAccounts
                .Where(a => !a.IsBlocked && a.InverseAmanatParty.Any())
                .OrderBy(a => a.Name)
                .ToList();

            cmbAmanatParty.Items.Clear();
            foreach (var acc in _amanatParties)
            {
                cmbAmanatParty.Items.Add($"{acc.Code} - {acc.Name}");
            }
        }

        private async void cmbAmanatParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadLedgerAsync();
        }

        private async void dtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            await LoadLedgerAsync();
        }

        private async void dtpToDate_ValueChanged(object sender, EventArgs e)
        {
            await LoadLedgerAsync();
        }

        private async void chkWeekTotal_CheckedChanged(object sender, EventArgs e)
        {
            await LoadLedgerAsync();
        }

        private async Task LoadLedgerAsync()
        {
            var mySeq = ++_loadSeq;

            if (cmbAmanatParty.SelectedIndex < 0)
            {
                dgvLedger.Rows.Clear();
                return;
            }

            var amanatParty = _amanatParties[cmbAmanatParty.SelectedIndex];
            var fromDate = DateOnly.FromDateTime(dtpFromDate.Value);
            var toDate = DateOnly.FromDateTime(dtpToDate.Value);

            if (fromDate > toDate)
            {
                MessageBox.Show("From date cannot be after To date", "Ledger");
                return;
            }

            var result = await _ledgerService.GetLedgerAsync(amanatParty.AccountId, _financialYearId, fromDate, toDate,
                voucherTypeFilter: null, includeOpeningBalance: true);

            if (mySeq != _loadSeq)
                return;

            dgvLedger.Rows.Clear();

            AddBalanceRow("Opening Balance :", result.OpeningBalance);

            var displayRows = chkWeekTotal.Checked ? LedgerService.AggregateByWeek(result.Rows) : result.Rows;
            int sr = 1;
            foreach (var row in displayRows)
            {
                int rowIndex = dgvLedger.Rows.Add(
                    sr++,
                    row.VoucherCode,
                    row.Date.ToString("dd/MM/yyyy"),
                    row.Detail,
                    row.ChequeOrUser,
                    row.Debit == 0 ? "" : row.Debit.ToString("N2", CultureInfo.InvariantCulture),
                    row.Credit == 0 ? "" : row.Credit.ToString("N2", CultureInfo.InvariantCulture),
                    FormatBalance(row.RunningBalance));
                dgvLedger.Rows[rowIndex].Tag = row;
            }

            dgvLedger.Rows.Add("", "", "", "Grand Total :-", "",
                result.TotalDebit.ToString("N2", CultureInfo.InvariantCulture),
                result.TotalCredit.ToString("N2", CultureInfo.InvariantCulture),
                "");
            AddBalanceRow("Balance :-", result.ClosingBalance);
        }

        private void AddBalanceRow(string label, decimal signedBalance)
        {
            var amount = Math.Abs(signedBalance).ToString("N2", CultureInfo.InvariantCulture);
            var balanceText = FormatBalance(signedBalance);
            if (signedBalance >= 0)
                dgvLedger.Rows.Add("", "", "", label, "", amount, "", balanceText);
            else
                dgvLedger.Rows.Add("", "", "", label, "", "", amount, balanceText);
        }

        private static string FormatBalance(decimal signedBalance)
        {
            var suffix = signedBalance < 0 ? "Cr" : "Db";
            return $"{Math.Abs(signedBalance):N2} {suffix}";
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void btnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Print / PDF export will be added in the next step (FastReport .NET).", "Ledger");
        }

        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.pnlTop = new Panel();
            this.lblAmanatParty = new Label();
            this.cmbAmanatParty = new ComboBox();
            this.lblFromDate = new Label();
            this.dtpFromDate = new DateTimePicker();
            this.lblToDate = new Label();
            this.dtpToDate = new DateTimePicker();
            this.chkWeekTotal = new CheckBox();
            this.dgvLedger = new DataGridView();
            this.pnlBottom = new Panel();
            this.btnPrint = new Button();
            this.btnClose = new Button();

            this.SuspendLayout();

            // Title
            this.lblTitle.Dock = DockStyle.Top;
            this.lblTitle.Height = 35;
            this.lblTitle.Text = "Amanat Group Wise Ledger";
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            this.lblTitle.BackColor = Color.LightSteelBlue;

            // Top panel
            this.pnlTop.Dock = DockStyle.Top;
            this.pnlTop.Height = 60;
            this.pnlTop.Controls.Add(this.lblAmanatParty);
            this.pnlTop.Controls.Add(this.cmbAmanatParty);
            this.pnlTop.Controls.Add(this.lblFromDate);
            this.pnlTop.Controls.Add(this.dtpFromDate);
            this.pnlTop.Controls.Add(this.lblToDate);
            this.pnlTop.Controls.Add(this.dtpToDate);
            this.pnlTop.Controls.Add(this.chkWeekTotal);

            this.lblAmanatParty.Text = "Amanat Party:";
            this.lblAmanatParty.Location = new Point(10, 15);
            this.lblAmanatParty.Size = new Size(70, 20);

            this.cmbAmanatParty.Location = new Point(85, 15);
            this.cmbAmanatParty.Size = new Size(200, 23);
            this.cmbAmanatParty.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbAmanatParty.SelectedIndexChanged += cmbAmanatParty_SelectedIndexChanged;

            this.lblFromDate.Text = "From:";
            this.lblFromDate.Location = new Point(300, 15);
            this.lblFromDate.Size = new Size(40, 20);

            this.dtpFromDate.Location = new Point(345, 15);
            this.dtpFromDate.Size = new Size(120, 24);
            this.dtpFromDate.Format = DateTimePickerFormat.Short;
            this.dtpFromDate.ValueChanged += dtpFromDate_ValueChanged;

            this.lblToDate.Text = "To:";
            this.lblToDate.Location = new Point(480, 15);
            this.lblToDate.Size = new Size(25, 20);

            this.dtpToDate.Location = new Point(510, 15);
            this.dtpToDate.Size = new Size(120, 24);
            this.dtpToDate.Format = DateTimePickerFormat.Short;
            this.dtpToDate.ValueChanged += dtpToDate_ValueChanged;

            this.chkWeekTotal.Text = "Weekly";
            this.chkWeekTotal.Location = new Point(650, 17);
            this.chkWeekTotal.Size = new Size(70, 20);
            this.chkWeekTotal.CheckedChanged += chkWeekTotal_CheckedChanged;

            // Data grid
            this.dgvLedger.Dock = DockStyle.Fill;
            this.dgvLedger.AllowUserToAddRows = false;
            this.dgvLedger.ReadOnly = true;
            this.dgvLedger.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvLedger.Columns.Add("colSr", "Sr");
            this.dgvLedger.Columns.Add("colVoucherCode", "Voucher Code");
            this.dgvLedger.Columns.Add("colDate", "Date");
            this.dgvLedger.Columns.Add("colDetail", "Detail");
            this.dgvLedger.Columns.Add("colChequeOrUser", "Cheque / User");
            this.dgvLedger.Columns.Add("colDebit", "Debit");
            this.dgvLedger.Columns.Add("colCredit", "Credit");
            this.dgvLedger.Columns.Add("colBalance", "Balance");

            this.dgvLedger.Columns["colSr"].Width = 40;
            this.dgvLedger.Columns["colVoucherCode"].Width = 80;
            this.dgvLedger.Columns["colDate"].Width = 80;
            this.dgvLedger.Columns["colDetail"].Width = 250;
            this.dgvLedger.Columns["colChequeOrUser"].Width = 100;
            this.dgvLedger.Columns["colDebit"].Width = 100;
            this.dgvLedger.Columns["colCredit"].Width = 100;
            this.dgvLedger.Columns["colBalance"].Width = 100;

            // Right-align numeric columns
            this.dgvLedger.Columns["colDebit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvLedger.Columns["colCredit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgvLedger.Columns["colBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Bottom panel
            this.pnlBottom.Dock = DockStyle.Bottom;
            this.pnlBottom.Height = 50;
            this.pnlBottom.Controls.Add(this.btnPrint);
            this.pnlBottom.Controls.Add(this.btnClose);

            this.btnPrint.Text = "Print";
            this.btnPrint.Location = new Point(600, 10);
            this.btnPrint.Size = new Size(80, 28);
            this.btnPrint.Click += btnPrint_Click;

            this.btnClose.Text = "Close";
            this.btnClose.Location = new Point(690, 10);
            this.btnClose.Size = new Size(80, 28);
            this.btnClose.Click += btnClose_Click;

            // Form
            this.ClientSize = new Size(1000, 600);
            this.Controls.Add(this.dgvLedger);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.lblTitle);
            this.Text = "Amanat Group Wise Ledger";
            this.StartPosition = FormStartPosition.CenterParent;
            this.WindowState = FormWindowState.Maximized;

            this.ResumeLayout(false);
        }

        private Label lblTitle;
        private Panel pnlTop;
        private Label lblAmanatParty;
        private ComboBox cmbAmanatParty;
        private Label lblFromDate;
        private DateTimePicker dtpFromDate;
        private Label lblToDate;
        private DateTimePicker dtpToDate;
        private CheckBox chkWeekTotal;
        private DataGridView dgvLedger;
        private Panel pnlBottom;
        private Button btnPrint;
        private Button btnClose;
    }
}
