using FluentValidation;
using Ws.WebApiScales.Features.Box.Dto;

namespace Ws.WebApiScales.Features.Box.Validators;

internal sealed class ValidatorBoxDto : AbstractValidator<BoxDto>
{
    public ValidatorBoxDto()
    {
        RuleFor(dto => dto.Uid)
            .NotEmpty().WithMessage("'UID' обязателен");
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("'Наименование' обязательно")
            .MaximumLength(128).WithMessage("'Наименование' не должно превышать 128 символов.");
        RuleFor(dto => dto.Weight)
            .GreaterThan(0.001m).WithMessage("'Вес коробки' обязателен");
    }
}