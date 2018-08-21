using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PasswordWallet
{
    /// <summary>
    /// SQL Server implementation of IDataLayer
    /// </summary>
    class SQLDataLayer : IDataLayer
    {
        #region public Properties

        public string ConnectionString { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public SQLDataLayer(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Method for adding or editing an item in the database
        /// </summary>
        public bool AddOrEditItem(AccountData item)
        {
            bool ret = false;
            try
            {
                using (var connection = OpenConnection())
                {
                    using (var sqlCommand = new SqlCommand("sp_AddorEditAccountDataItem", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add(new SqlParameter("@Name", item.Name));
                        sqlCommand.Parameters.Add(new SqlParameter("@Website", item.Website));
                        sqlCommand.Parameters.Add(new SqlParameter("@Username", item.UserName));
                        sqlCommand.Parameters.Add(new SqlParameter("@Password", item.Password));
                        sqlCommand.Parameters.Add(new SqlParameter("@Passcode", item.Passcode));
                        sqlCommand.Parameters.Add(new SqlParameter("@Other", item.Other));
                        sqlCommand.ExecuteNonQuery();
                        ret = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return ret;
        }

        /// <summary>
        /// Load AccountData.
        /// </summary>
        public IEnumerable<AccountData> LoadAccountData()
        {
            using (var connection = OpenConnection())
            {
                using (var sqlCommand = new SqlCommand("sp_LoadAccountDataItems", connection))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new AccountData
                            {
                                Name = GetDBValue<string>("Name", reader),
                                Website = GetDBValue<string>("Website", reader),
                                UserName = GetDBValue<string>("Username", reader),
                                Password = GetDBValue<string>("Password", reader),
                                Passcode = GetDBValue<string>("Passcode", reader),
                                Other = GetDBValue<string>("Other", reader),
                            };      
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Remove AccountData item.
        /// </summary>
        public bool RemoveAccountDataItem(AccountData item)
        {
            bool ret = false;
            try
            {
                using (var connection = OpenConnection())
                {
                    using (var sqlCommand = new SqlCommand("sp_RemoveAccountDataItem", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add(new SqlParameter("@Name", item.Name));
                        sqlCommand.ExecuteNonQuery();
                        ret = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return ret;

        }

        /// <summary>
        /// Detach database to allow file to be copied
        /// </summary>
        public void DetachDatabase()
        {
            try
            {
                using (var connection = OpenConnection())
                {
                    using (var sqlCommand = new SqlCommand("sp_detach_db", connection))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add(new SqlParameter("@dbname", "PassportWallet"));
                        sqlCommand.Parameters.Add(new SqlParameter("@keepfulltextindexfile", true));
                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Open a database connection
        /// </summary>
        private SqlConnection OpenConnection()
        {
           
             var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// helper method for extracting a database value in a recordset
        /// </summary>
        protected T GetDBValue<T>(string name, SqlDataReader reader, T defaultVal = default(T))
        {
            var result = reader[name];
            if (result.GetType() != typeof(System.DBNull))
            {
                return (T)result;
            }

            return defaultVal;
        }

        #endregion

        #region Private Data Members

        private ILogger _logger;

        #endregion
    }
}
