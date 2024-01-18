// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

namespace Ws.StorageCore.Entities.SchemaRef.StorageMethods;

[Serializable]
[DebuggerDisplay("{ToString()}")]
public class SqlStorageMethodEntity : SqlEntityBase
{
    public virtual string Zpl { get; set; }
    
    public SqlStorageMethodEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Zpl = string.Empty;
    }

    public SqlStorageMethodEntity(SqlStorageMethodEntity item) : base(item)
    {
        Zpl = item.Zpl;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlStorageMethodEntity)obj);
    }

    public override int GetHashCode() => IdentityValueUid.GetHashCode();
    
    public virtual bool Equals(SqlStorageMethodEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Zpl, item.Zpl);
}