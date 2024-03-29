using FluentValidation;
using Ws.PalychExchangeApi.Features.Boxes.Dto;
using Ws.PalychExchangeApi.Utils;

namespace Ws.PalychExchangeApi.Features.Boxes.Services.Validators;

public class BoxDtoValidator : AbstractValidator<BoxDto>
{
    public BoxDtoValidator()
    {
        RuleFor(dto => dto.Uid)
            .NotEqual(Guid.Empty).WithMessage("'UID' обязателен");
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("'Наименование' обязательно")
            .MaximumLength(64).WithMessage("'Наименование' не должно превышать 64 символов.");
        RuleFor(dto => dto.Weight)
            .Must(ValidatorUtils.BeValidWeight)
            .WithMessage("'Вес' должен быть в диапазоне от 0 до 1.");
    }
}