using System;
using System.Windows.Forms;

namespace PasswordWallet
{
    public partial class Settings : Form
    {
        public string DirectLoadFile {  get { return txtDirectLoadFile.Text; } }

        public string EncryptedFile { get { return txtEncryptedFile.Text;  } }

        public Settings(string directLoadFile, string encryptedFile)
        { 
            InitializeComponent();
            txtDirectLoadFile.Text = directLoadFile;
            txtEncryptedFile.Text = encryptedFile;
        }

        private void btnLoadDirectFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtDirectLoadFile.Text = openFileDialog1.FileName;
            }
        }

        private void btnLoadEncryptedFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtEncryptedFile.Text = openFileDialog1.FileName;
            }
        }
    }
}
