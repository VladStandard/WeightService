namespace Ws.StorageCore.Views.ViewRefModels.PluNestings;

[DebuggerDisplay("{ToString()}")]
public sealed class SqlViewPluNestingModel : SqlViewBase
{
    #region Public and private fields, properties, constructor

    public bool IsMarked { get; init; }
    public bool IsDefault { get; init; }
    public short BundleCount { get; init; }
    public Guid PluUid { get; init; }
    public Guid PluUid1C { get; init; }
    public bool PluIsMarked { get; init; }
    public bool PluIsWeight { get; init; }
    public bool PluIsGroup { get; init; }
    public ushort PluNumber { get; init; }
    public string PluName { get; init; }
    public short PluShelfLifeDays { get; init; }
    public string PluGtin { get; init; }
    public string PluEan13 { get; init; }
    public string PluItf14 { get; init; }
    public Guid BundleUid { get; init; }
    public Guid BundleUid1C { get; init; }
    public bool BundleIsMarked { get; init; }
    public string BundleName { get; init; }
    public decimal BundleWeight { get; init; }
    public Guid BoxUid { get; init; }
    public Guid BoxUid1C { get; init; }
    public bool BoxIsMarked { get; init; }
    public string BoxName { get; init; }
    public decimal BoxWeight { get; init; }
    public decimal TareWeight { get; init; }
    public string TareWeightWithKg => $"{TareWeight} {LocaleCore.LabelPrint.WeightUnitKg}";
    public string TareWeightDescription =>
        $"{(string.IsNullOrEmpty(BoxName) ? LocaleCore.WebService.BoxZero : BoxName)} + " +
        $"({(string.IsNullOrEmpty(BundleName) ? LocaleCore.WebService.BundleZero : BundleName)} * {BundleCount})" +
        $"{(IsDefault ? " + (По умолчанию)" : string.Empty)}";
    
    public string TareWeightValue => $"{BoxWeight} + ({BundleWeight} * {BundleCount})";
    public string PluNumberName => $"{PluNumber} | {PluName}";
    public DateTime WebServiceChange { get; init; }
    public bool WebServiceIsEnabled { get; init; }
    public string WebServiceXml { get; init; }

    public SqlViewPluNestingModel() : this(Guid.Empty, default, default, default,
        Guid.Empty, Guid.Empty, default, default, default, default, string.Empty,
        default, string.Empty, string.Empty, string.Empty,
        Guid.Empty, Guid.Empty, default, string.Empty, default,
        Guid.Empty, Guid.Empty, default, string.Empty, default, default, 
        DateTime.MinValue, false, string.Empty)
    { }

    public SqlViewPluNestingModel(Guid uid, bool isMarked, bool isDefault, short bundleCount,
        Guid pluUid, Guid pluUid1C, bool pluIsMarked, bool pluIsWeight, bool pluIsGroup, ushort pluNumber, string pluName,
        short pluShelfLifeDays, string pluGtin, string pluEan13, string pluItf14,
        Guid bundleUid, Guid bundleUid1C, bool bundleIsMarked, string bundleName, decimal bundleWeight,
        Guid boxUid, Guid boxUid1C, bool boxIsMarked, string boxName, decimal boxWeight, decimal tareWeight,
        DateTime webServiceChange, bool webServiceIsEnabled, string webServiceXml) : base(uid)
    {
        IsMarked = isMarked;
        IsDefault = isDefault;
        BundleCount = bundleCount;
        PluUid = pluUid;
        PluUid1C = pluUid1C;
        PluIsMarked = pluIsMarked;
        PluIsWeight = pluIsWeight;
        PluIsGroup = pluIsGroup;
        PluNumber = pluNumber;
        PluName = pluName;
        PluShelfLifeDays = pluShelfLifeDays;
        PluGtin = pluGtin;
        PluEan13 = pluEan13;
        PluItf14 = pluItf14;
        BundleUid = bundleUid;
        BundleUid1C = bundleUid1C;
        BundleIsMarked = bundleIsMarked;
        BundleName = bundleName;
        BundleWeight = bundleWeight;
        BoxUid = boxUid;
        BoxUid1C = boxUid1C;
        BoxIsMarked = boxIsMarked;
        BoxName = boxName;
        BoxWeight = boxWeight;
        TareWeight = tareWeight;
        WebServiceChange = webServiceChange;
        WebServiceIsEnabled = webServiceIsEnabled;
        WebServiceXml = webServiceXml;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{TareWeightDescription} | {TareWeight}";
    
    public string GetSmartName() => TareWeight > 0 ? $"{TareWeight} {LocaleCore.LabelPrint.WeightUnitKg} | {PluName}" : "- 0 -";
    
    #endregion
}