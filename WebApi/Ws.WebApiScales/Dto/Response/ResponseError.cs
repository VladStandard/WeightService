namespace Ws.WebApiScales.Dto.Response;

[Serializable]
public class ResponseError
{
    [XmlAttribute("Guid")]
    public Guid Guid { get; set; }

    [XmlAttribute("Message")]
    public string Message { get; set; } = string.Empty;
}