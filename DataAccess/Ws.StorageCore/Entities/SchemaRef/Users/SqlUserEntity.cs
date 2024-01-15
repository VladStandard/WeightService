// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

namespace Ws.StorageCore.Entities.SchemaRef.Users;

[DebuggerDisplay("{ToString()}")]
public class SqlUserEntity : SqlEntityBase
{
    public virtual DateTime LoginDt { get; set; }
    
    public SqlUserEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        LoginDt = DateTime.MinValue;
    }

    public SqlUserEntity(SqlUserEntity item) : base(item)
    {
        LoginDt = item.LoginDt;
    }
    
    public override string ToString() => $"{Name}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlUserEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public virtual bool Equals(SqlUserEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(LoginDt, item.LoginDt);

}