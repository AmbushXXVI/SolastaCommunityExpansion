﻿// using System.Diagnostics.CodeAnalysis;
// using HarmonyLib;
// using SolastaCommunityExpansion.Models;
//
// namespace SolastaCommunityExpansion.Patches.CustomFeatures.PactMagic;
//
// // creates different slots colors and pop up messages depending on slot types
// [HarmonyPatch(typeof(CharacterReactionSubitem), "Bind")]
// [SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
// internal static class CharacterReactionSubitem_Bind
// {
//     public static void Postfix(
//         CharacterReactionSubitem __instance,
//         RulesetSpellRepertoire spellRepertoire,
//         int slotLevel)
//     {
//         var heroWithSpellRepertoire = SharedSpellsContext.GetHero(spellRepertoire.CharacterName);
//
//         if (heroWithSpellRepertoire is null)
//         {
//             return;
//         }
//
//         spellRepertoire.GetSlotsNumber(slotLevel, out var totalSlotsRemainingCount, out var totalSlotsCount);
//
//         MulticlassGameUiContext.PaintPactSlots(
//             heroWithSpellRepertoire, totalSlotsCount, totalSlotsRemainingCount, slotLevel,
//             __instance.slotStatusTable);
//     }
// }
//
// // ensures slot colors are white before getting back to pool
// [HarmonyPatch(typeof(CharacterReactionSubitem), "Unbind")]
// [SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
// internal static class CharacterReactionSubitem_Unbind
// {
//     public static void Prefix(CharacterReactionSubitem __instance)
//     {
//         MulticlassGameUiContext.PaintSlotsWhite(__instance.slotStatusTable);
//     }
// }
