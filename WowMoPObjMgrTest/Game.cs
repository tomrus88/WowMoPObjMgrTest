using System;

namespace WowMoPObjMgrTest
{
    class Game
    {
        public static readonly ObjectManager ObjMgr = new ObjectManager();

        public static readonly DBC<AreaTableRec> AreaTableDB = new DBC<AreaTableRec>(Memory.BaseAddress +
            (IntPtr.Size == 4 ? 0xC8CFDC : 0), false);

        public static readonly DBC<FactionTemplateRec> FactionTemplateDB = new DBC<FactionTemplateRec>(Memory.BaseAddress +
            (IntPtr.Size == 4 ? Offsets.g_FactionTemplateDB_x86 : Offsets.g_FactionTemplateDB_x64), false);

        public static readonly DBC<SpellMiscRec> SpellMiscDB = new DBC<SpellMiscRec>(Memory.BaseAddress +
            (IntPtr.Size == 4 ? Offsets.g_SpellMiscDB_x86 : Offsets.g_SpellMiscDB_x64), false);
    }
}
