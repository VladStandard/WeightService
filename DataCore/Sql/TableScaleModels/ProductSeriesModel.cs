// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "ProductSeries".
/// </summary>
[Serializable]
public class ProductSeriesModel : TableBaseModel, ICloneable, ISqlDbBase, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual ScaleModel Scale { get; set; }
	[XmlElement] public virtual bool IsClose { get; set; }
	[XmlElement] public virtual string Sscc { get; set; }
	[XmlElement] public virtual Guid Uid { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public ProductSeriesModel() : base(SqlFieldIdentityEnum.Id)
    {
	    Scale = new();
	    IsClose = false;
	    Sscc = string.Empty;
	    Uid = Guid.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private ProductSeriesModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
		Scale = (ScaleModel)info.GetValue(nameof(Scale), typeof(ScaleModel));
		IsClose = info.GetBoolean(nameof(IsClose));
		Sscc = info.GetString(nameof(Sscc));
		Uid = (Guid)info.GetValue(nameof(Uid), typeof(Guid));
	}

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
		$"{nameof(IsMarked)}: {IsMarked}. " +
		$"{nameof(Scale)}: {Scale}. " +
		$"{nameof(IsClose)}: {IsClose}. " +
		$"{nameof(Sscc)}: {Sscc}.";

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((ProductSeriesModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

	public override bool EqualsNew() => Equals(new());

	public override bool EqualsDefault()
    {
        if (!Scale.EqualsDefault())
            return false;
        return 
	        base.EqualsDefault() &&
            Equals(IsClose, false) &&
            Equals(Sscc, string.Empty) &&
	        Equals(Uid, Guid.Empty);
    }

    public override object Clone()
    {
        ProductSeriesModel item = new();
        item.Scale = Scale.CloneCast();
        item.IsClose = IsClose;
        item.Sscc = Sscc;
        item.Uid = Uid;
        item.CloneSetup(base.CloneCast());
        return item;
    }

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Scale), Scale);
        info.AddValue(nameof(IsClose), IsClose);
        info.AddValue(nameof(Sscc), Sscc);
        info.AddValue(nameof(Uid), Uid);
    }

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(ProductSeriesModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		if (!Scale.Equals(item.Scale))
			return false;
		return
			base.Equals(item) &&
			Equals(CreateDt, item.CreateDt) &&
			Equals(IsClose, item.IsClose) &&
			Equals(Sscc, item.Sscc);
	}

    public new virtual ProductSeriesModel CloneCast() => (ProductSeriesModel)Clone();

	#endregion
}
