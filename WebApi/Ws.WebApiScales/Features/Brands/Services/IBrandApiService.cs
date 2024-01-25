using Ws.WebApiScales.Features.Brands.Dto;

namespace Ws.WebApiScales.Features.Brands.Services;

internal interface IBrandApiService
{
    public void Load(BrandsWrapper brandsWrapper);
}