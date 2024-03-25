using Ws.PalychExchangeApi.Features.Clips.Dto;

namespace Ws.PalychExchangeApi.Features.Clips.Common;

public interface IClipService
{
    ClipWrapper Load(ClipWrapper dto);
}