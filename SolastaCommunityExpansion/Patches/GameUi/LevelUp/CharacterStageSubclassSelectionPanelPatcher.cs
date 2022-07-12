﻿using System;
using System.Diagnostics.CodeAnalysis;
using HarmonyLib;

namespace SolastaCommunityExpansion.Patches.GameUi.LevelUp;

[HarmonyPatch(typeof(CharacterStageSubclassSelectionPanel), "Compare")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
internal static class CharacterStageSubclassSelectionPanel_Compare
{
    internal static void Postfix(CharacterSubclassDefinition left, CharacterSubclassDefinition right,
        ref int __result)
    {
        if (!Main.Settings.EnableSortingSubclasses)
        {
            return;
        }

        __result = String.Compare(left.FormatTitle(), right.FormatTitle(), StringComparison.CurrentCultureIgnoreCase);
    }
}
