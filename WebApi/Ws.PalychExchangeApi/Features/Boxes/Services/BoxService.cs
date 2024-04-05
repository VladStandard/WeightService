using Ws.Database.EntityFramework;
using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Boxes.Common;
using Ws.PalychExchangeApi.Features.Boxes.Dto;
using Ws.PalychExchangeApi.Features.Boxes.Services.Validators;

namespace Ws.PalychExchangeApi.Features.Boxes.Services;

// ReSharper disable once SuggestBaseTypeForParameterInConstructor
internal partial class BoxService(WsDbContext dbContext) : IBoxService
{
    private ResponseDto OutputDto { get; } = new();
    private BoxDtoValidator Validator { get; } = new();

    public ResponseDto Load(BoxesWrapper dtoWrapper)
    {
        ResolveUniqueUidLocal(dtoWrapper.Boxes);
        IEnumerable<BoxDto> validDtos = FilterValidDtos(dtoWrapper.Boxes);
        SaveBoxes(validDtos);
        return OutputDto;
    }
}