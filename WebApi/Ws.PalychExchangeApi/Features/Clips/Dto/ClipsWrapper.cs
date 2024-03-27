using System.Xml.Serialization;

namespace Ws.PalychExchangeApi.Features.Clips.Dto;

[XmlRoot("Clip")]
public sealed class ClipsWrapper
{
    [XmlElement("Clip")]
    public List<ClipDto> Clips { get; set; } = [];
}