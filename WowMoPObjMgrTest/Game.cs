using System;

namespace WowMoPObjMgrTest
{
    class Game
    {
        private static ObjectManager objmgr;

        public static ObjectManager ObjMgr
        {
            get
            {
                if (objmgr == null)
                {
                    try
                    {
                        objmgr = new ObjectManager();
                    }
                    catch
                    {

                    }
                }

                return objmgr;
            }
        }

        public static readonly DBC<AreaTableRec> AreaTableDB = new DBC<AreaTableRec>(Memory.BaseAddress +
            (IntPtr.Size == 4 ? 0xC8E11C : 0));

        public static readonly DBC<FactionTemplateRec> FactionTemplateDB = new DBC<FactionTemplateRec>(Memory.BaseAddress +
            (IntPtr.Size == 4 ? Offsets.g_FactionTemplateDB_x86 : Offsets.g_FactionTemplateDB_x64));

        public static readonly DBC<SpellMiscRec> SpellMiscDB = new DBC<SpellMiscRec>(Memory.BaseAddress +
            (IntPtr.Size == 4 ? Offsets.g_SpellMiscDB_x86 : Offsets.g_SpellMiscDB_x64));
    }
}
