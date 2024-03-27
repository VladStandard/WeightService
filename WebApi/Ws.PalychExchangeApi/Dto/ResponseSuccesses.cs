using System.Xml.Serialization;

namespace Ws.PalychExchangeApi.Dto;

[Serializable]
public sealed record ResponseSuccesses(Guid Uid)
{
    [XmlAttribute("Guid")]
    public Guid Uid = Uid;

    public ResponseSuccesses() : this(Guid.Empty)
    {
    }
}