using System.Xml.Serialization;
using Ws.WebApiScales.Common;

namespace Ws.WebApiScales.Features.Plus.Dto;

[XmlRoot("Plus")]
internal sealed class PlusWrapper : BaseWrapper
{
    [XmlElement("Plu")]
    public List<PluDto> Plus { get; set; } = [];
}