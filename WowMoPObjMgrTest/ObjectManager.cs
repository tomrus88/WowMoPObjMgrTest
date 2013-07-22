using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace WowMoPObjMgrTest
{
    [StructLayout(LayoutKind.Sequential)]
    struct TSExplicitList
    {
        public TSList baseClass;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct TSList
    {
        public int m_linkoffset;
        public TSLink m_terminator;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct TSLink
    {
        public IntPtr m_prevlink; //TSLink *m_prevlink
        public IntPtr m_next; // C_OBJECTHASH *m_next
    }

    [StructLayout(LayoutKind.Sequential)]
    struct TSHashTable
    {
        public IntPtr vtable;
        public TSExplicitList m_fulllist;
        public int m_fullnessIndicator;
        public TSGrowableArray m_slotlistarray;
        public int m_slotmask;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct TSBaseArray
    {
        public IntPtr vtable;
        public uint m_alloc;
        public uint m_count;
        public IntPtr m_data;//TSExplicitList* m_data;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct TSFixedArray
    {
        public TSBaseArray baseClass;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct TSGrowableArray
    {
        public TSFixedArray baseclass;
        public uint m_chunk;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct CurMgr
    {
        public TSHashTable VisibleObjects; // m_objects
        public TSHashTable ToBeFreedObjects; // m_lazyCleanupObjects
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        // m_lazyCleanupFifo, m_freeObjects, m_visibleObjects, m_reenabledObjects, ...
        public TSExplicitList[] Links; // Links[9] has all objects stored in VisibleObjects it seems
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

        public WowGuid ActivePlayer
        {
            //get { return Memory.Read<ulong>(BaseAddress + LocalGuidOfs); }
            get { return (WowGuid)Memory.Read<CurMgr>(BaseAddress).ActivePlayer; }
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

        public WowObject this[WowGuid guid]
        {
            get { return GetObjectByGUID(guid); }
        }

        private WowObject GetObjectByGUID(WowGuid guid)
        {
            foreach (WowObject obj in this)
                if (obj.Guid.Value == guid.Value)
                    return obj;

            return null;
        }

        public IntPtr FirstObject()
        {
            CurMgr mgr = Memory.Read<CurMgr>(BaseAddress);
            //return mgr.Links[ListIndex].First;
            return mgr.VisibleObjects.m_fulllist.baseClass.m_terminator.m_next;
            //return Memory.Read<IntPtr>(BaseAddress + FirstObjectOfs);
        }

        public IntPtr NextObject(IntPtr current)
        {
            CurMgr mgr = Memory.Read<CurMgr>(BaseAddress);
            //return Memory.Read<IntPtr>(current + mgr.Links[ListIndex].Next + 4);
            return Memory.Read<IntPtr>(current + mgr.VisibleObjects.m_fulllist.baseClass.m_linkoffset + IntPtr.Size);
            //return Memory.Read<IntPtr>(current + Memory.Read<int>(BaseAddress + NextObjectOfs) + 4);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<WowObject> GetEnumerator()
        {
            IntPtr first = FirstObject();

            // Must keep struct up to date in order for this to work
            IntPtr typeOffset = Marshal.OffsetOf(typeof(WowObjStruct), "ObjectType");

            while (((first.ToInt64() & 1) == 0) && first != IntPtr.Zero)
            {
                WowObjectType type = Memory.Read<WowObjectType>(first + typeOffset.ToInt32());

                //WowObject obj = new WowObject(first);

                //switch(obj.Type)
                switch (type)
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
                        //yield return obj;
                        yield return new WowObject(first);
                        break;
                }
                //yield return new WowObject(first);

                first = NextObject(first);
            }
        }
    }
}
