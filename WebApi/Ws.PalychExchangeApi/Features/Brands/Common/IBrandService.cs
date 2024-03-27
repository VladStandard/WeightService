using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Brands.Dto;

namespace Ws.PalychExchangeApi.Features.Brands.Common;

public interface IBrandService
{
    public ResponseDto Load(BrandsWrapper dtoWrapper);
}