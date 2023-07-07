// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

namespace WsStorageCore.TableScaleFkModels.PlusWeighingsFks;

/// <summary>
/// Table "PLUS_WEIGHINGS".
/// </summary>
[Serializable]
[DebuggerDisplay("{ToString()}")]
public class WsSqlPluWeighingModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual WsSqlPluScaleModel PluScale { get; set; }
    [XmlElement(IsNullable = true)] public virtual WsSqlProductSeriesModel? Series { get; set; }
    [XmlElement] public virtual short Kneading { get; set; }
    [XmlElement] public virtual string Sscc { get; set; }
    [XmlElement] public virtual decimal NettoWeight { get; set; }
    [XmlElement] public virtual decimal WeightTare { get; set; }
    [XmlElement] public virtual int RegNum { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluWeighingModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        PluScale = new();
        Series = null;
        Kneading = 0;
        Sscc = string.Empty;
        NettoWeight = 0;
        WeightTare = 0;
        RegNum = 0;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlPluWeighingModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        PluScale = (WsSqlPluScaleModel)info.GetValue(nameof(PluScale), typeof(WsSqlPluScaleModel));
        Series = (WsSqlProductSeriesModel?)info.GetValue(nameof(Series), typeof(WsSqlProductSeriesModel));
        Kneading = info.GetInt16(nameof(Kneading));
        Sscc = info.GetString(nameof(Sscc));
        NettoWeight = info.GetDecimal(nameof(NettoWeight));
        WeightTare = info.GetDecimal(nameof(WeightTare));
        RegNum = info.GetInt32(nameof(RegNum));
    }

    public WsSqlPluWeighingModel(WsSqlPluWeighingModel item) : base(item)
    {
        PluScale = new(item.PluScale);
        Series = item.Series is null ? null : new(item.Series);
        Kneading = item.Kneading;
        Sscc = item.Sscc;
        NettoWeight = item.NettoWeight;
        WeightTare = item.WeightTare;
        RegNum = item.RegNum;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Kneading)}: {Kneading}. " +
        $"{nameof(PluScale)}: {PluScale}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlPluWeighingModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Kneading, default(short)) &&
        Equals(Sscc, string.Empty) &&
        Equals(NettoWeight, default(decimal)) &&
        Equals(WeightTare, default(decimal)) &&
        Equals(RegNum, default(int)) &&
        PluScale.EqualsDefault() &&
        (Series is null || Series.EqualsDefault());

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(PluScale), PluScale);
        info.AddValue(nameof(Series), Series);
        info.AddValue(nameof(Kneading), Kneading);
        info.AddValue(nameof(Sscc), Sscc);
        info.AddValue(nameof(NettoWeight), NettoWeight);
        info.AddValue(nameof(WeightTare), WeightTare);
        info.AddValue(nameof(RegNum), RegNum);
    }

    public override void ClearNullProperties()
    {
        if (Series is not null && Series.Identity.EqualsDefault())
            Series = null;
    }

    public override void FillProperties()
    {
        base.FillProperties();
        Sscc = WsLocaleCore.Sql.SqlItemFieldSscc;
        NettoWeight = 1.1M;
        WeightTare = 0.25M;
        RegNum = 1;
        Kneading = 1;
        PluScale.FillProperties();
        Series?.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPluWeighingModel item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(Kneading, item.Kneading) &&
        Equals(PluScale, item.PluScale) &&
        Equals(Sscc, item.Sscc) &&
        Equals(NettoWeight, item.NettoWeight) &&
        Equals(WeightTare, item.WeightTare) &&
        Equals(RegNum, item.RegNum) &&
        PluScale.Equals(item.PluScale) &&
        (Series is null && item.Series is null ||
         Series is not null && item.Series is not null && Series.Equals(item.Series));

    #endregion
}
