using System.Xml.Serialization;

namespace Ws.PalychExchange.Api.Features.Bundles.Dto;

[XmlRoot("Bundles")]
public sealed class BundlesWrapper
{
    [XmlElement("Bundle")]
    public List<BundleDto> Bundles { get; set; } = [];
}