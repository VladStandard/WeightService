using FluentValidation;
using Ws.WebApiScales.Features.Clips.Dto;

namespace Ws.WebApiScales.Features.Clips.Validators;

internal sealed class ValidatorClipDto : AbstractValidator<ClipDto>
{
    public ValidatorClipDto()
    {
        RuleFor(dto => dto.Uid)
            .NotEmpty().WithMessage("'UID' обязателен");
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("'Наименование' обязательно")
            .MaximumLength(128).WithMessage("'Наименование' не должно превышать 128 символов.");
        RuleFor(dto => dto.Weight)
            .GreaterThan(0.001m).WithMessage("'Вес клипсы' обязателен");
    }
}