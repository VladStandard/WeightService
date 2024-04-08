// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Models.Entities.Scale;

[DebuggerDisplay("{ToString()}")]
public class PluNestingEntity : EntityBase
{
    public virtual BoxEntity Box { get; set; } = new();
    public virtual PluEntity Plu { get; set; } = new();
    public virtual short BundleCount { get; set; }
    public virtual decimal WeightTare => (Plu.Bundle.Weight + Plu.Clip.Weight) * BundleCount + Box.Weight;

    protected override bool CastEquals(EntityBase obj)
    {
        PluNestingEntity item = (PluNestingEntity)obj;
        return Equals(Box, item.Box) &&
               Equals(Plu, item.Plu) &&
               Equals(BundleCount, item.BundleCount);
    }
}