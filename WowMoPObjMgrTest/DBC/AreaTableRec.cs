using System;
using System.Runtime.InteropServices;

namespace WowMoPObjMgrTest
{
    #pragma warning disable 649
    [StructLayout(LayoutKind.Sequential)]
    struct AreaTableRec
    {
        public int ID;
        public int ContinentID;
        public int ParentAreaID;
        public int AreaBit;
        public int flags_0;
        public int flags_1;
        public int SoundProviderPref;
        public int SoundProviderPrefUnderwater;
        public int AmbienceID;
        public int ZoneMusic;
        public IntPtr _ZoneName;
        public int IntroSound;
        public int ExplorationLevel;
        public IntPtr _AreaName_lang;
        public int factionGroupMask;
        public int liquidTypeID_0;
        public int liquidTypeID_1;
        public int liquidTypeID_2;
        public int liquidTypeID_3;
        public float ambient_multiplier;
        public int mountFlags;
        public int uwIntroSound;
        public int uwZoneMusic;
        public int uwAmbience;
        public int world_pvp_id;
        public int pvpCombatWorldStateID;
        public int wildBattlePetLevelMin;
        public int wildBattlePetLevelMax;
        public int windSettingsID;

        public string ZoneName
        {
            get { return Memory.ReadString(_ZoneName, 255); }
        }
    };
    #pragma warning restore 649
}
