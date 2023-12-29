using Ws.WebApiScales.Common;

namespace Ws.WebApiScales.Dto.Plu;

[XmlRoot("Nomenclatures")]
public class PlusWrapper : BaseWrapper
{
    [XmlElement("Nomenclature")]
    public List<PluDto> Plus { get; set; } = new();
}