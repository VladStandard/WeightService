// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global
using System.Diagnostics;
using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Models.Entities.Ref1c;

[DebuggerDisplay("{ToString()}")]
public class PluEntity : EntityBase
{
    public virtual Guid? TemplateUid { get; set; }
    public virtual short Number { get; set; }
    public virtual string FullName { get; set; } = string.Empty;
    public virtual short ShelfLifeDays { get; set; }
    public virtual string Ean13 { get; set; } = string.Empty;
    public virtual string Itf14 { get; set; } = string.Empty;
    public virtual bool IsCheckWeight { get; set; }
    public virtual BundleEntity Bundle { get; set; } = new();
    public virtual BrandEntity Brand { get; set; } = new();
    public virtual ClipEntity Clip { get; set; } = new();
    public virtual string StorageMethod { get; set; } = string.Empty;
    public virtual NestingEntity Nesting { get; set; } = new();
    public virtual ISet<CharacteristicEntity> Characteristics { get; set; } = new HashSet<CharacteristicEntity>();
    public virtual string Description { get; set; } = string.Empty;
    public virtual decimal Weight { get; set; }
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Gtin => IsCheckWeight ? $"0{Ean13}" : $"{Itf14}";

    public virtual IEnumerable<CharacteristicEntity> CharacteristicsWithNesting
    {
        get
        {
            List<CharacteristicEntity> characteristics = Characteristics.ToList();
            characteristics.Insert(0, Nesting.ToCharacteristic());
            return characteristics;
        }
    }

    public virtual string DisplayName => $"{Number} | {Name}";

    public virtual decimal DefaultWeightTare => CalculateTotalWeight(Nesting.Box, Nesting.BundleCount);

    public virtual decimal GetWeightWithCharacteristic(CharacteristicEntity characteristic) =>
        CalculateTotalWeight(characteristic.Box, characteristic.BundleCount);

    private decimal CalculateTotalWeight(BoxEntity box, short bundleCount) =>
        ((IsCheckWeight ? 0 : Weight) + Bundle.Weight + Clip.Weight) * bundleCount + box.Weight;

    protected override bool CastEquals(EntityBase obj)
    {
        PluEntity item = (PluEntity)obj;
        return Equals(StorageMethod, item.StorageMethod) &&
               Equals(Number, item.Number) &&
               Equals(Clip, item.Clip) &&
               Equals(TemplateUid, item.TemplateUid) &&
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