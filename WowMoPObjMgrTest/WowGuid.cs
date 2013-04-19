using System;

namespace WowMoPObjMgrTest
{
    class WowGuid
    {
        private ulong m_guid;

        public ulong Value { get { return m_guid; } }

        public WowGuid(ulong guid)
        {
            m_guid = guid;
        }

        public override string ToString()
        {
            return String.Format("0x{0:X16}", m_guid);
        }

        public static implicit operator ulong(WowGuid guid)
        {
            if (guid == null)
                return 0;

            return guid.Value;
        }

        public static explicit operator WowGuid(ulong guid)
        {
            return new WowGuid(guid);
        }

        public static bool operator==(WowGuid guid, ulong guid2)
        {
            return guid.Value == guid2;
        }

        public static bool operator !=(WowGuid guid, ulong guid2)
        {
            return guid.Value != guid2;
        }

        public override bool Equals(object obj)
        {
            var temp = obj as WowGuid;
            if (temp == null)
                return false;

            return m_guid == temp.Value;
        }

        public override int GetHashCode()
        {
            return (int)m_guid;
        }
    }
}
