namespace WsStorageCore.Common;

public enum WsSqlEnumTableName
{
    None,
    All,
    Areas,// вместо ProductionFacilities
    Boxes,
    Bundles,
    Clips,
    Lines,// вместо Scales
    PluClipsFks,
    PluFks,
    PlusNestingFks,
    Plus,
    ViewPlusLines,// вместо PlusScales
    ViewPlusNesting,// вместо PluNestingFks
    ViewPlusStorageMethods,// вместо PluStorageMethodsFks
    WorkShops,
}