// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "ORGANIZATIONS".
/// </summary>
[Serializable]
public class OrganizationModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual string Name { get; set; }
	[XmlElement] public virtual int Gln { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public OrganizationModel() : base(SqlFieldIdentityEnum.Uid)
	{
		Name = string.Empty;
		Gln = 0;
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	private OrganizationModel(SerializationInfo info, StreamingContext context) : base(info, context)
	{
		Name = info.GetString(nameof(Name));
		Gln = info.GetInt32(nameof(Gln));
	}

	#endregion

	#region Public and private methods - override

    public override string ToString() =>
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Description)}: {Description}. " +
        $"{nameof(Gln)}: {Gln}. ";

    public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((OrganizationModel)obj);
    }

	public override int GetHashCode() => base.GetHashCode();

	public override bool EqualsNew() => Equals(new());

	public override bool EqualsDefault() =>
		base.EqualsDefault() &&
		Equals(Name, string.Empty) &&
		Equals(Gln, 0);

	public override object Clone()
    {
        OrganizationModel item = new();
        item.Name = Name;
        item.Gln = Gln;
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
		info.AddValue(nameof(Gln), Gln);
	}

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(OrganizationModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		return 
            base.Equals(item) &&
		    Equals(Name, item.Name) &&
		    Equals(Gln, item.Gln);
	}

	public new virtual OrganizationModel CloneCast() => (OrganizationModel)Clone();

	#endregion
}
