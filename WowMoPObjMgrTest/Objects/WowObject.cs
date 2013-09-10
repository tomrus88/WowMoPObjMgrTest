using System;
using System.Runtime.InteropServices;

namespace WowMoPObjMgrTest
{
    [StructLayout(LayoutKind.Sequential)]
    struct Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", X, Y, Z);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    struct WowObjStruct
    {                               // x86  x64
        IntPtr vtable;              // 0x00 0x00
        public IntPtr Descriptors;  // 0x08 0x10
        //int unk0;                   // 0x04 0x08
        IntPtr unk1;                // 0x0C 0x18
        public int ObjectType;      // 0x10 0x20
        int unk3;                   // 0x14 0x24
        IntPtr unk4;                // 0x18 0x28
        IntPtr unk5;                // 0x1C 0x30
        IntPtr unk6;                // 0x20 0x38
        IntPtr unk7;                // 0x24 0x40
        IntPtr unk8;                // 0x28 0x48
        public ulong Guid;          // 0x30 0x50
    }

    class WowObject
    {
        private IntPtr BaseAddress;
        private WowObjStruct ObjectData;

        public WowObject(IntPtr address)
        {
            BaseAddress = address;

            ObjectData = Memory.Read<WowObjStruct>(BaseAddress);
        }

        public IntPtr Pointer
        {
            get { return BaseAddress; }
        }

        public WowObjectType Type
        {
            get { return (WowObjectType)ObjectData.ObjectType; }
        }

        public WowGuid Guid
        {
            get { return new WowGuid(ObjectData.Guid); }
        }

        public WowGuid VisibleGuid
        {
            get { return GetValue<WowGuid>(CGObjectData.Guid); }
        }

        public int Entry
        {
            get { return GetValue<int>(CGObjectData.EntryID); }
        }

        public float Scale
        {
            get { return GetValue<float>(CGObjectData.Scale); }
        }

        public uint DynamicFlags
        {
            get { return GetValue<uint>(CGObjectData.DynamicFlags); }
            set { SetValue<uint>(CGObjectData.DynamicFlags, value); }
        }

        public ulong Data
        {
            get { return GetValue<ulong>(CGObjectData.Data); }
        }

        public T GetValue<T>(Enum index) where T : struct
        {
            return Memory.Read<T>(ObjectData.Descriptors + Convert.ToInt32(index) * 4);
        }

        public void SetValue<T>(Enum index, T val) where T : struct
        {
            Memory.Write<T>(ObjectData.Descriptors + Convert.ToInt32(index) * 4, val);
        }

        public bool IsA(ObjectTypeFlags flags)
        {
            return (GetValue<int>(CGObjectData.Type) & (int)flags) != 0;
        }

        public override string ToString()
        {
            return String.Format("0x{0:X16}", Pointer.ToInt64());
        }
    }
}
