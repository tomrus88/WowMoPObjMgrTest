using System.Runtime.InteropServices;

namespace WowMoPObjMgrTest
{
    [StructLayout(LayoutKind.Sequential)]
    struct AreaPOIRec
    {
        public readonly int Id;
        public readonly int Flags;
        public readonly int Importance;
        public readonly int FactionId;
        public readonly int ContinentId;
        public readonly int AreaId;
        public readonly int Icon;
        public readonly float PosX;
        public readonly float PosY;
        public readonly int Name;
        public readonly int Description;
        public readonly int WorldStateId;
        public readonly int PlayerConditionId;
        public readonly int WorldMapLink;
        public readonly int PortLocId;
    }
}
