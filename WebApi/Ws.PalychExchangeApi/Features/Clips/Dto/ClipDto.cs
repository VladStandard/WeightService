using System.Xml.Serialization;
using Ws.Database.EntityFramework.Entities.Ref1C.Clips;

namespace Ws.PalychExchangeApi.Features.Clips.Dto;

[Serializable]
public sealed class ClipDto
{
    [XmlAttribute("Uid")]
    public Guid Uid { get; set; }

    [XmlAttribute("Name")]
    public string Name { get; set; } = string.Empty;

    [XmlAttribute("Weight")]
    public decimal Weight { get; set; }
}

internal static class ClipDtoExtensions
{
    internal static ClipEntity ToEntity(this ClipDto dto) => new(dto.Uid, dto.Name, dto.Weight);
}