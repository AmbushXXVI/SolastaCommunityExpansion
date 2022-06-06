﻿using SolastaModApi.Extensions;
using UnityEngine.AddressableAssets;
using static EquipmentDefinitions;

namespace SolastaCommunityExpansion.Builders;

public class UsableDeviceDescriptionBuilder
{
    private readonly UsableDeviceDescription description;

    public UsableDeviceDescriptionBuilder()
    {
        description = new UsableDeviceDescription();

        description.usage = ItemUsage.ByFunction;

        description.chargesCapital = ItemChargesCapital.Fixed;
        description.chargesCapitalNumber = 1;
        description.chargesCapitalDie = RuleDefinitions.DieType.D1;
        description.chargesCapitalBonus = 0;

        description.SetRechargeRate(RuleDefinitions.RechargeRate.Dawn);
        description.SetRechargeNumber(1);
        description.SetRechargeDie(RuleDefinitions.DieType.D1);
        description.SetRechargeBonus(0);

        description.SetOutOfChargesConsequence(ItemOutOfCharges.Persist);
        description.SetMagicAttackBonus(0);
        description.SetSaveDC(10);
        description.SetOnUseParticle(new AssetReference());
    }

    public UsableDeviceDescriptionBuilder SetUsage(ItemUsage usage)
    {
        description.SetUsage(usage);
        return this;
    }

    public UsableDeviceDescriptionBuilder SetOutOfChargesConsequence(ItemOutOfCharges consequence)
    {
        description.SetOutOfChargesConsequence(consequence);
        return this;
    }

    public UsableDeviceDescriptionBuilder SetMagicAttackBonus(int bonus)
    {
        description.SetMagicAttackBonus(bonus);
        return this;
    }

    public UsableDeviceDescriptionBuilder SetSaveDC(int DC)
    {
        description.SetSaveDC(DC);
        return this;
    }

    public UsableDeviceDescriptionBuilder SetOnUseParticle(AssetReference asset)
    {
        description.SetOnUseParticle(asset);
        return this;
    }

    public UsableDeviceDescriptionBuilder AddFunctions(params DeviceFunctionDescription[] functions)
    {
        description.DeviceFunctions.AddRange(functions);
        return this;
    }

    public UsableDeviceDescription Build()
    {
        return description;
    }

    #region Charge

    public UsableDeviceDescriptionBuilder SetCharges(
        ItemChargesCapital capital = ItemChargesCapital.Fixed,
        int number = 1,
        RuleDefinitions.DieType dieType = RuleDefinitions.DieType.D1,
        int bonus = 0)
    {
        description.SetChargesCapital(capital);
        description.SetChargesCapitalNumber(number);
        description.SetChargesCapitalDie(dieType);
        description.SetChargesCapitalBonus(bonus);
        return this;
    }

    public UsableDeviceDescriptionBuilder SetChargesCapital(ItemChargesCapital capital)
    {
        description.SetChargesCapital(capital);
        return this;
    }

    public UsableDeviceDescriptionBuilder SetChargesCapitalNumber(int number)
    {
        description.SetChargesCapitalNumber(number);
        return this;
    }

    public UsableDeviceDescriptionBuilder SetChargesCapitalDie(RuleDefinitions.DieType dieType)
    {
        description.SetChargesCapitalDie(dieType);
        return this;
    }

    public UsableDeviceDescriptionBuilder SetChargesCapitalBonus(int bonus)
    {
        description.SetChargesCapitalBonus(bonus);
        return this;
    }

    #endregion

    #region Recharge

    public UsableDeviceDescriptionBuilder SetRecharge(
        RuleDefinitions.RechargeRate rate = RuleDefinitions.RechargeRate.Dawn,
        int number = 1,
        RuleDefinitions.DieType dieType = RuleDefinitions.DieType.D1,
        int bonus = 0)
    {
        description.SetRechargeRate(rate);
        description.SetRechargeNumber(number);
        description.SetRechargeDie(dieType);
        description.SetRechargeBonus(bonus);
        return this;
    }

    public UsableDeviceDescriptionBuilder SetRechargeRate(RuleDefinitions.RechargeRate rate)
    {
        description.SetRechargeRate(rate);
        return this;
    }

    public UsableDeviceDescriptionBuilder SetRechargeNumber(int number)
    {
        description.SetRechargeNumber(number);
        return this;
    }

    public UsableDeviceDescriptionBuilder SetRechargeDie(RuleDefinitions.DieType dieType)
    {
        description.SetRechargeDie(dieType);
        return this;
    }

    public UsableDeviceDescriptionBuilder SetRechargeBonus(int bonus)
    {
        description.SetRechargeBonus(bonus);
        return this;
    }

    #endregion
}
