using Ws.WebApiScales.Common;

namespace Ws.WebApiScales.Dto.PluCharacteristic;

[XmlRoot("Characteristics")]
public class PluCharacteristicsWrapper : BaseWrapper
{
    [XmlElement("Characteristic")]
    public List<PluCharacteristicDto> Characteristics { get; set; } = new();
}