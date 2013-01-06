using System;

namespace WowMoPObjMgrTest
{
    class WowItem : WowObject
    {
        public WowItem(IntPtr address)
            : base(address)
        {

        }

        public int StackCount
        {
            get { return GetValue<int>(CGItemData.StackCount); }
        }

        public int Durability
        {
            get { return GetValue<int>(CGItemData.Durability); }
        }

        public int MaxDurability
        {
            get { return GetValue<int>(CGItemData.MaxDurability); }
        }
    }
}
