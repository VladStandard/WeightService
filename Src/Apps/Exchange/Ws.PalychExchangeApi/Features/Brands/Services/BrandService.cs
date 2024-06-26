using Ws.PalychExchangeApi.Common;
using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Brands.Common;
using Ws.PalychExchangeApi.Features.Brands.Dto;

namespace Ws.PalychExchangeApi.Features.Brands.Services;

internal partial class BrandService(BrandDtoValidator validator) : BaseService<BrandDto>(validator), IBrandService
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