using Ws.WebApiScales.Features.Brand.Dto;

namespace Ws.WebApiScales.Features.Brand;

public interface IBrandService
{
    public void Load(BrandsWrapper brandsWrapper);
}