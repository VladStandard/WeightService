using Ws.PalychExchange.Api.App.Features.Brands.Dto;

namespace Ws.PalychExchange.Api.App.Features.Brands.Common;

public interface IBrandService
{
    public ResponseDto Load(HashSet<BrandDto> dtos);
}