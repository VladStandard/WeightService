using Ws.PalychExchange.Api.Dto;
using Ws.PalychExchange.Api.Features.Clips.Dto;

namespace Ws.PalychExchange.Api.Features.Clips.Common;

public interface IClipService
{
    ResponseDto Load(ClipsWrapper dtoWrapper);
}