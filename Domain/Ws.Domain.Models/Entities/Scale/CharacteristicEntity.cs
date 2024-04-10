// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Models.Entities.Scale;

[DebuggerDisplay("{ToString()}")]
public class CharacteristicEntity : EntityBase
{
    public virtual BoxEntity Box { get; set; } = new();
    public virtual PluEntity Plu { get; set; } = new();
    public virtual short BundleCount { get; set; }
    public virtual string Name { get; set; } = string.Empty;
    public virtual decimal WeightTare => (Plu.Weight + Plu.Bundle.Weight + Plu.Clip.Weight) * BundleCount + Box.Weight;

    protected override bool CastEquals(EntityBase obj)
    {
        CharacteristicEntity item = (CharacteristicEntity)obj;
        return Equals(Box, item.Box) &&
               Equals(Name, item.Name) &&
               Equals(Plu, item.Plu) &&
               Equals(BundleCount, item.BundleCount);
    }
}