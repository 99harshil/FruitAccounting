using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

namespace FruitAccounting.UI
{
    public partial class DelegateLedgerSelectorForm : Form
    {
        private readonly LedgerService _ledgerService;
        private readonly AccountService _accountService;
        private readonly long _companyId;
        private readonly long _financialYearId;

        private List<Account> _allAccounts = new();
        private List<Account> _amanatPartyAccounts = new(); // Accounts that have delegates
        private List<Account> _delegateAccounts = new(); // Delegates of selected Amanat Party

        public Account? SelectedAmanatParty { get; private set; }
        public Account? SelectedDelegate { get; private set; }
        public DateOnly FromDate { get; private set; }
        public DateOnly ToDate { get; private set; }

        public DelegateLedgerSelectorForm(LedgerService ledgerService, AccountService accountService,
            long companyId, long financialYearId)
        {
            InitializeComponent();
            _ledgerService = ledgerService;
            _accountService = accountService;
            _companyId = companyId;
            _financialYearId = financialYearId;
        }

        private async void DelegateLedgerSelectorForm_Load(object sender, EventArgs e)
        {
            dtFromDate.Value = DateTime.Now.AddMonths(-1);
            dtToDate.Value = DateTime.Now;

            try
            {
                _allAccounts = await _accountService.GetAllAccountsAsync(_companyId);

                // Filter to Amanat Party accounts (accounts that have delegates - where InverseAmanatParty.Count > 0)
                _amanatPartyAccounts = _allAccounts
                    .Where(a => !a.IsBlocked && a.InverseAmanatParty.Any())
                    .OrderBy(a => a.Name)
                    .ToList();

                cmbAmanatParty.Items.Clear();
                foreach (var acc in _amanatPartyAccounts)
                {
                    cmbAmanatParty.Items.Add($"{acc.Code} - {acc.Name}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading accounts: {ex.Message}", "Error");
            }
        }

        private void cmbAmanatParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDelegate.Items.Clear();
            _delegateAccounts.Clear();

            if (cmbAmanatParty.SelectedIndex < 0)
                return;

            var selectedAmanatParty = _amanatPartyAccounts[cmbAmanatParty.SelectedIndex];

            // Filter delegates of selected Amanat Party
            _delegateAccounts = selectedAmanatParty.InverseAmanatParty
                .Where(a => !a.IsBlocked)
                .OrderBy(a => a.Name)
                .ToList();

            foreach (var acc in _delegateAccounts)
            {
                cmbDelegate.Items.Add($"{acc.Code} - {acc.Name}");
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbAmanatParty.SelectedIndex < 0)
            {
                MessageBox.Show("Please select an Amanat Party account", "Validation");
                return;
            }

            if (cmbDelegate.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a Delegate account", "Validation");
                return;
            }

            SelectedAmanatParty = _amanatPartyAccounts[cmbAmanatParty.SelectedIndex];
            SelectedDelegate = _delegateAccounts[cmbDelegate.SelectedIndex];
            FromDate = DateOnly.FromDateTime(dtFromDate.Value);
            ToDate = DateOnly.FromDateTime(dtToDate.Value);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void InitializeComponent()
        {
            lblAmanatParty = new Label();
            cmbAmanatParty = new ComboBox();
            lblDelegate = new Label();
            cmbDelegate = new ComboBox();
            lblFromDate = new Label();
            dtFromDate = new DateTimePicker();
            lblToDate = new Label();
            dtToDate = new DateTimePicker();
            btnOK = new Button();
            btnCancel = new Button();

            SuspendLayout();

            // lblAmanatParty
            lblAmanatParty.AutoSize = true;
            lblAmanatParty.Location = new Point(12, 20);
            lblAmanatParty.Text = "Amanat Party:";

            // cmbAmanatParty
            cmbAmanatParty.Location = new Point(120, 20);
            cmbAmanatParty.Size = new Size(250, 23);
            cmbAmanatParty.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAmanatParty.SelectedIndexChanged += cmbAmanatParty_SelectedIndexChanged;

            // lblDelegate
            lblDelegate.AutoSize = true;
            lblDelegate.Location = new Point(12, 60);
            lblDelegate.Text = "Delegate Account:";

            // cmbDelegate
            cmbDelegate.Location = new Point(120, 60);
            cmbDelegate.Size = new Size(250, 23);
            cmbDelegate.DropDownStyle = ComboBoxStyle.DropDownList;

            // lblFromDate
            lblFromDate.AutoSize = true;
            lblFromDate.Location = new Point(12, 100);
            lblFromDate.Text = "From Date:";

            // dtFromDate
            dtFromDate.Location = new Point(120, 100);
            dtFromDate.Size = new Size(150, 23);
            dtFromDate.Format = DateTimePickerFormat.Short;

            // lblToDate
            lblToDate.AutoSize = true;
            lblToDate.Location = new Point(280, 100);
            lblToDate.Text = "To Date:";

            // dtToDate
            dtToDate.Location = new Point(350, 100);
            dtToDate.Size = new Size(150, 23);
            dtToDate.Format = DateTimePickerFormat.Short;

            // btnOK
            btnOK.Text = "OK";
            btnOK.Location = new Point(200, 150);
            btnOK.Size = new Size(80, 28);
            btnOK.Click += btnOK_Click;

            // btnCancel
            btnCancel.Text = "Cancel";
            btnCancel.Location = new Point(290, 150);
            btnCancel.Size = new Size(80, 28);
            btnCancel.Click += btnCancel_Click;

            // Form
            ClientSize = new Size(530, 200);
            Controls.Add(lblAmanatParty);
            Controls.Add(cmbAmanatParty);
            Controls.Add(lblDelegate);
            Controls.Add(cmbDelegate);
            Controls.Add(lblFromDate);
            Controls.Add(dtFromDate);
            Controls.Add(lblToDate);
            Controls.Add(dtToDate);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Text = "Select Delegate Ledger";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblAmanatParty;
        private ComboBox cmbAmanatParty;
        private Label lblDelegate;
        private ComboBox cmbDelegate;
        private Label lblFromDate;
        private DateTimePicker dtFromDate;
        private Label lblToDate;
        private DateTimePicker dtToDate;
        private Button btnOK;
        private Button btnCancel;
    }
}
