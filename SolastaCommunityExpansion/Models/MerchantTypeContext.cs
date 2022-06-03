﻿using System.Collections.Generic;
using System.Linq;
using static SolastaModApi.DatabaseHelper.ItemFlagDefinitions;

namespace SolastaCommunityExpansion.Models;

public class MerchantTypeContext
{
    internal static List<(MerchantDefinition, MerchantType)> MerchantTypes = new();

    private static readonly string[] RangedWeaponTypes = new[]
    {
        "LightCrossbowType", "HeavyCrossbowType", "ShortbowType", "LongbowType", "DartType"
    };

    internal static void Load()
    {
        var dbMerchantDefinition = DatabaseRepository.GetDatabase<MerchantDefinition>();

        foreach (var merchant in dbMerchantDefinition)
        {
            MerchantTypes.Add((merchant, GetMerchantType(merchant)));
        }
    }

    public static MerchantType GetMerchantType(MerchantDefinition merchant)
    {
        var isDocumentMerchant = merchant.StockUnitDescriptions
            .Any(x =>
                x.ItemDefinition.IsDocument);

        var isAmmunitionMerchant = merchant.StockUnitDescriptions
            .Any(x =>
                x.ItemDefinition.IsAmmunition
                && !x.ItemDefinition.Magical);

        var isArmorMerchant = merchant.StockUnitDescriptions
            .Any(x =>
                x.ItemDefinition.IsArmor
                && !x.ItemDefinition.Magical);

        var isMeleeWeaponMerchant = merchant.StockUnitDescriptions
            .Any(x =>
                x.ItemDefinition.IsWeapon
                && !RangedWeaponTypes.Contains(x.ItemDefinition.WeaponDescription.WeaponType)
                && !x.ItemDefinition.Magical);

        var isRangeWeaponMerchant = merchant.StockUnitDescriptions
            .Any(x =>
                x.ItemDefinition.IsWeapon
                && RangedWeaponTypes.Contains(x.ItemDefinition.WeaponDescription.WeaponType)
                && !x.ItemDefinition.Magical);

        var isMagicalAmmunitionMerchant = merchant.StockUnitDescriptions
            .Any(x =>
                x.ItemDefinition.IsAmmunition
                && x.ItemDefinition.Magical);

        var isMagicalArmorMerchant = merchant.StockUnitDescriptions
            .Any(x =>
                x.ItemDefinition.IsArmor
                && x.ItemDefinition.Magical);

        var isMagicalMeleeWeaponMerchant = merchant.StockUnitDescriptions
            .Any(x =>
                x.ItemDefinition.IsWeapon
                && !RangedWeaponTypes.Contains(x.ItemDefinition.WeaponDescription.WeaponType)
                && x.ItemDefinition.Magical);

        var isMagicalRangeWeaponMerchant = merchant.StockUnitDescriptions
            .Any(x =>
                x.ItemDefinition.IsWeapon
                && RangedWeaponTypes.Contains(x.ItemDefinition.WeaponDescription.WeaponType)
                && x.ItemDefinition.Magical);

        var isPrimedArmorMerchant = merchant.StockUnitDescriptions
            .Any(x =>
                x.ItemDefinition.IsArmor
                && x.ItemDefinition.ItemPresentation.ItemFlags.Contains(ItemFlagPrimed));

        var isPrimedMeleeWeaponMerchant = merchant.StockUnitDescriptions
            .Any(x =>
                x.ItemDefinition.IsWeapon
                && !RangedWeaponTypes.Contains(x.ItemDefinition.WeaponDescription.WeaponType)
                && x.ItemDefinition.ItemPresentation.ItemFlags.Contains(ItemFlagPrimed));

        var isPrimedRangeWeaponMerchant = merchant.StockUnitDescriptions
            .Any(x =>
                x.ItemDefinition.IsWeapon
                && RangedWeaponTypes.Contains(x.ItemDefinition.WeaponDescription.WeaponType)
                && x.ItemDefinition.ItemPresentation.ItemFlags.Contains(ItemFlagPrimed));

        return new MerchantType
        {
            IsDocument = isDocumentMerchant,
            IsAmmunition = isAmmunitionMerchant,
            IsArmor = isArmorMerchant,
            IsMeleeWeapon = isMeleeWeaponMerchant,
            IsRangeWeapon = isRangeWeaponMerchant,
            IsMagicalAmmunition = isMagicalAmmunitionMerchant,
            IsMagicalArmor = isMagicalArmorMerchant,
            IsMagicalMeleeWeapon = isMagicalMeleeWeaponMerchant,
            IsMagicalRangeWeapon = isMagicalRangeWeaponMerchant,
            IsPrimedArmor = isPrimedArmorMerchant,
            IsPrimedMeleeWeapon = isPrimedMeleeWeaponMerchant,
            IsPrimedRangeWeapon = isPrimedRangeWeaponMerchant
        };
    }

    public class MerchantType
    {
        public bool IsAmmunition;
        public bool IsArmor;
        public bool IsDocument;

        public bool IsMagicalAmmunition;
        public bool IsMagicalArmor;
        public bool IsMagicalMeleeWeapon;
        public bool IsMagicalRangeWeapon;
        public bool IsMeleeWeapon;

        public bool IsPrimedArmor;
        public bool IsPrimedMeleeWeapon;
        public bool IsPrimedRangeWeapon;
        public bool IsRangeWeapon;
    }
}