// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Models.Entities.Scale;

[DebuggerDisplay("{ToString()}")]
public class PluNestingEntity() : Entity1CBase(SqlEnumFieldIdentity.Uid)
{
    public virtual BoxEntity Box { get; set; } = new();
    public virtual PluEntity Plu { get; set; } = new();
    public virtual short BundleCount { get; set; }
    public virtual bool IsDefault => Uid1C.Equals(Guid.Empty);
    public override string Name => $"{Plu.Bundle.Name} | {Box.Name}";
    public virtual decimal WeightTare => (Plu.Bundle.Weight + Plu.Clip.Weight) * BundleCount + Box.Weight;

    public override string ToString() =>
        $"{Plu.Number} | {Plu.Name} | " +
        $"{Plu.Bundle.Weight} * {BundleCount} + {Box.Weight} = {WeightTare}";
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluNestingEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public virtual bool Equals(PluNestingEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Box.Equals(item.Box) &&
        Plu.Equals(item.Plu) && 
        Equals(IsDefault, item.IsDefault) &&
        Equals(BundleCount, item.BundleCount);
}