using System.Xml.Serialization;

namespace Ws.PalychExchange.Api.Features.Plus.Dto;

[XmlRoot("Plus")]
public sealed class PlusWrapper
{
    [XmlElement("Plu")]
    public List<PluDto> Plus { get; set; } = [];
}