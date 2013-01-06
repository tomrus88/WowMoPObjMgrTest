using System;

namespace WowMoPObjMgrTest
{
    class WowUnit : WowObject
    {
        public WowUnit(IntPtr address)
            : base(address)
        {

        }

        public Reaction UnitReaction
        {
            get
            {
                WowUnit me = Game.ObjMgr.ActivePlayerObj;

                if (me == null)
                    return Reaction.Neutral;

                int ftid_src = me.GetValue<int>(CGUnitData.FactionTemplate);
                int ftid_dst = GetValue<int>(CGUnitData.FactionTemplate);

                FactionTemplateRec faction_src, faction_dst;

                try
                {
                    faction_src = Game.g_FactionTemplateDB[ftid_src];
                    faction_dst = Game.g_FactionTemplateDB[ftid_dst];
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
        }

        public WowUnit Target
        {
            get
            {
                return (WowUnit)Game.ObjMgr[GetValue<ulong>(CGUnitData.Target)];
            }
        }

        public bool IsPet
        {
            get { return GetValue<ulong>(CGUnitData.SummonedBy) != 0; }
        }
    }
}
