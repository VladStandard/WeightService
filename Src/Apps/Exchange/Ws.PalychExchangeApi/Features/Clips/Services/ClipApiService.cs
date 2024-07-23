using Ws.PalychExchangeApi.Common;
using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Clips.Common;
using Ws.PalychExchangeApi.Features.Clips.Dto;

namespace Ws.PalychExchangeApi.Features.Clips.Services;

internal partial class ClipApiService(ClipDtoValidator validator) : BaseService<ClipDto>(validator), IClipService
{
    public ResponseDto Load(ClipsWrapper dtoWrapper)
    {
        ResolveUniqueUidLocal(dtoWrapper.Clips);
        IEnumerable<ClipDto> validDtos = FilterValidDtos(dtoWrapper.Clips);
        SaveClips(validDtos);
        return OutputDto;
    }
}