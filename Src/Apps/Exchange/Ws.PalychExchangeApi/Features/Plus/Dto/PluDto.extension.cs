using Ws.Database.EntityFramework.Entities.Ref1C.Nestings;
using Ws.Database.EntityFramework.Entities.Ref1C.Plus;

namespace Ws.PalychExchangeApi.Features.Plus.Dto;

internal static class PluDtoExtensions
{
    internal static PluEntity ToPluEntity(this PluDto dto, DateTime updateDt) =>
        new(dto.Uid, updateDt)
        {
            IsWeight = dto.IsWeight,
            Ean13 = dto.Ean13,
            Itf14 = dto.Itf14,
            Name = dto.Name,
            FullName = dto.FullName,
            Description = dto.Description,
            Number = dto.Number,
            ShelfLifeDays = dto.ShelfLifeDays,
            BrandId = dto.BrandUid,
            ClipId = dto.ClipUid,
            BundleId = dto.BundleUid,
            Weight = dto.Weight,
            StorageMethod = dto.StorageMethod
        };

    internal static NestingEntity ToNestingEntity(this PluDto dto, DateTime updateDt) =>
        new(dto.Uid, updateDt)
        {
            BoxId = dto.BoxUid,
            BundleCount = dto.BundleCount
        };
}