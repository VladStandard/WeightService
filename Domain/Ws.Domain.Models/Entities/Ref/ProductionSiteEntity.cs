// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Ref;

[DebuggerDisplay("{ToString()}")]
public class ProductionSiteEntity : EntityBase
{
    public virtual string Address { get; set; }
    
    public ProductionSiteEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Address = string.Empty;
    }

    public ProductionSiteEntity(ProductionSiteEntity item) : base(item)
    {
        Address = item.Address;
    }
    
    public override string ToString() => $"{Address}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ProductionSiteEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();
    
    public virtual bool Equals(ProductionSiteEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Address, item.Address);
    
}