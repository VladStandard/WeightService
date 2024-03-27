using System.Xml.Serialization;

namespace Ws.PalychExchangeApi.Features.Nestings.Dto;

[XmlRoot("Characteristics")]
internal sealed class CharacteristicsWrapper
{
    [XmlElement("Plu")]
    public List<PluCharacteristicDto> PluCharacteristics { get; set; } = [];
}