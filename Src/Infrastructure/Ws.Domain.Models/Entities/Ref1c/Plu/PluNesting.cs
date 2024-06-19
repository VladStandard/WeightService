using Ws.Domain.Models.Common;

namespace Ws.Domain.Models.Entities.Ref1c.Plu;

public class PluNesting : EntityBase
{
    public virtual Box Box { get; set; } = new();
    // public virtual PluEntity Plu { get; set; } = new();
    public virtual short BundleCount { get; set; }

    protected override bool CastEquals(EntityBase obj)
    {
        PluCharacteristic item = (PluCharacteristic)obj;
        return Equals(Box, item.Box) && // Equals(Plu, item.Plu) &&
               Equals(BundleCount, item.BundleCount);
    }
}

public static class NestingExtension
{
    public static PluCharacteristic ToCharacteristic(this PluNesting pluNesting) => new()
    { Name = "По умолчанию", Box = pluNesting.Box, BundleCount = pluNesting.BundleCount, PluUid = pluNesting.Uid };
}