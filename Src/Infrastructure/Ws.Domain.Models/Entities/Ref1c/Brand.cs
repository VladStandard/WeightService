// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Ref1c;

[DebuggerDisplay("{ToString()}")]
public class Brand : EntityBase
{
    public virtual string Name { get; set; } = string.Empty;

    protected override bool CastEquals(EntityBase obj)
    {
        Brand item = (Brand)obj;
        return Equals(Name, item.Name);
    }
}