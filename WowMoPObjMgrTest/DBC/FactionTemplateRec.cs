using System;
using System.Runtime.InteropServices;

namespace WowMoPObjMgrTest
{
    #pragma warning disable 649
    [StructLayout(LayoutKind.Sequential)]
    struct FactionTemplateRec // sizeof(0x38)
    {
        public readonly uint m_ID;           // 0
        public readonly uint m_faction;      // 1
        public readonly uint m_flags;        // 2
        public readonly uint m_factionGroup; // 3
        public readonly uint m_friendGroup;  // 4
        public readonly uint m_enemyGroup;   // 5
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public readonly uint[] m_enemies;    // 6-9
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public readonly uint[] m_friend;     // 10-13
    };
    #pragma warning restore 649

    [Flags]
    enum FactionMask
    {
        Neutral     = 0,                                    // non-aggressive creature
        Player      = 1,                                    // any player
        Alliance    = 2,                                    // player or creature from alliance team
        Horde       = 4,                                    // player or creature from horde team
        Monster     = 8                                     // aggressive creature from monster team
    };
}
