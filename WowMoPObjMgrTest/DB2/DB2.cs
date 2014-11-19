using System;
using System.Collections;
using System.Collections.Generic;

namespace WowMoPObjMgrTest
{
    class DB2<T> : IReadOnlyDictionary<int, DB2Row<T>> where T : struct
    {
        private WoWClientDB2 db2Internal;
        private Dictionary<int, DB2Row<T>> cache = new Dictionary<int, DB2Row<T>>();

        public int MinIndex { get { return db2Internal.MinIndex; } }
        public int MaxIndex { get { return db2Internal.MaxIndex; } }

        public DB2(IntPtr address)
        {
            db2Internal = Memory.Read<WoWClientDB2>(address);
        }

        public IEnumerable<int> Keys
        {
            get
            {
                for (int i = MinIndex; i <= MaxIndex; ++i)
                {
                    if (ContainsKey(i))
                        yield return i;
                }
            }
        }

        public IEnumerable<DB2Row<T>> Values
        {
            get
            {
                for (int i = MinIndex; i <= MaxIndex; ++i)
                {
                    if (ContainsKey(i))
                        yield return this[i];
                }
            }
        }

        public int Count
        {
            get
            {
                return db2Internal.NumRows;
            }
        }

        public DB2Row<T> this[int key]
        {
            get
            {
                DB2Row<T> result;

                if (cache.TryGetValue(key, out result))
                    return result;

                IntPtr rowPtr = Memory.Read<IntPtr>(db2Internal.Rows + (key - db2Internal.MinIndex) * IntPtr.Size);

                if (rowPtr != IntPtr.Zero)
                {
                    result = new DB2Row<T>(rowPtr);
                    cache.Add(key, result);
                    return result;
                }

                return null;
            }
        }

        public bool ContainsKey(int key)
        {
            if (cache.ContainsKey(key))
                return true;

            return this[key] != null;
        }

        public bool TryGetValue(int key, out DB2Row<T> value)
        {
            value = this[key];

            return value != null;
        }

        public IEnumerator<KeyValuePair<int, DB2Row<T>>> GetEnumerator()
        {
            for (int i = MinIndex; i <= MaxIndex; ++i)
            {
                DB2Row<T> row = this[i];

                if (row != null)
                    yield return new KeyValuePair<int, DB2Row<T>>(i, row);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
