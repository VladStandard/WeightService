using Ws.WebApiScales.Features.Brand.Dto;

namespace Ws.WebApiScales.Features.Brand;

public interface IBrandApiService
{
    public void Load(BrandsWrapper brandsWrapper);
}