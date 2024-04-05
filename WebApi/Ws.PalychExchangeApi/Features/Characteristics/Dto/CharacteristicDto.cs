using System.Xml.Serialization;
using Ws.Database.EntityFramework.Entities.Ref1C.Characteristics;

namespace Ws.PalychExchangeApi.Features.Characteristics.Dto;

[Serializable]
public sealed class CharacteristicDto
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

internal static class BoxDtoExtensions
{
    internal static CharacteristicEntity ToEntity(this CharacteristicDto dto, Guid pluUid, DateTime updateDt) =>
        new(dto.Uid, pluUid, updateDt)
        {
            Name = dto.Name,
            BoxId = dto.BoxUid,
            BundleCount = dto.BundleCount
        };
}