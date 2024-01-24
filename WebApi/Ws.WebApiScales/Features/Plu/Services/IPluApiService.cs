using Ws.WebApiScales.Features.Plu.Dto;

namespace Ws.WebApiScales.Features.Plu.Services;

internal interface IPluApiService
{
    public void Load(PlusWrapper brandsWrapper);
}