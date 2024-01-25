using System.Xml.Serialization;

namespace Ws.WebApiScales.Features.Boxes.Dto;

[XmlRoot("Boxes")]
internal sealed class BoxWrapper
{
    [XmlElement("Box")]
    public List<BoxDto> Boxes { get; set; } = [];
}