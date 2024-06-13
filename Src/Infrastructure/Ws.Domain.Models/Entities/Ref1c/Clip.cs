// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Ref1c;

[DebuggerDisplay("{ToString()}")]
public class Clip : EntityBase
{
    public virtual string Name { get; set; } = string.Empty;
    public virtual decimal Weight { get; set; }
    public override bool IsNew => CreateDt == DateTime.MinValue;

    protected override bool CastEquals(EntityBase obj)
    {
        Clip item = (Clip)obj;
        return Equals(Weight, item.Weight) && Equals(Name, item.Name);
    }
}