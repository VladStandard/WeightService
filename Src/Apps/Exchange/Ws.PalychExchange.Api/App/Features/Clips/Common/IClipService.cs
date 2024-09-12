using Ws.PalychExchange.Api.App.Features.Clips.Dto;

namespace Ws.PalychExchange.Api.App.Features.Clips.Common;

public interface IClipService
{
    ResponseDto Load(HashSet<ClipDto> dtos);
}