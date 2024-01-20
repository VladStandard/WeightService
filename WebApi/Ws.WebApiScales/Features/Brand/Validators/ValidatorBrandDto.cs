using FluentValidation;
using Ws.WebApiScales.Features.Brand.Dto;

namespace Ws.WebApiScales.Features.Brand.Validators;

public class ValidatorBrandDto : AbstractValidator<BrandDto>
{
    public ValidatorBrandDto()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("'Наименование' обязательно")
            .MaximumLength(128).WithMessage("'Наименование' не должно превышать 128 символов.");
        RuleFor(dto => dto.Code)
            .NotEmpty().WithMessage("Код' обязателен")
            .Length(9).WithMessage("'Код' должен состоять из 9 цифр.");
        RuleFor(dto => dto.Guid)
            .NotEmpty().WithMessage("'UID' обязателен");
    }
}