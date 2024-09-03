using Ws.PalychExchange.Api.Common;
using Ws.PalychExchange.Api.Dto;
using Ws.PalychExchange.Api.Features.Brands.Common;
using Ws.PalychExchange.Api.Features.Brands.Dto;

namespace Ws.PalychExchange.Api.Features.Brands.Services;

internal sealed partial class BrandApiService(BrandDtoValidator validator) : BaseService<BrandDto>(validator), IBrandService
{
    public ResponseDto Load(BrandsWrapper dtoWrapper)
    {
        ResolveUniqueUidLocal(dtoWrapper.Brands);
        ResolveUniqueLocal(dtoWrapper.Brands, dto => dto.Name, "Name - не уникален");

        List<BrandDto> validDtos = FilterValidDtos(dtoWrapper.Brands);

        ResolveUniqueNameDb(validDtos);
        SaveBrands(validDtos);

        return OutputDto;
    }
}