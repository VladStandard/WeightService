using System.Xml.Serialization;

namespace Ws.PalychExchangeApi.Features.Characteristics.Dto;

public sealed record PluCharacteristicsDto
{
    [XmlAttribute("Uid")]
    public Guid Uid { get; set; }

    [XmlElement("Characteristic")]
    public List<CharacteristicDto> Characteristics { get; set; } = [];
}