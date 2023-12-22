namespace Ws.StorageCore.Entities.SchemaRef1c.Clips;

[DebuggerDisplay("{ToString()}")]
public class SqlClipEntity : SqlTable1CBase
{
    public virtual decimal Weight { get; set; }

    public SqlClipEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Weight = 0;
    }
    
    public SqlClipEntity(SqlClipEntity item) : base(item)
    {
        Weight = item.Weight;
    }
    
    public override string ToString() => $"{Name} | {Weight}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlClipEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();
    
    public virtual bool Equals(SqlClipEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Weight, item.Weight);
}