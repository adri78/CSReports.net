﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace CSReportScript
{

    public class cReportCompilerGlobals : NameObjectCollectionBase
    {

        // Creates an empty collection.
        public cReportCompilerGlobals()
        {
        }

        // Adds elements from an IDictionary into the new collection.
        public cReportCompilerGlobals(IDictionary d, Boolean bReadOnly)
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

        private eReportCompilerMode m_mode = 0;

        public eReportCompilerMode getMode()
        {
            return m_mode;
        }
        
        public void setMode(eReportCompilerMode rhs)
        {
            m_mode = rhs;
        }

        public cReportCompilerVar addVar(String varName)
        {
            try
            {
                Add(varName, new cReportCompilerVar());
                return getVar(varName);
            }
            catch
            {
                return null;
            }
        }

        public int count()
        {
            return this.Count;
        }

        public cReportCompilerVar getVar(String varName)
        {
            try
            {
                return (cReportCompilerVar)this.BaseGet(varName);
            }
            catch 
            {
                return null;
            }
        }

    }

    public enum eReportCompilerMode
    { 
        C_EVAL,
        C_RESULT
    }

}
