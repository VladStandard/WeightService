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
    private PluDtoValidator Validator { get; } = new();

    public ResponseDto Load(PlusWrapper dtoWrapper)
    {
        List<PluDto> validDtos = [];

        dtoWrapper.Plus.RemoveAll(i => i.IsDelete);

        ResolveUniqueUidLocal(dtoWrapper.Plus);
        ResolveUniqueNumberLocal(dtoWrapper.Plus);

        foreach (PluDto dto in dtoWrapper.Plus)
        {
            ValidationResult validationResult = Validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                OutputDto.AddError(dto.Uid, validationResult.Errors.First().ErrorMessage);
                continue;
            }
            validDtos.Add(dto);
        }

        ResolveUniqueNumberDb(validDtos);
        ResolveNotExistsBoxFkDb(validDtos);
        ResolveNotExistClipFkDb(validDtos);
        ResolveNotExistsBrandFkDb(validDtos);
        ResolveNotExistsBundleFkDb(validDtos);

        SavePlus(validDtos);
        return OutputDto;
    }
}