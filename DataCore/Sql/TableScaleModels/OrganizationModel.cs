// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "Organization".
/// </summary>
[Serializable]
public class OrganizationModel : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual string Name { get; set; }
	[XmlElement] public virtual int Gln { get; set; }
	[XmlElement] public virtual string Xml { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public OrganizationModel() : base(ColumnName.Id)
	{
		Name = string.Empty;
		Gln = 0;
		Xml = string.Empty;
	}

    #endregion

    #region Public and private methods

    public override string ToString() =>
	    $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Gln)}: {Gln}. " +
        $"{nameof(Xml)}: {Xml.Length}. ";

    public virtual bool Equals(OrganizationModel item)
    {
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
        return Equals((OrganizationModel)obj);
    }

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return 
	        base.EqualsDefault() &&
            Equals(Name, string.Empty) &&
            Equals(Gln, 0) &&
            Equals(Xml, string.Empty);
    }

    public new virtual object Clone()
    {
        OrganizationModel item = new();
        item.Name = Name;
        item.Gln = Gln;
        item.Xml = Xml;
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual OrganizationModel CloneCast() => (OrganizationModel)Clone();

    #endregion
}
