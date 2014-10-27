using System;

namespace WowMoPObjMgrTest
{
    struct GameobjectStats
    {
        public int TypeId;
        public int DisplayId;
    }

    class WowGameObject : WowObject
    {
        public WowGameObject(IntPtr address)
            : base(address)
        {

        }

        public Vector3 Position
        {
            get { return Memory.Read<Vector3>(Pointer + (IntPtr.Size == 4 ? Offsets.GOPosition_x86 : Offsets.GOPosition_x64)); }
        }

        public float DistanceTo(WowUnit unit)
        {
            if (unit == null)
                return 0.0f;

            var myPos = Position;
            var hisPos = unit.Position;

            var dx = myPos.X - hisPos.X;
            var dy = myPos.Y - hisPos.Y;
            var dz = myPos.Z - hisPos.Z;
            return (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        public float DistanceToMe
        {
            get { return DistanceTo(Game.ObjMgr.ActivePlayerObj); }
        }

        public WowGuid CreatedBy
        {
            get { return GetValue<WowGuid>(CGGameObjectData.CreatedBy); }
        }

        public int DisplayID
        {
            get { return GetValue<int>(CGGameObjectData.DisplayID); }
        }

        public uint Flags
        {
            get { return GetValue<uint>(CGGameObjectData.Flags); }
            set { SetValue<uint>(CGGameObjectData.Flags, value); }
        }

        public int FactionTemplate
        {
            get { return GetValue<int>(CGGameObjectData.FactionTemplate); }
        }

        public int Level
        {
            get { return GetValue<int>(CGGameObjectData.Level); }
        }

        public uint PercentHealth
        {
            get { return GetValue<uint>(CGGameObjectData.PercentHealth); }
            set { SetValue<uint>(CGGameObjectData.PercentHealth, value); }
        }

        public int StateSpellVisualID
        {
            get { return GetValue<int>(CGGameObjectData.StateSpellVisualID); }
            set { SetValue<int>(CGGameObjectData.StateSpellVisualID, value); }
        }

        public int StateAnimID
        {
            get { return GetValue<int>(CGGameObjectData.StateAnimID); }
            set { SetValue<int>(CGGameObjectData.StateAnimID, value); }
        }

        public int StateAnimKitID
        {
            get { return GetValue<int>(CGGameObjectData.StateAnimKitID); }
            set { SetValue<int>(CGGameObjectData.StateAnimKitID, value); }
        }

        public int StateWorldEffectID
        {
            get { return GetValue<int>(CGGameObjectData.StateWorldEffectID); }
            set { SetValue<int>(CGGameObjectData.StateWorldEffectID, value); }
        }

        public IntPtr Stats
        {
            get { return Memory.Read<IntPtr>(Pointer + (IntPtr.Size == 4 ? Offsets.GOStats_x86 : Offsets.GOStats_x64)); }
        }

        public GameObjectTypeId TypeId
        {
            get { return (GameObjectTypeId)Memory.Read<GameobjectStats>(Stats).TypeId; }
        }
    }
}
