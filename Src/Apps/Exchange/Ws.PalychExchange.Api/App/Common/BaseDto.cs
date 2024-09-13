namespace Ws.PalychExchange.Api.App.Common;

public abstract record BaseDto
{
    [XmlAttribute("Uid")]
    public Guid Uid;
}