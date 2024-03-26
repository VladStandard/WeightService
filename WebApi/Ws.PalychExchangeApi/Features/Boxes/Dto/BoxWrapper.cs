using System.Xml.Serialization;

namespace Ws.PalychExchangeApi.Features.Boxes.Dto;

[XmlRoot("Boxes")]
public sealed record BoxWrapper
{
    [XmlElement("Box")]
    public List<BoxDto> Boxes { get; set; } = [];
}