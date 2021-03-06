﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using CSReportGlobals;

namespace CSReportDll
{
    public class cReportVariables : NameObjectCollectionBase
    {

        // Creates an empty collection.
        public cReportVariables()
        {
        }

        // Adds elements from an IDictionary into the new collection.
        public cReportVariables(IDictionary d, Boolean bReadOnly)
        {
            foreach (DictionaryEntry de in d)
            {
                this.BaseAdd((String)de.Key, de.Value);
            }
            this.IsReadOnly = bReadOnly;
        }

        // Gets a key-and-value pair (DictionaryEntry) using an index.
        public DictionaryEntry this[int index]
        {
            get
            {
                return (new DictionaryEntry(
                    this.BaseGetKey(index), this.BaseGet(index)));
            }
        }

        // Gets or sets the value associated with the specified key.
        public Object this[String key]
        {
            get
            {
                return (this.BaseGet(key));
            }
            set
            {
                this.BaseSet(key, value);
            }
        }

        // Gets a String array that contains all the keys in the collection.
        public String[] AllKeys
        {
            get
            {
                return (this.BaseGetAllKeys());
            }
        }

        // Gets an Object array that contains all the values in the collection.
        public Array AllValues
        {
            get
            {
                return (this.BaseGetAllValues());
            }
        }

        // Gets a String array that contains all the values in the collection.
        public String[] AllStringValues
        {
            get
            {
                return ((String[])this.BaseGetAllValues(typeof(String)));
            }
        }

        // Gets a value indicating if the collection contains keys that are not null.
        public Boolean HasKeys
        {
            get
            {
                return (this.BaseHasKeys());
            }
        }

        // Adds an entry to the collection.
        public void Add(String key, Object value)
        {
            this.BaseAdd(key, value);
        }

        // Removes an entry with the specified key from the collection.
        public void Remove(String key)
        {
            this.BaseRemove(key);
        }

        // Removes an entry in the specified index from the collection.
        public void Remove(int index)
        {
            this.BaseRemoveAt(index);
        }

        // Clears all the elements in the collection.
        public void Clear()
        {
            this.BaseClear();
        }

        // Removes an entry with the specified key from the collection.
        public void remove(String key)
        {
            this.BaseRemove(key);
        }

        // Removes an entry in the specified index from the collection.
        public void remove(int index)
        {
            this.BaseRemoveAt(index);
        }

        // Clears all the elements in the collection.
        public void clear()
        {
            this.BaseClear();
        }

        public int count()
        {
            return this.Count;
        }

        public cReportVariable item(String key)
        {
            try
            {
                return (cReportVariable)this.BaseGet(key);
            }
            catch
            {
                return null;
            }
        }

        public cReportVariable item(int index)
        {
            try
            {
                return (cReportVariable)this.BaseGet(index);
            }
            catch
            {
                return null;
            }
        }

        internal cReportVariable add(cReportVariable c, String key)
        {
            try
            {

                if (c == null)
                {
                    c = new cReportVariable();
                }

                if (key == "")
                {
                    key = cReportGlobals.getKey(cReportGlobals.getNextKey().ToString());
                }
                else
                {
                    cReportGlobals.refreshNextKey(key);
                    key = cReportGlobals.getKey(key);
                }

                Add(key, c);

                return c;
            }
            catch
            {
                return null;
            }
        }

    }

}
