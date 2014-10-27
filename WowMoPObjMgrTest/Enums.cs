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
        MIRROR_UNK2 = 0x100, // 6.0 unused
        MIRROR_URGENT = 0x200,
        MIRROR_URGENT_SELF_ONLY = 0x400,
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

    enum GameObjectTypeId
    {
        Door = 0,
        Button = 1,
        Questgiver = 2,
        Chest = 3,
        Binder = 4,
        Generic = 5,
        Trap = 6,
        Chair = 7,
        SpellFocus = 8,
        Text = 9,
        Goober = 10,
        TransportElevator = 11,
        AreaDamage = 12,
        Camera = 13,
        Mapobject = 14,
        MoTransportShip = 15,
        DuelFlag = 16,
        FishingNode = 17,
        Ritual = 18,
        Mailbox = 19,
        DONOTUSE1 = 20,
        GuardPost = 21,
        SpellCaster = 22,
        MeetingStone = 23,
        FlagStand = 24,
        FishingHole = 25,
        FlagDrop = 26,
        DONOTUSE2 = 27,
        DONOTUSE3 = 28,
        ControlZone = 29,
        AuraGenerator = 30,
        DungeonDifficulty = 31,
        BarberChair = 32,
        DestructibleBuilding = 33,
        GuildBank = 34,
        Trapdoor = 35,
        Newflag = 36,
        Newflagdrop = 37,
        GarrisonBuilding = 38,
        GarrisonPlot = 39,
        ClientCreature = 40,
        ClientItem = 41,
        CapturePoint = 42,
        PhaseableMO = 43,
        GarrisonMonument = 44,
        GarrisonShipment = 45,
        GarrisonMonumentPlaque = 46,
        NUM_GAMEOBJECT_TYPE = 47
    }

    [Flags]
    enum TrackObjectFlags : uint
    {
        None = 0x00000000,
        Lockpicking = 0x00000001,
        Herbs = 0x00000002,
        Minerals = 0x00000004,
        DisarmTrap = 0x00000008,
        Open = 0x00000010,
        Treasure = 0x00000020,
        CalcifiedElvenGems = 0x00000040,
        Close = 0x00000080,
        ArmTrap = 0x00000100,
        QuickOpen = 0x00000200,
        QuickClose = 0x00000400,
        OpenTinkering = 0x00000800,
        OpenKneeling = 0x00001000,
        OpenAttacking = 0x00002000,
        Gahzridian = 0x00004000,
        Blasting = 0x00008000,
        PvPOpen = 0x00010000,
        PvPClose = 0x00020000,
        Fishing = 0x00040000,
        Inscription = 0x00080000,
        OpenFromVehicle = 0x00100000,
        NotSure = 0x00200000,
        F00400000 = 0x00400000,
        F00800000 = 0x00800000,
        F01000000 = 0x01000000,
        F02000000 = 0x02000000,
        F04000000 = 0x04000000,
        F08000000 = 0x08000000,
        F10000000 = 0x10000000,
        F20000000 = 0x20000000,
        F40000000 = 0x40000000,
        F80000000 = 0x80000000,
        All = 0xFFFFFFFF,
    }

    [Flags]
    enum TrackCreatureFlags
    {
        None = 0x00000000,
        Beasts = 0x00000001,
        Dragons = 0x00000002,
        Demons = 0x00000004,
        Elementals = 0x00000008,
        Giants = 0x00000010,
        Undead = 0x00000020,
        Humanoids = 0x00000040,
        Critters = 0x00000080,
        Machines = 0x00000100,
        Slimes = 0x00000200,
        Totem = 0x00000400,
        NonCombatPet = 0x00000800,
        GasCloud = 0x00001000,
        BattlePets = 0x00002000,
        All = -1, // 0xFFFFFFFF
    }

    enum CGObjectData
    {
        Guid = 0, // size 4, flags MIRROR_ALL
        Data = 4, // size 4, flags MIRROR_ALL
        Type = 8, // size 1, flags MIRROR_ALL
        EntryID = 9, // size 1, flags MIRROR_VIEWER_DEPENDENT
        DynamicFlags = 10, // size 1, flags MIRROR_VIEWER_DEPENDENT, MIRROR_URGENT
        Scale = 11, // size 1, flags MIRROR_ALL
        End = 12
    }

    enum CGItemData
    {
        Owner = CGObjectData.End + 0, // size 4, flags MIRROR_ALL
        ContainedIn = CGObjectData.End + 4, // size 4, flags MIRROR_ALL
        Creator = CGObjectData.End + 8, // size 4, flags MIRROR_ALL
        GiftCreator = CGObjectData.End + 12, // size 4, flags MIRROR_ALL
        StackCount = CGObjectData.End + 16, // size 1, flags MIRROR_OWNER
        Expiration = CGObjectData.End + 17, // size 1, flags MIRROR_OWNER
        SpellCharges = CGObjectData.End + 18, // size 5, flags MIRROR_OWNER
        DynamicFlags = CGObjectData.End + 23, // size 1, flags MIRROR_ALL
        Enchantment = CGObjectData.End + 24, // size 39, flags MIRROR_ALL
        PropertySeed = CGObjectData.End + 63, // size 1, flags MIRROR_ALL
        RandomPropertiesID = CGObjectData.End + 64, // size 1, flags MIRROR_ALL
        Durability = CGObjectData.End + 65, // size 1, flags MIRROR_OWNER
        MaxDurability = CGObjectData.End + 66, // size 1, flags MIRROR_OWNER
        CreatePlayedTime = CGObjectData.End + 67, // size 1, flags MIRROR_ALL
        ModifiersMask = CGObjectData.End + 68, // size 1, flags MIRROR_OWNER
        Context = CGObjectData.End + 69, // size 1, flags MIRROR_ALL
        End = CGObjectData.End + 70
    }

    enum CGContainerData
    {
        Slots = CGItemData.End + 0, // size 144, flags MIRROR_ALL
        NumSlots = CGItemData.End + 144, // size 1, flags MIRROR_ALL
        End = CGItemData.End + 145
    }

    enum CGUnitData
    {
        Charm = CGObjectData.End + 0, // size 4, flags MIRROR_ALL
        Summon = CGObjectData.End + 4, // size 4, flags MIRROR_ALL
        Critter = CGObjectData.End + 8, // size 4, flags MIRROR_SELF
        CharmedBy = CGObjectData.End + 12, // size 4, flags MIRROR_ALL
        SummonedBy = CGObjectData.End + 16, // size 4, flags MIRROR_ALL
        CreatedBy = CGObjectData.End + 20, // size 4, flags MIRROR_ALL
        DemonCreator = CGObjectData.End + 24, // size 4, flags MIRROR_ALL
        Target = CGObjectData.End + 28, // size 4, flags MIRROR_ALL
        BattlePetCompanionGUID = CGObjectData.End + 32, // size 4, flags MIRROR_ALL
        BattlePetDBID = CGObjectData.End + 36, // size 2, flags MIRROR_ALL
        ChannelObject = CGObjectData.End + 38, // size 4, flags MIRROR_ALL, MIRROR_URGENT
        ChannelSpell = CGObjectData.End + 42, // size 1, flags MIRROR_ALL, MIRROR_URGENT
        SummonedByHomeRealm = CGObjectData.End + 43, // size 1, flags MIRROR_ALL
        Sex = CGObjectData.End + 44, // size 1, flags MIRROR_ALL
        DisplayPower = CGObjectData.End + 45, // size 1, flags MIRROR_ALL
        OverrideDisplayPowerID = CGObjectData.End + 46, // size 1, flags MIRROR_ALL
        Health = CGObjectData.End + 47, // size 1, flags MIRROR_ALL
        Power = CGObjectData.End + 48, // size 6, flags MIRROR_ALL, MIRROR_URGENT_SELF_ONLY
        MaxHealth = CGObjectData.End + 54, // size 1, flags MIRROR_ALL
        MaxPower = CGObjectData.End + 55, // size 6, flags MIRROR_ALL
        PowerRegenFlatModifier = CGObjectData.End + 61, // size 6, flags MIRROR_SELF, MIRROR_OWNER, MIRROR_UNIT_ALL
        PowerRegenInterruptedFlatModifier = CGObjectData.End + 67, // size 6, flags MIRROR_SELF, MIRROR_OWNER, MIRROR_UNIT_ALL
        Level = CGObjectData.End + 73, // size 1, flags MIRROR_ALL
        EffectiveLevel = CGObjectData.End + 74, // size 1, flags MIRROR_ALL
        FactionTemplate = CGObjectData.End + 75, // size 1, flags MIRROR_ALL
        VirtualItemID = CGObjectData.End + 76, // size 3, flags MIRROR_ALL
        Flags = CGObjectData.End + 79, // size 1, flags MIRROR_ALL
        Flags2 = CGObjectData.End + 80, // size 1, flags MIRROR_ALL
        Flags3 = CGObjectData.End + 81, // size 1, flags MIRROR_ALL
        AuraState = CGObjectData.End + 82, // size 1, flags MIRROR_ALL
        AttackRoundBaseTime = CGObjectData.End + 83, // size 2, flags MIRROR_ALL
        RangedAttackRoundBaseTime = CGObjectData.End + 85, // size 1, flags MIRROR_SELF
        BoundingRadius = CGObjectData.End + 86, // size 1, flags MIRROR_ALL
        CombatReach = CGObjectData.End + 87, // size 1, flags MIRROR_ALL
        DisplayID = CGObjectData.End + 88, // size 1, flags MIRROR_VIEWER_DEPENDENT, MIRROR_URGENT
        NativeDisplayID = CGObjectData.End + 89, // size 1, flags MIRROR_ALL, MIRROR_URGENT
        MountDisplayID = CGObjectData.End + 90, // size 1, flags MIRROR_ALL, MIRROR_URGENT
        MinDamage = CGObjectData.End + 91, // size 1, flags MIRROR_SELF, MIRROR_OWNER, MIRROR_EMPATH
        MaxDamage = CGObjectData.End + 92, // size 1, flags MIRROR_SELF, MIRROR_OWNER, MIRROR_EMPATH
        MinOffHandDamage = CGObjectData.End + 93, // size 1, flags MIRROR_SELF, MIRROR_OWNER, MIRROR_EMPATH
        MaxOffHandDamage = CGObjectData.End + 94, // size 1, flags MIRROR_SELF, MIRROR_OWNER, MIRROR_EMPATH
        AnimTier = CGObjectData.End + 95, // size 1, flags MIRROR_ALL
        PetNumber = CGObjectData.End + 96, // size 1, flags MIRROR_ALL
        PetNameTimestamp = CGObjectData.End + 97, // size 1, flags MIRROR_ALL
        PetExperience = CGObjectData.End + 98, // size 1, flags MIRROR_OWNER
        PetNextLevelExperience = CGObjectData.End + 99, // size 1, flags MIRROR_OWNER
        ModCastingSpeed = CGObjectData.End + 100, // size 1, flags MIRROR_ALL
        ModSpellHaste = CGObjectData.End + 101, // size 1, flags MIRROR_ALL
        ModHaste = CGObjectData.End + 102, // size 1, flags MIRROR_ALL
        ModRangedHaste = CGObjectData.End + 103, // size 1, flags MIRROR_ALL
        ModHasteRegen = CGObjectData.End + 104, // size 1, flags MIRROR_ALL
        CreatedBySpell = CGObjectData.End + 105, // size 1, flags MIRROR_ALL
        NpcFlags = CGObjectData.End + 106, // size 2, flags MIRROR_ALL, MIRROR_VIEWER_DEPENDENT
        EmoteState = CGObjectData.End + 108, // size 1, flags MIRROR_ALL
        Stats = CGObjectData.End + 109, // size 5, flags MIRROR_SELF, MIRROR_OWNER
        StatPosBuff = CGObjectData.End + 114, // size 5, flags MIRROR_SELF, MIRROR_OWNER
        StatNegBuff = CGObjectData.End + 119, // size 5, flags MIRROR_SELF, MIRROR_OWNER
        Resistances = CGObjectData.End + 124, // size 7, flags MIRROR_SELF, MIRROR_OWNER, MIRROR_EMPATH
        ResistanceBuffModsPositive = CGObjectData.End + 131, // size 7, flags MIRROR_SELF, MIRROR_OWNER
        ResistanceBuffModsNegative = CGObjectData.End + 138, // size 7, flags MIRROR_SELF, MIRROR_OWNER
        ModBonusArmor = CGObjectData.End + 145, // size 1, flags MIRROR_SELF, MIRROR_OWNER
        BaseMana = CGObjectData.End + 146, // size 1, flags MIRROR_ALL
        BaseHealth = CGObjectData.End + 147, // size 1, flags MIRROR_SELF, MIRROR_OWNER
        ShapeshiftForm = CGObjectData.End + 148, // size 1, flags MIRROR_ALL
        AttackPower = CGObjectData.End + 149, // size 1, flags MIRROR_SELF, MIRROR_OWNER
        AttackPowerModPos = CGObjectData.End + 150, // size 1, flags MIRROR_SELF, MIRROR_OWNER
        AttackPowerModNeg = CGObjectData.End + 151, // size 1, flags MIRROR_SELF, MIRROR_OWNER
        AttackPowerMultiplier = CGObjectData.End + 152, // size 1, flags MIRROR_SELF, MIRROR_OWNER
        RangedAttackPower = CGObjectData.End + 153, // size 1, flags MIRROR_SELF, MIRROR_OWNER
        RangedAttackPowerModPos = CGObjectData.End + 154, // size 1, flags MIRROR_SELF, MIRROR_OWNER
        RangedAttackPowerModNeg = CGObjectData.End + 155, // size 1, flags MIRROR_SELF, MIRROR_OWNER
        RangedAttackPowerMultiplier = CGObjectData.End + 156, // size 1, flags MIRROR_SELF, MIRROR_OWNER
        MinRangedDamage = CGObjectData.End + 157, // size 1, flags MIRROR_SELF, MIRROR_OWNER
        MaxRangedDamage = CGObjectData.End + 158, // size 1, flags MIRROR_SELF, MIRROR_OWNER
        PowerCostModifier = CGObjectData.End + 159, // size 7, flags MIRROR_SELF, MIRROR_OWNER
        PowerCostMultiplier = CGObjectData.End + 166, // size 7, flags MIRROR_SELF, MIRROR_OWNER
        MaxHealthModifier = CGObjectData.End + 173, // size 1, flags MIRROR_SELF, MIRROR_OWNER
        HoverHeight = CGObjectData.End + 174, // size 1, flags MIRROR_ALL
        MinItemLevelCutoff = CGObjectData.End + 175, // size 1, flags MIRROR_ALL
        MinItemLevel = CGObjectData.End + 176, // size 1, flags MIRROR_ALL
        MaxItemLevel = CGObjectData.End + 177, // size 1, flags MIRROR_ALL
        WildBattlePetLevel = CGObjectData.End + 178, // size 1, flags MIRROR_ALL
        BattlePetCompanionNameTimestamp = CGObjectData.End + 179, // size 1, flags MIRROR_ALL
        InteractSpellID = CGObjectData.End + 180, // size 1, flags MIRROR_ALL
        StateSpellVisualID = CGObjectData.End + 181, // size 1, flags MIRROR_VIEWER_DEPENDENT, MIRROR_URGENT
        StateAnimID = CGObjectData.End + 182, // size 1, flags MIRROR_VIEWER_DEPENDENT, MIRROR_URGENT
        StateAnimKitID = CGObjectData.End + 183, // size 1, flags MIRROR_VIEWER_DEPENDENT, MIRROR_URGENT
        StateWorldEffectID = CGObjectData.End + 184, // size 4, flags MIRROR_VIEWER_DEPENDENT, MIRROR_URGENT
        ScaleDuration = CGObjectData.End + 188, // size 1, flags MIRROR_ALL
        LooksLikeMountID = CGObjectData.End + 189, // size 1, flags MIRROR_ALL
        LooksLikeCreatureID = CGObjectData.End + 190, // size 1, flags MIRROR_ALL
        End = CGObjectData.End + 191
    }

    enum CGPlayerData
    {
        DuelArbiter = CGUnitData.End + 0, // size 4, flags MIRROR_ALL
        WowAccount = CGUnitData.End + 4, // size 4, flags MIRROR_ALL
        LootTargetGUID = CGUnitData.End + 8, // size 4, flags MIRROR_ALL
        PlayerFlags = CGUnitData.End + 12, // size 1, flags MIRROR_ALL
        PlayerFlagsEx = CGUnitData.End + 13, // size 1, flags MIRROR_ALL
        GuildRankID = CGUnitData.End + 14, // size 1, flags MIRROR_ALL
        GuildDeleteDate = CGUnitData.End + 15, // size 1, flags MIRROR_ALL
        GuildLevel = CGUnitData.End + 16, // size 1, flags MIRROR_ALL
        HairColorID = CGUnitData.End + 17, // size 1, flags MIRROR_ALL
        RestState = CGUnitData.End + 18, // size 1, flags MIRROR_ALL
        ArenaFaction = CGUnitData.End + 19, // size 1, flags MIRROR_ALL
        DuelTeam = CGUnitData.End + 20, // size 1, flags MIRROR_ALL
        GuildTimeStamp = CGUnitData.End + 21, // size 1, flags MIRROR_ALL
        QuestLog = CGUnitData.End + 22, // size 750, flags MIRROR_PARTY
        VisibleItems = CGUnitData.End + 772, // size 57, flags MIRROR_ALL
        PlayerTitle = CGUnitData.End + 829, // size 1, flags MIRROR_ALL
        FakeInebriation = CGUnitData.End + 830, // size 1, flags MIRROR_ALL
        VirtualPlayerRealm = CGUnitData.End + 831, // size 1, flags MIRROR_ALL
        CurrentSpecID = CGUnitData.End + 832, // size 1, flags MIRROR_ALL
        TaxiMountAnimKitID = CGUnitData.End + 833, // size 1, flags MIRROR_ALL
        AvgItemLevelTotal = CGUnitData.End + 834, // size 1, flags MIRROR_ALL
        AvgItemLevelEquipped = CGUnitData.End + 835, // size 1, flags MIRROR_ALL
        CurrentBattlePetBreedQuality = CGUnitData.End + 836, // size 1, flags MIRROR_ALL
        InvSlots = CGUnitData.End + 837, // size 736, flags MIRROR_SELF
        FarsightObject = CGUnitData.End + 1573, // size 4, flags MIRROR_SELF
        KnownTitles = CGUnitData.End + 1577, // size 10, flags MIRROR_SELF
        Coinage = CGUnitData.End + 1587, // size 2, flags MIRROR_SELF
        XP = CGUnitData.End + 1589, // size 1, flags MIRROR_SELF
        NextLevelXP = CGUnitData.End + 1590, // size 1, flags MIRROR_SELF
        Skill = CGUnitData.End + 1591, // size 448, flags MIRROR_SELF
        CharacterPoints = CGUnitData.End + 2039, // size 1, flags MIRROR_SELF
        MaxTalentTiers = CGUnitData.End + 2040, // size 1, flags MIRROR_SELF
        TrackCreatureMask = CGUnitData.End + 2041, // size 1, flags MIRROR_SELF
        TrackResourceMask = CGUnitData.End + 2042, // size 1, flags MIRROR_SELF
        MainhandExpertise = CGUnitData.End + 2043, // size 1, flags MIRROR_SELF
        OffhandExpertise = CGUnitData.End + 2044, // size 1, flags MIRROR_SELF
        RangedExpertise = CGUnitData.End + 2045, // size 1, flags MIRROR_SELF
        CombatRatingExpertise = CGUnitData.End + 2046, // size 1, flags MIRROR_SELF
        BlockPercentage = CGUnitData.End + 2047, // size 1, flags MIRROR_SELF
        DodgePercentage = CGUnitData.End + 2048, // size 1, flags MIRROR_SELF
        ParryPercentage = CGUnitData.End + 2049, // size 1, flags MIRROR_SELF
        CritPercentage = CGUnitData.End + 2050, // size 1, flags MIRROR_SELF
        RangedCritPercentage = CGUnitData.End + 2051, // size 1, flags MIRROR_SELF
        OffhandCritPercentage = CGUnitData.End + 2052, // size 1, flags MIRROR_SELF
        SpellCritPercentage = CGUnitData.End + 2053, // size 7, flags MIRROR_SELF
        ShieldBlock = CGUnitData.End + 2060, // size 1, flags MIRROR_SELF
        ShieldBlockCritPercentage = CGUnitData.End + 2061, // size 1, flags MIRROR_SELF
        Mastery = CGUnitData.End + 2062, // size 1, flags MIRROR_SELF
        Amplify = CGUnitData.End + 2063, // size 1, flags MIRROR_SELF
        Multistrike = CGUnitData.End + 2064, // size 1, flags MIRROR_SELF
        MultistrikeEffect = CGUnitData.End + 2065, // size 1, flags MIRROR_SELF
        Readiness = CGUnitData.End + 2066, // size 1, flags MIRROR_SELF
        Speed = CGUnitData.End + 2067, // size 1, flags MIRROR_SELF
        Lifesteal = CGUnitData.End + 2068, // size 1, flags MIRROR_SELF
        Avoidance = CGUnitData.End + 2069, // size 1, flags MIRROR_SELF
        Sturdiness = CGUnitData.End + 2070, // size 1, flags MIRROR_SELF
        Cleave = CGUnitData.End + 2071, // size 1, flags MIRROR_SELF
        Versatility = CGUnitData.End + 2072, // size 1, flags MIRROR_SELF
        VersatilityBonus = CGUnitData.End + 2073, // size 1, flags MIRROR_SELF
        PvpPowerDamage = CGUnitData.End + 2074, // size 1, flags MIRROR_SELF
        PvpPowerHealing = CGUnitData.End + 2075, // size 1, flags MIRROR_SELF
        ExploredZones = CGUnitData.End + 2076, // size 200, flags MIRROR_SELF
        RestStateBonusPool = CGUnitData.End + 2276, // size 1, flags MIRROR_SELF
        ModDamageDonePos = CGUnitData.End + 2277, // size 7, flags MIRROR_SELF
        ModDamageDoneNeg = CGUnitData.End + 2284, // size 7, flags MIRROR_SELF
        ModDamageDonePercent = CGUnitData.End + 2291, // size 7, flags MIRROR_SELF
        ModHealingDonePos = CGUnitData.End + 2298, // size 1, flags MIRROR_SELF
        ModHealingPercent = CGUnitData.End + 2299, // size 1, flags MIRROR_SELF
        ModHealingDonePercent = CGUnitData.End + 2300, // size 1, flags MIRROR_SELF
        ModPeriodicHealingDonePercent = CGUnitData.End + 2301, // size 1, flags MIRROR_SELF
        WeaponDmgMultipliers = CGUnitData.End + 2302, // size 3, flags MIRROR_SELF
        WeaponAtkSpeedMultipliers = CGUnitData.End + 2305, // size 3, flags MIRROR_SELF
        ModSpellPowerPercent = CGUnitData.End + 2308, // size 1, flags MIRROR_SELF
        ModResiliencePercent = CGUnitData.End + 2309, // size 1, flags MIRROR_SELF
        OverrideSpellPowerByAPPercent = CGUnitData.End + 2310, // size 1, flags MIRROR_SELF
        OverrideAPBySpellPowerPercent = CGUnitData.End + 2311, // size 1, flags MIRROR_SELF
        ModTargetResistance = CGUnitData.End + 2312, // size 1, flags MIRROR_SELF
        ModTargetPhysicalResistance = CGUnitData.End + 2313, // size 1, flags MIRROR_SELF
        LocalFlags = CGUnitData.End + 2314, // size 1, flags MIRROR_SELF
        LifetimeMaxRank = CGUnitData.End + 2315, // size 1, flags MIRROR_SELF
        SelfResSpell = CGUnitData.End + 2316, // size 1, flags MIRROR_SELF
        PvpMedals = CGUnitData.End + 2317, // size 1, flags MIRROR_SELF
        BuybackPrice = CGUnitData.End + 2318, // size 12, flags MIRROR_SELF
        BuybackTimestamp = CGUnitData.End + 2330, // size 12, flags MIRROR_SELF
        YesterdayHonorableKills = CGUnitData.End + 2342, // size 1, flags MIRROR_SELF
        LifetimeHonorableKills = CGUnitData.End + 2343, // size 1, flags MIRROR_SELF
        WatchedFactionIndex = CGUnitData.End + 2344, // size 1, flags MIRROR_SELF
        CombatRatings = CGUnitData.End + 2345, // size 32, flags MIRROR_SELF
        PvpInfo = CGUnitData.End + 2377, // size 36, flags MIRROR_SELF
        MaxLevel = CGUnitData.End + 2413, // size 1, flags MIRROR_SELF
        RuneRegen = CGUnitData.End + 2414, // size 4, flags MIRROR_SELF
        NoReagentCostMask = CGUnitData.End + 2418, // size 4, flags MIRROR_SELF
        GlyphSlots = CGUnitData.End + 2422, // size 6, flags MIRROR_SELF
        Glyphs = CGUnitData.End + 2428, // size 6, flags MIRROR_SELF
        GlyphSlotsEnabled = CGUnitData.End + 2434, // size 1, flags MIRROR_SELF
        PetSpellPower = CGUnitData.End + 2435, // size 1, flags MIRROR_SELF
        Researching = CGUnitData.End + 2436, // size 10, flags MIRROR_SELF
        ProfessionSkillLine = CGUnitData.End + 2446, // size 2, flags MIRROR_SELF
        UiHitModifier = CGUnitData.End + 2448, // size 1, flags MIRROR_SELF
        UiSpellHitModifier = CGUnitData.End + 2449, // size 1, flags MIRROR_SELF
        HomeRealmTimeOffset = CGUnitData.End + 2450, // size 1, flags MIRROR_SELF
        ModPetHaste = CGUnitData.End + 2451, // size 1, flags MIRROR_SELF
        SummonedBattlePetGUID = CGUnitData.End + 2452, // size 4, flags MIRROR_SELF
        OverrideSpellsID = CGUnitData.End + 2456, // size 1, flags MIRROR_SELF, MIRROR_URGENT_SELF_ONLY
        LfgBonusFactionID = CGUnitData.End + 2457, // size 1, flags MIRROR_SELF
        LootSpecID = CGUnitData.End + 2458, // size 1, flags MIRROR_SELF
        OverrideZonePVPType = CGUnitData.End + 2459, // size 1, flags MIRROR_SELF, MIRROR_URGENT_SELF_ONLY
        ItemLevelDelta = CGUnitData.End + 2460, // size 1, flags MIRROR_SELF
        BagSlotFlags = CGUnitData.End + 2461, // size 4, flags MIRROR_SELF
        BankBagSlotFlags = CGUnitData.End + 2465, // size 7, flags MIRROR_SELF
        InsertItemsLeftToRight = CGUnitData.End + 2472, // size 1, flags MIRROR_SELF
        End = CGUnitData.End + 2473
    }

    enum CGGameObjectData
    {
        CreatedBy = CGObjectData.End + 0, // size 4, flags MIRROR_ALL
        DisplayID = CGObjectData.End + 4, // size 1, flags MIRROR_VIEWER_DEPENDENT, MIRROR_URGENT
        Flags = CGObjectData.End + 5, // size 1, flags MIRROR_ALL, MIRROR_URGENT
        ParentRotation = CGObjectData.End + 6, // size 4, flags MIRROR_ALL
        FactionTemplate = CGObjectData.End + 10, // size 1, flags MIRROR_ALL
        Level = CGObjectData.End + 11, // size 1, flags MIRROR_ALL
        PercentHealth = CGObjectData.End + 12, // size 1, flags MIRROR_ALL, MIRROR_URGENT
        SpellVisualID = CGObjectData.End + 13, // size 1, flags MIRROR_ALL, MIRROR_URGENT
        StateSpellVisualID = CGObjectData.End + 14, // size 1, flags MIRROR_VIEWER_DEPENDENT, MIRROR_URGENT
        StateAnimID = CGObjectData.End + 15, // size 1, flags MIRROR_VIEWER_DEPENDENT, MIRROR_URGENT
        StateAnimKitID = CGObjectData.End + 16, // size 1, flags MIRROR_VIEWER_DEPENDENT, MIRROR_URGENT
        StateWorldEffectID = CGObjectData.End + 17, // size 4, flags MIRROR_VIEWER_DEPENDENT, MIRROR_URGENT
        End = CGObjectData.End + 21
    }

    enum CGDynamicObjectData
    {
        Caster = CGObjectData.End + 0, // size 4, flags MIRROR_ALL
        TypeAndVisualID = CGObjectData.End + 4, // size 1, flags MIRROR_VIEWER_DEPENDENT
        SpellID = CGObjectData.End + 5, // size 1, flags MIRROR_ALL
        Radius = CGObjectData.End + 6, // size 1, flags MIRROR_ALL
        CastTime = CGObjectData.End + 7, // size 1, flags MIRROR_ALL
        End = CGObjectData.End + 8
    }

    enum CGCorpseData
    {
        Owner = CGObjectData.End + 0, // size 4, flags MIRROR_ALL
        PartyGUID = CGObjectData.End + 4, // size 4, flags MIRROR_ALL
        DisplayID = CGObjectData.End + 8, // size 1, flags MIRROR_ALL
        Items = CGObjectData.End + 9, // size 19, flags MIRROR_ALL
        SkinID = CGObjectData.End + 28, // size 1, flags MIRROR_ALL
        FacialHairStyleID = CGObjectData.End + 29, // size 1, flags MIRROR_ALL
        Flags = CGObjectData.End + 30, // size 1, flags MIRROR_ALL
        DynamicFlags = CGObjectData.End + 31, // size 1, flags MIRROR_VIEWER_DEPENDENT
        FactionTemplate = CGObjectData.End + 32, // size 1, flags MIRROR_ALL
        End = CGObjectData.End + 33
    }

    enum CGAreaTriggerData
    {
        Caster = CGObjectData.End + 0, // size 4, flags MIRROR_ALL
        Duration = CGObjectData.End + 4, // size 1, flags MIRROR_ALL
        SpellID = CGObjectData.End + 5, // size 1, flags MIRROR_ALL
        SpellVisualID = CGObjectData.End + 6, // size 1, flags MIRROR_VIEWER_DEPENDENT
        ExplicitScale = CGObjectData.End + 7, // size 1, flags MIRROR_ALL, MIRROR_URGENT
        End = CGObjectData.End + 8
    }

    enum CGSceneObjectData
    {
        ScriptPackageID = CGObjectData.End + 0, // size 1, flags MIRROR_ALL
        RndSeedVal = CGObjectData.End + 1, // size 1, flags MIRROR_ALL
        CreatedBy = CGObjectData.End + 2, // size 4, flags MIRROR_ALL
        SceneType = CGObjectData.End + 6, // size 1, flags MIRROR_ALL
        End = CGObjectData.End + 7
    }

    enum CGConversationData
    {
        Dummy = CGObjectData.End + 0, // size 1, flags MIRROR_SELF
        End = CGObjectData.End + 1
    }
}
