namespace Ws.StorageCore.Entities.SchemaRef1c.Brands;

[DebuggerDisplay("{ToString()}")]
public class SqlBrandEntity : SqlTable1CBase
{
    public virtual string Code { get; set; }

    public SqlBrandEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Code = string.Empty;
    }

    public SqlBrandEntity(SqlBrandEntity item) : base(item)
    {
        Code = item.Code;
    }

    public override string ToString() =>
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(Code)}: {Code}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlBrandEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();
    
    public virtual bool Equals(SqlBrandEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&//-V3130
        Equals(Code, item.Code);
}