using System.Xml.Serialization;

namespace Ws.PalychExchangeApi.Common;

public abstract record BaseDto
{
    [XmlAttribute("Uid")]
    public Guid Uid;
}