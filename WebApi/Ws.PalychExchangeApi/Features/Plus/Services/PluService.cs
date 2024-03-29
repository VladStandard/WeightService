using FluentValidation.Results;
using Ws.Database.EntityFramework;
using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Plus.Common;
using Ws.PalychExchangeApi.Features.Plus.Dto;
using Ws.PalychExchangeApi.Features.Plus.Services.Validators;

namespace Ws.PalychExchangeApi.Features.Plus.Services;

// ReSharper disable once SuggestBaseTypeForParameterInConstructor
internal sealed partial class PluService(WsDbContext dbContext) : IPluService
{
    private ResponseDto OutputDto { get; } = new();

    public ResponseDto Load(PlusWrapper dtoWrapper)
    {
        PluDtoValidator validator = new();
        List<PluDto> validDtos = [];

        ResolveUniqueUidLocal(dtoWrapper.Plus);
        ResolveUniqueNumberLocal(dtoWrapper.Plus);

        ResolveNotExistsBoxFkDb(dtoWrapper.Plus);
        ResolveNotExistClipFkDb(dtoWrapper.Plus);
        ResolveNotExistsBrandFkDb(dtoWrapper.Plus);
        ResolveNotExistsBundleFkDb(dtoWrapper.Plus);

        foreach (PluDto dto in dtoWrapper.Plus)
        {
            ValidationResult validationResult = validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                OutputDto.AddError(dto.Uid, validationResult.Errors.First().ErrorMessage);
                continue;
            }
            validDtos.Add(dto);
        }

        SavePlus(validDtos);
        return OutputDto;
    }
}