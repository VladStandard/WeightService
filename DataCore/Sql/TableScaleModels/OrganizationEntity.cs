// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "Organization".
/// </summary>
[Serializable]
public class OrganizationEntity : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Identity name.
	/// </summary>
	[XmlElement] public static ColumnName IdentityName => ColumnName.Id;
	[XmlElement] public virtual string Name { get; set; }
	[XmlElement] public virtual int Gln { get; set; }
	[XmlElement] public virtual string Xml { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public OrganizationEntity() : base(0, false)
	{
		Init();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityId"></param>
	/// <param name="isSetupDates"></param>
	public OrganizationEntity(long identityId, bool isSetupDates) : base(identityId, isSetupDates)
    {
		Init();
	}

    #endregion

    #region Public and private methods

    public new virtual void Init()
    {
	    base.Init();
		Name = string.Empty;
		Gln = 0;
		Xml = string.Empty;
	}

    public override string ToString() =>
	    $"{nameof(IdentityId)}: {IdentityId}. " +
	    $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Gln)}: {Gln}. " +
        $"{nameof(Xml)}: {Xml.Length}. ";

    public virtual bool Equals(OrganizationEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(Name, item.Name) &&
               Equals(Gln, item.Gln) &&
               Equals(Xml, item.Xml);
    }

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
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
               Equals(Xml, string.Empty);
    }

    public new virtual object Clone()
    {
        OrganizationEntity item = new();
        item.Name = Name;
        item.Gln = Gln;
        item.Xml = Xml;
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual OrganizationEntity CloneCast() => (OrganizationEntity)Clone();

    #endregion
}
