using FluentValidation.Results;
using Ws.Database.EntityFramework;
using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Brands.Common;
using Ws.PalychExchangeApi.Features.Brands.Dto;
using Ws.PalychExchangeApi.Features.Brands.Services.Validators;

namespace Ws.PalychExchangeApi.Features.Brands.Services;

// ReSharper disable once SuggestBaseTypeForParameterInConstructor
internal partial class BrandService(WsDbContext dbContext) : IBrandService
{
    private ResponseDto OutputDto { get; } = new();

    public ResponseDto Load(BrandsWrapper dtoWrapper)
    {
        BrandDtoValidator validator = new();
        List<BrandDto> validDtos = [];

        ResolveUniqueUidLocal(dtoWrapper.Brands);
        DeleteBrands(dtoWrapper.Brands);
        ResolveUniqueNameLocal(dtoWrapper.Brands);

        foreach (BrandDto dto in dtoWrapper.Brands)
        {
            ValidationResult validationResult = validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                OutputDto.AddError(dto.Uid, validationResult.Errors.First().ErrorMessage);
                continue;
            }
            validDtos.Add(dto);
        }

        SaveBrands(validDtos);
        return OutputDto;
    }
}