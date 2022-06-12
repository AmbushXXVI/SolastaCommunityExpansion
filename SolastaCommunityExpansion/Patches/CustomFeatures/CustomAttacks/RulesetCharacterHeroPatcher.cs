﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using SolastaCommunityExpansion.Api.Extensions;
using SolastaCommunityExpansion.CustomInterfaces;

namespace SolastaCommunityExpansion.Patches.CustomFeatures.CustomAttacks;

// Allows changing what attribute is used for weapon's attack and damage rolls
[HarmonyPatch(typeof(RulesetCharacterHero), "ComputeAttackModeAbilityScoreReplacement")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
internal static class RulesetCharacterHero_ComputeAttackModeAbilityScoreReplacement
{
    internal static void Prefix(RulesetCharacterHero __instance, RulesetAttackMode attackMode, RulesetItem weapon)
    {
        var attributeModifiers = __instance.GetSubFeaturesByType<IModifyAttackAttributeForWeapon>();

        foreach (var modifier in attributeModifiers)
        {
            modifier.ModifyAttribute(__instance, attackMode, weapon);
        }
    }
}

// Allows changing damage and other stats of an attack mode
[HarmonyPatch(typeof(RulesetCharacterHero), "RefreshAttackMode")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
internal static class RulesetCharacterHero_RefreshAttackMode
{
    internal static void Postfix(RulesetCharacterHero __instance,
        ref RulesetAttackMode __result,
        ActionDefinitions.ActionType actionType,
        ItemDefinition itemDefinition,
        WeaponDescription weaponDescription,
        bool freeOffHand,
        bool canAddAbilityDamageBonus,
        string slotName,
        List<IAttackModificationProvider> attackModifiers,
        Dictionary<FeatureDefinition, RuleDefinitions.FeatureOrigin> featuresOrigin,
        RulesetItem weapon = null)
    {
        var attributeModifiers = __instance.GetSubFeaturesByType<IModifyAttackModeForWeapon>();

        foreach (var modifier in attributeModifiers)
        {
            modifier.ModifyAttackMode(__instance, __result, weapon);
        }
    }
}

// Allows adding extra attack modes
[HarmonyPatch(typeof(RulesetCharacterHero), "RefreshAttackModes")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
internal static class RulesetCharacterHero_RefreshAttackModes
{
    private static bool _callRefresh;

    internal static void Prefix(RulesetCharacterHero __instance, ref bool callRefresh)
    {
        _callRefresh = callRefresh;
        callRefresh = false;
    }

    internal static void Postfix(RulesetCharacterHero __instance, bool callRefresh = false)
    {
        var providers = __instance.GetSubFeaturesByType<IAddExtraAttack>();

        foreach (var provider in providers)
        {
            provider.TryAddExtraAttack(__instance);
        }

        if (!_callRefresh || __instance.CharacterRefreshed == null)
        {
            return;
        }

        __instance.CharacterRefreshed(__instance);
    }
}

// Support for `IHeroRefreshedListener`
[HarmonyPatch(typeof(RulesetCharacterHero), "RefreshAll")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
internal static class RulesetCharacterHero_RefreshAll
{
    internal static void Prefix(RulesetCharacterHero __instance)
    {
        var listeners = __instance.GetSubFeaturesByType<IHeroRefreshedListener>();
        if (listeners == null)
        {
            return;
        }

        foreach (var listener in listeners)
        {
            listener.OnHeroRefreshed(__instance);
        }
    }
}

// Make crossbows and hand crossbows benefit from anything that grants benefits on using bows
[HarmonyPatch(typeof(RulesetCharacterHero), "IsWieldingBow")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
internal static class RulesetCharacterHero_IsWieldingBow
{
    internal static bool Prefix(RulesetCharacterHero __instance, ref bool __result)
    {
        if (!Main.Settings.AllowCrossbowsToUseBowFeatures)
        {
            return true;
        }

        var equipedItem = __instance.characterInventory.InventorySlotsByName[EquipmentDefinitions.SlotTypeMainHand]
            .EquipedItem;

        // TODO: might be better to keep the original code and leverage tags here. for now this works
        __result = equipedItem != null
                   && equipedItem.ItemDefinition.IsWeapon && DatabaseRepository
                       .GetDatabase<WeaponTypeDefinition>().Name.ToLower().Contains("bow");

        return false;
    }
}
