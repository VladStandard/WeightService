using Ws.WebApiScales.Features.Box.Dto;

namespace Ws.WebApiScales.Features.Box.Services;

internal interface IBoxApiService
{
    public void Load(BoxWrapper boxWrapper);
}