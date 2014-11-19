using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace WowMoPObjMgrTest
{
    class DBC<T> : IReadOnlyDictionary<int, T> where T : struct
    {
        private Dictionary<int, T> m_cache;
        private readonly WoWClientDB m_dbInfo;
        private readonly DBCFile m_fileHdr;
        private readonly bool m_cacheEnabled;

        public int MinIndex { get { return m_dbInfo.MinIndex; } }
        public int MaxIndex { get { return m_dbInfo.MaxIndex; } }

        /// <summary>
        /// Initializes a new instance of DBC class using specified memory address
        /// </summary>
        /// <param name="dbcBase">DBC's memory address</param>
        public DBC(IntPtr dbcBase, bool enableCache = true)
        {
            m_dbInfo = Memory.Read<WoWClientDB>(dbcBase);
            m_fileHdr = Memory.Read<DBCFile>(m_dbInfo.Data);

            if (enableCache)
            {
                m_cache = new Dictionary<int, T>();

                for (int i = MinIndex; i <= MaxIndex; ++i)
                    if (ContainsKey(i))
                        m_cache[i] = this[i];
            }

            m_cacheEnabled = enableCache;
        }

        public IntPtr GetRowPtr(int id)
        {
            if (id < MinIndex || id > MaxIndex)
                return IntPtr.Zero;

            int actualIndex = id - MinIndex;

            int v1 = Memory.Read<int>(m_dbInfo.Unk1 + (4 * (actualIndex >> 5)));

            int v2 = actualIndex & 0x1F;

            if (((1 << v2) & v1) != 0)
            {
                int bitsSet = CountBitsSet(v1 << (31 - v2));
                int entry = bitsSet + GetArrayEntryBySizeType(m_dbInfo.Unk3, actualIndex >> 5) - 1;

                if (m_dbInfo.Unk2 == 0)
                {
                    entry = GetArrayEntryBySizeType(m_dbInfo.Rows, entry);
                }
                return m_dbInfo.FirstRow + m_fileHdr.RecordSize * entry;
            }

            return IntPtr.Zero;
        }

        private int CountBitsSet(int a1)
        {
            return 0x1010101 *
                           ((((a1 - ((a1 >> 1) & 0x55555555)) & 0x33333333) +
                             (((a1 - ((a1 >> 1) & 0x55555555)) >> 2) & 0x33333333) +
                             ((((a1 - ((a1 >> 1) & 0x55555555)) & 0x33333333) +
                               (((a1 - ((a1 >> 1) & 0x55555555)) >> 2) & 0x33333333)) >> 4)) & 0x0F0F0F0F) >> 24;
        }

        private int GetArrayEntryBySizeType(IntPtr arrayPtr, int index)
        {
            if (m_dbInfo.RowEntrySize == 2)
                return Memory.Read<short>(arrayPtr + (2 * index));
            else
                return Memory.Read<int>(arrayPtr + (4 * index));
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (m_cacheEnabled)
            {
                var e = m_cache.GetEnumerator();

                while (e.MoveNext())
                    yield return e.Current.Value;
            }
            else
            {
                for (int i = 0; i < m_dbInfo.NumRows; ++i)
                    yield return Memory.Read<T>(m_dbInfo.FirstRow + i * m_fileHdr.RecordSize);
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool ContainsKey(int key)
        {
            return m_cacheEnabled ? m_cache.ContainsKey(key) : GetRowPtr(key) != IntPtr.Zero;
        }

        public IEnumerable<int> Keys
        {
            get
            {
                if (m_cacheEnabled)
                {
                    var e = m_cache.GetEnumerator();

                    while (e.MoveNext())
                        yield return e.Current.Key;
                }
                else
                {
                    for (int i = MinIndex; i <= MaxIndex; ++i)
                    {
                        if (ContainsKey(i))
                            yield return i;
                    }
                }
            }
        }

        public IEnumerable<T> Values
        {
            get
            {
                if (m_cacheEnabled)
                {
                    var e = m_cache.GetEnumerator();

                    while (e.MoveNext())
                        yield return e.Current.Value;
                }
                else
                {
                    for (int i = MinIndex; i <= MaxIndex; ++i)
                    {
                        if (ContainsKey(i))
                            yield return this[i];
                    }
                }
            }
        }

        public bool TryGetValue(int key, out T value)
        {
            if (m_cacheEnabled)
            {
                return m_cache.TryGetValue(key, out value);
            }
            else
            {
                if (ContainsKey(key))
                {
                    value = this[key];
                    return true;
                }
                else
                {
                    value = default(T);
                    return false;
                }
            }
        }

        public T this[int key]
        {
            get
            {
                if (m_cacheEnabled)
                {
                    return m_cache[key];
                }
                else
                {
                    IntPtr rowPtr = GetRowPtr(key);

                    if (rowPtr != IntPtr.Zero)
                    {
                        // can't find anything better than this
                        var row = Memory.Read<T>(rowPtr);
                        Type t = typeof(T);
                        var fields = t.GetFields();

                        foreach (var field in fields)
                        {
                            if (field.FieldType == typeof(IntPtr))
                            {
                                var oldValue = (IntPtr)field.GetValue(row);
                                if (oldValue != IntPtr.Zero)
                                {
                                    var offset = Marshal.OffsetOf(t, field.Name);
                                    field.SetValueDirect(__makeref(row), rowPtr + (int)offset + (int)oldValue);
                                }
                            }
                        }
                        return row;
                    }

                    throw new KeyNotFoundException();
                }
            }
        }

        public int Count
        {
            get { return m_dbInfo.NumRows; }
        }

        IEnumerator<KeyValuePair<int, T>> IEnumerable<KeyValuePair<int, T>>.GetEnumerator()
        {
            if (m_cacheEnabled)
            {
                var e = m_cache.GetEnumerator();

                while (e.MoveNext())
                    yield return e.Current;
            }
            else
            {
                for (int i = MinIndex; i <= MaxIndex; ++i)
                {
                    if (ContainsKey(i))
                        yield return new KeyValuePair<int, T>(i, this[i]);
                }
            }
        }
    }
}
