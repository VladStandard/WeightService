using System.Xml.Serialization;

namespace Ws.PalychExchangeApi.Features.Clips.Dto;

[Serializable]
public sealed class ClipDto
{
    [XmlAttribute("Uid")]
    public Guid Uid { get; set; }

    [XmlAttribute("Name")]
    public string Name { get; set; } = string.Empty;

    [XmlAttribute("Weight")]
    public decimal Weight { get; set; }
}