using System.Xml.Serialization;
using Ws.WebApiScales.Common;

namespace Ws.WebApiScales.Features.Characteristics.Dto;

[XmlRoot("Characteristics")]
internal sealed class CharacteristicsWrapper : BaseWrapper
{
    [XmlElement("Plu")]
    public List<PluCharacteristicDto> PluCharacteristics { get; set; } = [];
}