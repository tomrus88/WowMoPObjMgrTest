using System;
using System.Runtime.InteropServices;

namespace WowMoPObjMgrTest
{
    #pragma warning disable 649
    [StructLayout(LayoutKind.Sequential)]
    struct AreaTableRec
    {
        public readonly int ID;
        public readonly int ContinentID;
        public readonly int ParentAreaID;
        public readonly int AreaBit;
        public readonly int flags_0;
        public readonly int flags_1;
        public readonly int SoundProviderPref;
        public readonly int SoundProviderPrefUnderwater;
        public readonly int AmbienceID;
        public readonly int ZoneMusic;
        public readonly IntPtr _ZoneName;
        public readonly int IntroSound;
        public readonly int ExplorationLevel;
        public readonly IntPtr _AreaName_lang;
        public readonly int factionGroupMask;
        public readonly int liquidTypeID_0;
        public readonly int liquidTypeID_1;
        public readonly int liquidTypeID_2;
        public readonly int liquidTypeID_3;
        public readonly float ambient_multiplier;
        public readonly int mountFlags;
        public readonly int uwIntroSound;
        public readonly int uwZoneMusic;
        public readonly int uwAmbience;
        public readonly int world_pvp_id;
        public readonly int pvpCombatWorldStateID;
        public readonly int wildBattlePetLevelMin;
        public readonly int wildBattlePetLevelMax;
        public readonly int windSettingsID;

        public string ZoneName
        {
            get { return Memory.ReadString(_ZoneName, 255); }
        }

        public string AreaName
        {
            get { return Memory.ReadString(_AreaName_lang, 255); }
        }
    };
    #pragma warning restore 649
}
