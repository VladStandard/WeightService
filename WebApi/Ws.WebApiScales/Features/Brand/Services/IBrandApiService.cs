using Ws.WebApiScales.Features.Brand.Dto;

namespace Ws.WebApiScales.Features.Brand.Services;

internal interface IBrandApiService
{
    public void Load(BrandsWrapper brandsWrapper);
}