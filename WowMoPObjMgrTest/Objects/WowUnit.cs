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

                int ftid1 = me.GetValue<int>(CGUnitData.FactionTemplate);
                int ftid2 = GetValue<int>(CGUnitData.FactionTemplate);

                FactionTemplateRec? factionn1 = Game.g_FactionTemplateDB[ftid1];
                FactionTemplateRec? factionn2 = Game.g_FactionTemplateDB[ftid2];

                if (!factionn1.HasValue || !factionn2.HasValue)
                    return Reaction.Neutral;

                FactionTemplateRec faction1 = factionn1.Value;
                FactionTemplateRec faction2 = factionn2.Value;

                if ((faction2.m_factionGroup & faction1.m_enemyGroup) != 0)
                    return Reaction.Hostile;

                for (int i = 0; i < faction1.m_enemies.Length; ++i)
                {
                    if (faction1.m_enemies[i] == 0)
                        break;
                    if (faction1.m_enemies[i] == faction2.m_faction)
                        return Reaction.Hostile;
                }

                if ((faction2.m_factionGroup & faction1.m_friendGroup) != 0)
                    return Reaction.Friendly;

                for (int i = 0; i < faction1.m_friend.Length; ++i)
                {
                    if (faction1.m_friend[i] == 0)
                        break;
                    if (faction1.m_friend[i] == faction2.m_faction)
                        return Reaction.Friendly;
                }

                if ((faction1.m_factionGroup & faction2.m_friendGroup) != 0)
                    return Reaction.Friendly;

                for (int i = 0; i < faction2.m_friend.Length; ++i)
                {
                    if (faction2.m_friend[i] == 0)
                        break;
                    if (faction2.m_friend[i] == faction1.m_faction)
                        return Reaction.Friendly;
                }

                return (Reaction)(~(faction1.m_flags >> 12) & 2 | 1); // it seems checking for (factionFlags & 0x2000) != 0 ? 1 : 3
            }
        }
    }
}
