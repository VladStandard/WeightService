namespace WsStorageCore.Common;

public enum WsSqlEnumTableName
{
    None,
    All,
    Areas,                  // вместо ProductionFacilities
    Boxes,
    Bundles,
    Clips,
    //DeviceSettings,
    //DeviceSettingsFks,
    Lines,                  // вместо Scales
    PluBrandsFks,
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