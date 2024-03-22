using System.Xml.Serialization;

namespace Ws.WebApiScales.Dto;

[Serializable]
public sealed class ResponseError
{
    [XmlAttribute("Guid")]
    public Guid Guid { get; set; }

    [XmlAttribute("Message")]
    public string Message { get; set; } = string.Empty;
}