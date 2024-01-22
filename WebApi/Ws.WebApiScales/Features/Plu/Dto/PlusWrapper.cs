using System.Xml.Serialization;
using Ws.WebApiScales.Common;

namespace Ws.WebApiScales.Features.Plu.Dto;

[XmlRoot("Nomenclatures")]
public class PlusWrapper : BaseWrapper
{
    [XmlElement("Nomenclature")]
    public List<PluDto> Plus { get; set; } = [];
}