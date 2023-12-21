namespace Ws.WebApiScales.Dto.Response;

[Serializable]
public class ResponseSuccesses
{
    [XmlAttribute("Guid")]
    public Guid Guid { get; set; }
    
    [XmlAttribute("Message")]
    public string Message { get; set; } = string.Empty;
}