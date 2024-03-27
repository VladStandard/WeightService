using FluentValidation;
using Ws.PalychExchangeApi.Features.Clips.Dto;
using Ws.PalychExchangeApi.Utils;

namespace Ws.PalychExchangeApi.Features.Clips.Services.Validators;

public class ClipDtoValidator : AbstractValidator<ClipDto>
{
    public ClipDtoValidator()
    {
        RuleFor(dto => dto.Uid)
            .NotEmpty()
            .WithMessage("'UID' обязателен");
        RuleFor(dto => dto.Name)
            .NotEmpty()
            .WithMessage("'Наименование' обязательно")
            .MaximumLength(64)
            .WithMessage("'Наименование' не должно превышать 64 символов.");
        RuleFor(dto => dto.Weight)
            .Must(ValidatorUtils.BeValidWeight)
            .WithMessage("'Вес клипсы' должен быть в диапазоне от 0 до 1.");
    }
}