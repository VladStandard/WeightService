using System.Xml.Serialization;
namespace WsWebApiScales.Dto.PluCharacteristic;

[XmlRoot("Characteristics")]
public class PluCharacteristicsDto
{
    [XmlElement("Characteristic")]
    public List<PluCharacteristicDto> Characteristics { get; set; }
}