using System;
using SoftFluent;

namespace WowMoPObjMgrTest
{
    struct WowGuid
    {
        private Int128 m_guid;

        public Int128 Value { get { return m_guid; } }
        public static readonly WowGuid Zero = new WowGuid(0);

        public WowGuid(Int128 guid)
        {
            m_guid = guid;
        }

        public override string ToString()
        {
            return String.Format("0x{0:X32}", m_guid);
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

        public byte Type
        {
            get { return (byte)((m_guid >> 58) & 0x1F); }
            set { m_guid |= (Int128)value << 58; }
        }

        public byte SubType
        {
            get { return (byte)((m_guid >> 120) & 0x1F); }
            set { m_guid |= (Int128)value << 120; }
        }

        public ushort RealmId
        {
            get { return (ushort)((m_guid >> 42) & 0x1FFF); }
            set { m_guid |= (Int128)value << 42; }
        }

        public ushort ServerId
        {
            get { return (ushort)((m_guid >> 104) & 0x1FFF); }
            set { m_guid |= (Int128)value << 104; }
        }

        public ushort MapId
        {
            get { return (ushort)((m_guid >> 29) & 0x1FFF); }
            set { m_guid |= (Int128)value << 29; }
        }

        public uint Id
        {
            get { return (uint)(m_guid & 0xFFFFFF) >> 6; }
            set { m_guid |= (Int128)value << 6; }
        }

        public ulong CreationBits
        {
            get { return (ulong)((m_guid >> 64) & 0xFFFFFFFFFF); }
            set { m_guid |= (Int128)value << 64; }
        }
    }
}
