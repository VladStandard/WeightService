// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "PLUS_LABELS".
/// </summary>
[Serializable]
public class PluLabelModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement(IsNullable = true)] public virtual PluWeighingModel? PluWeighing { get; set; }
    [XmlElement] public virtual string Zpl { get; set; }
    [XmlElement] public virtual DateTime ProductDt { get; set; }
    [XmlIgnore] public virtual string ProductDtRus
	{
		get => $"{ProductDt:dd.MM.yyyy}";
		// This code need for print labels.
		set => _ = value;
	}
	[XmlElement]
    public virtual DateTime ExpirationDt
    {
	    get => PluWeighing is null ? DateTime.MinValue : ProductDt.AddDays(PluWeighing.PluScale.Plu.ShelfLifeDays);
	    // This code need for print labels.
	    set => _ = value;
    }

	/// <summary>
	/// Constructor.
	/// </summary>
	public PluLabelModel() : base(SqlFieldIdentityEnum.Uid)
	{
	    PluWeighing = null;
	    Zpl = string.Empty;
	    ProductDt = DateTime.MinValue;
	    ExpirationDt = DateTime.MinValue;
    }

	/// <summary>
	/// Constructor for serialization.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	protected PluLabelModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        PluWeighing = (PluWeighingModel?)info.GetValue(nameof(PluWeighing), typeof(PluWeighingModel));
        Zpl = info.GetString(nameof(Zpl));
        ProductDt = info.GetDateTime(nameof(ProductDt));
        ExpirationDt = info.GetDateTime(nameof(ExpirationDt));
    }

	#endregion

	#region Public and private methods - override

	public override string ToString() => 
		$"{nameof(Zpl)}: {Zpl.Length}. ";

    public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((PluLabelModel)obj);
    }
    
    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault()
    {
	    if (PluWeighing is not null && !PluWeighing.EqualsDefault())
		    return false;
	    return
		    base.EqualsDefault() &&
		    Equals(Zpl, string.Empty) &&
		    Equals(ProductDt, DateTime.MinValue);
            //Equals(ExpirationDt, DateTime.MinValue) &&
	}

	public override object Clone()
    {
        PluLabelModel item = new();
        item.IsMarked = IsMarked;
        item.PluWeighing = PluWeighing?.CloneCast();
        item.Zpl = Zpl;
        item.ProductDt = ProductDt;
        item.ExpirationDt = ExpirationDt;
        item.CloneSetup(base.CloneCast());
		return item;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(PluWeighing), PluWeighing);
        info.AddValue(nameof(Zpl), Zpl);
        info.AddValue(nameof(ProductDt), ProductDt);
        info.AddValue(nameof(ExpirationDt), ExpirationDt);
    }

	public override void ClearNullProperties()
    {
	    if (PluWeighing is not null && PluWeighing.Identity.EqualsDefault())
		    PluWeighing = null;
    }

	public override void FillProperties()
    {
	    base.FillProperties();
		Zpl = LocaleCore.Sql.SqlItemFieldZpl;
		//PluWeighing = new();
		ProductDt = DateTime.Now;
		//ExpirationDt = DateTime.Now;
	}

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(PluLabelModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		if (PluWeighing is not null && item.PluWeighing is not null && !PluWeighing.Equals(item.PluWeighing))
			return false;
		return
			base.Equals(item) &&
			Equals(PluWeighing, item.PluWeighing) &&
			Equals(Zpl, item.Zpl) &&
			Equals(ProductDt, item.ProductDt) &&
			Equals(ExpirationDt, item.ExpirationDt);
	}

	public new virtual PluLabelModel CloneCast() => (PluLabelModel)Clone();

	#endregion
}
