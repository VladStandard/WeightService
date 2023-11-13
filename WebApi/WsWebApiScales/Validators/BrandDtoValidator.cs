using FluentValidation;
using WsWebApiScales.Dto.Brand;

namespace WsWebApiScales.Validators;

public class BrandDtoValidator : AbstractValidator<BrandDto>
{
    public BrandDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("Поле 'Name' обязательно для заполнения.")
            .MaximumLength(128).WithMessage("Поле 'Name' не должно превышать 128 символов.");
        RuleFor(dto => dto.Code)
            .NotEmpty().WithMessage("Поле 'Code' обязательно для заполнения.")
            .Length(9).WithMessage("Поле 'Code' должно состоять из 9 цифр.");
        RuleFor(dto => dto.Guid)
            .NotEmpty().WithMessage("Поле 'Guid' обязательно для заполнения.");
    }
    
}