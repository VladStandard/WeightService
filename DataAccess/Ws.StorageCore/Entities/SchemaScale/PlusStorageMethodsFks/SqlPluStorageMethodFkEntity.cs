// ReSharper disable VirtualMemberCallInConstructor
namespace Ws.StorageCore.Entities.SchemaScale.PlusStorageMethodsFks;

[DebuggerDisplay("{ToString()}")]
public class SqlPluStorageMethodFkEntity : SqlEntityBase
{
    public virtual SqlPluEntity Plu { get; set; }
    public virtual SqlPluStorageMethodEntity Method { get; set; }
    public virtual SqlTemplateResourceEntity Resource { get; set; }
    
    public SqlPluStorageMethodFkEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Plu = new();
        Method = new();
        Resource = new();
    }

    public SqlPluStorageMethodFkEntity(SqlPluStorageMethodFkEntity item) : base(item)
    {
        Plu = new(item.Plu);
        Method = new(item.Method);
        Resource = new(item.Resource);
    }
    
    public override string ToString() =>
        $"{nameof(Plu)}: {Plu}. " + 
        $"{nameof(Method)}: {Method}. " +
        $"{nameof(Resource)}: {Resource}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlPluStorageMethodFkEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public virtual bool Equals(SqlPluStorageMethodFkEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Plu.Equals(item.Plu) &&
        Method.Equals(item.Method) &&
        Resource.Equals(item.Resource);
}