using System.Xml.Serialization;
using Ws.WebApiScales.Common;

namespace Ws.WebApiScales.Features.PluOld.Dto;

[XmlRoot("Nomenclatures")]
internal sealed class PlusWrapper : BaseWrapper
{
    [XmlElement("Nomenclature")]
    public List<PluDto> Plus { get; set; } = [];
}