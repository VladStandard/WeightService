// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Abstractions.Entities.Common;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Models.Entities.Ref1c;

[DebuggerDisplay("{ToString()}")]
public class PluEntity() : Entity1CBase
{
    public virtual short Number { get; set; }
    public virtual string FullName { get; set; } = string.Empty;
    public virtual byte ShelfLifeDays { get; set; }
    public virtual string Gtin { get; set; } = string.Empty;
    public virtual string Ean13 { get; set; } = string.Empty;
    public virtual string Itf14 { get; set; } = string.Empty;
    public virtual bool IsCheckWeight { get; set; }
    public virtual BundleEntity Bundle { get; set; } = new();
    public virtual BrandEntity Brand { get; set; } = new();
    public virtual ClipEntity Clip { get; set; } = new();
    public virtual StorageMethodEntity StorageMethod { get; set; } = new();
    public virtual string Description { get; set; } = string.Empty;
    public virtual string Name { get; set; } = string.Empty;
    
    public virtual string DisplayName => $"{Number} | {Name}";

    public override string ToString() => DisplayName;

    protected override bool CastEquals(EntityBase obj)
    {
        PluEntity item = (PluEntity)obj;
        return Equals(StorageMethod, item.StorageMethod) &&
               Equals(Number, item.Number) &&
               Equals(Clip, item.Clip) &&
               Equals(Brand, item.Brand) &&
               Equals(Bundle, item.Bundle) &&
               Equals(FullName, item.FullName) &&
               Equals(ShelfLifeDays, item.ShelfLifeDays) &&
               Equals(Gtin, item.Gtin) &&
               Equals(Ean13, item.Ean13) &&
               Equals(Itf14, item.Itf14) &&
               Equals(Description, item.Description) &&
               Equals(Name, item.Name) &&
               Equals(IsCheckWeight, item.IsCheckWeight);
    }
}