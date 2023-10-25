namespace WsStorageCore.Entities.SchemaScale.TasksTypes;

/// <summary>
/// Table "TASKS_TYPES".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlTaskTypeEntity : WsSqlEntityBase
{
    #region Public and private fields, properties, constructor

    public WsSqlTaskTypeEntity() : base(WsSqlEnumFieldIdentity.Uid)
    {
        //
    }

    public WsSqlTaskTypeEntity(WsSqlTaskTypeEntity item) : base(item) { }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Name)}: {Name}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlTaskTypeEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlTaskTypeEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item);

    #endregion
}