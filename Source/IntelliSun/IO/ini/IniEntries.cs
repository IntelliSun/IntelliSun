using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IntelliSun.IO
{
    public class IniEntries : ICollection<IniEntry>
    {
        private readonly Dictionary<string, IniEntry> entries;

        public IniEntries()
        {
            this.entries = new Dictionary<string, IniEntry>();
        }

        public void AddRange(IEnumerable<IniEntry> items)
        {
            if (items == null)
                throw new ArgumentNullException("items");

            foreach (var item in items)
            {
                this.Add(item);
            }
        }

        public bool Contains(string key)
        {
            return this.entries.ContainsKey(key);
        }

        public Dictionary<string, IniEntry>.Enumerator GetKeysEnumerator()
        {
            return this.entries.GetEnumerator();
        }

        public bool Remove(string key)
        {
            if (this.Contains(key))
                return this.entries.Remove(key);

            return false;
        }

        public IniEntry this[string key]
        {
            get { return this.entries[key]; }
            set { this.entries[key] = value; }
        }

        public void Add(IniEntry item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (this.Contains(item.Key))
                throw new ArgumentException("An item with the same key is already exists.");

            this.entries.Add(item.Key, item);
        }

        public void Clear()
        {
            this.entries.Clear();
        }

        public bool Contains(IniEntry item)
        {
            return this.entries.Values.Contains(item);
        }

        public void CopyTo(IniEntry[] array, int arrayIndex)
        {
            this.entries.Values.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.entries.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(IniEntry item)
        {
            if (this.Contains(item))
                return this.entries.Remove(item.Key);

            return false;
        }

        public IEnumerator<IniEntry> GetEnumerator()
        {
            return this.entries.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}