using Ws.PalychExchange.Api.Common;
using Ws.PalychExchange.Api.Dto;
using Ws.PalychExchange.Api.Features.Boxes.Common;
using Ws.PalychExchange.Api.Features.Boxes.Dto;

namespace Ws.PalychExchange.Api.Features.Boxes.Services;

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