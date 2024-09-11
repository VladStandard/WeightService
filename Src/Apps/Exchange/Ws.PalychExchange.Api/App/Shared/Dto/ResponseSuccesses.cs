namespace Ws.PalychExchange.Api.App.Shared.Dto;

[Serializable]
public sealed record ResponseSuccesses(Guid Uid)
{
    [XmlAttribute("Guid")]
    public Guid Uid = Uid;
}