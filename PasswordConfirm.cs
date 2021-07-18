using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordWallet
{
    public partial class PasswordConfirm : Form
    {
        public string EnteredPassword { get { return txtPassword.Text; } }

        public PasswordConfirm()
        {
            InitializeComponent();
        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {
            ValidatePassword();
        }

        private void TxtPassword2_TextChanged(object sender, EventArgs e)
        {
            ValidatePassword();
        }

        private void ValidatePassword()
        {
            bool valid = txtPassword.Text.Length >= 8 &&
                         txtPassword.Text == txtPassword2.Text;
            lblValid.Visible = !valid;
            btnOK.Enabled = valid;
        }
    }
}
