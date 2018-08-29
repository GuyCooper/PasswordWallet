using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;
using System;

namespace PasswordWallet
{
    /// <summary>
    /// Wallet form
    /// </summary>
    public partial class Wallet : Form
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public Wallet()
        {
            m_logger = new Logger();
            m_datalayer = new XMLDataLayer();

            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;
            AddColumn(dataGridView, "Name", "Name");
            AddColumn(dataGridView, "Website", "Website");
            AddColumn(dataGridView, "UserName", "UserName");
            AddColumn(dataGridView, "Password", "Password");
            AddColumn(dataGridView, "Passcode", "Passcode");
            AddColumn(dataGridView, "Other", "Other");

            UpdateDisplayState();
        }

        #endregion

        /// <summary>
        /// Handle shortcut commands for the toolbar buttons
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case (Keys.Control | Keys.A):
                    BtnAddEntry.PerformClick();
                    break;
                case (Keys.Control | Keys.E):
                    BtnEditEntry.PerformClick();
                    break;
                case (Keys.Control | Keys.R):
                    BtnRemoveEntry.PerformClick();
                    break;
                case (Keys.Control | Keys.L):
                    BtnLoadData.PerformClick();
                    break;
                case (Keys.Control | Keys.M):
                    BtnMagnify.PerformClick();
                    break;
                case (Keys.Control | Keys.S):
                    BtnSaveChanges.PerformClick();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #region Private Methods

        /// <summary>
        /// Helper method for aadding a column to a datagrid view
        /// </summary>
        private void AddColumn(DataGridView gridView, string name, string propertyName)
        {
            gridView.Columns.Add(new DataGridViewColumn
            {
                HeaderText = name,
                Name = name,
                DataPropertyName = propertyName,
                CellTemplate = new DataGridViewTextBoxCell()
            });
        }

        /// <summary>
        /// Handle Add Entry event.
        /// </summary>
        private void BtnAddEntry_Click(object sender, System.EventArgs e)
        {
            try
            {
                var addEntry = new AddEntry();
                if (addEntry.ShowDialog() == DialogResult.OK)
                {
                    m_modified = true;
                    var data = addEntry.GetAccountData();
                    m_accountData.Add(data);
                    m_displayedaccountData.Add(data);
                    dataGridView.AutoResizeColumns();
                    dataGridView.AutoResizeRows();

                    UpdateDisplayState();
                }
            }
            catch(Exception ex)
            {
                m_logger.LogError(ex.Message);
            }
        }

        /// <summary>
        /// Handle rmove entry event.
        /// </summary>
        private void BtnRemoveEntry_Click(object sender, System.EventArgs e)
        {
            try
            { 
                if (dataGridView.SelectedRows.Count > 0)
                {
                    var accountItem = dataGridView.SelectedRows[0].DataBoundItem as AccountData;
                    if(accountItem != null)
                    {
                        if(MessageBox.Show(null, $"Are You Sure you want to delete account item {accountItem.Name}?","Delete Item", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            m_modified = true;
                            accountItem.UpdateAccountAction(AccountAction.REMOVE);
                            m_displayedaccountData.Remove(accountItem);

                            UpdateDisplayState();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                m_logger.LogError(ex.Message);
            }
        }

        /// <summary>
        /// Handle Edit entry event.
        /// </summary>
        private void BtnEditEntry_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (dataGridView.SelectedRows.Count > 0)
                {
                    var accountItem = dataGridView.SelectedRows[0].DataBoundItem as AccountData;
                    if (accountItem != null)
                    {
                        var editEntry = new AddEntry(accountItem);
                        if (editEntry.ShowDialog() == DialogResult.OK)
                        {
                            m_modified = true;
                            //m_accountData.Remove(accountItem);
                            //m_accountData.Add(editEntry.GetAccountData());

                            dataGridView.AutoResizeColumns();
                            dataGridView.AutoResizeRows();

                            UpdateDisplayState();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                m_logger.LogError(ex.Message);
            }
        }

        /// <summary>
        /// Handle save changes event.
        /// </summary>
        private void BtnSaveChanges_Click(object sender, System.EventArgs e)
        {
            try
            { 
                //remove accounts
                foreach (var account in m_accountData.Where(a => a.Action == AccountAction.REMOVE))
                {
                    if(m_datalayer.RemoveAccountDataItem(account) == false)
                    {
                        m_logger.LogError($"Failed to remove account: {account.Name}. Already exists");
                    }
                }

                //now add and edit accounts
                foreach (var account in m_accountData.Where(a => a.Action == AccountAction.ADD || a.Action == AccountAction.EDIT))
                {
                    if(m_datalayer.AddOrEditItem(account) == false)
                    {
                        m_logger.LogError($"Failed to AddorRemove account {account.Name}.");
                    }
                }
                //now pesist the data
                m_datalayer.CommitData();
                m_modified = false;
                UpdateDisplayState();

                if (MessageBox.Show(null, "Database successfully updated. Would you like to encrpyt it?", "Save Data", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    var encryptDlg = new DataEncrypt(m_encryptedFile, m_decryptedFile, m_certificate);
                    if (encryptDlg.ShowDialog() == DialogResult.OK)
                    {
                        m_encryptedFile = encryptDlg.EncryptedFile;
                        m_decryptedFile = encryptDlg.DecryptedFile;
                        m_certificate = encryptDlg.Certificate;

                        Encryption.EncryptFile(m_decryptedFile, m_encryptedFile, m_certificate);
                        MessageBox.Show("Database successfully encrypted!");
                    }
                }
            }
            catch (Exception ex)
            {
                m_logger.LogError(ex.Message);
            }
        }

        #endregion

        private void PopulateDisplayedList(IEnumerable<AccountData> list)
        {
            m_displayedaccountData.Clear();
            foreach (var item in list)
            {
                m_displayedaccountData.Add(item);
            }
        }

        /// <summary>
        /// invoked when the user clicks the load data button
        /// </summary>
        private void BtnLoadData_Click(object sender, EventArgs e)
        {
            var encryptDlg = new DataEncrypt(DefaultEncryptedFile, DefaultDecryptedFile, DefaultCertificate);
            if (encryptDlg.ShowDialog() == DialogResult.OK)
            {
                m_encryptedFile = encryptDlg.EncryptedFile;
                m_decryptedFile = encryptDlg.DecryptedFile;
                m_certificate = encryptDlg.Certificate;

                Cursor previous = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                if (string.IsNullOrWhiteSpace(m_encryptedFile) == false)
                {
                    Encryption.DecryptFile(m_encryptedFile, m_decryptedFile, encryptDlg.Certificate);
                }

                m_datalayer.ConnectionString = m_decryptedFile;
                loadData();
                Cursor.Current = previous;

                m_loaded = true;
                UpdateDisplayState();      
            }
        }

        /// <summary>
        /// reload the account data from the database
        /// </summary>
        private void loadData()
        {
            try
            {
                m_accountData.Clear();
                m_accountData.AddRange(m_datalayer.LoadAccountData().ToList());
                PopulateDisplayedList(m_accountData);

                passwordDataSource.DataSource = m_displayedaccountData;
                dataGridView.DataSource = passwordDataSource;

                //dataGridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
                dataGridView.AutoResizeColumns();
                dataGridView.AutoResizeRows();
            }
            catch(Exception ex)
            {
                m_logger.LogError(ex.Message);              
            }
        }

        /// <summary>
        /// updates the display state of the toolbar buttons
        /// </summary>
        private void UpdateDisplayState()
        {
            BtnAddEntry.Enabled = m_loaded == true;
            BtnEditEntry.Enabled = dataGridView.SelectedRows.Count > 0;
            BtnLoadData.Enabled = m_loaded == false;
            BtnRemoveEntry.Enabled = dataGridView.SelectedRows.Count > 0;
            BtnSaveChanges.Enabled = m_modified == true;
            BtnMagnify.Enabled = dataGridView.SelectedRows.Count > 0;
        }

        /// <summary>
        /// Method invoked when user clicks on the magnify button
        /// </summary>
        private void BtnMagnify_Click(object sender, EventArgs e)
        {
            var accountItem = dataGridView.SelectedRows[0].DataBoundItem as AccountData;
            if (accountItem != null)
            {
                var magnifer = new Magnify(accountItem);
                magnifer.ShowDialog();
            }
        }

        /// <summary>
        /// Invoked when the selection on the grid view changes.
        /// </summary>
        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            UpdateDisplayState();
        }

        /// <summary>
        /// Invoked on a key jup event on the filter text box.
        /// </summary>
        private void txtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtFilter.Text.Length == 0)
            {
                PopulateDisplayedList(m_accountData);
            }
            else
            {
                PopulateDisplayedList(m_accountData.Where(a => a.Name.ToUpper().Contains(txtFilter.Text.ToUpper())));
            }
        }

        #region Private Data Members

        private readonly List<AccountData> m_accountData = new List<AccountData>();
        private readonly BindingList<AccountData> m_displayedaccountData = new BindingList<AccountData>();
        private readonly IDataLayer m_datalayer;

        private string m_encryptedFile;
        private string m_decryptedFile;
        private string m_certificate;

        private static readonly string DefaultEncryptedFile = @"C:\Users\guy\OneDrive\Documents\Holidays.enc";
        private static readonly string DefaultCertificate = "CERT_SIGN_PASSWORD_DATA";
        private static readonly string DefaultDecryptedFile = @"C:\Data\PasswordData.xml";

        private bool m_loaded = false;
        private bool m_modified = false;
        private ILogger m_logger;

        #endregion
    }

    /// <summary>
    /// Logger class for logging errors. 
    /// </summary>
    class Logger : ILogger
    {
        public void LogError(string error)
        {
            MessageBox.Show(null, error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
