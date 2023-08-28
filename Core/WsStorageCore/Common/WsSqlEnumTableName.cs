namespace WsStorageCore.Common;

public enum WsSqlEnumTableName
{
    None,
    All,
    Areas,                  // вместо ProductionFacilities
    Boxes,
    Brands,
    Bundles,
    Clips,
    //DeviceSettings,
    //DeviceSettingsFks,
    Lines,                  // вместо Scales
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
    ViewPlusLines,          // вместо PlusScales
    ViewPlusNesting,        // вместо PluNestingFks
    ViewPlusStorageMethods, // вместо PluStorageMethodsFks
    WorkShops,
}