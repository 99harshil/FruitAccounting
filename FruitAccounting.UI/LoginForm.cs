using FruitAccounting.Core.services;
using FruitAccounting.Data.Entities;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FruitAccounting.UI
{
    public partial class LoginForm : Form
    {
        private readonly AuthService _authService;
        public User? LoggedInUser { get; private set; }

        public LoginForm(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
            lblError.Visible = false;
            AcceptButton = btnOK;
            CancelButton = btnCancel;
        }

        private async void btnOK_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            btnOK.Enabled = false;

            if (string.IsNullOrWhiteSpace(txtUserId.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblError.Text = "Please enter username and password.";
                lblError.Visible = true;
                btnOK.Enabled = true;
                return;
            }

            try
            {
                var user = await _authService.LoginAsync(
                    txtUserId.Text.Trim(),
                    txtPassword.Text);

                if (user == null)
                {
                    lblError.Text = "Invalid username or password.";
                    lblError.Visible = true;
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
                else
                {
                    LoggedInUser = user;
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Connection error: " + ex.Message;
                lblError.Visible = true;
            }
            finally
            {
                btnOK.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserId.Text))
            {
                MessageBox.Show("Please enter a username first.", "Info");
                return;
            }

            using var form = new ChangePasswordForm(_authService, txtUserId.Text.Trim());
            form.ShowDialog();
        }
    }
}