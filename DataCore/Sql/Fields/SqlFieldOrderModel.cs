// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.Fields;

/// <summary>
/// DB table sorting model.
/// </summary>
[Serializable]
public class SqlFieldOrderModel : SerializeBase, ICloneable, ISqlDbBase, ISerializable
{
    #region Public and private fields, properties, constructor

    public string Name { get; private set; }
    public SqlFieldOrderEnum Direction { get; private set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public SqlFieldOrderModel()
    {
	    Name = string.Empty;
	    Direction = SqlFieldOrderEnum.Asc;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="direction"></param>
    public SqlFieldOrderModel(string name, SqlFieldOrderEnum direction)
    {
        Name = name;
        Direction = direction;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected SqlFieldOrderModel(SerializationInfo info, StreamingContext context)// : base(info, context)
    {
        Name = info.GetString(nameof(Name));
        Direction = (SqlFieldOrderEnum)info.GetValue(nameof(Direction), typeof(SqlFieldOrderEnum));
    }

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
		$"{nameof(Name)}: {Name}. " +
        $"{nameof(Direction)}: {Direction}. ";

    public override bool Equals(object obj)
	{
	    if (ReferenceEquals(null, obj)) return false;
	    if (ReferenceEquals(this, obj)) return true;
	    if (obj.GetType() != GetType()) return false;
	    return Equals((SqlFieldOrderModel)obj);
    }

	public override int GetHashCode() => (Name, Direction).GetHashCode();

	public virtual bool EqualsNew() => Equals(new());

	/// <summary>
	/// Get object data for serialization info.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
	public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        //base.GetObjectData(info, context);
        info.AddValue(nameof(Name), Name);
        info.AddValue(nameof(Direction), Direction);
    }

	public virtual bool EqualsDefault() =>
		Equals(Name, string.Empty) &&
		Equals(Direction, SqlFieldOrderEnum.Asc);

	public virtual object Clone() => new SqlFieldOrderModel(Name, Direction);

	#endregion

	#region Public and private methods - virtual

	protected virtual bool Equals(SqlFieldOrderModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		return
            Equals(Name, item.Name) &&
			Direction.Equals(item.Direction);
	}

	public virtual SqlFieldOrderModel CloneCast() => (SqlFieldOrderModel)Clone();

	public virtual void ClearNullProperties()
	{
		throw new NotImplementedException();
	}

	#endregion
}
