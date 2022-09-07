// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Fields;

/// <summary>
/// DB table sorting model.
/// </summary>
[Serializable]
public class SqlFieldOrderModel : SerializeBase, ICloneable, ISqlDbBase, ISerializable
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Name order.
    /// </summary>
    public SqlFieldEnum Name { get; private set; }
    /// <summary>
    /// Direction order.
    /// </summary>
    public SqlFieldOrderDirectionEnum Direction { get; private set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public SqlFieldOrderModel()
    {
	    Name = SqlFieldEnum.Empty;
	    Direction = SqlFieldOrderDirectionEnum.Asc;
    }

    /// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="name"></param>
	/// <param name="direction"></param>
	public SqlFieldOrderModel(SqlFieldEnum name, SqlFieldOrderDirectionEnum direction = SqlFieldOrderDirectionEnum.Asc)
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
        Name = (SqlFieldEnum)info.GetValue(nameof(Name), typeof(SqlFieldEnum));
        Direction = (SqlFieldOrderDirectionEnum)info.GetValue(nameof(Direction), typeof(SqlFieldOrderDirectionEnum));
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
		Equals(Name, SqlFieldIdentityEnum.Empty) &&
		Equals(Direction, SqlFieldOrderDirectionEnum.Asc);

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

	#endregion
}
