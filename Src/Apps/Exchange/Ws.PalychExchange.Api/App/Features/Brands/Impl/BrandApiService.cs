using Ws.PalychExchange.Api.App.Features.Brands.Common;
using Ws.PalychExchange.Api.App.Features.Brands.Dto;

namespace Ws.PalychExchange.Api.App.Features.Brands.Impl;

internal sealed partial class BrandApiService(BrandDtoValidator validator, ILogger<BrandApiService> logger) : BaseService<BrandDto>(validator), IBrandService
{
    public ResponseDto Load(HashSet<BrandDto> dtos)
    {
        ResolveUniqueUidLocal(dtos);
        ResolveUniqueLocal(dtos, dto => dto.Name, "Name - не уникален");

        FilterValidDtos(dtos);
        ResolveUniqueNameDb(dtos);
        SaveBrands(dtos);

        return OutputDto;
    }
}