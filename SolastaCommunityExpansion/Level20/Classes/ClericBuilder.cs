﻿using System.Collections.Generic;
using SolastaCommunityExpansion.Api.Infrastructure;
using static SolastaCommunityExpansion.Level20.Features.PowerClericDivineInterventionImprovementBuilder;
using static SolastaCommunityExpansion.Level20.Features.PowerClericTurnUndeadBuilder;
using static SolastaCommunityExpansion.Api.DatabaseHelper.CharacterClassDefinitions;
using static SolastaCommunityExpansion.Api.DatabaseHelper.CharacterSubclassDefinitions;
using static SolastaCommunityExpansion.Api.DatabaseHelper.FeatureDefinitionAttributeModifiers;
using static SolastaCommunityExpansion.Api.DatabaseHelper.FeatureDefinitionCastSpells;
using static SolastaCommunityExpansion.Api.DatabaseHelper.FeatureDefinitionFeatureSets;

namespace SolastaCommunityExpansion.Level20.Classes;

internal static class ClericBuilder
{
    internal static void Load()
    {
        Cleric.FeatureUnlocks.AddRange(new List<FeatureUnlockByLevel>
        {
            new(PowerClericTurnUndead14, 14),
            new(FeatureSetAbilityScoreChoice, 16),
            new(PowerClericTurnUndead17, 17),
            new(AttributeModifierClericChannelDivinityAdd, 18),
            new(FeatureSetAbilityScoreChoice, 19)
            // Solasta handles divine intervention on the subclasses, added below.
        });

        CastSpellCleric.spellCastingLevel = 9;

        CastSpellCleric.SlotsPerLevels.SetRange(SpellsHelper.FullCastingSlots);
        CastSpellCleric.ReplacedSpells.SetRange(SpellsHelper.EmptyReplacedSpells);

        DomainBattle.FeatureUnlocks.Add(new FeatureUnlockByLevel(PowerClericDivineInterventionImprovementPaladin,
            20));
        DomainElementalCold.FeatureUnlocks.Add(
            new FeatureUnlockByLevel(PowerClericDivineInterventionImprovementWizard, 20));
        DomainElementalFire.FeatureUnlocks.Add(
            new FeatureUnlockByLevel(PowerClericDivineInterventionImprovementWizard, 20));
        DomainElementalLighting.FeatureUnlocks.Add(
            new FeatureUnlockByLevel(PowerClericDivineInterventionImprovementWizard, 20));
        DomainInsight.FeatureUnlocks.Add(new FeatureUnlockByLevel(PowerClericDivineInterventionImprovementCleric,
            20));
        DomainLaw.FeatureUnlocks.Add(new FeatureUnlockByLevel(PowerClericDivineInterventionImprovementPaladin, 20));
        DomainLife.FeatureUnlocks.Add(new FeatureUnlockByLevel(PowerClericDivineInterventionImprovementCleric, 20));
        DomainOblivion.FeatureUnlocks.Add(new FeatureUnlockByLevel(PowerClericDivineInterventionImprovementCleric,
            20));
        DomainSun.FeatureUnlocks.Add(new FeatureUnlockByLevel(PowerClericDivineInterventionImprovementWizard, 20));
    }
}
