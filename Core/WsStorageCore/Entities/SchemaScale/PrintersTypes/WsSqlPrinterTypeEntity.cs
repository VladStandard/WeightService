namespace WsStorageCore.Entities.SchemaScale.PrintersTypes;

[DebuggerDisplay("{ToString()}")]
public class WsSqlPrinterTypeEntity : WsSqlEntityBase
{
    #region Public and private fields, properties, constructor
    
    public WsSqlPrinterTypeEntity() : base(WsSqlEnumFieldIdentity.Id)
    {
        //
    }

    public WsSqlPrinterTypeEntity(WsSqlPrinterTypeEntity item) : base(item) { }

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
        return Equals((WsSqlPrinterTypeEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlPrinterTypeEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item);

    #endregion
}
