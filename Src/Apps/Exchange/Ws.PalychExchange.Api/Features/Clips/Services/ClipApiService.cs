using Ws.PalychExchange.Api.Common;
using Ws.PalychExchange.Api.Dto;
using Ws.PalychExchange.Api.Features.Clips.Common;
using Ws.PalychExchange.Api.Features.Clips.Dto;

namespace Ws.PalychExchange.Api.Features.Clips.Services;

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