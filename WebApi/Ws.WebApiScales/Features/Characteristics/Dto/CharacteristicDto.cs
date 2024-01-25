using System.Xml.Serialization;

namespace Ws.WebApiScales.Features.Characteristics.Dto;

[Serializable]
public sealed class CharacteristicDto
{
    [XmlAttribute("Uid")]
    public Guid Uid { get; set; }

    [XmlAttribute("IsDelete")]
    public bool IsDelete { get; set; }

    [XmlAttribute("BundleCount")]
    public int BundleCount { get; set; }
}