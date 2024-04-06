using System.Xml.Serialization;

namespace Ws.PalychExchangeApi.Features.Characteristics.Dto.PluCharacteristicsWrapper;

[XmlRoot("Characteristics")]
public sealed record PluCharacteristicsWrapper
{
    [XmlElement("Plu")]
    public List<PluCharacteristicsDto> PluCharacteristics { get; set; } = [];
}