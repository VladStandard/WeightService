namespace Ws.PalychExchange.Api.App.Features.Bundles.Dto;

[XmlRoot("Bundles")]
public sealed class BundlesWrapper
{
    [XmlElement("Bundle")]
    public List<BundleDto> Bundles { get; set; } = [];
}