using Ws.Domain.Models.Common;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Domain.Models.Entities.Ref1c;

public class NestingEntity : EntityBase
{
    public virtual BoxEntity Box { get; set; } = new();
    // public virtual PluEntity Plu { get; set; } = new();
    public virtual short BundleCount { get; set; }

    protected override bool CastEquals(EntityBase obj)
    {
        CharacteristicEntity item = (CharacteristicEntity)obj;
        return Equals(Box, item.Box) && // Equals(Plu, item.Plu) &&
               Equals(BundleCount, item.BundleCount);
    }
}

public static class NestingExtension
{
    public static CharacteristicEntity ToCharacteristic(this NestingEntity nesting) => new()
        { Name = "По умолчанию", Box = nesting.Box, BundleCount = nesting.BundleCount, PluUid = nesting.Uid };
}