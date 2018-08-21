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

        public string DatabaseFile { get { return txtDatabaseFile.Text; } }

        public string Certificate { get { return txtCertificate.Text; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public DataEncrypt(string encryptedFile, string databaseFile, string certificate)
        {
            InitializeComponent();
            txtCertificate.Text = certificate;
            txtDatabaseFile.Text = databaseFile;
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
                txtDatabaseFile.Text = openFileDialog1.FileName;
            }
        }

        #endregion
    }
}
