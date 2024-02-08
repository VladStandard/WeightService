// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;

namespace Ws.Domain.Models.Entities.Ref;

[DebuggerDisplay("{ToString()}")]
public class ProductionSiteEntity() : EntityBase(SqlEnumFieldIdentity.Uid)
{
    public virtual string Address { get; set; } = string.Empty;

    public override string ToString() => $"{Address}";

    protected override bool CastEquals(EntityBase obj)
    {
        ProductionSiteEntity item = (ProductionSiteEntity)obj;
        return Equals(Address, item.Address);
    }
}