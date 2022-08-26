// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "PLUS_WEIGHINGS".
/// </summary>
[Serializable]
public class PluWeighingEntity : BaseEntity, ISerializable, IBaseEntity
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Identity name.
    /// </summary>
    [XmlElement] public static ColumnName IdentityName => ColumnName.Uid;

    [XmlElement] public virtual PluScaleEntity PluScale { get; set; }
    [XmlElement] public virtual ProductSeriesEntity Series { get; set; }
    [XmlElement] public virtual short Kneading { get; set; }
    [XmlElement] public virtual string Sscc { get; set; }
    [XmlElement] public virtual decimal NettoWeight { get; set; }
    [XmlElement] public virtual decimal TareWeight { get; set; }
    [XmlElement] public virtual DateTime ProductDt { get; set; }
    [XmlElement] public virtual int RegNum { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluWeighingEntity() : base(Guid.Empty, false)
    {
        Init();
    }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityUid"></param>
	/// <param name="isSetupDates"></param>
	public PluWeighingEntity(Guid identityUid, bool isSetupDates) : base(identityUid, isSetupDates)
    {
        Init();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluWeighingEntity(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        PluScale = (PluScaleEntity)info.GetValue(nameof(PluScale), typeof(PluScaleEntity));
        Series = (ProductSeriesEntity)info.GetValue(nameof(Series), typeof(ProductSeriesEntity));
        Kneading = info.GetInt16(nameof(Kneading));
		Sscc = info.GetString(nameof(Sscc));
        NettoWeight = info.GetDecimal(nameof(NettoWeight));
        TareWeight = info.GetDecimal(nameof(TareWeight));
        ProductDt = info.GetDateTime(nameof(ProductDt));
        RegNum = info.GetInt32(nameof(RegNum));
    }

    #endregion

    #region Public and private methods

    public new virtual void Init()
    {
        base.Init();
        PluScale = new();
        Series = new();
        Kneading = 0;
        Sscc = string.Empty;
        NettoWeight = 0;
        TareWeight = 0;
        ProductDt = DateTime.MinValue;
        RegNum = 0;
    }

    public override string ToString()
    {
        return
            $"{nameof(IdentityUid)}: {IdentityUid}. " +
            $"{nameof(IsMarked)}: {IsMarked}. " +
            $"{nameof(Kneading)}: {Kneading}. " +
            $"{nameof(PluScale)}: {PluScale}. " + 
            $"{nameof(Series)}: {Series}. ";
    }

    public virtual bool Equals(PluWeighingEntity item)
    {
        //if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        if (!PluScale.Equals(item.PluScale))
            return false;
        return
            base.Equals(item) &&
            Equals(Kneading, item.Kneading) &&
            Equals(PluScale, item.PluScale) &&
            Equals(Sscc, item.Sscc) &&
            Equals(NettoWeight, item.NettoWeight) &&
            Equals(TareWeight, item.TareWeight) &&
            Equals(ProductDt, item.ProductDt) &&
            Equals(RegNum, item.RegNum);
    }

    public override bool Equals(object obj)
    {
        //if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluWeighingEntity)obj);
    }

    public override int GetHashCode() => IdentityUid.GetHashCode();

    public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        if (!PluScale.EqualsDefault())
            return false;
        if (!Series.EqualsDefault())
            return false;
        return
            base.EqualsDefault() &&
            Equals(Kneading, default(short)) &&
            Equals(Sscc, string.Empty) &&
            Equals(NettoWeight, default(decimal)) &&
            Equals(TareWeight, default(decimal)) &&
            Equals(ProductDt, DateTime.MinValue) &&
            Equals(RegNum, default(int));
    }

    public new virtual object Clone()
    {
        PluWeighingEntity item = new();
        item.Kneading = Kneading;
        item.PluScale = PluScale.CloneCast();
        item.Sscc = Sscc;
        item.NettoWeight = NettoWeight;
        item.TareWeight = TareWeight;
        item.ProductDt = ProductDt;
        item.RegNum = RegNum;
		item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual PluWeighingEntity CloneCast() => (PluWeighingEntity)Clone();

    public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Kneading), Kneading);
        info.AddValue(nameof(PluScale), PluScale);
        info.AddValue(nameof(Sscc), Sscc);
        info.AddValue(nameof(NettoWeight), NettoWeight);
        info.AddValue(nameof(TareWeight), TareWeight);
        info.AddValue(nameof(ProductDt), ProductDt);
        info.AddValue(nameof(RegNum), RegNum);
    }

    #endregion
}
