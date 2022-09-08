﻿using SolastaCommunityExpansion.Builders.Features;
using static SolastaCommunityExpansion.Api.DatabaseHelper.FeatureDefinitionSenses;

namespace SolastaCommunityExpansion.Level20;

internal sealed class
    RangerFeralSensesBuilder : FeatureDefinitionBuilder<FeatureDefinitionSense, RangerFeralSensesBuilder>
{
    private const string RangerFeralSensesName = "ZSRangerFeralSenses";
    private const string RangerFeralSensesGuid = "0e3207505ac04a499477ca1185287117";

    internal static readonly FeatureDefinitionSense RangerFeralSenses =
        CreateAndAddToDB(RangerFeralSensesName, RangerFeralSensesGuid);

    private RangerFeralSensesBuilder(string name, string guid) : base(SenseSeeInvisible12, name, guid)
    {
        Definition.senseRange = 6;
        Definition.GuiPresentation.Title = "Feature/&RangerFeralSensesTitle";
        Definition.GuiPresentation.Description = "Feature/&RangerFeralSensesDescription";
    }

    private static FeatureDefinitionSense CreateAndAddToDB(string name, string guid)
    {
        return new RangerFeralSensesBuilder(name, guid).AddToDB();
    }
}
