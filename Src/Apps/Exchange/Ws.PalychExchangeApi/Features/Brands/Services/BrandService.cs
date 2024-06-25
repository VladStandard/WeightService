using System.Text;
using Ws.PalychExchangeApi.Common;
using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Brands.Common;
using Ws.PalychExchangeApi.Features.Brands.Dto;

namespace Ws.PalychExchangeApi.Features.Brands.Services;

internal partial class BrandService(BrandDtoValidator validator, ILogger<BrandService> logger)
    : BaseService<BrandDto>(validator), IBrandService
{
    public ResponseDto Load(BrandsWrapper dtoWrapper)
    {
        ResolveUniqueUidLocal(dtoWrapper.Brands);
        ResolveUniqueLocal(dtoWrapper.Brands, dto => dto.Name, "Name - не уникален");

        List<BrandDto> validDtos = FilterValidDtos(dtoWrapper.Brands);

        ResolveUniqueNameDb(validDtos);
        SaveBrands(validDtos);

        if (OutputDto.Errors.Count == 0) return OutputDto;

        StringBuilder errors = new();

        foreach (ResponseError error in OutputDto.Errors)
            errors.AppendLine($"{error.Uid} : {error.Message}");

        logger.LogWarning("The following errors occurred:\n{Errors}", errors.ToString());
        return OutputDto;
    }
}