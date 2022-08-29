// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using static DataCore.ShareEnums;

namespace DataCore.Sql.Fields;

/// <summary>
/// DB table sorting model.
/// </summary>
public class FieldOrderModel
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

    #endregion

    #region Public and private methods

    protected virtual bool Equals(FieldOrderModel item)
    {
        if (ReferenceEquals(this, item)) return true;
        return
	        Equals(Name, item.Name) &&
            Direction.Equals(item.Direction);
    }

    public override bool Equals(object obj)
    {
	    if (ReferenceEquals(null, obj)) return false;
	    if (ReferenceEquals(this, obj)) return true;
	    if (obj.GetType() != GetType()) return false;
	    return Equals((FieldOrderModel)obj);
    }

    public override int GetHashCode() => (Name, Direction).GetHashCode();

    public override string ToString() => 
	    $"{nameof(Name)}: {Name}. " +
        $"{nameof(Direction)}: {Direction}. ";

    #endregion
}
