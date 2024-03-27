using FluentValidation.Results;
using Ws.Database.EntityFramework;
using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Bundles.Common;
using Ws.PalychExchangeApi.Features.Bundles.Dto;
using Ws.PalychExchangeApi.Features.Bundles.Services.Validators;

namespace Ws.PalychExchangeApi.Features.Bundles.Services;

// ReSharper disable once SuggestBaseTypeForParameterInConstructor
internal partial class BundleService(WsDbContext dbContext) : IBundleService
{
    private ResponseDto OutputDto { get; } = new();

    public ResponseDto Load(BundlesWrapper dtoWrapper)
    {
        BundleDtoValidator validator = new();
        HashSet<BundleDto> validDtos = [];

        ResolveUniqueUidLocal(dtoWrapper.Bundles);

        foreach (BundleDto dto in dtoWrapper.Bundles)
        {
            ValidationResult validationResult = validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                OutputDto.AddError(dto.Uid, validationResult.Errors.First().ErrorMessage);
                continue;
            }
            validDtos.Add(dto);
        }
        SaveBundles(validDtos);
        return OutputDto;
    }
}