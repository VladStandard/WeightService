using System.Xml.Serialization;

namespace Ws.WebApiScales.Features.Bundles.Dto;

[XmlRoot("Bundles")]
internal sealed class BundlesWrapper
{
    [XmlElement("Bundle")]
    public List<BundleDto> Bundles { get; set; } = [];
}