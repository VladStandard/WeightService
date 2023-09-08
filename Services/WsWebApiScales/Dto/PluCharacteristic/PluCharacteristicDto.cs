using System.Xml.Serialization;
using WsWebApiScales.Utils;

namespace WsWebApiScales.Dto.PluCharacteristic;

[Serializable]
public class PluCharacteristicDto
{
    [XmlAttribute("GUID")]
    public Guid Guid { get; set; }

    [XmlAttribute("IsMarked")]
    public int IsMarked { get; set; }

    [XmlAttribute("NomenclatureGuid")]
    public Guid PluGuid { get; set; }

    [XmlAttribute("Name")]
    public string Name { get; set; } = string.Empty;

    [XmlAttribute("AttachmentsCount")]
    public string AttachmentsCount { get; set; } = string.Empty;
    
    public decimal AttachmentsCountAsDecimal => CastUtil.StringToDecimal(AttachmentsCount);
 
    public bool IsMarkedAsBool => IsMarked != 0;
}