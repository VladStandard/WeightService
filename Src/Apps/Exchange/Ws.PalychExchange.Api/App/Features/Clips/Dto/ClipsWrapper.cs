namespace Ws.PalychExchange.Api.App.Features.Clips.Dto;

[XmlRoot("Clips")]
public sealed class ClipsWrapper
{
    [XmlElement("Clip")]
    public HashSet<ClipDto> Clips { get; set; } = [];
}