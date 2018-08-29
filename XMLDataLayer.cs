using System;
using System.Collections.Generic;
using XmlStorage;

namespace PasswordWallet
{
    /// <summary>
    ///XML Data Layer. wraps the XMLStorage class that persists data to an xml file
    /// </summary>
    class XMLDataLayer : IDataLayer
    {
        #region Public Properties

        public string ConnectionString { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Add or edit item to storage
        /// </summary>
        public bool AddOrEditItem(AccountData item)
        {
            item.Action = AccountAction.NONE;
            GetDataStorage().AddorUpdatetem(item);
            return true;
        }

        /// <summary>
        /// Load all items. deferred load.
        /// </summary>
        public IEnumerable<AccountData> LoadAccountData()
        {
            return GetDataStorage().GetAllItems();
        }

        /// <summary>
        /// remove item from cache
        /// </summary>
        public bool RemoveAccountDataItem(AccountData item)
        {
            return GetDataStorage().RemoveItem(item);
        }

        /// <summary>
        /// Persist data to disk
        /// </summary>
        public void CommitData()
        {
            GetDataStorage().Commit();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Return the storage item
        /// </summary>
        private XmlDataStorage<AccountData> GetDataStorage()
        {
            if(m_dataStorage == null)
            {
                if(string.IsNullOrWhiteSpace(ConnectionString) == true)
                {
                    throw new ArgumentException("Connection strign mjst be set before use");
                }
                //index of the Name parameter
                m_dataStorage = new XmlDataStorage<AccountData>(ConnectionString, "Name");
            }
            return m_dataStorage;
        }

        #endregion

        #region Private Member Data

        private XmlDataStorage<AccountData> m_dataStorage;

        #endregion

    }
}
