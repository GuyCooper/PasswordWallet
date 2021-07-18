using System.Windows.Forms;

namespace PasswordWallet
{
    /// <summary>
    /// Magnify Form. Allows user to filter password and passcodes to view just characheters at the indexes
    /// specified in the filter fields.
    /// </summary>
    public partial class Magnify : Form
    {
        public Magnify(AccountData data)
        {
            InitializeComponent();
            m_Data = data;
            txtPassCode.Text = data.Passcode;
            txtPassword.Text = data.Password;
        }

        /// <summary>
        /// Method called when key up event fired on password filter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtFilterPassword_KeyUp(object sender, KeyEventArgs e)
        {
            FilterValue(txtPassword, txtFilterPassword.Text, m_Data.Password);
        }

        /// <summary>
        /// Method called when key up event occurs on passcode filter
        /// </summary>
        private void TxtFilterPasscode_KeyUp(object sender, KeyEventArgs e)
        {
            FilterValue(txtPassCode, txtFilterPasscode.Text, m_Data.Passcode);
        }

        /// <summary>
        /// Helper method for filtering the password or passcode
        /// </summary>
        private void FilterValue(TextBox target, string filter, string defaultValue)
        {
            if (filter.Length == 0)
            {
                target.Text = defaultValue;
            }
            else
            {
                var indexes = filter.Split(',');
                string filtered = "";
                foreach (var i in indexes)
                {
                    if ((int.TryParse(i, out int index) == true) && (index <= defaultValue.Length))
                    {
                        filtered += defaultValue[index - 1];
                    }
                }
                target.Text = filtered;
            }
        }

        #region Private Data Members

        private readonly AccountData m_Data;

        #endregion

    }
}
