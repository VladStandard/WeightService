// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Ref1c.Plu;

namespace Ws.Domain.Models.Entities.Devices.Arms;

[DebuggerDisplay("{ToString()}")]
public class ArmLine : EntityBase
{
    public virtual Plu Plu { get; set; } = new();
    public virtual Arm Line { get; set; } = new();

    protected override bool CastEquals(EntityBase obj)
    {
        ArmLine item = (ArmLine)obj;
        return Equals(Plu, item.Plu) && Equals(Line, item.Line);
    }
}