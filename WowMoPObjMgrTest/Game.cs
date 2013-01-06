using System;

namespace WowMoPObjMgrTest
{
    class Game
    {
        public static readonly ObjectManager ObjMgr = new ObjectManager();

        public static readonly DBC<FactionTemplateRec> g_FactionTemplateDB = new DBC<FactionTemplateRec>(Memory.BaseAddress +
            (IntPtr.Size == 4 ? Offsets.g_FactionTemplateDB_x86 : Offsets.g_FactionTemplateDB_x64), false);
    }
}
