namespace Ws.StorageCore.Entities.SchemaScale.Apps;

/// <summary>
/// Table "APPS".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class SqlAppEntity : SqlEntityBase
{
    #region Public and private fields, properties, constructor

    public SqlAppEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        //
    }

    public SqlAppEntity(SqlAppEntity item) : base(item) { }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{GetIsMarked()} | {DisplayName}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlAppEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(SqlAppEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item);

    #endregion
}
