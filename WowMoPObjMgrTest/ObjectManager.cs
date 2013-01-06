using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace WowMoPObjMgrTest
{
    [StructLayout(LayoutKind.Sequential)]
    struct TSList // TSList<CGObject_C,TSGetExplicitLink<CGObject_C>>
    {
        public int Next;
        public IntPtr unk1;
        public IntPtr First; // usually equal to &unk1 | 1
    }

    [StructLayout(LayoutKind.Sequential)]
    struct TSHashTable // TSHashTable<CGObject_C,CHashKeyGUID>
    {
        public IntPtr vtable;
        public TSList Link;
        public int unk1;
        public int unk2;
        public int count; // count of links?
        public IntPtr unk4; // some pointer
        public int unk5;
        public int unk6;
        public int unk7;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct CurMgr
    {
        public TSHashTable VisibleObjects;
        public TSHashTable ToBeFreedObjects;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public TSList[] Links; // Links[9] has all objects stored in VisibleObjects it seems
        public ulong ActivePlayer;
        public int PlayerType;
        public int MapId;
        public IntPtr ClientConnection;
        public IntPtr MovementGlobals;
    }

    class ObjectManager : IEnumerable<WowObject>
    {
        private IntPtr BaseAddress;

        //private const int FirstObjectOfs = 0xC;
        //private const int NextObjectOfs = 0x4;
        //private const int FirstObjectOfs = 0xCC;
        //private const int NextObjectOfs = 0xC4;
        //private const int LocalGuidOfs = 0xD0;
        //private const int ListIndex = 9;

        public ObjectManager()
        {
            BaseAddress = Memory.Read<IntPtr>(Memory.BaseAddress + (IntPtr.Size == 4 ? Offsets.s_curMgr_x86 : Offsets.s_curMgr_x64));
        }

        public ulong ActivePlayer
        {
            //get { return Memory.Read<ulong>(BaseAddress + LocalGuidOfs); }
            get { return Memory.Read<CurMgr>(BaseAddress).ActivePlayer; }
        }

        public WowUnit ActivePlayerObj
        {
            //get { return Memory.Read<ulong>(BaseAddress + LocalGuidOfs); }
            get { return (WowUnit)GetObjectByGUID(ActivePlayer); }
        }

        public int MapId
        {
            get { return Memory.Read<CurMgr>(BaseAddress).MapId; }
        }

        public IntPtr ClientConnection
        {
            get { return Memory.Read<CurMgr>(BaseAddress).ClientConnection; }
        }

        public IntPtr MovementGlobals
        {
            get { return Memory.Read<CurMgr>(BaseAddress).MovementGlobals; }
        }

        public WowObject this[ulong guid]
        {
            get { return GetObjectByGUID(guid); }
        }

        private WowObject GetObjectByGUID(ulong guid)
        {
            foreach (WowObject obj in this)
                if (obj.Guid == guid)
                    return obj;

            return null;
        }

        public IntPtr FirstObject()
        {
            CurMgr mgr = Memory.Read<CurMgr>(BaseAddress);
            //return mgr.Links[ListIndex].First;
            return mgr.VisibleObjects.Link.First;
            //return Memory.Read<IntPtr>(BaseAddress + FirstObjectOfs);
        }

        public IntPtr NextObject(IntPtr current)
        {
            CurMgr mgr = Memory.Read<CurMgr>(BaseAddress);
            //return Memory.Read<IntPtr>(current + mgr.Links[ListIndex].Next + 4);
            return Memory.Read<IntPtr>(current + mgr.VisibleObjects.Link.Next + IntPtr.Size);
            //return Memory.Read<IntPtr>(current + Memory.Read<int>(BaseAddress + NextObjectOfs) + 4);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<WowObject> GetEnumerator()
        {
            IntPtr first = FirstObject();

            while (((first.ToInt64() & 1) == 0) && first != IntPtr.Zero)
            {
                WowObject obj = new WowObject(first);

                switch(obj.Type)
                {
                    case WowObjectType.Item:
                        yield return new WowItem(first);
                        break;
                    case WowObjectType.Container:
                        yield return new WowContainer(first);
                        break;
                    case WowObjectType.Unit:
                        yield return new WowUnit(first);
                        break;
                    case WowObjectType.Player:
                        yield return new WowPlayer(first);
                        break;
                    default:
                        yield return obj;
                        break;
                }
                //yield return new WowObject(first);

                first = NextObject(first);
            }
        }
    }
}
