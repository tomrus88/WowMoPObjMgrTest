using System.Runtime.InteropServices;

namespace WowMoPObjMgrTest
{
    [StructLayout(LayoutKind.Sequential)]
    struct DBCFile
    {
        public uint Magic;
        public int RecordsCount;
        public int FieldsCount;
        public int RecordSize;
        public int StringTableSize;
    }
}
