using System.Xml.Serialization;

namespace Ws.WebApiScales.Features.Brands.Dto;

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