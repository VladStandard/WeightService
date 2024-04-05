using System.Xml.Serialization;
using Ws.Database.EntityFramework.Entities.Ref1C.Characteristics;

namespace Ws.PalychExchangeApi.Features.Characteristics.Dto;

[Serializable]
public sealed record CharacteristicDto
{
    [XmlAttribute("Uid")]
    public Guid Uid { get; set; }

    [XmlAttribute("BoxUid")]
    public Guid BoxUid { get; set; }

    [XmlAttribute("IsDelete")]
    public bool IsDelete { get; set; }

    [XmlAttribute("BundleCount")]
    public short BundleCount { get; set; }

    [XmlAttribute("Name")]
    public string Name { get; set; } = string.Empty;
}