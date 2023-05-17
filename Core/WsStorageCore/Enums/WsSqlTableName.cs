// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Enums;

public enum WsSqlTableName
{
    None,
    All,
    Boxes,
    Brands,
    Bundles,
    Clips,
    PluBrandsFks,
    PluBundlesFks,
    PluCharacteristics,
    PluCharacteristicsFks,
    PluClipsFks,
    PluFks,
    PluGroups,
    PluGroupsFks,
    Plus,
    Plus1CFks,
    Scales,
    ProductionFacilities,
    ViewPlusScales, // вместо PlusScales
    ViewPluStorageMethods, // вместо PluStorageMethodsFks
    ViewPluNesting, // вместо PluNestingFks
}