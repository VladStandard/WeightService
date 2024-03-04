// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Ref1c;

[DebuggerDisplay("{ToString()}")]
public class BrandEntity : Entity1CBase
{
    public virtual string Name { get; set; } = string.Empty;

    protected override bool CastEquals(EntityBase obj)
    {
        BrandEntity item = (BrandEntity)obj;
        return Equals(Name, item.Name);
    }
}