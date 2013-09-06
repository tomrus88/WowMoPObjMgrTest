using System;

namespace WowMoPObjMgrTest
{
    [Flags]
    enum PlayerFlags
    {
        Commentator = 0x80000,
        CommentatorUber = 0x400000,
        CommentatorMask = Commentator | CommentatorUber
    }

    class WowPlayer : WowUnit
    {
        public WowPlayer(IntPtr address)
            : base(address)
        {

        }

        public int RealmId
        {
            get { return GetValue<int>(CGPlayerData.VirtualPlayerRealm); }
        }

        public PlayerFlags Flags
        {
            get { return (PlayerFlags)GetValue<int>(CGPlayerData.PlayerFlags); }
            set { SetValue<int>(CGPlayerData.PlayerFlags, (int)value); }
        }

        public TrackCreatureFlags TrackCreatureMask
        {
            get { return (TrackCreatureFlags)GetValue<int>(CGPlayerData.TrackCreatureMask); }
            set { SetValue<int>(CGPlayerData.TrackCreatureMask, (int)value); }
        }

        public TrackObjectFlags TrackResourceMask
        {
            get { return (TrackObjectFlags)GetValue<int>(CGPlayerData.TrackResourceMask); }
            set { SetValue<int>(CGPlayerData.TrackResourceMask, (int)value); }
        }
    }
}
