using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace WowMoPObjMgrTest
{
    class DB2Row<T> where T : struct
    {
        public readonly T Data;
        private IntPtr address;
        private Type type = typeof(T);

        public DB2Row(IntPtr address)
        {
            this.address = address;
            this.Data = Memory.Read<T>(address);
        }

        public string GetString(string fieldName)
        {
            FieldInfo f = type.GetField(fieldName);
            int ofs = Marshal.OffsetOf(type, fieldName).ToInt32();
            int val = (int)f.GetValue(Data);
            return Memory.ReadString(address + ofs + val, 255);
        }
    }
}
