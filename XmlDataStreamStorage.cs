using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PasswordWallet
{
    internal class XmlDataStreamStorage : IDataLayer
    {
        public string ConnectionString
        {
            get => _data; set 
                {
                    _data = value;
                    LoadData();
                } }

        public XmlDataStreamStorage()
        {

        }

        public bool AddOrEditItem(AccountData item)
        {
            item.Action = AccountAction.NONE;
            var key = item.Name;
            string serialisedItem = SerialiseItem(item);
            if (m_index.ContainsKey(key) == true)
            {
                m_index[key] = serialisedItem;
            }
            else
            {
                m_index.Add(key, serialisedItem);
            }
            m_dirty = true;
            return true;
        }

        public void CommitData()
        {
            if (m_dirty == false)
            {
                return;
            }

            using (var ms = new MemoryStream())
            {
                var serialiser = new XmlSerializer(typeof(List<string>));
                var items = m_index.Values.ToList();
                serialiser.Serialize(ms, items);

                _data = Encoding.UTF8.GetString(ms.ToArray());
            }

            m_dirty = false;

        }

        public IEnumerable<AccountData> LoadAccountData()
        {
            return m_index.Values.Select(item => DeSerialiseItem(item));
        }

        public bool RemoveAccountDataItem(AccountData item)
        {
            var key = item.Name;
            var removed = m_index.Remove(key);
            m_dirty |= removed;
            return removed;
        }

        /// <summary>
        /// Serialise an item
        /// </summary>
        private string SerialiseItem(AccountData item)
        {
            var serialiser = new XmlSerializer(typeof(AccountData));
            using (var sw = new StringWriter())
            {
                serialiser.Serialize(sw, item);
                return sw.ToString();
            }
        }

        /// <summary>
        /// deserialise an item
        /// </summary>
        private AccountData DeSerialiseItem(string item)
        {
            var serialiser = new XmlSerializer(typeof(AccountData));
            using (var sr = new StringReader(item))
            {
                return (AccountData)serialiser.Deserialize(sr);
            }
        }

        private void LoadData()
        {
            if(string.IsNullOrEmpty(_data ))
            {
                return;
            }

            m_index.Clear();
            IEnumerable<string> items;
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(_data)))
            {
                var serialiser = new XmlSerializer(typeof(List<string>));
                items = (List<string>)serialiser.Deserialize(ms);
            }

            foreach (var str in items)
            {
                var item = DeSerialiseItem(str);
                m_index.Add(item.Name, str);
            }
        }

        #region Private Data Members

        //indexed lookup of item to serialised value
        private readonly Dictionary<string, string> m_index = new Dictionary<string, string>();

        private bool m_dirty;

        private string _data;

        #endregion
    }
}
