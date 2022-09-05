﻿using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using SolastaCommunityExpansion.Models;

namespace SolastaCommunityExpansion.Patches;

// this patch prevents game from receive input if Mod UI is open / handles all hotkeys defined in the mod
[HarmonyPatch(typeof(GameLocationBaseScreen), "HandleInput")]
[SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Patch")]
internal static class GameLocationBaseScreen_HandleInput
{
    internal static bool Prefix(GameLocationBaseScreen __instance, InputCommands.Id command, ref bool __result)
    {
        if (ModManagerUI.IsOpen)
        {
            __result = true;

            return false;
        }

        GameUiContext.HandleInput(__instance, command);

        return true;
    }
}
