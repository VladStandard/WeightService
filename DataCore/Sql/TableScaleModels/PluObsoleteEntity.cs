// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "PLU".
/// </summary>
[Serializable]
public class PluObsoleteEntity : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual TemplateEntity Template { get; set; }
	[XmlElement] public virtual ScaleEntity Scale { get; set; }
	[XmlElement] public virtual NomenclatureEntity Nomenclature { get; set; }
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
    public PluObsoleteEntity() : base(ColumnName.Id, 0, false)
	{
		Init();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityId"></param>
	/// <param name="isSetupDates"></param>
	public PluObsoleteEntity(long identityId, bool isSetupDates) : base(ColumnName.Id, identityId, isSetupDates)
	{
		Init();
	}

	/// <summary>
	/// Constructor for serialization.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
    protected PluObsoleteEntity(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Template = (TemplateEntity)info.GetValue(nameof(Template), typeof(TemplateEntity));
        Scale = (ScaleEntity)info.GetValue(nameof(Scale), typeof(ScaleEntity));
        Nomenclature = (NomenclatureEntity)info.GetValue(nameof(Nomenclature), typeof(NomenclatureEntity));
        GoodsName = info.GetString(nameof(GoodsName));
        GoodsFullName = info.GetString(nameof(GoodsFullName));
        GoodsDescription = info.GetString(nameof(GoodsDescription));
        Gtin = info.GetString(nameof(Gtin));
        Ean13 = info.GetString(nameof(Ean13));
        Itf14 = info.GetString(nameof(Itf14));
    }

	#endregion

	#region Public and private methods

	public new virtual void Init()
	{
		base.Init();
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

	public override string ToString()
    {
        string strTemplates = Template != null ? Template.IdentityId.ToString() : "null";
        string strScale = Scale != null ? Scale.IdentityId.ToString() : "null";
        string strNomenclature = Nomenclature != null ? Nomenclature.IdentityId.ToString() : "null";
        return
			$"{nameof(IdentityId)}: {IdentityId}. " + 
            $"{nameof(IsMarked)}: {IsMarked}. " +
			$"{nameof(Template)}: {strTemplates}. " +
			$"{nameof(Scale)}: {strScale}. " +
			$"{nameof(Nomenclature)}: {strNomenclature}. " +
			$"{nameof(GoodsName)}: {GoodsName}. " +
			$"{nameof(GoodsFullName)}: {GoodsFullName}. " +
			$"{nameof(GoodsDescription)}: {GoodsDescription}. " +
			$"{nameof(Gtin)}: {Gtin}. " +
			$"{nameof(Ean13)}: {Ean13}. " +
			$"{nameof(Itf14)}: {Itf14}. " +
			$"{nameof(GoodsShelfLifeDays)}: {GoodsShelfLifeDays}. " +
			$"{nameof(GoodsTareWeight)}: {GoodsTareWeight}. " +
			$"{nameof(GoodsBoxQuantly)}: {GoodsBoxQuantly}. " +
			$"{nameof(PluNumber)}: {PluNumber}. " +
			$"{nameof(Active)}: {Active}. " +
			$"{nameof(UpperWeightThreshold)}: {UpperWeightThreshold}. " +
			$"{nameof(NominalWeight)}: {NominalWeight}. " +
			$"{nameof(LowerWeightThreshold)}: {LowerWeightThreshold}. " +
			$"{nameof(IsCheckWeight)}: {IsCheckWeight}. ";
    }

    public virtual bool Equals(PluObsoleteEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        if (Template != null && item.Template != null && !Template.Equals(item.Template))
            return false;
        if (Scale != null && item.Scale != null && !Scale.Equals(item.Scale))
            return false;
        if (Nomenclature != null && item.Nomenclature != null && !Nomenclature.Equals(item.Nomenclature))
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

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((PluObsoleteEntity)obj);
    }

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        if (Template != null && !Template.EqualsDefault())
            return false;
        if (Scale != null && !Scale.EqualsDefault())
            return false;
        if (Nomenclature != null && !Nomenclature.EqualsDefault())
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

    public new virtual object Clone()
    {
        PluObsoleteEntity item = new();
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
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual PluObsoleteEntity CloneCast() => (PluObsoleteEntity)Clone();

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
