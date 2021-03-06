﻿using System.IO;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Reflection;
using System.Linq;

namespace XmlStorage
{
    /// <summary>
    /// Xml Storage class. Persists data to xml file
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class XmlDataStorage<T> 
    {
        #region Public methods

        public XmlDataStorage(string source, string index)
        {
            m_keyPropertyInfo = typeof(T).GetProperty(index);
            m_source = source;
            LoadFile();
        }

        /// <summary>
        /// Commit changes to disk
        /// </summary>
        public void Commit()
        {
            if(m_dirty == false)
            {
                return;
            }

            using (var fs = File.Open(m_source, FileMode.Create, FileAccess.Write))
            {
                var serialiser = new XmlSerializer(typeof(List<string>));
                var items = m_index.Values.ToList();
                serialiser.Serialize(fs, items);
            }

            m_dirty = false;
        }

        /// <summary>
        /// Return a copy of the item keyed on lookup
        /// </summary>
        public T GetItem(string lookup)
        {
            m_index.TryGetValue(lookup, out string val);
            if (val != null)
            {
                return DeSerialiseItem(val);
            }
            return default;
        }

        /// <summary>
        /// return a list of all items in the cache.
        /// </summary>
        public IEnumerable<T> GetAllItems()
        {
            return m_index.Values.Select(item => DeSerialiseItem(item));
        }

        /// <summary>
        /// Add or update an item.
        /// </summary>
        public void AddorUpdatetem(T item)
        {
            var key = m_keyPropertyInfo.GetValue(item).ToString();
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
        }

        /// <summary>
        /// Remove an item.
        /// </summary>
        public bool RemoveItem(T item)
        {
            var key = m_keyPropertyInfo.GetValue(item).ToString();
            var removed = m_index.Remove(key);
            m_dirty |= removed;
            return removed;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// load file from disk and popluate the index
        /// </summary>
        private void LoadFile()
        {
            if (m_keyPropertyInfo == null)
            {
                throw new ApplicationException("Must be setup with valid index before loading!");
            }

            //if file doesn't exist don't try and open it and populate cache.
            if(File.Exists(m_source) == false)
            {
                return;
            }

            IEnumerable<string> items;
            using (var fs = File.OpenRead(m_source))
            {
                var serialiser = new XmlSerializer(typeof(List<string>));
                items = (List<string>)serialiser.Deserialize(fs);
            }

            foreach (var str in items)
            {
                var item = DeSerialiseItem(str);
                m_index.Add(m_keyPropertyInfo.GetValue(item).ToString(), str);
            }
        }

        /// <summary>
        /// Serialise an item
        /// </summary>
        private string SerialiseItem(T item)
        {
            var serialiser = new XmlSerializer(typeof(T));
            using (var sw = new StringWriter())
            {
                serialiser.Serialize(sw, item);
                return sw.ToString();
            }
        }

        /// <summary>
        /// deserialise an item
        /// </summary>
        private T DeSerialiseItem(string item)
        {
            var serialiser = new XmlSerializer(typeof(T));
            using (var sr = new StringReader(item))
            {
                return (T)serialiser.Deserialize(sr);
            }
        }

        #endregion

        #region Private Data Members

        //indexed lookup of item to serialised value
        private readonly Dictionary<string, string> m_index = new Dictionary<string, string>();

        private readonly PropertyInfo m_keyPropertyInfo;

        private readonly string m_source;

        private bool m_dirty;

        #endregion
    }
}
