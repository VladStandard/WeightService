// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "Organization".
/// </summary>
[Serializable]
public class OrganizationEntity : BaseEntity
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Identity name.
	/// </summary>
	[XmlElement] public static ColumnName IdentityName => ColumnName.Id;
	[XmlElement] public virtual string Name { get; set; }
	[XmlElement] public virtual int Gln { get; set; }
	[XmlElement] public virtual string SerializedRepresentationObject { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public OrganizationEntity() : this(0)
    {
        //
    }

	/// <summary>
	/// Constructor.
	/// </summary>
    public OrganizationEntity(long id) : base(id)
    {
        Name = string.Empty;
        Gln = default;
        SerializedRepresentationObject = string.Empty;
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
	    $"{nameof(IdentityId)}: {IdentityId}. " +
	    $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Gln)}: {Gln}. " +
        $"{nameof(SerializedRepresentationObject)}: {SerializedRepresentationObject.Length}. ";

    public virtual bool Equals(OrganizationEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(Name, item.Name) &&
               Equals(Gln, item.Gln) &&
               Equals(SerializedRepresentationObject, item.SerializedRepresentationObject);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((OrganizationEntity)obj);
    }

	public override int GetHashCode() => IdentityId.GetHashCode();

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return base.EqualsDefault() &&
               Equals(Name, string.Empty) &&
               Equals(Gln, 0) &&
               Equals(SerializedRepresentationObject, string.Empty);
    }

    public new virtual object Clone()
    {
        OrganizationEntity item = new();
        item.Name = Name;
        item.Gln = Gln;
        item.SerializedRepresentationObject = SerializedRepresentationObject;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual OrganizationEntity CloneCast() => (OrganizationEntity)Clone();

    #endregion
}
