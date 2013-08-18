using System;

namespace WowMoPObjMgrTest
{
    struct WowGuid
    {
        private ulong m_guid;

        public ulong Value { get { return m_guid; } }
        public static readonly WowGuid Zero = new WowGuid(0);

        public WowGuid(ulong guid)
        {
            m_guid = guid;
        }

        public override string ToString()
        {
            return String.Format("0x{0:X16}", m_guid);
        }

        public static bool operator ==(WowGuid guid, WowGuid guid2)
        {
            return guid.Value == guid2.Value;
        }

        public static bool operator !=(WowGuid guid, WowGuid guid2)
        {
            return guid.Value != guid2.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is WowGuid)
                return m_guid == ((WowGuid)obj).Value;
            return false;
        }

        public override int GetHashCode()
        {
            return m_guid.GetHashCode();
        }
    }
}
