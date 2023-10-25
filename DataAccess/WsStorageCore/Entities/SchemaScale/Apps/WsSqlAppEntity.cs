namespace WsStorageCore.Entities.SchemaScale.Apps;

/// <summary>
/// Table "APPS".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlAppEntity : WsSqlEntityBase
{
    #region Public and private fields, properties, constructor

    public WsSqlAppEntity() : base(WsSqlEnumFieldIdentity.Uid)
    {
        //
    }

    public WsSqlAppEntity(WsSqlAppEntity item) : base(item) { }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{GetIsMarked()} | {DisplayName}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlAppEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlAppEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item);

    #endregion
}
