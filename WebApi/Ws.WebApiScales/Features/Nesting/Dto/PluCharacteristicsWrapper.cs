using System.Xml.Serialization;
using Ws.WebApiScales.Common;

namespace Ws.WebApiScales.Features.Nesting.Dto;

[XmlRoot("Characteristics")]
public class PluCharacteristicsWrapper : BaseWrapper
{
    [XmlElement("Characteristic")]
    public List<PluCharacteristicDto> Characteristics { get; set; } = [];
}