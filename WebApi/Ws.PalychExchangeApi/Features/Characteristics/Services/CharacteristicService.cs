using Ws.Database.EntityFramework;
using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Characteristics.Common;
using Ws.PalychExchangeApi.Features.Characteristics.Dto;
using Ws.PalychExchangeApi.Features.Characteristics.Services.Models;

namespace Ws.PalychExchangeApi.Features.Characteristics.Services;

internal sealed partial class CharacteristicService(WsDbContext dbContext) : ICharacteristicService
{
    private ResponseDto OutputDto { get; } = new();
    private GroupedCharacteristicValidator Validator { get; } = new();

    public ResponseDto Load(PluCharacteristicsWrapper dtoWrapper)
    {
        List<GroupedCharacteristic> grouped = dtoWrapper.ToGrouped();

        ResolveUniqueUidLocal(grouped);
        ResolveUniqueLocal(grouped);

        IEnumerable<GroupedCharacteristic> validDtos = FilterValidDtos(grouped);

        ResolveUniqueDb(grouped);
        ResolveNotExistsBoxFkDb(grouped);

        SaveCharacteristics(validDtos);
        return OutputDto;
    }
}