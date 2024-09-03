using System.Xml.Serialization;

namespace Ws.PalychExchange.Api.Features.Clips.Dto;

[XmlRoot("Clips")]
public sealed class ClipsWrapper
{
    [XmlElement("Clip")]
    public List<ClipDto> Clips { get; set; } = [];
}