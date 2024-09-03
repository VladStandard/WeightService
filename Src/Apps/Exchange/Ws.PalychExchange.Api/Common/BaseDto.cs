using System.Xml.Serialization;

namespace Ws.PalychExchange.Api.Common;

public abstract record BaseDto
{
    [XmlAttribute("Uid")]
    public Guid Uid;
}