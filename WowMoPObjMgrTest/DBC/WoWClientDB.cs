using System;
using System.Runtime.InteropServices;

namespace WowMoPObjMgrTest
{
    #pragma warning disable 649
    [StructLayout(LayoutKind.Sequential)]
    struct WoWClientDB
    {
        public IntPtr VTable;         // pointer to vtable
        public int NumRows;         // number of rows
        public int MaxIndex;        // maximal row index
        public int MinIndex;        // minimal row index
        public IntPtr Data;           // pointer to actual dbc file data
        public IntPtr FirstRow;       // pointer to first row
        public IntPtr Rows;           // pointer to rows array - not anymore?
        public IntPtr Unk1; // ptr
        public uint Unk2; // 1
        public IntPtr Unk3; // ptr
        public uint RowEntrySize; // 2 or 4
    };
    #pragma warning restore 649
}
