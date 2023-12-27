using Ws.WebApiScales.Dto.Brand;

namespace Ws.WebApiScales.Common.Services;

public interface IBrandService
{
    public void Load(BrandsWrapper brandsWrapper);
}