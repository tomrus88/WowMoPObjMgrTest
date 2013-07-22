using System;

namespace WowMoPObjMgrTest
{
    class WowUnit : WowObject
    {
        public WowUnit(IntPtr address)
            : base(address)
        {

        }

        public Reaction UnitReaction(WowUnit other)
        {
            if (other == null)
                return Reaction.Neutral;

            int ftid_src = other.GetValue<int>(CGUnitData.FactionTemplate);
            int ftid_dst = GetValue<int>(CGUnitData.FactionTemplate);

            FactionTemplateRec faction_src, faction_dst;

            try
            {
                faction_src = Game.FactionTemplateDB[ftid_src];
                faction_dst = Game.FactionTemplateDB[ftid_dst];
            }
            catch
            {
                return Reaction.Neutral;
            }

            if ((faction_dst.m_factionGroup & faction_src.m_enemyGroup) != 0)
                return Reaction.Hostile;

            for (int i = 0; i < faction_src.m_enemies.Length; ++i)
            {
                if (faction_src.m_enemies[i] == 0)
                    break;
                if (faction_src.m_enemies[i] == faction_dst.m_faction)
                    return Reaction.Hostile;
            }

            if ((faction_dst.m_factionGroup & faction_src.m_friendGroup) != 0)
                return Reaction.Friendly;

            for (int i = 0; i < faction_src.m_friend.Length; ++i)
            {
                if (faction_src.m_friend[i] == 0)
                    break;
                if (faction_src.m_friend[i] == faction_dst.m_faction)
                    return Reaction.Friendly;
            }

            if ((faction_src.m_factionGroup & faction_dst.m_friendGroup) != 0)
                return Reaction.Friendly;

            for (int i = 0; i < faction_dst.m_friend.Length; ++i)
            {
                if (faction_dst.m_friend[i] == 0)
                    break;
                if (faction_dst.m_friend[i] == faction_src.m_faction)
                    return Reaction.Friendly;
            }

            return (Reaction)(~(faction_src.m_flags >> 12) & 2 | 1); // it seems checking for (factionFlags & 0x2000) != 0 ? 1 : 3
        }

        public WowUnit Target
        {
            get
            {
                return (WowUnit)Game.ObjMgr[(WowGuid)GetValue<ulong>(CGUnitData.Target)];
            }
        }

        public bool IsPet
        {
            get { return GetValue<ulong>(CGUnitData.SummonedBy) != 0; }
        }

        public int FactionTemplate
        {
            get { return GetValue<int>(CGUnitData.FactionTemplate); }
        }

        public Faction Faction
        {
            get
            {
                FactionTemplateRec ftemp;

                try
                {
                    ftemp = Game.FactionTemplateDB[FactionTemplate];

                    if ((ftemp.m_factionGroup & 1) != 0 && (ftemp.m_factionGroup & 6) == 0)
                        return Faction.Neutral;
                    else if ((ftemp.m_factionGroup & 2) != 0)
                        return Faction.Alliance;
                    else if ((ftemp.m_factionGroup & 4) != 0)
                        return Faction.Horde;
                    else if ((ftemp.m_factionGroup & 8) != 0)
                        return Faction.Monster;
                    else
                        return Faction.Invalid;
                }
                catch
                {
                    return Faction.Invalid;
                }
            }
        }

        public Vector3 Position
        {
            get { return Memory.Read<Vector3>(Pointer + 0x800 - 8); }
        }
    }
}
