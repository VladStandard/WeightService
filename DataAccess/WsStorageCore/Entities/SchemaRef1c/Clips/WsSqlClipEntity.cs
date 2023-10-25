namespace WsStorageCore.Entities.SchemaRef1c.Clips;

[DebuggerDisplay("{ToString()}")]
public class WsSqlClipEntity : WsSqlTable1CBase
{
    #region Public and private fields, properties, constructor

    public virtual decimal Weight { get; set; }

    public WsSqlClipEntity() : base(WsSqlEnumFieldIdentity.Uid)
    {
        Weight = 0;
    }
    
    public WsSqlClipEntity(WsSqlClipEntity item) : base(item)
    {
        Weight = item.Weight;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{GetIsMarked()} | {Name} | {Weight}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((WsSqlClipEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public new virtual bool EqualsDefault() =>
        base.EqualsDefault() && Equals(Weight, (decimal)0);

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(WsSqlClipEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Weight, item.Weight);
    
    #endregion
}