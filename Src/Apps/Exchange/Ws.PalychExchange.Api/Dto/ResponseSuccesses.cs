using System.Xml.Serialization;

namespace Ws.PalychExchange.Api.Dto;

[Serializable]
public sealed record ResponseSuccesses(Guid Uid)
{
    [XmlAttribute("Guid")]
    public Guid Uid = Uid;
}