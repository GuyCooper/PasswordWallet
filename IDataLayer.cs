using System.Collections.Generic;

namespace PasswordWallet
{
    /// <summary>
    /// Interface to passport wallet datalayer
    /// </summary>
    public interface IDataLayer
    {
        string ConnectionString { get; set; }

        bool AddOrEditItem(AccountData item);
        IEnumerable<AccountData> LoadAccountData();
        bool RemoveAccountDataItem(AccountData item);
        void CommitData();
    }

    interface ILogger
    {
        void LogError(string error);
    }
}
