using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

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
        public Wallet(IDataLayer dataLayer)
        {
            m_logger = new Logger();
            m_datalayer = dataLayer;

            _encryptedFileName = DefaultEncryptedFile;
            _directLoadFileName = DirectLoadFile;

            InitializeComponent();

            dataGridView.AutoGenerateColumns = false;
            AddColumn(dataGridView, "Name", "Name");
            AddColumn(dataGridView, "Website", "Website");
            AddColumn(dataGridView, "UserName", "UserName");
            //AddColumn(dataGridView, "Password", "Password");
            //AddColumn(dataGridView, "Passcode", "Passcode");
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
                    if (dataGridView.SelectedRows[0].DataBoundItem is AccountData accountItem)
                    {
                        if (MessageBox.Show(null, $"Are You Sure you want to delete account item {accountItem.Name}?", "Delete Item", MessageBoxButtons.YesNo) == DialogResult.Yes)
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
                    if (dataGridView.SelectedRows[0].DataBoundItem is AccountData accountItem)
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
        private async void BtnSaveChanges_Click(object sender, System.EventArgs e)
        {
            Cursor previous = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

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

                if(string.IsNullOrEmpty(m_password))
                {
                    var passwordDlg = new PasswordConfirm();
                    if(passwordDlg.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    m_password = passwordDlg.EnteredPassword;
                }

                await PasswordFileEncrypter.StringEncrypt(m_datalayer.ConnectionString, _encryptedFileName, m_password);

            }
            catch (Exception ex)
            {
                m_logger.LogError(ex.Message);
                MessageBox.Show("Load Database Failed", $"{ex.Message}");
            }
            finally
            {
                Cursor.Current = previous;
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
        /// reload the account data from the database
        /// </summary>
        private async Task LoadData()
        {
            try
            {
                if (File.Exists(_directLoadFileName))
                {
                    m_datalayer.ConnectionString = File.ReadAllText(_directLoadFileName);
                }
                else
                {
                    var passwordDlg = new Password();
                    if (passwordDlg.ShowDialog() != DialogResult.OK)
                    {
                        return;
                    }

                    m_password = passwordDlg.EnteredPassword;

                    m_datalayer.ConnectionString = await PasswordFileEncrypter.FileDecrypt(_encryptedFileName, m_password);
                }

                m_accountData.Clear();
                m_accountData.AddRange(m_datalayer.LoadAccountData().ToList());
                PopulateDisplayedList(m_accountData);

                passwordDataSource.DataSource = m_displayedaccountData;
                dataGridView.DataSource = passwordDataSource;

                //dataGridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
                dataGridView.AutoResizeColumns();
                dataGridView.AutoResizeRows();

                UpdateDisplayState();
                m_loaded = true;
            }
            catch (Exception ex)
            {
                m_logger.LogError(ex.Message);
                MessageBox.Show("Load Database Failed", $"{ex.Message}");
            }
        }

        /// <summary>
        /// updates the display state of the toolbar buttons
        /// </summary>
        private void UpdateDisplayState()
        {
            BtnAddEntry.Enabled = m_loaded == true;
            BtnEditEntry.Enabled = dataGridView.SelectedRows.Count > 0;
            BtnRemoveEntry.Enabled = dataGridView.SelectedRows.Count > 0;
            BtnSaveChanges.Enabled = m_modified == true;
            BtnMagnify.Enabled = dataGridView.SelectedRows.Count > 0;
        }

        /// <summary>
        /// Method invoked when user clicks on the magnify button
        /// </summary>
        private void BtnMagnify_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows[0].DataBoundItem is AccountData accountItem)
            {
                var magnifer = new Magnify(accountItem);
                magnifer.ShowDialog();
            }
        }

        /// <summary>
        /// Invoked when the selection on the grid view changes.
        /// </summary>
        private void DataGridView_SelectionChanged(object sender, EventArgs e)
        {
            UpdateDisplayState();
        }

        /// <summary>
        /// Invoked on a key jup event on the filter text box.
        /// </summary>
        private void TxtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtFilter.Text.Length == 0)
            {
                PopulateDisplayedList(m_accountData);
            }
            else
            {
                var filteredData = m_accountData.Where(
                    a => a.Name.ToUpper().Contains(txtFilter.Text.ToUpper())
                    || a.Other.ToUpper().Contains(txtFilter.Text.ToUpper())).ToList();

                PopulateDisplayedList(filteredData);
            }
        }

        private async void Wallet_Load(object sender, EventArgs e)
        {
            Cursor previous = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                await LoadData();
            }
            finally
            {
                Cursor.Current = previous;
            }
        }

        #region Private Data Members

        private readonly List<AccountData> m_accountData = new List<AccountData>();
        private readonly BindingList<AccountData> m_displayedaccountData = new BindingList<AccountData>();
        private readonly IDataLayer m_datalayer;

        private string m_password = null;

        private static readonly string DefaultEncryptedFile = @"C:\Users\guy\OneDrive\Documents\Holidays1.enc";
        //private static readonly string DefaultDecryptedFile = @"C:\Projects\Data\TmpData.xml";
        private static readonly string DirectLoadFile = @"C:\Projects\Data\PasswordData.xml";

        private string _encryptedFileName;
        private string _directLoadFileName;

        private bool m_loaded = false;
        private bool m_modified = false;
        private readonly ILogger m_logger;

        #endregion

        private void btnSettings_Click(object sender, EventArgs e)
        {
            var settings = new Settings(_directLoadFileName, _encryptedFileName);
            if(settings.ShowDialog() == DialogResult.OK)
            {
                _directLoadFileName = settings.DirectLoadFile;
                _encryptedFileName = settings.EncryptedFile;
            }
        }
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
