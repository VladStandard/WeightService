namespace Ws.PalychExchange.Api.App.Features.Brands.Dto;

[XmlRoot("Brands")]
public sealed record BrandsWrapper
{
    [XmlElement("Brand")]
    public List<BrandDto> Brands { get; set; } = [];
}