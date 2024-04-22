// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Ref1c;

[DebuggerDisplay("{ToString()}")]
public class ClipEntity : EntityBase
{
    public virtual string Name { get; set; } = string.Empty;
    public virtual decimal Weight { get; set; }
    public override bool IsNew => CreateDt == DateTime.MinValue;

    protected override bool CastEquals(EntityBase obj)
    {
        ClipEntity item = (ClipEntity)obj;
        return Equals(Weight, item.Weight) && Equals(Name, item.Name);
    }
}