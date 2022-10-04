﻿using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using SolastaUnfinishedBusiness.Subclasses;

namespace SolastaUnfinishedBusiness.Models;

internal static class SubclassesContext
{
    private static Dictionary<CharacterSubclassDefinition, FeatureDefinitionSubclassChoice> SubclassesChoiceList
    {
        get;
    } = new();

    internal static HashSet<CharacterSubclassDefinition> Subclasses { get; private set; } = new();

    private static void SortSubclassesFeatures()
    {
        var dbCharacterSubclassDefinition = DatabaseRepository.GetDatabase<CharacterSubclassDefinition>();

        foreach (var characterSubclassDefinition in dbCharacterSubclassDefinition)
        {
            characterSubclassDefinition.FeatureUnlocks.Sort((a, b) =>
            {
                var result = a.Level - b.Level;

                if (result == 0)
                {
                    result = String.Compare(a.FeatureDefinition.FormatTitle(), b.FeatureDefinition.FormatTitle(),
                        StringComparison.CurrentCultureIgnoreCase);
                }

                return result;
            });
        }
    }

    internal static void Load()
    {
        LoadSubclass(new CircleOfTheForestGuardian());
        LoadSubclass(new MartialMarshal());
        LoadSubclass(new MartialSpellShield());
        LoadSubclass(new MartialTactician());
        LoadSubclass(new PathOfTheLight());
        LoadSubclass(new PathOfTheRageMage());
        LoadSubclass(new PatronAncientForest());
        LoadSubclass(new PatronElementalist());
        LoadSubclass(new PatronMoonlit());
        LoadSubclass(new PatronRiftWalker());
        LoadSubclass(new PatronSoulBlade());
        LoadSubclass(new RangerArcanist());
        LoadSubclass(new RoguishConArtist());
        LoadSubclass(new RoguishOpportunist());
        LoadSubclass(new RoguishRaven());
        LoadSubclass(new SorcerousDivineHeart());
        LoadSubclass(new WayOfTheDistantHand());
        LoadSubclass(new WizardArcaneFighter());
        LoadSubclass(new WizardBladeDancer());
        LoadSubclass(new WizardDeadMaster());
        LoadSubclass(new WizardLifeTransmuter());
        LoadSubclass(new WizardMasterManipulator());
        LoadSubclass(new WizardSpellMaster());

        // sorting
        Subclasses = Subclasses.OrderBy(x =>
        {
            var className = LevelUpContext.GetClassForSubclass(x).Name;
            
            return className + x.FormatTitle();
        }).ToHashSet();

        // settings paring
        foreach (var featName in Main.Settings.SubclassEnabled.ToList()
                     .Where(featName => Subclasses.All(x => x.Name != featName)))
        {
            Main.Settings.SubclassEnabled.Remove(featName);
        }

        if (Main.Settings.EnableSortingFutureFeatures)
        {
            SortSubclassesFeatures();
        }
    }

    private static void LoadSubclass([NotNull] AbstractSubclass subclassBuilder)
    {
        var subclass = subclassBuilder.Subclass;

        if (!Subclasses.Contains(subclass))
        {
            SubclassesChoiceList.Add(subclass, subclassBuilder.SubclassChoice);
            Subclasses.Add(subclass);
        }

        UpdateSubclassVisibility(subclass);
    }

    private static void UpdateSubclassVisibility([NotNull] CharacterSubclassDefinition characterSubclassDefinition)
    {
        var name = characterSubclassDefinition.Name;
        var choiceList = SubclassesChoiceList[characterSubclassDefinition];

        if (Main.Settings.SubclassEnabled.Contains(name))
        {
            choiceList.Subclasses.TryAdd(name);
        }
        else
        {
            choiceList.Subclasses.Remove(name);
        }
    }

    internal static void Switch(CharacterSubclassDefinition characterSubclassDefinition, bool active)
    {
        if (!Subclasses.Contains(characterSubclassDefinition))
        {
            return;
        }

        var name = characterSubclassDefinition.Name;

        if (active)
        {
            Main.Settings.SubclassEnabled.TryAdd(name);
        }
        else
        {
            Main.Settings.SubclassEnabled.Remove(name);
        }

        UpdateSubclassVisibility(characterSubclassDefinition);
    }
}
