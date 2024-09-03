using Ws.PalychExchange.Api.Dto;
using Ws.PalychExchange.Api.Features.Brands.Dto;

namespace Ws.PalychExchange.Api.Features.Brands.Common;

public interface IBrandService
{
    public ResponseDto Load(BrandsWrapper dtoWrapper);
}