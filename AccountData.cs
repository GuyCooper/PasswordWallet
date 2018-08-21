using System.Reflection;

namespace PasswordWallet
{
    public enum AccountAction
    {
        NONE = 0,
        ADD,
        REMOVE,
        EDIT
    }

    /// <summary>
    /// Class for storing password account data
    /// </summary>
    public class AccountData
    {
        #region Public Properties

        public string Name { get; set; }
        public string Website { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Passcode { get; set; }
        public string Other { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Default Constructor
        /// </summary>
        public AccountData()
        {
            Action = AccountAction.NONE;
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        public AccountData(AccountData data)
        {
            CopyObject(data, this);
        }

        /// <summary>
        /// return a clone of this account data instance
        /// </summary>
        public AccountData Clone()
        {
            var clone = new AccountData();
            CopyObject(this, clone);
            return clone;
        }

        /// <summary>
        /// Applies some logic for setting the account action
        /// </summary>
        public void UpdateAccountAction(AccountAction newAction)
        {
            switch (Action)
            {
                case AccountAction.NONE:
                    Action = newAction;
                    break;
                case AccountAction.ADD:
                    //can only remove an added account
                    if (newAction == AccountAction.REMOVE)
                    {
                        Action = newAction;
                    }
                    break;
                case AccountAction.REMOVE:
                    //can only add an account that has been removed
                    if (newAction == AccountAction.ADD)
                    {
                        Action = newAction;
                    }
                    break;
                case AccountAction.EDIT:
                    //can only remove an edited account
                    if (newAction == AccountAction.REMOVE)
                    {
                        Action = newAction;
                    }
                    break;
            }
        }

        #endregion

        public AccountAction Action { get; set; }

        #region Private Methods

        /// <summary>
        /// Helper method for cloning an account data object
        /// </summary>
        private static void CopyObject(AccountData copyFrom, AccountData copyTo )
        {
            var properties = copyTo.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var property in properties)
            {
                var val = property.GetMethod.Invoke(copyFrom, null);
                property.SetMethod.Invoke(copyTo, new object[] { val });
            }
        }

        #endregion
    }
}
