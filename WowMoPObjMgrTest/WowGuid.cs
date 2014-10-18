using System;
using SoftFluent;

namespace WowMoPObjMgrTest
{
    public enum GuidType : byte
    {
        Null = 0,
        Uniq = 1,
        Player = 2,
        Item = 3,
        StaticDoor = 4,
        Transport = 5,
        Conversation = 6,
        Creature = 7,
        Vehicle = 8,
        Pet = 9,
        GameObject = 10,
        DynamicObject = 11,
        AreaTrigger = 12,
        Corpse = 13,
        LootObject = 14,
        SceneObject = 15,
        Scenario = 16,
        AIGroup = 17,
        DynamicDoor = 18,
        ClientActor = 19,
        Vignette = 20,
        CallForHelp = 21,
        AIResource = 22,
        AILock = 23,
        AILockTicket = 24,
        ChatChannel = 25,
        Party = 26,
        Guild = 27,
        WowAccount = 28,
        BNetAccount = 29,
        GMTask = 30,
        MobileSession = 31,
        RaidGroup = 32,
        Spell = 33,
        Mail = 34,
        WebObj = 35,
        LFGObject = 36,
        LFGList = 37,
        UserRouter = 38,
        PVPQueueGroup = 39,
        UserClient = 40,
        PetBattle = 41,
        UniqueUserClient = 42,
        BattlePet = 43
    }

    struct WowGuid
    {
        private Int128 m_guid;

        public static readonly WowGuid Zero = new WowGuid(0);

        public WowGuid(Int128 guid)
        {
            m_guid = guid;
        }

        public override string ToString()
        {
            //Creature/Vehicle/Pet
            //<type>:<subtype>:<realmID>:<mapID>:<serverID>:<dbID>:<creationbits>
            //Player
            //<type>:<realmID>:<dbID>
            //Item
            //<type>:<realmID>:<???>:<dbID>
            switch (Type)
            {
                case GuidType.Creature:
                case GuidType.Vehicle:
                case GuidType.Pet:
                case GuidType.GameObject:
                case GuidType.AreaTrigger:
                case GuidType.DynamicObject:
                case GuidType.Corpse:
                case GuidType.LootObject:
                case GuidType.SceneObject:
                case GuidType.Scenario:
                case GuidType.AIGroup:
                case GuidType.DynamicDoor:
                case GuidType.Vignette:
                case GuidType.Conversation:
                case GuidType.CallForHelp:
                case GuidType.AIResource:
                case GuidType.AILock:
                case GuidType.AILockTicket:
                    return String.Format("{0}-{1}-{2}-{3}-{4}-{5}-{6:X10}", Type, SubType, RealmId, MapId, ServerId, Id, CreationBits);
                case GuidType.Player:
                    return String.Format("{0}-{1}-{2:X8}", Type, RealmId, (ulong)(m_guid >> 64));
                case GuidType.Item:
                    return String.Format("{0}-{1}-{2}-{3:X10}", Type, RealmId, (uint)((m_guid >> 18) & 0xFFFFFF), (ulong)(m_guid >> 64));
                //case GuidType.ClientActor:
                //    return String.Format("{0}-{1}-{2}", Type, RealmId, CreationBits);
                //case GuidType.Transport:
                //case GuidType.StaticDoor:
                //    return String.Format("{0}-{1}-{2}", Type, RealmId, CreationBits);
                default:
                    return String.Format("{0}-{1:X32}", Type, m_guid);
            }
        }

        public static bool operator ==(WowGuid left, WowGuid right)
        {
            return left.m_guid == right.m_guid;
        }

        public static bool operator !=(WowGuid left, WowGuid right)
        {
            return left.m_guid != right.m_guid;
        }

        public override bool Equals(object obj)
        {
            if (obj is WowGuid)
                return m_guid == ((WowGuid)obj).m_guid;
            return false;
        }

        public override int GetHashCode()
        {
            return m_guid.GetHashCode();
        }

        public GuidType Type
        {
            get { return (GuidType)(byte)((m_guid >> 58) & 0x3F); }
            //set
            //{
            //    m_guid &= ~(Int128)0x3F << 58;
            //    m_guid |= (Int128)(byte)value << 58;
            //}
        }

        public byte SubType
        {
            get { return (byte)((m_guid >> 120) & 0x3F); }
            //set { m_guid |= (Int128)value << 120; }
        }

        public ushort RealmId
        {
            get { return (ushort)((m_guid >> 42) & 0x1FFF); }
            //set { m_guid |= (Int128)value << 42; }
        }

        public ushort ServerId
        {
            get { return (ushort)((m_guid >> 104) & 0x1FFF); }
            //set { m_guid |= (Int128)value << 104; }
        }

        public ushort MapId
        {
            get { return (ushort)((m_guid >> 29) & 0x1FFF); }
            //set { m_guid |= (Int128)value << 29; }
        }

        // Creature, Pet, Vehicle
        public uint Id
        {
            get { return (uint)((m_guid >> 6) & 0x7FFFFF); }
            //set { m_guid |= (Int128)value << 6; }
        }

        public ulong CreationBits
        {
            get { return (ulong)((m_guid >> 64) & 0xFFFFFFFFFF); }
            //set { m_guid |= (Int128)value << 64; }
        }
    }
}
