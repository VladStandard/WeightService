using Ws.Database.EntityFramework.Entities.Ref1C.Characteristics;
using Ws.PalychExchange.Api.Common;

namespace Ws.PalychExchange.Api.Features.Characteristics.Services.Models;

internal record GroupedCharacteristic : BaseDto
{
    public required Guid PluUid { get; set; }
    public required Guid BoxUid { get; set; }
    public required short BundleCount { get; set; }
    public required bool IsDelete { get; set; }
    public required string Name { get; set; } = string.Empty;

    public CharacteristicEntity ToEntity(DateTime updateDt) => new()
    {
        Id = Uid,
        ChangeDt = updateDt,
        Name = Name,
        BundleCount = BundleCount,
        PluId = PluUid,
        BoxId = BoxUid
    };
}