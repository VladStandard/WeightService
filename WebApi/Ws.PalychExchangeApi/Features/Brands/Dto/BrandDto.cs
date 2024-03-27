using System.Xml.Serialization;
using Ws.Database.EntityFramework.Entities.Ref1C.Brands;

namespace Ws.PalychExchangeApi.Features.Brands.Dto;

[Serializable]
public sealed class BrandDto
{
    [XmlAttribute("Uid")]
    public Guid Uid { get; set; }

    [XmlAttribute("IsDelete")]
    public bool IsDelete { get; set; }

    [XmlAttribute("Name")]
    public string Name { get; set; } = string.Empty;
}


internal static class BrandDtoExtensions
{
    internal static BrandEntity ToEntity(this BrandDto dto, DateTime updateDate) =>
        new(dto.Uid, dto.Name, updateDate);
}