// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Shared.Enums;

namespace Ws.Domain.Models.Entities.Print;

[DebuggerDisplay("{ToString()}")]
public class ZplResource : EntityBase
{
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Zpl { get; set; } = string.Empty;
    public virtual ZplResourceType Type { get; set; } = ZplResourceType.Text;

    protected override bool CastEquals(EntityBase obj)
    {
        ZplResource item = (ZplResource)obj;
        return Equals(Zpl, item.Zpl) &&
               Equals(Name, item.Name) &&
               Equals(Type, item.Type);
    }
}