namespace Ws.PalychExchange.Api.App.Shared.Dto;

[Serializable]
public record ResponseError(Guid Uid, string Message)
{
    [XmlAttribute("Guid")]
    public Guid Uid = Uid;

    [XmlAttribute("Message")]
    public string Message = Message;
}