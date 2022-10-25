// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "PLUS_WEIGHINGS".
/// </summary>
[Serializable]
public class PluWeighingModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual PluScaleModel PluScale { get; set; }
    [XmlElement(IsNullable = true)] public virtual ProductSeriesModel? Series { get; set; }
    [XmlElement] public virtual short Kneading { get; set; }
	[XmlElement] public virtual string KneadingFormat
	{
		get => $"{Kneading:000}";
		// This code need for print labels.
		set => _ = value;
	}
    [XmlElement] public virtual string Sscc { get; set; }
    [XmlElement] public virtual decimal NettoWeight { get; set; }
    [XmlElement] public virtual string NettoWeightKgFormat2
	{
		get => $"{NettoWeight:00.000}".Replace(',', '.').Split('.')[0];
		// This code need for print labels.
		set => _ = value;
	}
	[XmlElement] public virtual string NettoWeightKgFormat3
	{
		get => $"{NettoWeight:000.000}".Replace(',', '.').Split('.')[0];
		// This code need for print labels.
		set => _ = value;
	}
	[XmlElement] public virtual string NettoWeightKgFormat1Dot3Eng
	{
		get => $"{NettoWeight:0.000}".Replace(',', '.');
		// This code need for print labels.
		set => _ = value;
	}
	[XmlElement] public virtual string NettoWeightKgFormat2Dot3Eng
	{
		get => $"{NettoWeight:#0.000}".Replace(',', '.');
		// This code need for print labels.
		set => _ = value;
	}
	[XmlElement] public virtual string NettoWeightKgFormat1Dot3Rus
	{
		get => $"{NettoWeight:0.000}".Replace('.', ',');
		// This code need for print labels.
		set => _ = value;
	}
	[XmlElement] public virtual string NettoWeightKgFormat2Dot3Rus
	{
		get => $"{NettoWeight:#0.000}".Replace('.', ',');
		// This code need for print labels.
		set => _ = value;
	}
	[XmlElement] public virtual string NettoWeightGrFormat2
	{
		get => $"{NettoWeight:#.00}".Replace(',', '.').Split('.')[1];
		// This code need for print labels.
		set => _ = value;
	}
	[XmlElement] public virtual string NettoWeightGrFormat3
	{
		get => $"{NettoWeight:#.000}".Replace(',', '.').Split('.')[1];
		// This code need for print labels.
		set => _ = value;
	}
	[XmlElement] public virtual decimal TareWeight { get; set; }
    [XmlElement] public virtual int RegNum { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluWeighingModel() : base(SqlFieldIdentityEnum.Uid)
	{
	    PluScale = new();
	    Series = null;
	    Kneading = 0;
	    Sscc = string.Empty;
	    NettoWeight = 0;
	    TareWeight = 0;
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
        TareWeight = info.GetDecimal(nameof(TareWeight));
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

    public override bool EqualsDefault()
    {
        if (!PluScale.EqualsDefault())
            return false;
        if (Series is not null && !Series.EqualsDefault())
            return false;
        return
            base.EqualsDefault() &&
            Equals(Kneading, default(short)) &&
            Equals(Sscc, string.Empty) &&
            Equals(NettoWeight, default(decimal)) &&
            Equals(TareWeight, default(decimal)) &&
            Equals(RegNum, default(int));
    }

	public override object Clone()
    {
        PluWeighingModel item = new();
        item.PluScale = PluScale.CloneCast();
        item.Series = Series?.CloneCast();
        item.Kneading = Kneading;
        item.Sscc = Sscc;
        item.NettoWeight = NettoWeight;
        item.TareWeight = TareWeight;
        item.RegNum = RegNum;
        item.CloneSetup(base.CloneCast());
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
        info.AddValue(nameof(TareWeight), TareWeight);
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
		TareWeight = 0.25M;
		RegNum = 1;
		Kneading = 1;
		PluScale.FillProperties();
		Series?.FillProperties();
    }

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(PluWeighingModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		if (!PluScale.Equals(item.PluScale))
			return false;
        if (Series is not null && item.Series is not null && !Series.Equals(item.Series))
            return false;
        return
			base.Equals(item) &&
			Equals(Kneading, item.Kneading) &&
			Equals(PluScale, item.PluScale) &&
			Equals(Sscc, item.Sscc) &&
			Equals(NettoWeight, item.NettoWeight) &&
			Equals(TareWeight, item.TareWeight) &&
			Equals(RegNum, item.RegNum);
	}

	public new virtual PluWeighingModel CloneCast() => (PluWeighingModel)Clone();

	#endregion
}
