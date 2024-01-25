using Ws.WebApiScales.Features.Boxes.Dto;

namespace Ws.WebApiScales.Features.Boxes.Services;

internal interface IBoxApiService
{
    public void Load(BoxWrapper boxWrapper);
}