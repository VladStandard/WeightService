using System.Xml.Serialization;

namespace Ws.WebApiScales.Features.Box.Dto;

[XmlRoot("Boxes")]
internal sealed class BoxWrapper
{
    [XmlElement("Box")]
    public List<BoxDto> Boxes { get; set; } = [];
}