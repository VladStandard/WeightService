// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;
using Ws.Domain.Models.Models;

namespace Ws.Domain.Models.Entities.Scale;

[DebuggerDisplay("{ToString()}")]
public class TemplateResourceEntity : EntityBase
{
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Zpl { get; set; } = string.Empty;

    protected override bool CastEquals(EntityBase obj)
    {
        TemplateResourceEntity item = (TemplateResourceEntity)obj;
        return Equals(Zpl, item.Zpl) && Equals(Name, item.Name);
    }
}