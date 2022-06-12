﻿using System.Collections.Generic;
using static SolastaCommunityExpansion.Level20.Features.AttributeModifierFighterIndomitableBuilder;
using static SolastaCommunityExpansion.Level20.Features.PowerFighterActionSurge2Builder;
using static SolastaCommunityExpansion.Api.DatabaseHelper.CharacterClassDefinitions;
using static SolastaCommunityExpansion.Api.DatabaseHelper.FeatureDefinitionAttributeModifiers;
using static SolastaCommunityExpansion.Api.DatabaseHelper.FeatureDefinitionFeatureSets;

namespace SolastaCommunityExpansion.Level20.Classes;

internal static class FighterBuilder
{
    internal static void Load()
    {
        Fighter.FeatureUnlocks.AddRange(new List<FeatureUnlockByLevel>
        {
            new(AttributeModifierFighterIndomitableAdd, 13),
            new(FeatureSetAbilityScoreChoice, 14),
            new(FeatureSetAbilityScoreChoice, 16),
            new(PowerFighterActionSurge2, 17),
            new(AttributeModifierFighterIndomitableAdd, 17),
            new(FeatureSetAbilityScoreChoice, 19),
            new(AttributeModifierFighterExtraAttack, 20)
        });
    }
}
