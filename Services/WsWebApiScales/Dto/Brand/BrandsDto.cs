using System.Xml.Serialization;

namespace WsWebApiScales.Dto.Brand;

[XmlRoot("Brands")]
public class BrandsDto
{
    [XmlElement("Brand")]
    public List<BrandDto> Brands { get; set; }
    
    [XmlAttribute("Count")]
    public int Count { get; set; }
}