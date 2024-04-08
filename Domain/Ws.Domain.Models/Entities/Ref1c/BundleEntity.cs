// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Ref1c;

[DebuggerDisplay("{ToString()}")]
public class BundleEntity : EntityBase
{
    public virtual decimal Weight { get; set; }
    public virtual string Name { get; set; } = string.Empty;

    public override string ToString() => $"{Name} | {Weight}";

    protected override bool CastEquals(EntityBase obj)
    {
        BundleEntity item = (BundleEntity)obj;
        return Equals(Weight, item.Weight) && Equals(Name, item.Name);
    }
}