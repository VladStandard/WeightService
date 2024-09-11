using Ws.PalychExchange.Api.App.Features.Clips.Common;
using Ws.PalychExchange.Api.App.Features.Clips.Dto;

namespace Ws.PalychExchange.Api.App.Features.Clips.Impl;

internal sealed partial class ClipApiService(ClipDtoValidator validator) : BaseService<ClipDto>(validator), IClipService
{
    public ResponseDto Load(ClipsWrapper dtoWrapper)
    {
        ResolveUniqueUidLocal(dtoWrapper.Clips);
        IEnumerable<ClipDto> validDtos = FilterValidDtos(dtoWrapper.Clips);
        SaveClips(validDtos);
        return OutputDto;
    }
}