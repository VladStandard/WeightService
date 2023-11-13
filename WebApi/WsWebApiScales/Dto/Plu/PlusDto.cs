using System.Xml.Serialization;

namespace WsWebApiScales.Dto.Plu;

[XmlRoot("Nomenclatures")]
public class PlusDto
{
    [XmlElement("Nomenclature")]
    public List<PluDto> plus { get; set; }
    
    [XmlAttribute("Count")]
    public int Count { get; set; }
}