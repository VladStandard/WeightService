using System.Xml.Serialization;
using Ws.Database.EntityFramework.Entities.Ref1C.Nestings;
using Ws.Database.EntityFramework.Entities.Ref1C.Plus;

namespace Ws.PalychExchangeApi.Features.Plus.Dto;

[Serializable]
public sealed class PluDto
{
    [XmlAttribute("Uid")]
    public Guid Uid { get; set; }

    [XmlAttribute("Name")]
    public string Name { get; set; } = string.Empty;

    [XmlAttribute("FullName")]
    public string FullName { get; set; } = string.Empty;

    [XmlAttribute("Description")]
    public string Description { get; set; } = string.Empty;

    [XmlAttribute("Number")]
    public short Number { get; set; }

    [XmlAttribute("BundleCount")]
    public short BundleCount { get; set; }

    [XmlAttribute("ShelfLife")]
    public short ShelfLifeDays { get; set; }

    #region Fk

    [XmlAttribute("BrandUid")]
    public Guid BrandUid { get; set; }

    [XmlAttribute("BoxUid")]
    public Guid BoxUid { get; set; }

    [XmlAttribute("ClipUid")]
    public Guid ClipUid { get; set; }

    [XmlAttribute("BundleUid")]
    public Guid BundleUid { get; set; }

    #endregion

    [XmlAttribute("Ean13")]
    public string Ean13 { get; set; } = string.Empty;

    [XmlAttribute("Itf14")]
    public string Itf14 { get; set; } = string.Empty;

    [XmlAttribute("IsWeight")]
    public bool IsWeight { get; set; }

    [XmlAttribute("StorageMethod")]
    public string StorageMethod { get; set; } = string.Empty;
}


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
            BrandEntityId = dto.BrandUid,
            ClipEntityId = dto.ClipUid,
            BundleEntityId = dto.BundleUid
        };

    internal static NestingEntity ToNestingEntity(this PluDto dto, DateTime updateDt) =>
        new(dto.Uid, updateDt)
        {
            BoxId = dto.BoxUid,
            BundleCount = dto.BundleCount
        };
}