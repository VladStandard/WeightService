// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Common;

public enum WsSqlEnumTableName
{
    None,
    All,
    Areas,                  // גלוסעמ ProductionFacilities
    Boxes,
    Brands,
    Bundles,
    Clips,
    DeviceSettings,
    DeviceSettingsFks,
    Lines,                  // גלוסעמ Scales
    PluBrandsFks,
    PluBundlesFks,
    PluCharacteristics,
    PluCharacteristicsFks,
    PluClipsFks,
    PluFks,
    PluGroups,
    PluGroupsFks,
    PlusNestingFks,
    Plus,
    Plus1CFks,
    ViewPlusLines,          // גלוסעמ PlusScales
    ViewPlusNesting,        // גלוסעמ PluNestingFks
    ViewPlusStorageMethods, // גלוסעמ PluStorageMethodsFks
    WorkShops,
}