using System.Xml.Serialization;
using Ws.WebApiScales.Features.Plus.Dto;

namespace Ws.PalychExchangeApi.Features.Plus.Dto;

[XmlRoot("Plus")]
internal sealed class PlusWrapper
{
    [XmlElement("Plu")]
    public List<PluDto> Plus { get; set; } = [];
}