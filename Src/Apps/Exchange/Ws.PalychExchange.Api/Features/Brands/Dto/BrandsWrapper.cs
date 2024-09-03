using System.Xml.Serialization;

namespace Ws.PalychExchange.Api.Features.Brands.Dto;

[XmlRoot("Brands")]
public sealed record BrandsWrapper
{
    [XmlElement("Brand")]
    public List<BrandDto> Brands { get; set; } = [];
}