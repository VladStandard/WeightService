using System.Xml.Serialization;

namespace Ws.PalychExchangeApi.Features.Characteristics.Dto;

public sealed class PluCharacteristicsDto
{
    [XmlAttribute("Uid")]
    public Guid Uid { get; set; }

    [XmlElement("Plu")]
    public List<CharacteristicDto> Characteristics { get; set; } = [];
}