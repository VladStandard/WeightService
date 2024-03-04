// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Ref;

[DebuggerDisplay("{ToString()}")]
public class TemplateEntity : EntityBase
{
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Body { get; set; } = string.Empty;

    protected override bool CastEquals(EntityBase obj)
    {
        TemplateEntity item = (TemplateEntity)obj;
        return Equals(Name, item.Name) && Equals(Body, item.Body);
    }
}