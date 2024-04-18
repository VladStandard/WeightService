using System.Xml.Serialization;

namespace Ws.PalychExchangeApi.Features.Bundles.Dto;

[XmlRoot("Bundles")]
public sealed class BundlesWrapper
{
    [XmlElement("Bundle")]
    public List<BundleDto> Bundles { get; set; } = [];
}