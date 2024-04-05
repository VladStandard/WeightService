using Ws.Database.EntityFramework;
using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Characteristics.Common;
using Ws.PalychExchangeApi.Features.Characteristics.Dto;
using Ws.PalychExchangeApi.Features.Characteristics.Services.Validators;

namespace Ws.PalychExchangeApi.Features.Characteristics.Services;

internal sealed partial class CharacteristicService(WsDbContext dbContext) : ICharacteristicService
{
    private ResponseDto OutputDto { get; } = new();
    private CharacteristicDtoValidator Validator { get; } = new();

    public ResponseDto Load(List<PluCharacteristicsDto> dtoWrapper)
    {
        ResolveUniqueUidLocal(dtoWrapper);
        ResolveUniqueLocal(dtoWrapper);
        ResolveNotExistsBoxFkDb(dtoWrapper);

        IEnumerable<PluCharacteristicsDto> validDtos = FilterValidDtos(dtoWrapper);

        SaveCharacteristics(validDtos);
        return OutputDto;
    }
}