using System;
using System.Runtime.InteropServices;

namespace WowMoPObjMgrTest
{
    [StructLayout(LayoutKind.Sequential)]
    struct WoWClientDB2
    {
        public readonly IntPtr vtable; // 0x00
        public readonly int NumRows; // 0x04
        public readonly int Unk1; // 0x08
        public readonly int MaxIndex; // 0x0C
        public readonly int MinIndex; // 0x10
        public readonly int Unk2; // 0x14
        public readonly int Unk3; // 0x18
        public readonly int Unk4; // 0x1C
        public readonly IntPtr FirstRow; // 0x20
        public readonly int Unk6; // 0x24
        public readonly IntPtr Rows; // 0x28
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 49)]
        //public readonly int[] Fields;
    }
}
