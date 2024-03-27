using FluentValidation;
using Ws.PalychExchangeApi.Features.Nestings.Dto;

namespace Ws.PalychExchangeApi.Features.Nestings.Validators;

public class ValidatorCharacteristicDto : AbstractValidator<CharacteristicDto>
{
    public ValidatorCharacteristicDto()
    {
        RuleFor(dto => dto.Uid)
            .NotEqual(Guid.Empty).WithMessage("'UID' обязателен");
        RuleFor(dto => dto.BundleCount)
            .GreaterThan(0).WithMessage("'Кол-во вложений' должно быть > 0");
    }
}