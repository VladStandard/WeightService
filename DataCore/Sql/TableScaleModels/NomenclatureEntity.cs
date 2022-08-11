// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "Nomenclature".
/// </summary>
[Serializable]
public class NomenclatureEntity : BaseEntity
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Identity name.
	/// </summary>
	[XmlElement] public static ColumnName IdentityName => ColumnName.Id;
	[XmlElement] public virtual string Name { get; set; } = string.Empty;
	[XmlElement] public virtual string Code { get; set; } = string.Empty;
	[XmlElement(IsNullable = true)] public virtual string? Xml { get; set; } = string.Empty;
	/// <summary>
	/// Is weighted or pcs.
	/// </summary>
	[XmlElement] public virtual bool Weighted { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public NomenclatureEntity() : this(0)
    {
        //
    }

	/// <summary>
	/// Constructor.
	/// </summary>
    public NomenclatureEntity(long id) : base(id)
    {
		//
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
	    $"{nameof(IdentityId)}: {IdentityId}. " +
	    $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Code)}: {Code}. " +
        $"{nameof(Xml)}.Length: {Xml?.Length ?? 0}. " +
        $"{nameof(Weighted)}: {Weighted}. ";

    public virtual bool Equals(NomenclatureEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(Code, item.Code) &&
               Equals(Name, item.Name) &&
               Equals(Xml, item.Xml) &&
               Equals(Weighted, item.Weighted);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((NomenclatureEntity)obj);
    }

	public override int GetHashCode() => IdentityId.GetHashCode();

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return base.EqualsDefault() &&
               Equals(Code, string.Empty) &&
               Equals(Name, string.Empty) &&
               Equals(Xml, string.Empty) &&
               Equals(Weighted, false);
    }

    public new virtual object Clone()
    {
        NomenclatureEntity item = new();
        item.Code = Code;
        item.Name = Name;
        item.Xml = Xml;
        item.Weighted = Weighted;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual NomenclatureEntity CloneCast() => (NomenclatureEntity)Clone();

    #endregion
}
