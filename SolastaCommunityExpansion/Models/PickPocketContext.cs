﻿using System.Collections.Generic;
using SolastaCommunityExpansion.Builders;
using SolastaCommunityExpansion.Builders.Features;
using SolastaModApi;
using SolastaModApi.Extensions;
using SolastaModApi.Infrastructure;
using static FeatureDefinitionAbilityCheckAffinity;
using static RuleDefinitions;
using static SolastaModApi.DatabaseHelper.LootPackDefinitions;

namespace SolastaCommunityExpansion.Models;

public static class PickPocketContext
{
    private static bool initialized;

    internal static void CreateFeats(ICollection<FeatDefinition> feats)
    {
        var pickpocket_check_affinity = FeatureDefinitionAbilityCheckAffinityBuilder
            .Create(DatabaseHelper.FeatureDefinitionAbilityCheckAffinitys.AbilityCheckAffinityFeatLockbreaker,
                "AbilityCheckAffinityFeatPickPocket", "30b1492a-053f-412e-b247-798fbc255038")
            .SetGuiPresentation("PickPocketFeat", Category.Feat)
            .AddToDB();

        var pickpocketAbilityCheckAffinityGroup = new AbilityCheckAffinityGroup
        {
            abilityScoreName = AttributeDefinitions.Dexterity,
            proficiencyName = SkillDefinitions.SleightOfHand,
            affinity = CharacterAbilityCheckAffinity.Advantage
        };

        pickpocket_check_affinity.AffinityGroups.SetRange(pickpocketAbilityCheckAffinityGroup);

        var pickpocket_proficiency = FeatureDefinitionProficiencyBuilder
            .Create(DatabaseHelper.FeatureDefinitionProficiencys.ProficiencyFeatLockbreaker,
                "ProficiencyFeatPickPocket", "d8046b0c-2f93-4b47-b2dd-110234a4a848")
            .SetGuiPresentation("PickPocketFeat", Category.Feat)
            .AddToDB();

        pickpocket_proficiency.SetProficiencyType(ProficiencyType.SkillOrExpertise);
        pickpocket_proficiency.Proficiencies.Clear();
        pickpocket_proficiency.Proficiencies.Add(SkillDefinitions.SleightOfHand);

        var pickPocketFeat = FeatDefinitionBuilder
            .Create(DatabaseHelper.FeatDefinitions.Lockbreaker, "PickPocketFeat",
                "947a31fc-4990-45a5-bcfd-6c478b4dff8a")
            .SetGuiPresentation(Category.Feat)
            .AddToDB();

        pickPocketFeat.Features.SetRange(pickpocket_check_affinity, pickpocket_proficiency);

        feats.Add(pickPocketFeat);
    }

    internal static void Load()
    {
        if (!Main.Settings.AddPickpocketableLoot || initialized)
        {
            return;
        }

        initialized = true;

        var sleight_of_hand = DatabaseHelper.SkillDefinitions.SleightOfHand;
        sleight_of_hand.GuiPresentation.SetUnusedInSolastaCOTM(false);

        var treasure_copper = new TreasureOption();
        treasure_copper.SetOdds(30);
        treasure_copper.SetItemDefinition(DatabaseHelper.ItemDefinitions._1D6_Copper_Coins);
        treasure_copper.SetAmount(3);

        var treasure_silver = new TreasureOption();
        treasure_silver.SetOdds(12);
        treasure_silver.SetItemDefinition(DatabaseHelper.ItemDefinitions._1D6_Silver_Coins);
        treasure_silver.SetAmount(2);

        var treasure_gold = new TreasureOption();
        treasure_gold.SetOdds(8);
        treasure_gold.SetItemDefinition(DatabaseHelper.ItemDefinitions._1D6_Gold_Coins);
        treasure_gold.SetAmount(1);

        var treasure_abyss_moss = new TreasureOption();
        treasure_abyss_moss.SetOdds(20);
        treasure_abyss_moss.SetItemDefinition(DatabaseHelper.ItemDefinitions.Ingredient_AbyssMoss);
        treasure_abyss_moss.SetAmount(1);

        var treasure_deeproot_lichen = new TreasureOption();
        treasure_deeproot_lichen.SetOdds(20);
        treasure_deeproot_lichen.SetItemDefinition(DatabaseHelper.ItemDefinitions.Ingredient_DeepRootLichen);
        treasure_deeproot_lichen.SetAmount(1);

        var treasure_goblinhair_fungus = new TreasureOption();
        treasure_goblinhair_fungus.SetOdds(20);
        treasure_goblinhair_fungus.SetItemDefinition(DatabaseHelper.ItemDefinitions.Ingredient_GoblinHairFungus);
        treasure_goblinhair_fungus.SetAmount(1);

        var treasure_scrollbless = new TreasureOption();
        treasure_scrollbless.SetOdds(1);
        treasure_scrollbless.SetItemDefinition(DatabaseHelper.ItemDefinitions.ScrollBless);
        treasure_scrollbless.SetAmount(1);

        var treasure_scrollcurewounds = new TreasureOption();
        treasure_scrollcurewounds.SetOdds(1);
        treasure_scrollcurewounds.SetItemDefinition(DatabaseHelper.ItemDefinitions.ScrollCureWounds);
        treasure_scrollcurewounds.SetAmount(1);

        var treasure_scrolldetectmagic = new TreasureOption();
        treasure_scrolldetectmagic.SetOdds(1);
        treasure_scrolldetectmagic.SetItemDefinition(DatabaseHelper.ItemDefinitions.ScrollDetectMagic);
        treasure_scrolldetectmagic.SetAmount(1);

        var treasure_scrollidentify = new TreasureOption();
        treasure_scrollidentify.SetOdds(1);
        treasure_scrollidentify.SetItemDefinition(DatabaseHelper.ItemDefinitions.ScrollIdentify);
        treasure_scrollidentify.SetAmount(1);

        var treasure_poisonbasic = new TreasureOption();
        treasure_poisonbasic.SetOdds(1);
        treasure_poisonbasic.SetItemDefinition(DatabaseHelper.ItemDefinitions.Poison_Basic);
        treasure_poisonbasic.SetAmount(1);

        var treasure_potionremedy = new TreasureOption();
        treasure_potionremedy.SetOdds(1);
        treasure_potionremedy.SetItemDefinition(DatabaseHelper.ItemDefinitions.PotionRemedy);
        treasure_potionremedy.SetAmount(1);

        var treasure_dagger = new TreasureOption();
        treasure_dagger.SetOdds(4);
        treasure_dagger.SetItemDefinition(DatabaseHelper.ItemDefinitions.Dagger);
        treasure_dagger.SetAmount(1);

        var treasure_refinedoil = new TreasureOption();
        treasure_refinedoil.SetOdds(1);
        treasure_refinedoil.SetItemDefinition(DatabaseHelper.ItemDefinitions.Ingredient_RefinedOil);
        treasure_refinedoil.SetAmount(1);

        var treasure_acid = new TreasureOption();
        treasure_acid.SetOdds(1);
        treasure_acid.SetItemDefinition(DatabaseHelper.ItemDefinitions.Ingredient_Acid);
        treasure_acid.SetAmount(1);

        var treasure_amethyst = new TreasureOption();
        treasure_amethyst.SetOdds(1);
        treasure_amethyst.SetItemDefinition(DatabaseHelper.ItemDefinitions._20_GP_Amethyst);
        treasure_amethyst.SetAmount(1);

        var pick_pocket_table = TreasureTableDefinitionBuilder
            .Create(DatabaseHelper.TreasureTableDefinitions.RandomTreasureTableG_25_GP_Art_Items, "PickPocketTable",
                "79cac3e5-0f00-4062-b263-adbc854223d7")
            .SetGuiPresentationNoContent()
            .AddToDB();

        pick_pocket_table.TreasureOptions.Add(treasure_copper);
        pick_pocket_table.TreasureOptions.Add(treasure_silver);
        pick_pocket_table.TreasureOptions.Add(treasure_gold);
        pick_pocket_table.TreasureOptions.Add(treasure_scrollbless);
        pick_pocket_table.TreasureOptions.Add(treasure_scrollcurewounds);
        pick_pocket_table.TreasureOptions.Add(treasure_scrolldetectmagic);
        pick_pocket_table.TreasureOptions.Add(treasure_scrollidentify);
        pick_pocket_table.TreasureOptions.Add(treasure_poisonbasic);
        pick_pocket_table.TreasureOptions.Add(treasure_potionremedy);
        pick_pocket_table.TreasureOptions.Add(treasure_dagger);
        pick_pocket_table.TreasureOptions.Add(treasure_refinedoil);
        pick_pocket_table.TreasureOptions.Add(treasure_acid);
        pick_pocket_table.TreasureOptions.Add(treasure_amethyst);

        var pick_pocket_table_undead = TreasureTableDefinitionBuilder
            .Create(DatabaseHelper.TreasureTableDefinitions.RandomTreasureTableG_25_GP_Art_Items,
                "PickPocketTableC", "f1bbd8e5-3e05-48da-9c70-2db676a280b4")
            .SetGuiPresentationNoContent()
            .AddTreasureOptions(treasure_copper, treasure_abyss_moss, treasure_deeproot_lichen,
                treasure_goblinhair_fungus)
            .AddToDB();

        var loot_pickpocket_table = new ItemOccurence(Zombie_loot_drop.ItemOccurencesList[0]);
        loot_pickpocket_table.itemMode = ItemOccurence.SelectionMode.TreasureTable;
        loot_pickpocket_table.treasureTableDefinition = pick_pocket_table;
        loot_pickpocket_table.diceNumber = 1;

        var loot_pickpocket_undead = new ItemOccurence(Zombie_loot_drop.ItemOccurencesList[0]);
        loot_pickpocket_undead.itemMode = ItemOccurence.SelectionMode.TreasureTable;
        loot_pickpocket_undead.treasureTableDefinition = pick_pocket_table_undead;
        loot_pickpocket_undead.diceNumber = 1;

        var pick_pocket_loot = LootPackDefinitionBuilder.CreateCopyFrom(Tutorial_04_Loot_Stealable,
                "PickPocketLoot", "30c308db-1ad7-4f93-9431-43ce32358493")
            .SetGuiPresentationNoContent()
            .AddToDB();

        pick_pocket_loot.lootChallengeMode = LootPackDefinition.LootChallenge.ByPartyLevel;
        pick_pocket_loot.ItemOccurencesList.Clear();
        pick_pocket_loot.ItemOccurencesList.Add(loot_pickpocket_table);

        var pick_pocket_undead = LootPackDefinitionBuilder.CreateCopyFrom(Tutorial_04_Loot_Stealable,
                "PickPocketUndead", "af2eb8e0-6a5a-40e2-8a62-160f80e2453e")
            .SetGuiPresentationNoContent()
            .AddToDB();

        pick_pocket_undead.lootChallengeMode = LootPackDefinition.LootChallenge.ByPartyLevel;
        pick_pocket_undead.ItemOccurencesList.Clear();
        pick_pocket_undead.ItemOccurencesList.Add(loot_pickpocket_undead);

        foreach (var monster in DatabaseRepository.GetDatabase<MonsterDefinition>())
        {
            if (monster.CharacterFamily == "Humanoid" &&
                monster.DefaultFaction == "HostileMonsters")
            {
                monster.SetStealableLootDefinition(pick_pocket_loot);
            }
        }

        var adam_12 = DatabaseHelper.MonsterDefinitions.Adam_The_Twelth;
        adam_12.SetStealableLootDefinition(pick_pocket_loot);

        var brood_of_blood = DatabaseHelper.MonsterDefinitions.Brood_of_blood;
        brood_of_blood.SetStealableLootDefinition(pick_pocket_loot);

        var brood_of_dread = DatabaseHelper.MonsterDefinitions.Brood_of_dread;
        brood_of_dread.SetStealableLootDefinition(pick_pocket_loot);

        var brood_of_flesh = DatabaseHelper.MonsterDefinitions.Brood_of_flesh;
        brood_of_flesh.SetStealableLootDefinition(pick_pocket_loot);

        var skeleton_basic = DatabaseHelper.MonsterDefinitions.Skeleton;
        skeleton_basic.SetStealableLootDefinition(pick_pocket_undead);

        var skeleton_archer = DatabaseHelper.MonsterDefinitions.Skeleton_Archer;
        skeleton_archer.SetStealableLootDefinition(pick_pocket_undead);

        var skeleton_enforcer = DatabaseHelper.MonsterDefinitions.Skeleton_Enforcer;
        skeleton_enforcer.SetStealableLootDefinition(pick_pocket_undead);

        var skeleton_knight = DatabaseHelper.MonsterDefinitions.Skeleton_Knight;
        skeleton_knight.SetStealableLootDefinition(pick_pocket_undead);

        var skeleton_marksman = DatabaseHelper.MonsterDefinitions.Skeleton_Marksman;
        skeleton_marksman.SetStealableLootDefinition(pick_pocket_undead);

        var skeleton_sorcerer = DatabaseHelper.MonsterDefinitions.Skeleton_Sorcerer;
        skeleton_sorcerer.SetStealableLootDefinition(pick_pocket_undead);
    }
}
