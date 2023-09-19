namespace WsStorageCore.Tables.TableScaleModels.TasksTypes;

/// <summary>
/// Table "TASKS_TYPES".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlTaskTypeModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public WsSqlTaskTypeModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        //
    }

    public WsSqlTaskTypeModel(WsSqlTaskTypeModel item) : base(item) { }

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
        return Equals((WsSqlTaskTypeModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlTaskTypeModel item) =>
        ReferenceEquals(this, item) || base.Equals(item);

    #endregion
}