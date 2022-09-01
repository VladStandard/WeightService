// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "PLU".
/// </summary>
[Serializable]
public class PluObsoleteModel : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual TemplateModel Template { get; set; }
	[XmlElement] public virtual ScaleModel Scale { get; set; }
	[XmlElement] public virtual NomenclatureModel Nomenclature { get; set; }
	[XmlElement] public virtual string GoodsName { get; set; }
	[XmlElement] public virtual string GoodsFullName { get; set; }
	[XmlElement] public virtual string GoodsDescription { get; set; }
	[XmlElement] public virtual string Gtin { get; set; }
	[XmlElement] public virtual string Ean13 { get; set; }
	[XmlElement] public virtual string Itf14 { get; set; }
	[XmlElement] public virtual short GoodsShelfLifeDays { get; set; }
	[XmlElement] public virtual decimal GoodsTareWeight { get; set; }
	[XmlElement] public virtual int GoodsBoxQuantly { get; set; }
	[XmlElement] public virtual int PluNumber { get; set; }
	[XmlElement] public virtual bool Active { get; set; }
	[XmlElement] public virtual decimal UpperWeightThreshold { get; set; }
	[XmlElement] public virtual decimal NominalWeight { get; set; }
	[XmlElement] public virtual decimal LowerWeightThreshold { get; set; }
	[XmlElement] public virtual bool IsCheckWeight { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public PluObsoleteModel() : base(ColumnName.Id)
	{
		Template = new();
		Scale = new();
		Nomenclature = new();
		GoodsName = string.Empty;
		GoodsFullName = string.Empty;
		GoodsDescription = string.Empty;
		Gtin = string.Empty;
		Ean13 = string.Empty;
		Itf14 = string.Empty;
		GoodsShelfLifeDays = 0;
		GoodsTareWeight = 0;
		GoodsBoxQuantly = 0;
		PluNumber = 0;
		Active = false;
		UpperWeightThreshold = 0;
		NominalWeight = 0;
		LowerWeightThreshold = 0;
		IsCheckWeight = false;
	}

	/// <summary>
	/// Constructor for serialization.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
    protected PluObsoleteModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Template = (TemplateModel)info.GetValue(nameof(Template), typeof(TemplateModel));
        Scale = (ScaleModel)info.GetValue(nameof(Scale), typeof(ScaleModel));
        Nomenclature = (NomenclatureModel)info.GetValue(nameof(Nomenclature), typeof(NomenclatureModel));
        GoodsName = info.GetString(nameof(GoodsName));
        GoodsFullName = info.GetString(nameof(GoodsFullName));
        GoodsDescription = info.GetString(nameof(GoodsDescription));
        Gtin = info.GetString(nameof(Gtin));
        Ean13 = info.GetString(nameof(Ean13));
        Itf14 = info.GetString(nameof(Itf14));
    }

	#endregion

	#region Public and private methods

	public new virtual string ToString() =>
		$"{nameof(IsMarked)}: {IsMarked}. " +
		$"{nameof(Template)}: {Template}. " +
		$"{nameof(Scale)}: {Scale}. " +
		$"{nameof(Nomenclature)}: {Nomenclature}. " +
		$"{nameof(GoodsName)}: {GoodsName}. ";

	public virtual bool Equals(PluObsoleteModel item)
    {
        if (ReferenceEquals(this, item)) return true;
        if (!Template.Equals(item.Template))
            return false;
        if (!Scale.Equals(item.Scale))
            return false;
        if (!Nomenclature.Equals(item.Nomenclature))
            return false;
        return base.Equals(item) &&
               Equals(GoodsName, item.GoodsName) &&
               Equals(GoodsFullName, item.GoodsFullName) &&
               Equals(GoodsDescription, item.GoodsDescription) &&
               Equals(Gtin, item.Gtin) &&
               Equals(Ean13, item.Ean13) &&
               Equals(Itf14, item.Itf14) &&
               Equals(GoodsShelfLifeDays, item.GoodsShelfLifeDays) &&
               Equals(GoodsTareWeight, item.GoodsTareWeight) &&
               Equals(GoodsBoxQuantly, item.GoodsBoxQuantly) &&
               Equals(PluNumber, item.PluNumber) &&
               Equals(Active, item.Active) &&
               Equals(UpperWeightThreshold, item.UpperWeightThreshold) &&
               Equals(NominalWeight, item.NominalWeight) &&
               Equals(LowerWeightThreshold, item.LowerWeightThreshold) &&
               Equals(IsCheckWeight, item.IsCheckWeight);
    }

	public new virtual bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((PluObsoleteModel)obj);
    }

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        if (!Template.EqualsDefault())
            return false;
        if (!Scale.EqualsDefault())
            return false;
        if (!Nomenclature.EqualsDefault())
            return false;
        return base.EqualsDefault() &&
               Equals(GoodsName, string.Empty) &&
               Equals(GoodsFullName, string.Empty) &&
               Equals(GoodsDescription, string.Empty) &&
               Equals(Gtin, string.Empty) &&
               Equals(Ean13, string.Empty) &&
               Equals(Itf14, string.Empty) &&
               Equals(GoodsShelfLifeDays, (short)0) &&
               Equals(GoodsTareWeight, (decimal)0) &&
               Equals(GoodsBoxQuantly, 0) &&
               Equals(PluNumber, 0) &&
               Equals(Active, false) &&
               Equals(UpperWeightThreshold, (decimal)0) &&
               Equals(NominalWeight, (decimal)0) &&
               Equals(LowerWeightThreshold, (decimal)0) &&
               Equals(IsCheckWeight, false);
    }

    public new virtual int GetHashCode() => base.GetHashCode();

	public new virtual object Clone()
    {
        PluObsoleteModel item = new();
        item.Template = Template.CloneCast();
        item.Scale = Scale.CloneCast();
        item.Nomenclature = Nomenclature.CloneCast();
        item.GoodsName = GoodsName;
        item.GoodsFullName = GoodsFullName;
        item.GoodsDescription = GoodsDescription;
        item.Gtin = Gtin;
        item.Ean13 = Ean13;
        item.Itf14 = Itf14;
        item.GoodsShelfLifeDays = GoodsShelfLifeDays;
        item.GoodsTareWeight = GoodsTareWeight;
        item.GoodsBoxQuantly = GoodsBoxQuantly;
        item.PluNumber = PluNumber;
        item.Active = Active;
        item.UpperWeightThreshold = UpperWeightThreshold;
        item.NominalWeight = NominalWeight;
        item.LowerWeightThreshold = LowerWeightThreshold;
        item.IsCheckWeight = IsCheckWeight;
		item.CloneSetup(base.CloneCast());
		return item;
    }

    public new virtual PluObsoleteModel CloneCast() => (PluObsoleteModel)Clone();

    public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Template), Template);
        info.AddValue(nameof(Scale), Scale);
        info.AddValue(nameof(Nomenclature), Nomenclature);
        info.AddValue(nameof(GoodsName), GoodsName);
        info.AddValue(nameof(GoodsFullName), GoodsFullName);
        info.AddValue(nameof(GoodsDescription), GoodsDescription);
        info.AddValue(nameof(Gtin), Gtin);
        info.AddValue(nameof(Ean13), Ean13);
        info.AddValue(nameof(Itf14), Itf14);
    }
    
    #endregion
}
