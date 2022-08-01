// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using static DataCore.ShareEnums;

namespace DataCore.Sql.Models;

/// <summary>
/// DB field order entity.
/// </summary>
public class FieldOrderEntity
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Is enabled order.
    /// </summary>
    public bool IsEnabled { get; set; }
    /// <summary>
    /// Name order.
    /// </summary>
    public DbField Name { get; set; }
    /// <summary>
    /// Direction order.
    /// </summary>
    public DbOrderDirection Direction { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="direction"></param>
    public FieldOrderEntity(DbField name, DbOrderDirection direction = DbOrderDirection.Asc)
    {
        IsEnabled = true;
        Name = name;
        Direction = direction;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public FieldOrderEntity()
    {
        IsEnabled = false;
    }

    #endregion

    #region Public and private methods

    public virtual bool Equals(FieldOrderEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return IsEnabled.Equals(item.IsEnabled) &&
               Name.Equals(item.Name) &&
               Direction.Equals(item.Direction);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((FieldOrderEntity)obj);
    }

    public override int GetHashCode() => Name.GetHashCode();

    public override string ToString() =>
        $"{nameof(IsEnabled)}: {IsEnabled}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Direction)}: {Direction}. ";

    #endregion
}
