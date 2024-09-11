using Ws.PalychExchange.Api.App.Features.Boxes.Common;
using Ws.PalychExchange.Api.App.Features.Boxes.Dto;

namespace Ws.PalychExchange.Api.App.Features.Boxes.Impl;

internal sealed partial class BoxApiService(BoxDtoValidator validator) : BaseService<BoxDto>(validator), IBoxService
{
    public ResponseDto Load(BoxesWrapper dtoWrapper)
    {
        ResolveUniqueUidLocal(dtoWrapper.Boxes);
        IEnumerable<BoxDto> validDtos = FilterValidDtos(dtoWrapper.Boxes);
        SaveBoxes(validDtos);
        return OutputDto;
    }
}