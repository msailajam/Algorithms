using System;
using System.Collections.Generic;
using System.Text;

/******************************************************************************
 A generice tablehash implementation
  TrimAccess
 ******************************************************************************/

namespace SM.Algorithms
{
    class TableHash<TKey, TValue>
    {
        // key value pair stored as linked list in case of hash collision the value is added to the linked list at the hashed index
        class Node
        {
            public TKey key;
            public TValue value;
            public Node next = null;

            public Node(TKey iKey, TValue iValue, Node iNext)
            {
                key = iKey;
                value = iValue;
                next = iNext;
            }
        }

        private int n = 0;
        private int m = 997;
        private Node[] kvtable;
        public TableHash()
        {
            InitializeKeyValueTable();
        }
        public TableHash(int iM)
        {
            m = iM;
            InitializeKeyValueTable();
        }

        private void InitializeKeyValueTable()
        {
            kvtable = new Node[m];
        }
        //returns modular hash value between 0 and m. to get more distributed key value table pick m as a prime number
        private int GetHash(TKey iKey)
        {
            return (iKey.GetHashCode() & 0x7fffffff) % m;
        }

        //insert key value pair into the table
        public void Add(TKey iKey, TValue iValue)
        {
            //perform any validations on iValue throw exception on failure
            int index = GetHash(iKey);
            n++;
            //check if key already exists then update with value
            for (Node temp = kvtable[index]; temp != null; temp = temp.next)
            {
                if (iKey.Equals(temp.key))
                {
                    kvtable[index].value = iValue;
                    return;
                }
            }
            n++;
            kvtable[index] = new Node(iKey, iValue, kvtable[index]);
        }

        public bool ContainsKey(TKey iKey)
        {
            int index = GetHash(iKey);
            for (Node temp = kvtable[index]; temp != null; temp = temp.next)
            {
                if (temp.key.Equals(iKey))
                {
                    return true;
                }
            }
            return false;
        }

        public bool ContainsValue(TValue iValue)
        {
            for (int i = 0; i < m; i++)
            {
                for (Node temp = kvtable[i]; temp != null; temp = temp.next)
                {
                    if (iValue.Equals(temp.value))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public TValue GetValue(TKey iKey)
        {
            int index = GetHash(iKey);
            for (Node temp = kvtable[index]; temp != null; temp = temp.next)
            {
                if (temp.key.Equals(iKey))
                {
                    return temp.value;
                }
            }
            throw new KeyNotFoundException("Key does not exist");
        }
        public void Remove(TKey iKey)
        {
            throw new NotImplementedException("Remove currently not supported");
        }
        public int Count()
        {
            return n;
        }

        public void Clear()
        {
            InitializeKeyValueTable();
            n = 0;
        }
        public int EnsureCapacity(int newCapacity)
        {
            // no need to implement as in case of collision we are storing in linked list
            //should duplicate or resize the existing array if we want to limit the linked list size.
            //if successfull return new capacity if not old capacity
            throw new NotImplementedException("Remove currently not supported");
        }

        public List<TKey> Keys
        {
            get
            {
                if (n > 0)
                {
                    List<TKey> allKeys = new List<TKey>();
                    for (int i = 0; i < m; i++)
                    {
                        for (Node temp = kvtable[i]; temp != null; temp = temp.next)
                        {
                            allKeys.Add(temp.key);
                        }
                    }
                    return allKeys;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<TValue> Values
        {
            get
            {
                if (n > 0)
                {
                    List<TValue> allValues = new List<TValue>();
                    for (int i = 0; i < m; i++)
                    {
                        for (Node temp = kvtable[i]; temp != null; temp = temp.next)
                        {
                            allValues.Add(temp.value);
                        }
                    }
                    return allValues;
                }
                else
                {
                    return null;
                }

            }
        }

        public TValue this[TKey iKey]
        {
            get
            {
                return GetValue(iKey);
            }

            set
            {
                Add(iKey, value);
            }
        }
    }
}