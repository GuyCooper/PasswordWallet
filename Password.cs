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
    public partial class Password : Form
    {
        public string EnteredPassword {  get { return this.txtPassword.Text; } }
        public Password()
        {
            InitializeComponent();
        }

        private void Password_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
