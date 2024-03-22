using System.Xml.Serialization;

namespace Ws.WebApiScales.Features.Characteristics.Dto;

internal sealed class PluCharacteristicDto
{
    [XmlAttribute("Uid")]
    public Guid Uid { get; set; }

    [XmlElement("Plu")]
    public List<CharacteristicDto> Characteristics { get; set; } = [];
}