using System.Xml.Serialization;

namespace WsWebApiScales.Dto.Response;

public class ResponseSuccesses
{
    [XmlAttribute("Guid")]
    public Guid Guid { get; set; }
}