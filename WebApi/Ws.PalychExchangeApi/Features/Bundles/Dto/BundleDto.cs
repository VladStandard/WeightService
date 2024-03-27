using System.Xml.Serialization;
using Ws.Database.EntityFramework.Entities.Ref1C.Bundles;

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

internal static class BundleDtoExtensions
{
    internal static BundleEntity ToEntity(this BundleDto dto, DateTime updateDt) =>
        new(dto.Uid, dto.Name, dto.Weight, updateDt);
}