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
            m_datalayer = new SQLDataLayer(m_logger);

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
                            m_accountData.Remove(accountItem);
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
                bool ok = false;
                var existingAccountData = m_datalayer.LoadAccountData();

                var removedAccounts = existingAccountData.Where(a =>
                {
                    return m_accountData.FirstOrDefault(i => string.Equals(i.Name, a.Name, System.StringComparison.InvariantCultureIgnoreCase) == false) != null;
                }).ToList();

                //remove accounts
                foreach (var account in removedAccounts)
                {
                    ok = m_datalayer.RemoveAccountDataItem(account);
                    if (ok == false)
                    {
                        return;
                    }
                }

                //now add and edit accounts
                foreach (var account in m_accountData)
                {
                    ok = m_datalayer.AddOrEditItem(account);
                    if (ok == false)
                    {
                        return;
                    }
                }

                if (ok == true)
                {
                    m_modified = false;
                    UpdateDisplayState();

                    if (MessageBox.Show(null, "Database successfully updated. Would you like to encrpyt it?", "Save Data", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        var encryptDlg = new DataEncrypt(DefaultEncryptedFile, DefaultDatabaseFile, DefaultCertificate);
                        if (encryptDlg.ShowDialog() == DialogResult.OK)
                        {
                            //m_datalayer.DetachDatabase();
                            Encryption.EncryptFile(encryptDlg.DatabaseFile, encryptDlg.EncryptedFile, encryptDlg.Certificate);
                        }
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

        /// <summary>
        /// invoked when the user clicks the load data button
        /// </summary>
        private void BtnLoadData_Click(object sender, EventArgs e)
        {
            var encryptDlg = new DataEncrypt(DefaultEncryptedFile, DefaultDatabaseFile, DefaultCertificate);
            if (encryptDlg.ShowDialog() == DialogResult.OK)
            {
                Cursor previous = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;
                if (string.IsNullOrWhiteSpace(encryptDlg.EncryptedFile) == false)
                {
                    Encryption.DecryptFile(encryptDlg.EncryptedFile, encryptDlg.DatabaseFile, encryptDlg.Certificate);
                }

                m_databaseFile = encryptDlg.DatabaseFile;
                var datasource = $@"Data Source = (LocalDb)\MSSQLLocalDB; AttachDbFilename = {m_databaseFile}; Initial Catalog = PassportWallet; Integrated Security = True";
                m_datalayer.ConnectionString = datasource;
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

        #region Private Data Members

        private readonly List<AccountData> m_accountData = new List<AccountData>();
        private readonly BindingList<AccountData> m_displayedaccountData = new BindingList<AccountData>();
        private readonly IDataLayer m_datalayer;

        private static readonly string DefaultEncryptedFile = @"C:\Users\guy\OneDrive\Documents\Holidays.enc";
        private static readonly string DefaultCertificate = "CERT_SIGN_PASSWORD_DATA";
        private static readonly string DefaultDatabaseFile = @"C:\Data\SQLServer\FileDB\PasswordWallet.mdf";

        private string m_databaseFile;
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
