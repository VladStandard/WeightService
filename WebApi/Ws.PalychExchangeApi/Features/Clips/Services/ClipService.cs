using Ws.Database.EntityFramework;
using Ws.PalychExchangeApi.Common;
using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Clips.Common;
using Ws.PalychExchangeApi.Features.Clips.Dto;

namespace Ws.PalychExchangeApi.Features.Clips.Services;

// ReSharper disable once SuggestBaseTypeForParameterInConstructor
internal partial class ClipService(WsDbContext dbContext, ClipDtoValidator validator) :
    BaseService<ClipDto>(dbContext, validator), IClipService
{
    public ResponseDto Load(ClipsWrapper dtoWrapper)
    {
        ResolveUniqueUidLocal(dtoWrapper.Clips);
        IEnumerable<ClipDto> validDtos = FilterValidDtos(dtoWrapper.Clips);
        SaveClips(validDtos);
        return OutputDto;
    }
}