using System.Collections.Generic;
using System.Windows.Forms;

namespace PasswordWallet
{
    public partial class Password : Form
    {
        private readonly Action _action;

        public enum Action
        {
            Enter,
            Show
        }

        private readonly Dictionary<Action, (string Title, string Label)> ActionLookup =
            new Dictionary<Action, (string Title, string Label)>
        {
                {Action.Enter, ("Password", "Enter Password") },
                {Action.Show, ("Show Password", "Password") }
        };

        public string EnteredPassword {  get { return this.txtPassword.Text; } }

        public Password(Action action, string password = null)
        {
            InitializeComponent();
            _action = action;
            this.Text = ActionLookup[action].Title;
            labelPassword.Text = ActionLookup[action].Label;
            if(_action == Action.Show)
            {
                txtPassword.Text = password;
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.ReadOnly = true;
            }
        }

        private void Password_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
