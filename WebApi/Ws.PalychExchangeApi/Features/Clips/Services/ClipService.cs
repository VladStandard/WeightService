using FluentValidation.Results;
using Ws.Database.EntityFramework;
using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Clips.Common;
using Ws.PalychExchangeApi.Features.Clips.Dto;
using Ws.PalychExchangeApi.Features.Clips.Services.Validators;

namespace Ws.PalychExchangeApi.Features.Clips.Services;

// ReSharper disable once SuggestBaseTypeForParameterInConstructor
internal partial class ClipService(WsDbContext dbContext) : IClipService
{
    private ResponseDto OutputDto { get; } = new();

    public ResponseDto Load(ClipsWrapper dtoWrapper)
    {
        ClipDtoValidator validator = new();
        HashSet<ClipDto> validDtos = [];

        ResolveUniqueUidLocal(dtoWrapper.Clips);

        foreach (ClipDto dto in dtoWrapper.Clips)
        {
            ValidationResult validationResult = validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                OutputDto.AddError(dto.Uid, validationResult.Errors.First().ErrorMessage);
                continue;
            }
            validDtos.Add(dto);
        }
        SaveClips(validDtos);
        return OutputDto;
    }
}