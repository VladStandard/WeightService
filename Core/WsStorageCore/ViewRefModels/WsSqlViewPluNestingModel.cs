// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.ViewRefModels;

[DebuggerDisplay("{ToString()}")]
public sealed record WsSqlViewPluNestingModel
{
    #region Public and private fields, properties, constructor

    public Guid Uid { get; init; }
    public bool IsMarked { get; init; }
    public bool IsDefault { get; init; }
    public short BundleCount { get; init; }
    public decimal WeightMax { get; init; }
    public decimal WeightMin { get; init; }
    public decimal WeightNom { get; init; }
    public Guid PluUid { get; init; }
    public bool PluIsMarked { get; init; }
    public bool PluIsWeight { get; init; }
    public ushort PluNumber { get; init; }
    public string PluName { get; init; }
    public short PluShelfLifeDays { get; init; }
    public string PluGtin { get; init; }
    public string PluEan13 { get; init; }
    public string PluItf14 { get; init; }
    public Guid BundleUid { get; init; }
    public bool BundleIsMarked { get; init; }
    public string BundleName { get; init; }
    public decimal BundleWeight { get; init; }
    public Guid BoxUid { get; init; }
    public bool BoxIsMarked { get; init; }
    public string BoxName { get; init; }
    public decimal BoxWeight { get; init; }
    public decimal TareWeight { get; init; }

    /// <summary>
    /// Empty constructor.
    /// </summary>
    public WsSqlViewPluNestingModel() : this(Guid.Empty, default, default, default,
        default, default, default,
        Guid.Empty, default, default, default, string.Empty,
        default, string.Empty, string.Empty, string.Empty,
        Guid.Empty, default, string.Empty, default,
        Guid.Empty, default, string.Empty, default, default) { }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="uid"></param>
    /// <param name="isMarked"></param>
    /// <param name="isDefault"></param>
    /// <param name="bundleCount"></param>
    /// <param name="weightMax"></param>
    /// <param name="weightMin"></param>
    /// <param name="weightNom"></param>
    /// <param name="pluUid"></param>
    /// <param name="pluIsMarked"></param>
    /// <param name="pluIsWeight"></param>
    /// <param name="pluNumber"></param>
    /// <param name="pluName"></param>
    /// <param name="pluShelfLifeDays"></param>
    /// <param name="pluGtin"></param>
    /// <param name="pluEan13"></param>
    /// <param name="pluItf14"></param>
    /// <param name="bundleUid"></param>
    /// <param name="bundleIsMarked"></param>
    /// <param name="bundleName"></param>
    /// <param name="bundleWeight"></param>
    /// <param name="boxUid"></param>
    /// <param name="boxIsMarked"></param>
    /// <param name="boxName"></param>
    /// <param name="boxWeight"></param>
    /// <param name="tareWeight"></param>
    public WsSqlViewPluNestingModel(Guid uid, bool isMarked, bool isDefault, short bundleCount,
        decimal weightMax, decimal weightMin, decimal weightNom,
        Guid pluUid, bool pluIsMarked, bool pluIsWeight, ushort pluNumber, string pluName,
        short pluShelfLifeDays, string pluGtin, string pluEan13, string pluItf14,
        Guid bundleUid, bool bundleIsMarked, string bundleName, decimal bundleWeight,
        Guid boxUid, bool boxIsMarked, string boxName, decimal boxWeight, decimal tareWeight)
    {
        Uid = uid;
        IsMarked = isMarked;
        IsDefault = isDefault;
        BundleCount = bundleCount;
        WeightMax = weightMax;
        WeightMin = weightMin;
        WeightNom = weightNom;
        PluUid = pluUid;
        PluIsMarked = pluIsMarked;
        PluIsWeight = pluIsWeight;
        PluNumber = pluNumber;
        PluName = pluName;
        PluShelfLifeDays = pluShelfLifeDays;
        PluGtin = pluGtin;
        PluEan13 = pluEan13;
        PluItf14 = pluItf14;
        BundleUid = bundleUid;
        BundleIsMarked = bundleIsMarked;
        BundleName = bundleName;
        BundleWeight = bundleWeight;
        BoxUid = boxUid;
        BoxIsMarked = boxIsMarked;
        BoxName = boxName;
        BoxWeight = boxWeight;
        TareWeight = tareWeight;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{TareWeight} | {BoxWeight} + {BundleCount} * {BundleWeight} | {BoxName} + {BundleName} * {BundleWeight}";

    #endregion
}