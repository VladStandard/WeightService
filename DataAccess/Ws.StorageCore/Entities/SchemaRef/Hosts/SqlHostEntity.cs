// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
namespace Ws.StorageCore.Entities.SchemaRef.Hosts;

[DebuggerDisplay("{ToString()}")]
public class SqlHostEntity : SqlEntityBase
{
    public virtual DateTime LoginDt { get; set; }
    public virtual string Ip { get; set; }
    public override string DisplayName => IsNew ?  string.Empty : $"{Name} | {Ip}";
    
    public SqlHostEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        LoginDt = DateTime.MinValue;
        Ip = string.Empty;
    }

    public SqlHostEntity(SqlHostEntity item) : base(item)
    {
        LoginDt = item.LoginDt;
        Ip = item.Ip;
    }
    
    public override string ToString() => $"{Name}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlHostEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();
    
    public virtual bool Equals(SqlHostEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(LoginDt, item.LoginDt) &&
        Equals(Ip, item.Ip);
    
}