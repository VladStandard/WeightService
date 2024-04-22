// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Ref;

[DebuggerDisplay("{ToString()}")]
public class TemplateEntity : EntityBase
{
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Body { get; set; } = string.Empty;

    public virtual bool IsWeight { get; set; } = false;
    public virtual short Width { get; set; }
    public virtual short Height { get; set; }

    public virtual string SizeView => $"{Width}x{Height}";

    protected override bool CastEquals(EntityBase obj)
    {
        TemplateEntity item = (TemplateEntity)obj;
        return Equals(Name, item.Name) && Equals(Body, item.Body);
    }
}