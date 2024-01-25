using FluentValidation;
using Ws.WebApiScales.Features.Characteristics.Dto;

namespace Ws.WebApiScales.Features.Characteristics.Validators;

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