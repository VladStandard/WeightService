using System.Xml.Serialization;
namespace WsWebApiScales.Dto.Response;

public class ResponseError
{
    [XmlAttribute("Guid")]
    public Guid Guid { get; set; }

    [XmlAttribute("Message")]
    public string Message { get; set; }
}