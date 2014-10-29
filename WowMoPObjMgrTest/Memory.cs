using System;
using Wlp;

namespace WowMoPObjMgrTest
{
    class Memory
    {
        static ProcessMemory Reader;

        static Memory()
        {
            if (IntPtr.Size == 4)
                Reader = new ProcessMemory("Wow");
            else
                Reader = new ProcessMemory("Wow-64");
        }

        public static T Read<T>(IntPtr address) where T : struct
        {
            return Reader.Read<T>(address);
        }

        public static void Write<T>(IntPtr address, T val) where T : struct
        {
            Reader.Write<T>(address, val);
        }

        public static string ReadString(IntPtr address, int maxLen)
        {
            return Reader.ReadCString(address, maxLen);
        }

        public static IntPtr BaseAddress
        {
            get { return Reader.BaseAddress; }
        }
    }
}
