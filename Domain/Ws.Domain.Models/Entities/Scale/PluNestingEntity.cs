// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.Domain.Models.Entities.Scale;

[DebuggerDisplay("{ToString()}")]
public class PluNestingEntity : EntityBase
{
    public virtual BoxEntity Box { get; set; }
    public virtual PluEntity Plu { get; set; }
    public virtual bool IsDefault { get; set; }
    public virtual short BundleCount { get; set; }
    public virtual Guid Uid1C { get; set; }
    public override string Name => $"{Plu.Bundle.Name} | {Box.Name}";
    public virtual decimal WeightTare { get => Plu.Bundle.Weight * BundleCount + Box.Weight; set => _ = value; }
    
    public PluNestingEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Box = new();
        //Plu = new();
        Plu = new();
        IsDefault = false;
        BundleCount = 0;
    }
    
    public PluNestingEntity(PluNestingEntity item) : base(item)
    {
        Box = new(item.Box);
        Plu = new(item.Plu);
        IsDefault = item.IsDefault;
        BundleCount = item.BundleCount;
    }
    
    public override string ToString() =>
        $"{GetIsDefault()} | {Plu.Number} | {Plu.Name} | " +
        $"{Plu.Bundle.Weight} * {BundleCount} + {Box.Weight} = {WeightTare}";
        //$" | {PluBundle.Bundle.Name} * {BundleCount} + {Box.Name}";

    protected virtual string GetIsDefault() => IsDefault ? "Is default" : "No default";


    public override bool Equals(object obj)
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