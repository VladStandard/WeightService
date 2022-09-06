// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using DataCore.Sql.Core;
using DataCore.Sql.Tables;
using static DataCore.ShareEnums;

namespace DataCore.Sql.Fields;

/// <summary>
/// DB table sorting model.
/// </summary>
[Serializable]
public class FieldOrderModel : SerializeModel, ICloneable, IDbBaseModel, ISerializable
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Name order.
    /// </summary>
    public DbField Name { get; private set; }
    /// <summary>
    /// Direction order.
    /// </summary>
    public DbOrderDirection Direction { get; private set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public FieldOrderModel()
    {
	    Name = DbField.Default;
	    Direction = DbOrderDirection.Asc;
    }

    /// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="name"></param>
	/// <param name="direction"></param>
	public FieldOrderModel(DbField name, DbOrderDirection direction = DbOrderDirection.Asc)
    {
        Name = name;
        Direction = direction;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected FieldOrderModel(SerializationInfo info, StreamingContext context)// : base(info, context)
    {
        Name = (DbField)info.GetValue(nameof(Name), typeof(DbField));
        Direction = (DbOrderDirection)info.GetValue(nameof(Direction), typeof(DbOrderDirection));
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
	    return Equals((FieldOrderModel)obj);
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
		Equals(Name, ColumnName.Default) &&
		Equals(Direction, DbOrderDirection.Asc);

	public virtual object Clone() => new FieldOrderModel(Name, Direction);

	#endregion

	#region Public and private methods - virtual

	protected virtual bool Equals(FieldOrderModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		return
			Equals(Name, item.Name) &&
			Direction.Equals(item.Direction);
	}

	public virtual FieldOrderModel CloneCast() => (FieldOrderModel)Clone();

	#endregion
}
