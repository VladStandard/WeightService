using Ws.WebApiScales.Features.Clips.Dto;

namespace Ws.WebApiScales.Features.Clips.Services;

internal interface IClipApiService
{
    public void Load(ClipsWrapper clipsWrapper);
}