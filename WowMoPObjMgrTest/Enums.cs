using System;

namespace WowMoPObjMgrTest
{
    enum WowObjectType : int
    {
        Object = 0,
        Item = 1,
        Container = 2,
        Unit = 3,
        Player = 4,
        GameObject = 5,
        DynamicObject = 6,
        Corpse = 7,
        AreaTrigger = 8,
        SceneObject = 9,
        NumClientObjectTypes = 0xA
    }

    [Flags]
    enum ObjectTypeFlags : int
    {
        Object = 1 << WowObjectType.Object,
        Item = 1 << WowObjectType.Item,
        Container = 1 << WowObjectType.Container,
        Unit = 1 << WowObjectType.Unit,
        Player = 1 << WowObjectType.Player,
        GameObject = 1 << WowObjectType.GameObject,
        DynamicObject = 1 << WowObjectType.DynamicObject,
        Corpse = 1 << WowObjectType.Corpse,
        AreaTrigger = 1 << WowObjectType.AreaTrigger,
        SceneObject = 1 << WowObjectType.SceneObject
    }

    [Flags]
    enum MIRROR_FLAGS
    {
        MIRROR_NONE = 0x0,
        MIRROR_ALL = 0x1,
        MIRROR_SELF = 0x2,
        MIRROR_OWNER = 0x4,
        MIRROR_UNK1 = 0x8, // unused
        MIRROR_EMPATH = 0x10,
        MIRROR_PARTY = 0x20,
        MIRROR_UNIT_ALL = 0x40,
        MIRROR_VIEWER_DEPENDENT = 0x80,
        MIRROR_URGENT = 0x100,
        MIRROR_URGENT_SELF_ONLY = 0x200,
    }

    public enum Reaction
    {
        Hostile = 1,
        Neutral = 3,
        Friendly = 4,
    }

    public enum Faction
    {
        Invalid = -1,
        Neutral = 0, // 1
        Alliance = 1, // 2
        Horde = 2, // 4
        Monster = 3 // 8
    }

    enum CGObjectData
    {
        Guid = 0, // size 2
        Data = 2, // size 2
        Type = 4,
        Entry = 5,
        Scale = 6,
        End = 7
    }

    enum CGItemData
    {
        Owner = CGObjectData.End + 0, // size 2
        ContainedIn = CGObjectData.End + 2, // size 2
        Creator = CGObjectData.End + 4, // size 2
        GiftCreator = CGObjectData.End + 6, // size 2
        StackCount = CGObjectData.End + 8,
        Expiration = CGObjectData.End + 9,
        SpellCharges = CGObjectData.End + 10, // size 5
        DynamicFlags = CGObjectData.End + 15,
        Enchantment = CGObjectData.End + 16, // size 39
        PropertySeed = CGObjectData.End + 55,
        RandomPropertiesID = CGObjectData.End + 56,
        Durability = CGObjectData.End + 57,
        MaxDurability = CGObjectData.End + 58,
        CreatePlayedTime = CGObjectData.End + 59,
        ModifiersMask = CGObjectData.End + 60,
        End = CGObjectData.End + 61
    }

    enum CGContainerData
    {
        Slots = CGItemData.End + 0, // size 72
        NumSlots = CGItemData.End + 72,
        End = CGItemData.End + 73
    }

    enum CGUnitData
    {
        Charm = CGObjectData.End + 0, // size 2
        Summon = CGObjectData.End + 2, // size 2
        Critter = CGObjectData.End + 4, // size 2
        CharmedBy = CGObjectData.End + 6, // size 2
        SummonedBy = CGObjectData.End + 8, // size 2
        CreatedBy = CGObjectData.End + 10, // size 2
        Target = CGObjectData.End + 12, // size 2
        ChannelObject = CGObjectData.End + 14, // size 2
        SummonedByHomeRealm = CGObjectData.End + 16,
        ChannelSpell = CGObjectData.End + 17,
        DisplayPower = CGObjectData.End + 18,
        OverrideDisplayPowerID = CGObjectData.End + 19,
        Health = CGObjectData.End + 20,
        Power = CGObjectData.End + 21, // size 5
        MaxHealth = CGObjectData.End + 26,
        MaxPower = CGObjectData.End + 27, // size 5
        PowerRegenFlatModifier = CGObjectData.End + 32, // size 5
        PowerRegenInterruptedFlatModifier = CGObjectData.End + 37, // size 5
        Level = CGObjectData.End + 42,
        FactionTemplate = CGObjectData.End + 43,
        VirtualItemID = CGObjectData.End + 44, // size 3
        Flags = CGObjectData.End + 47,
        Flags2 = CGObjectData.End + 48,
        AuraState = CGObjectData.End + 49,
        AttackRoundBaseTime = CGObjectData.End + 50, // size 2
        RangedAttackRoundBaseTime = CGObjectData.End + 52,
        boundingRadius = CGObjectData.End + 53,
        combatReach = CGObjectData.End + 54,
        displayID = CGObjectData.End + 55,
        nativeDisplayID = CGObjectData.End + 56,
        mountDisplayID = CGObjectData.End + 57,
        minDamage = CGObjectData.End + 58,
        maxDamage = CGObjectData.End + 59,
        minOffHandDamage = CGObjectData.End + 60,
        maxOffHandDamage = CGObjectData.End + 61,
        animTier = CGObjectData.End + 62,
        petNumber = CGObjectData.End + 63,
        petNameTimestamp = CGObjectData.End + 64,
        petExperience = CGObjectData.End + 65,
        petNextLevelExperience = CGObjectData.End + 66,
        dynamicFlags = CGObjectData.End + 67,
        modCastingSpeed = CGObjectData.End + 68,
        modSpellHaste = CGObjectData.End + 69,
        modHaste = CGObjectData.End + 70,
        modHasteRegen = CGObjectData.End + 71,
        createdBySpell = CGObjectData.End + 72,
        npcFlags = CGObjectData.End + 73,
        emoteState = CGObjectData.End + 75,
        stats = CGObjectData.End + 76,
        statPosBuff = CGObjectData.End + 81,
        statNegBuff = CGObjectData.End + 86,
        resistances = CGObjectData.End + 91,
        resistanceBuffModsPositive = CGObjectData.End + 98,
        resistanceBuffModsNegative = CGObjectData.End + 105,
        baseMana = CGObjectData.End + 112,
        baseHealth = CGObjectData.End + 113,
        shapeshiftForm = CGObjectData.End + 114,
        attackPower = CGObjectData.End + 115,
        attackPowerModPos = CGObjectData.End + 116,
        attackPowerModNeg = CGObjectData.End + 117,
        attackPowerMultiplier = CGObjectData.End + 118,
        rangedAttackPower = CGObjectData.End + 119,
        rangedAttackPowerModPos = CGObjectData.End + 120,
        rangedAttackPowerModNeg = CGObjectData.End + 121,
        rangedAttackPowerMultiplier = CGObjectData.End + 122,
        minRangedDamage = CGObjectData.End + 123,
        maxRangedDamage = CGObjectData.End + 124,
        powerCostModifier = CGObjectData.End + 125,
        powerCostMultiplier = CGObjectData.End + 132,
        maxHealthModifier = CGObjectData.End + 139,
        hoverHeight = CGObjectData.End + 140,
        minItemLevel = CGObjectData.End + 141,
        maxItemLevel = CGObjectData.End + 142,
        wildBattlePetLevel = CGObjectData.End + 143,
        battlePetCompanionGUID = CGObjectData.End + 144,
        battlePetCompanionNameTimestamp = CGObjectData.End + 146,
        End = CGObjectData.End + 147
    }

    public enum CGPlayerData
    {
        duelArbiter = CGUnitData.End + 0,
        playerFlags = CGUnitData.End + 2,
        guildRankID = CGUnitData.End + 3,
        guildDeleteDate = CGUnitData.End + 4,
        guildLevel = CGUnitData.End + 5,
        hairColorID = CGUnitData.End + 6,
        restState = CGUnitData.End + 7,
        arenaFaction = CGUnitData.End + 8,
        duelTeam = CGUnitData.End + 9,
        guildTimeStamp = CGUnitData.End + 10,
        questLog = CGUnitData.End + 11,
        visibleItems = CGUnitData.End + 761,
        playerTitle = CGUnitData.End + 799,
        fakeInebriation = CGUnitData.End + 800,
        homePlayerRealm = CGUnitData.End + 801,
        currentSpecID = CGUnitData.End + 802,
        taxiMountAnimKitID = CGUnitData.End + 803,
        currentBattlePetBreedQuality = CGUnitData.End + 804,
        invSlots = CGUnitData.End + 805,
        farsightObject = CGUnitData.End + 977,
        knownTitles = CGUnitData.End + 979,
        coinage = CGUnitData.End + 987,
        XP = CGUnitData.End + 989,
        nextLevelXP = CGUnitData.End + 990,
        skill = CGUnitData.End + 991,
        characterPoints = CGUnitData.End + 1439,
        maxTalentTiers = CGUnitData.End + 1440,
        trackCreatureMask = CGUnitData.End + 1441,
        trackResourceMask = CGUnitData.End + 1442,
        expertise = CGUnitData.End + 1443,
        offhandExpertise = CGUnitData.End + 1444,
        rangedExpertise = CGUnitData.End + 1445,
        blockPercentage = CGUnitData.End + 1446,
        dodgePercentage = CGUnitData.End + 1447,
        parryPercentage = CGUnitData.End + 1448,
        critPercentage = CGUnitData.End + 1449,
        rangedCritPercentage = CGUnitData.End + 1450,
        offhandCritPercentage = CGUnitData.End + 1451,
        spellCritPercentage = CGUnitData.End + 1452,
        shieldBlock = CGUnitData.End + 1459,
        shieldBlockCritPercentage = CGUnitData.End + 1460,
        mastery = CGUnitData.End + 1461,
        pvpPowerDamage = CGUnitData.End + 1462,
        pvpPowerHealing = CGUnitData.End + 1463,
        exploredZones = CGUnitData.End + 1464,
        restStateBonusPool = CGUnitData.End + 1664,
        modDamageDonePos = CGUnitData.End + 1665,
        modDamageDoneNeg = CGUnitData.End + 1672,
        modDamageDonePercent = CGUnitData.End + 1679,
        modHealingDonePos = CGUnitData.End + 1686,
        modHealingPercent = CGUnitData.End + 1687,
        modHealingDonePercent = CGUnitData.End + 1688,
        modPeriodicHealingDonePercent = CGUnitData.End + 1689,
        weaponDmgMultipliers = CGUnitData.End + 1690,
        modSpellPowerPercent = CGUnitData.End + 1693,
        modResiliencePercent = CGUnitData.End + 1694,
        overrideSpellPowerByAPPercent = CGUnitData.End + 1695,
        overrideAPBySpellPowerPercent = CGUnitData.End + 1696,
        modTargetResistance = CGUnitData.End + 1697,
        modTargetPhysicalResistance = CGUnitData.End + 1698,
        lifetimeMaxRank = CGUnitData.End + 1699,
        selfResSpell = CGUnitData.End + 1700,
        pvpMedals = CGUnitData.End + 1701,
        buybackPrice = CGUnitData.End + 1702,
        buybackTimestamp = CGUnitData.End + 1714,
        yesterdayHonorableKills = CGUnitData.End + 1726,
        lifetimeHonorableKills = CGUnitData.End + 1727,
        watchedFactionIndex = CGUnitData.End + 1728,
        combatRatings = CGUnitData.End + 1729,
        arenaTeams = CGUnitData.End + 1756,
        battlegroundRating = CGUnitData.End + 1777,
        maxLevel = CGUnitData.End + 1778,
        runeRegen = CGUnitData.End + 1779,
        noReagentCostMask = CGUnitData.End + 1783,
        glyphSlots = CGUnitData.End + 1787,
        glyphs = CGUnitData.End + 1793,
        glyphSlotsEnabled = CGUnitData.End + 1799,
        petSpellPower = CGUnitData.End + 1800,
        researching = CGUnitData.End + 1801,
        professionSkillLine = CGUnitData.End + 1809,
        uiHitModifier = CGUnitData.End + 1811,
        uiSpellHitModifier = CGUnitData.End + 1812,
        homeRealmTimeOffset = CGUnitData.End + 1813,
        modRangedHaste = CGUnitData.End + 1814,
        modPetHaste = CGUnitData.End + 1815,
        summonedBattlePetGUID = CGUnitData.End + 1816,
        overrideSpellsID = CGUnitData.End + 1818,
        End = CGUnitData.End + 1819
    }

    public enum CGGameObjectData
    {
        createdBy = CGObjectData.End + 0,
        displayID = CGObjectData.End + 2,
        flags = CGObjectData.End + 3,
        parentRotation = CGObjectData.End + 4,
        animProgress = CGObjectData.End + 8,
        factionTemplate = CGObjectData.End + 9,
        level = CGObjectData.End + 10,
        percentHealth = CGObjectData.End + 11,
        End = CGObjectData.End + 12
    }

    public enum CGDynamicObjectData
    {
        caster = CGObjectData.End + 0,
        typeAndVisualID = CGObjectData.End + 2,
        spellID = CGObjectData.End + 3,
        radius = CGObjectData.End + 4,
        castTime = CGObjectData.End + 5,
        End = CGObjectData.End + 6
    }

    public enum CGCorpseData
    {
        owner = CGObjectData.End + 0,
        partyGUID = CGObjectData.End + 2,
        displayID = CGObjectData.End + 4,
        items = CGObjectData.End + 5,
        skinID = CGObjectData.End + 24,
        facialHairStyleID = CGObjectData.End + 25,
        flags = CGObjectData.End + 26,
        dynamicFlags = CGObjectData.End + 27,
        End = CGObjectData.End + 28
    }

    public enum CGAreaTriggerData
    {
        caster = CGObjectData.End + 0,
        duration = CGObjectData.End + 2,
        spellID = CGObjectData.End + 3,
        spellVisualID = CGObjectData.End + 4,
        End = CGObjectData.End + 5
    }

    public enum CGSceneObjectData
    {
        scriptPackageID = CGObjectData.End + 0,
        rndSeedVal = CGObjectData.End + 1,
        createdBy = CGObjectData.End + 2,
        End = CGObjectData.End + 4
    }
}
