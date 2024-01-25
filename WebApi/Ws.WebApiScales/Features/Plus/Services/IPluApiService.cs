using Ws.WebApiScales.Features.Plus.Dto;

namespace Ws.WebApiScales.Features.Plus.Services;

internal interface IPluApiService
{
    public void Load(PlusWrapper brandsWrapper);
}