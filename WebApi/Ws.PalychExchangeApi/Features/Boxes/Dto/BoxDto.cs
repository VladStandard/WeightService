using System.Xml.Serialization;
using Ws.Database.EntityFramework.Entities.Ref1C.Boxes;

namespace Ws.PalychExchangeApi.Features.Boxes.Dto;

[Serializable]
public sealed record BoxDto
{
    [XmlAttribute("Uid")]
    public Guid Uid { get; set; }

    [XmlAttribute("Name")]
    public string Name { get; set; } = string.Empty;

    [XmlAttribute("Weight")]
    public decimal Weight { get; set; }
}

internal static class BoxDtoExtensions
{
    internal static BoxEntity ToEntity(this BoxDto dto) => new(dto.Uid, dto.Name, dto.Weight);
}