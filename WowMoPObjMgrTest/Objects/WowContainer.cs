using System;

namespace WowMoPObjMgrTest
{
    class WowContainer : WowItem
    {
        public WowContainer(IntPtr address)
            : base(address)
        {

        }

        public int NumSlots
        {
            get { return GetValue<int>(CGContainerData.NumSlots); }
        }
    }
}
