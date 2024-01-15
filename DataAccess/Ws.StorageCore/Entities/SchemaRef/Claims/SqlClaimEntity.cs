// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
namespace Ws.StorageCore.Entities.SchemaRef.Claims;

[DebuggerDisplay("{ToString()}")]
public class SqlClaimEntity : SqlEntityBase
{
    public SqlClaimEntity() : base(SqlEnumFieldIdentity.Uid) {}

    public SqlClaimEntity(SqlClaimEntity item) : base(item) {}
    
    public override string ToString() => $"{Name}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlClaimEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public virtual bool Equals(SqlClaimEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item);
}