// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

using DataCore.Sql.Core.Enums;
using DataCore.Sql.TableScaleModels.PlusScales;
using DataCore.Sql.TableScaleModels.ProductSeries;

namespace DataCore.Sql.TableScaleModels.PlusWeighings;

/// <summary>
/// Table "PLUS_WEIGHINGS".
/// </summary>
[Serializable]
[DebuggerDisplay("{nameof(PluWeighingModel)}")]
public class PluWeighingModel : SqlTableBase
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual PluScaleModel PluScale { get; set; }
    [XmlElement(IsNullable = true)] public virtual ProductSeriesModel? Series { get; set; }
    [XmlElement] public virtual short Kneading { get; set; }
    [XmlElement]
    public virtual string KneadingFormat
    {
        get => $"{Kneading:000}";
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement] public virtual string Sscc { get; set; }
    [XmlElement] public virtual decimal NettoWeight { get; set; }
    [XmlElement]
    public virtual string NettoWeightKgFormat2
    {
        get => $"{NettoWeight:00.000}".Replace(',', '.').Split('.')[0];
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string NettoWeightKgFormat3
    {
        get => $"{NettoWeight:000.000}".Replace(',', '.').Split('.')[0];
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string NettoWeightKgFormat1Dot3Eng
    {
        get => $"{NettoWeight:0.000}".Replace(',', '.');
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string NettoWeightKgFormat2Dot3Eng
    {
        get => $"{NettoWeight:#0.000}".Replace(',', '.');
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string NettoWeightKgFormat1Dot3Rus
    {
        get => $"{NettoWeight:0.000}".Replace('.', ',');
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string NettoWeightKgFormat2Dot3Rus
    {
        get => $"{NettoWeight:#0.000}".Replace('.', ',');
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string NettoWeightGrFormat2
    {
        get => $"{NettoWeight:#.00}".Replace(',', '.').Split('.')[1];
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement]
    public virtual string NettoWeightGrFormat3
    {
        get => $"{NettoWeight:#.000}".Replace(',', '.').Split('.')[1];
        // This code need for print labels.
        set => _ = value;
    }
    [XmlElement] public virtual decimal WeightTare { get; set; }
    [XmlElement] public virtual int RegNum { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluWeighingModel() : base(SqlFieldIdentity.Uid)
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
    protected PluWeighingModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        PluScale = (PluScaleModel)info.GetValue(nameof(PluScale), typeof(PluScaleModel));
        Series = (ProductSeriesModel?)info.GetValue(nameof(Series), typeof(ProductSeriesModel));
        Kneading = info.GetInt16(nameof(Kneading));
        Sscc = info.GetString(nameof(Sscc));
        NettoWeight = info.GetDecimal(nameof(NettoWeight));
        WeightTare = info.GetDecimal(nameof(WeightTare));
        RegNum = info.GetInt32(nameof(RegNum));
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Kneading)}: {Kneading}. " +
        $"{nameof(PluScale)}: {PluScale}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluWeighingModel)obj);
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

    public override object Clone()
    {
        PluWeighingModel item = new();
        item.CloneSetup(base.CloneCast());
        item.PluScale = PluScale.CloneCast();
        item.Series = Series?.CloneCast();
        item.Kneading = Kneading;
        item.Sscc = Sscc;
        item.NettoWeight = NettoWeight;
        item.WeightTare = WeightTare;
        item.RegNum = RegNum;
        return item;
    }

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
        Sscc = LocaleCore.Sql.SqlItemFieldSscc;
        NettoWeight = 1.1M;
        WeightTare = 0.25M;
        RegNum = 1;
        Kneading = 1;
        PluScale.FillProperties();
        Series?.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(PluWeighingModel item) =>
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

    public new virtual PluWeighingModel CloneCast() => (PluWeighingModel)Clone();

    #endregion
}
