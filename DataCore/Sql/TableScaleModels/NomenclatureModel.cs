// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "Nomenclature".
/// </summary>
[Serializable]
public class NomenclatureModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual string Name { get; set; }
	[XmlElement] public virtual string Code { get; set; }
	[XmlElement(IsNullable = true)] public virtual string? Xml { get; set; }
	/// <summary>
	/// Is weighted or pcs.
	/// </summary>
	[XmlElement] public virtual bool Weighted { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public NomenclatureModel() : base(SqlFieldIdentityEnum.Id)
	{
		Name = string.Empty;
		Code = string.Empty;
		Xml = string.Empty;
		Weighted = false;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	private NomenclatureModel(SerializationInfo info, StreamingContext context) : base(info, context)
	{
		Name = info.GetString(nameof(Name));
		Code = info.GetString(nameof(Code));
		Xml = (string?)info.GetValue(nameof(Xml), typeof(string));
		Weighted = info.GetBoolean(nameof(Weighted));
	}

	#endregion

	#region Public and private methods - override

	public override string ToString() => 
		$"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Code)}: {Code}. " +
        $"{nameof(Xml)}.Length: {Xml?.Length ?? 0}. " +
        $"{nameof(Weighted)}: {Weighted}. ";

    public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((NomenclatureModel)obj);
    }
    
    public override int GetHashCode() => base.GetHashCode();

	public override bool EqualsNew() => Equals(new());

	public new virtual bool EqualsDefault() =>
	    base.EqualsDefault() &&
	    Equals(Code, string.Empty) &&
	    Equals(Name, string.Empty) &&
	    Equals(Xml, string.Empty) &&
	    Equals(Weighted, false);

    public override object Clone()
    {
        NomenclatureModel item = new();
        item.Code = Code;
        item.Name = Name;
        item.Xml = Xml;
        item.Weighted = Weighted;
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
		info.AddValue(nameof(Name), Name);
		info.AddValue(nameof(Code), Code);
		info.AddValue(nameof(Xml), Xml);
		info.AddValue(nameof(Weighted), Weighted);
	}

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(NomenclatureModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		return
			base.Equals(item) &&
			Equals(Code, item.Code) &&
			Equals(Name, item.Name) &&
			Equals(Xml, item.Xml) &&
			Equals(Weighted, item.Weighted);
	}

	public new virtual NomenclatureModel CloneCast() => (NomenclatureModel)Clone();

	#endregion
}
