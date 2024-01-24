using System.Xml.Serialization;

namespace Ws.WebApiScales.Features.Clips.Dto;

[XmlRoot("Clips")]
internal sealed class ClipsWrapper
{
    [XmlElement("Clip")]
    public List<ClipDto> Clips { get; set; } = [];
}