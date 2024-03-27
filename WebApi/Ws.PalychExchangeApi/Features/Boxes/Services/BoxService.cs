using FluentValidation.Results;
using Ws.Database.EntityFramework;
using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Boxes.Common;
using Ws.PalychExchangeApi.Features.Boxes.Dto;
using Ws.PalychExchangeApi.Features.Boxes.Services.Validators;

namespace Ws.PalychExchangeApi.Features.Boxes.Services;

// ReSharper disable once SuggestBaseTypeForParameterInConstructor
internal partial class BoxService(WsDbContext dbContext) : IBoxService
{
    private ResponseDto OutputDto { get; } = new();

    public ResponseDto Load(BoxesWrapper dtoWrapper)
    {
        BoxDtoValidator validator = new();
        HashSet<BoxDto> validDtos = [];

        ResolveUniqueUidLocal(dtoWrapper.Boxes);

        foreach (BoxDto dto in dtoWrapper.Boxes)
        {
            ValidationResult validationResult = validator.Validate(dto);
            if (!validationResult.IsValid)
            {
                OutputDto.AddError(dto.Uid, validationResult.Errors.First().ErrorMessage);
                continue;
            }
            validDtos.Add(dto);
        }
        SaveBoxes(validDtos);
        return OutputDto;
    }
}