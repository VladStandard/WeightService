using System.Xml.Serialization;

namespace Ws.PalychExchangeApi.Features.Bundles.Dto;

[Serializable]
public sealed class BundleDto
{
    [XmlAttribute("Uid")]
    public Guid Uid { get; set; }

    [XmlAttribute("Name")]
    public string Name { get; set; } = string.Empty;

    [XmlAttribute("Weight")]
    public decimal Weight { get; set; }
}