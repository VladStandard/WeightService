using Ws.WebApiScales.Common;

namespace Ws.WebApiScales.Dto.Brand;

[XmlRoot("Brands")]
public class BrandsWrapper : BaseWrapper
{
    [XmlElement("Brand")]
    public List<BrandDto> Brands { get; set; } = new();
}