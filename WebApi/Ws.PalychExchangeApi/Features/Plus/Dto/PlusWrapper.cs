using System.Xml.Serialization;

namespace Ws.PalychExchangeApi.Features.Plus.Dto;

[XmlRoot("Plus")]
public sealed class PlusWrapper
{
    [XmlElement("Plu")]
    public List<PluDto.PluDto> Plus { get; set; } = [];
}