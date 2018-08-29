using System;
using System.Windows.Forms;

namespace PasswordWallet
{
    /// <summary>
    /// Form for selecting the encrypted database file and the target database file
    /// </summary>
    public partial class DataEncrypt : Form
    {
        #region Public Properties

        public string EncryptedFile { get { return txtEncryptedFile.Text; } }

        public string DecryptedFile { get { return txtDecryptedFile.Text; } }

        public string Certificate { get { return txtCertificate.Text; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public DataEncrypt(string encryptedFile, string decryptedFile, string certificate)
        {
            InitializeComponent();
            txtCertificate.Text = certificate;
            txtDecryptedFile.Text = decryptedFile;
            txtEncryptedFile.Text = encryptedFile;
        }

        #endregion

        #region Private Methods

        private void btnPickFile_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtEncryptedFile.Text = openFileDialog1.FileName;
            }
        }

        private void btnPickFile1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtDecryptedFile.Text = openFileDialog1.FileName;
            }
        }

        #endregion
    }
}
