using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Clips.Dto;

namespace Ws.PalychExchangeApi.Features.Clips.Common;

public interface IClipService
{
    ResponseDto Load(ClipsWrapper dtoWrapper);
}