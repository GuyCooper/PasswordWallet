using System;
using System.Windows.Forms;

namespace PasswordWallet
{
    /// <summary>
    /// Add a new account entry.
    /// </summary>
    public partial class AddEntry : Form
    {
        #region Constructors

        /// <summary>
        /// Add Entry constructor
        /// </summary>
        public AddEntry()
        {
            InitializeComponent();
            _action = AccountAction.ADD;
            _data = new AccountData();
            btnCopyPassword.Hide();
            btnCopyPasscode.Hide();
        }

        /// <summary>
        /// Edit entry constructor.
        /// </summary>
        public AddEntry(AccountData data)
        {
            InitializeComponent();
            _data = data;
            this.txtName.Text = _data.Name;
            this.txtWebsite.Text = _data.Website;
            this.txtUserName.Text = _data.UserName;
            this.txtPassword.Text = _data.Password;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPasscode.Text = _data.Passcode;
            this.txtPasscode.UseSystemPasswordChar = true;
            this.txtOther.Text = _data.Other;

            this.Text = "Edit Entry";
            this.btnAdd.Text = "Edit";

            if(string.IsNullOrEmpty(this.txtPasscode.Text))
            {
                btnCopyPasscode.Hide();
            }

            _action = AccountAction.EDIT;
        }

        /// <summary>
        /// return a Account data object from the entry values
        /// </summary>
        /// <returns></returns>
        public AccountData GetAccountData()
        {
            return _data;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles the Add click event
        /// </summary>
        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            try
            {
                _data.Name = ValidateValue("Name", txtName);
                _data.Website = txtWebsite.Text;
                _data.UserName = ValidateValue("User Name", txtUserName);
                _data.Password = ValidateValue("Password", txtPassword); ;
                _data.Passcode = txtPasscode.Text;
                _data.Other = txtOther.Text;

                _data.UpdateAccountAction(_action);
            }
            catch (Exception ex)
            {
                MessageBox.Show(null, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
            }
        }

        /// <summary>
        /// validate a field and throw an excpetion if it has not been populated
        /// </summary>
        private string ValidateValue(string field, TextBox text)
        {
            if (string.IsNullOrWhiteSpace(text.Text) == true)
            {
                throw new ArgumentException($"field {field} must be populated");
            }
            return text.Text;
        }

        #endregion

        #region Private Data Members

        private readonly AccountAction _action;

        private readonly AccountData _data;

        #endregion

        private void btnCopyPassword_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtPassword.Text);
        }

        private void btnCopyPasscode_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtPasscode.Text);
        }
    }
}
