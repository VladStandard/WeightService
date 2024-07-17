// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Ref1c.Plus;

namespace Ws.Domain.Models.Entities.Devices.Arms;

[DebuggerDisplay("{ToString()}")]
public class ArmPlu : EntityBase
{
    public virtual Plu Plu { get; set; } = new();
    public virtual Arm Line { get; set; } = new();
    public override bool IsNew => Plu.IsNew && Line.IsNew;

    protected override bool CastEquals(EntityBase obj)
    {
        ArmPlu item = (ArmPlu)obj;
        return Equals(Plu, item.Plu) && Equals(Line, item.Line);
    }
}