// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableDwhModels;

[Serializable]
public class StatusModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
    #region Public and private fields, properties, constructor

    public virtual string Name { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public StatusModel() : base(SqlFieldIdentityEnum.Id)
    {
	    Name = string.Empty;
    }

	#endregion

	#region Public and private methods - override

	public override string ToString() => 
		$"{nameof(Name)}: {Name}. ";

    public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((StatusModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew()
    {
        return Equals(new());
    }

    public override bool EqualsDefault()
    {
        return base.EqualsDefault() &&
               Equals(Name, string.Empty);
    }

    public override object Clone()
    {
        StatusModel item = new();
        item.Name = Name;
		item.CloneSetup(base.CloneCast());
		return item;
    }

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(StatusModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		return base.Equals(item) &&
		       Equals(Name, item.Name);
	}

	public new virtual StatusModel CloneCast() => (StatusModel)Clone();

    #endregion
}
