// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "PLUS".
/// </summary>
[Serializable]
public class PluEntity : BaseEntity, ISerializable, IBaseEntity
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Identity name.
	/// </summary>
	[XmlElement] public static ColumnName IdentityName => ColumnName.Uid;
    [XmlElement] public virtual int Number { get; set; }
    [XmlElement] public virtual string Name { get; set; }
	[XmlElement] public virtual string FullName { get; set; }
	[XmlElement] public virtual string Description { get; set; }
	[XmlElement] public virtual short ShelfLifeDays { get; set; }
    [XmlElement] public virtual decimal TareWeight { get; set; }
    [XmlElement] public virtual int BoxQuantly { get; set; }
    [XmlElement] public virtual string Gtin { get; set; }
    [XmlElement] public virtual string PrettyGtin14
    {
	    get
	    {
		    return Gtin.Length switch
		    {
			    13 => BarcodeHelper.Instance.GetGtinWithCheckDigit(Gtin[..13]),
			    14 => Gtin,
			    _ => "ERROR"
		    };
	    }
	    // This code need for print labels.
	    set => _ = value;
    }
	[XmlElement] public virtual string Ean13 { get; set; }
    [XmlElement] public virtual string Itf14 { get; set; }
    [XmlElement] public virtual decimal UpperThreshold { get; set; }
    [XmlElement] public virtual decimal NominalWeight { get; set; }
    [XmlElement] public virtual decimal LowerThreshold { get; set; }
    [XmlElement] public virtual bool IsCheckWeight { get; set; }
    [XmlElement] public virtual TemplateEntity Template { get; set; }
    [XmlElement] public virtual NomenclatureEntity Nomenclature { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public PluEntity() : base(Guid.Empty, false)
	{
		Init();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityUid"></param>
	/// <param name="isSetupDates"></param>
	public PluEntity(Guid identityUid, bool isSetupDates) : base(identityUid, isSetupDates)
	{
		Init();
	}

	/// <summary>
	/// Constructor for serialization.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
    protected PluEntity(SerializationInfo info, StreamingContext context) : base(info, context)
    {
	    Number = info.GetInt32(nameof(Number));
	    Name = info.GetString(nameof(Name));
	    FullName = info.GetString(nameof(FullName));
	    Description = info.GetString(nameof(Description));
	    ShelfLifeDays = info.GetInt16(nameof(ShelfLifeDays));
	    TareWeight = info.GetDecimal(nameof(TareWeight));
	    BoxQuantly = info.GetInt32(nameof(BoxQuantly));
	    Gtin = info.GetString(nameof(Gtin));
	    Ean13 = info.GetString(nameof(Ean13));
	    Itf14 = info.GetString(nameof(Itf14));
	    UpperThreshold = info.GetDecimal(nameof(UpperThreshold));
	    NominalWeight = info.GetDecimal(nameof(NominalWeight));
	    LowerThreshold = info.GetDecimal(nameof(LowerThreshold));
	    IsCheckWeight = info.GetBoolean(nameof(IsCheckWeight));
	    Template = (TemplateEntity)info.GetValue(nameof(Template), typeof(TemplateEntity));
	    Nomenclature = (NomenclatureEntity)info.GetValue(nameof(Template), typeof(NomenclatureEntity));
    }

    #endregion

    #region Public and private methods

    public new virtual void Init()
    {
	    base.Init();
	    Number = 0;
        Name = string.Empty;
		FullName = string.Empty;
		Description = string.Empty;
		ShelfLifeDays = 0;
		TareWeight = 0;
		BoxQuantly = 0;
		Gtin = string.Empty;
		Ean13 = string.Empty;
		Itf14 = string.Empty;
		UpperThreshold = 0;
		NominalWeight = 0;
		LowerThreshold = 0;
		IsCheckWeight = false;
		Template = new();
		Nomenclature = new();
	}

    public override string ToString()
    {
	    return
		    $"{nameof(IdentityUid)}: {IdentityUid}. " +
		    $"{nameof(IsMarked)}: {IsMarked}. " +
		    $"{nameof(Number)}: {Number}. " +
		    $"{nameof(Name)}: {Name}. ";
    }

    public virtual bool Equals(PluEntity item)
    {
        //if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        if (!Template.Equals(item.Template))
            return false;
        if (!Nomenclature.Equals(item.Nomenclature))
            return false;
        return 
	        base.Equals(item) &&
			Equals(Number, item.Number) &&
			Equals(Name, item.Name) &&
			Equals(FullName, item.FullName) &&
			Equals(Description, item.Description) &&
			Equals(ShelfLifeDays, item.ShelfLifeDays) &&
			Equals(TareWeight, item.TareWeight) &&
			Equals(BoxQuantly, item.BoxQuantly) &&
			Equals(Gtin, item.Gtin) &&
			Equals(Ean13, item.Ean13) &&
			Equals(Itf14, item.Itf14) &&
			Equals(UpperThreshold, item.UpperThreshold) &&
			Equals(NominalWeight, item.NominalWeight) &&
			Equals(LowerThreshold, item.LowerThreshold) &&
			Equals(IsCheckWeight, item.IsCheckWeight);
    }

    public override bool Equals(object obj)
    {
        //if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluEntity)obj);
    }

	public override int GetHashCode() => IdentityUid.GetHashCode();

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        if (!Template.EqualsDefault())
            return false;
        if (!Nomenclature.EqualsDefault())
            return false;
        return 
			base.EqualsDefault() &&
			Equals(Number, default(int)) &&
			Equals(Name, string.Empty) &&
			Equals(FullName, string.Empty) &&
			Equals(Description, string.Empty) &&
			Equals(ShelfLifeDays, default(short)) &&
			Equals(TareWeight, default(decimal)) &&
			Equals(BoxQuantly, default(int)) &&
			Equals(Gtin, string.Empty) &&
			Equals(Ean13, string.Empty) &&
			Equals(Itf14, string.Empty) &&
			Equals(UpperThreshold, default(decimal)) &&
			Equals(NominalWeight, default(decimal)) &&
			Equals(LowerThreshold, default(decimal)) &&
			Equals(IsCheckWeight, false);
    }

    public new virtual object Clone()
    {
        PluEntity item = new();
        item.Number = Number;
        item.Name = Name;
        item.FullName = FullName;
        item.Description = Description;
        item.ShelfLifeDays = ShelfLifeDays;
        item.TareWeight = TareWeight;
        item.BoxQuantly = BoxQuantly;
        item.Gtin = Gtin;
        item.Ean13 = Ean13;
        item.Itf14 = Itf14;
        item.UpperThreshold = UpperThreshold;
        item.NominalWeight = NominalWeight;
        item.LowerThreshold = LowerThreshold;
        item.IsCheckWeight = IsCheckWeight;
        item.Template = Template.CloneCast();
        item.Nomenclature = Nomenclature.CloneCast();
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual PluEntity CloneCast() => (PluEntity)Clone();

    public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Number), Number);
        info.AddValue(nameof(Name), Name);
        info.AddValue(nameof(FullName), FullName);
        info.AddValue(nameof(Description), Description);
        info.AddValue(nameof(ShelfLifeDays), ShelfLifeDays);
        info.AddValue(nameof(TareWeight), TareWeight);
        info.AddValue(nameof(BoxQuantly), BoxQuantly);
        info.AddValue(nameof(Gtin), Gtin);
        info.AddValue(nameof(Ean13), Ean13);
        info.AddValue(nameof(Itf14), Itf14);
        info.AddValue(nameof(UpperThreshold), UpperThreshold);
        info.AddValue(nameof(NominalWeight), NominalWeight);
        info.AddValue(nameof(LowerThreshold), LowerThreshold);
        info.AddValue(nameof(IsCheckWeight), IsCheckWeight);
		info.AddValue(nameof(Template), Template);
        info.AddValue(nameof(Nomenclature), Nomenclature);
    }
    
    #endregion
}
