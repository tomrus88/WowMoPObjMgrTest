using System;

namespace WowMoPObjMgrTest
{
    class WowPlayer : WowUnit
    {
        public WowPlayer(IntPtr address)
            : base(address)
        {

        }

        public int RealmId
        {
            get { return GetValue<int>(CGPlayerData.HomePlayerRealm); }
        }
    }
}
