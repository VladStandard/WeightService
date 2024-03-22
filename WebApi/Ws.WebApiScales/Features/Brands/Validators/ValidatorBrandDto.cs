using FluentValidation;
using Ws.WebApiScales.Features.Brands.Dto;

namespace Ws.WebApiScales.Features.Brands.Validators;

internal sealed class ValidatorBrandDto : AbstractValidator<BrandDto>
{
    public ValidatorBrandDto()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("'Наименование' обязательно")
            .MaximumLength(128).WithMessage("'Наименование' не должно превышать 128 символов.");
        RuleFor(dto => dto.Uid)
            .NotEmpty().WithMessage("'UID' обязателен");
    }
}