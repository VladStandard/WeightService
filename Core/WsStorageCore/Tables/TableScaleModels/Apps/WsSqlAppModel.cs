namespace WsStorageCore.Tables.TableScaleModels.Apps;

/// <summary>
/// Table "APPS".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlAppModel : WsSqlTableBase
{
    #region Public and private fields, properties, constructor

    public WsSqlAppModel() : base(WsSqlEnumFieldIdentity.Uid)
    {
        //
    }

    public WsSqlAppModel(WsSqlAppModel item) : base(item) { }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{GetIsMarked()} | {DisplayName}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlAppModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlAppModel item) =>
        ReferenceEquals(this, item) || base.Equals(item);

    #endregion
}
