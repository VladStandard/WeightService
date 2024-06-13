// ReSharper disable VirtualMemberCallInConstructor, ClassWithVirtualMembersNeverInherited.Global

using System.Diagnostics;
using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Ref1c.Plu;

[DebuggerDisplay("{ToString()}")]
public class Plu : EntityBase
{
    public virtual Guid? TemplateUid { get; set; }
    public virtual short Number { get; set; }
    public virtual decimal Weight { get; set; }
    public virtual bool IsCheckWeight { get; set; }
    public virtual short ShelfLifeDays { get; set; }
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Ean13 { get; set; } = string.Empty;
    public virtual string Itf14 { get; set; } = string.Empty;
    public virtual string FullName { get; set; } = string.Empty;
    public virtual string Description { get; set; } = string.Empty;
    public virtual string StorageMethod { get; set; } = string.Empty;

    public virtual Clip Clip { get; set; } = new();
    public virtual Brand Brand { get; set; } = new();
    public virtual Bundle Bundle { get; set; } = new();
    public virtual PluNesting PluNesting { get; set; } = new();
    public virtual ISet<PluCharacteristic> Characteristics { get; set; } = new HashSet<PluCharacteristic>();

    #region Getters

    public virtual string Gtin => IsCheckWeight ? $"0{Ean13}" : $"{Itf14}";

    public virtual IEnumerable<PluCharacteristic> CharacteristicsWithNesting =>
        Characteristics.Prepend(PluNesting.ToCharacteristic());

    public virtual string DisplayName => $"{Number} | {Name}";

    public virtual decimal GetWeightWithNesting => CalculateTotalWeight(PluNesting.Box, PluNesting.BundleCount);

    #endregion

    public virtual decimal GetTareWeightByCharacteristic(PluCharacteristic pluCharacteristic) =>
        (Bundle.Weight + Clip.Weight) * pluCharacteristic.BundleCount + pluCharacteristic.Box.Weight;

    public virtual decimal GetWeightByCharacteristic(PluCharacteristic pluCharacteristic) =>
        CalculateTotalWeight(pluCharacteristic.Box, pluCharacteristic.BundleCount);

    private decimal CalculateTotalWeight(Box box, short bundleCount) =>
        ((IsCheckWeight ? 0 : Weight) + Bundle.Weight + Clip.Weight) * bundleCount + box.Weight;

    protected override bool CastEquals(EntityBase obj)
    {
        Plu item = (Plu)obj;
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