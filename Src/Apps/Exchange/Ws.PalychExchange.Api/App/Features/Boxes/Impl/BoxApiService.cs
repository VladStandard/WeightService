using Ws.PalychExchange.Api.App.Features.Boxes.Common;
using Ws.PalychExchange.Api.App.Features.Boxes.Dto;

namespace Ws.PalychExchange.Api.App.Features.Boxes.Impl;

internal sealed partial class BoxApiService(BoxDtoValidator validator) : BaseService<BoxDto>(validator), IBoxService
{
    public ResponseDto Load(HashSet<BoxDto> dtos)
    {
        ResolveUniqueUidLocal(dtos);
        FilterValidDtos(dtos);
        SaveBoxes(dtos);
        return OutputDto;
    }
}