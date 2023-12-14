namespace Ws.WebApiScales.Dto.Brand;

[Serializable]
public class BrandDto
{
    [XmlAttribute("GUID")]
    public Guid Guid { get; set; }

    [XmlAttribute("IsMarked")]
    public bool IsMarked { get; set; }

    [XmlAttribute("Name")]
    public string Name { get; set; } = string.Empty;

    [XmlAttribute("Code")]
    public string Code { get; set; } = string.Empty;
}