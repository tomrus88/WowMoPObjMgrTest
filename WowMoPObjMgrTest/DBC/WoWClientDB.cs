using System;
using System.Runtime.InteropServices;

namespace WowMoPObjMgrTest
{
    #pragma warning disable 649
    [StructLayout(LayoutKind.Sequential)]
    struct WoWClientDB
    {
        public readonly IntPtr VTable;         // pointer to vtable
        public readonly int NumRows;         // number of rows
        public readonly int MaxIndex;        // maximal row index
        public readonly int MinIndex;        // minimal row index
        public readonly IntPtr Unk0;
        public readonly IntPtr Data;           // pointer to actual dbc file data
        public readonly IntPtr FirstRow;       // pointer to first row
        public readonly IntPtr Rows;           // pointer to rows array - not anymore? rows offset?
        public readonly IntPtr Unk1; // ptr
        public readonly uint Unk2; // 1
        public readonly IntPtr Unk3; // ptr
        public readonly uint RowEntrySize; // 2 or 4
        public readonly int RowSize;
    };
    #pragma warning restore 649
}
