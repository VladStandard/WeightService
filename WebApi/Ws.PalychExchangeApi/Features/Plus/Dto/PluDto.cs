using Ws.Database.EntityFramework.Entities.Ref1C.Plus;

namespace Ws.PalychExchangeApi.Features.Plus.Dto;

[Serializable]
public sealed class PluDto
{
    public Guid Uid { get; set; }
    public string Name { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public short Number { get; set; }
    public short BundleCount { get; set; }
    public byte ShelfLifeDays { get; set; }

    #region Fk

    public Guid BrandUid { get; set; }
    public Guid BoxUid { get; set; }
    public Guid ClipUid { get; set; }
    public Guid BundleUid { get; set; }

    #endregion

    public string Ean13 { get; set; } = string.Empty;
    public string Itf14 { get; set; } = string.Empty;
    public bool IsWeight { get; set; }
    public string StorageMethod { get; set; } = string.Empty;
}


internal static class PluDtoExtensions
{
    internal static PluEntity ToEntity(this PluDto dto, DateTime updateDt) =>
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
            BrandEntityId = dto.BrandUid,
            ClipEntityId = dto.ClipUid,
            BundleEntityId = dto.BundleUid
        };
}