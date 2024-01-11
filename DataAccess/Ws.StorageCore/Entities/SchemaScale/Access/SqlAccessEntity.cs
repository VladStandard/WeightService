// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using Ws.Shared.Enums;

namespace Ws.StorageCore.Entities.SchemaScale.Access;

[DebuggerDisplay("{ToString()}")]
public class SqlAccessEntity : SqlEntityBase
{
    public virtual DateTime LoginDt { get; set; }
    public virtual byte Rights { get; set; }
    public virtual EnumAccessRights RightsEnum => (EnumAccessRights)Rights;
    
    public SqlAccessEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        LoginDt = DateTime.MinValue;
        Name = string.Empty;
        Rights = (byte)EnumAccessRights.None;
    }

    public SqlAccessEntity(SqlAccessEntity item) : base(item)
    {
        LoginDt = item.LoginDt;
        Rights = item.Rights;
    }
    
    public override string ToString() => $"{Name} | {RightsEnum}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlAccessEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public virtual bool Equals(SqlAccessEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(LoginDt, item.LoginDt) &&
        Equals(Rights, item.Rights);
}
