using Ws.Database.EntityFramework;
using Ws.PalychExchangeApi.Common;
using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Boxes.Common;
using Ws.PalychExchangeApi.Features.Boxes.Dto;

namespace Ws.PalychExchangeApi.Features.Boxes.Services;

// ReSharper disable once SuggestBaseTypeForParameterInConstructor
internal partial class BoxService(WsDbContext dbContext, BoxDtoValidator validator) :
    BaseService<BoxDto>(dbContext, validator), IBoxService
{
    public ResponseDto Load(BoxesWrapper dtoWrapper)
    {
        ResolveUniqueUidLocal(dtoWrapper.Boxes);
        IEnumerable<BoxDto> validDtos = FilterValidDtos(dtoWrapper.Boxes);
        SaveBoxes(validDtos);
        return OutputDto;
    }
}