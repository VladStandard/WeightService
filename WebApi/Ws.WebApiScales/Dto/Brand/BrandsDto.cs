using System.Xml.Serialization;

namespace Ws.WebApiScales.Dto.Brand;

[XmlRoot("Brands")]
public class BrandsDto
{
    [XmlElement("Brand")]
    public List<BrandDto> Brands { get; set; }
    
    [XmlAttribute("Count")]
    public int Count { get; set; }
}