using FluentValidation;
using Ws.PalychExchangeApi.Features.Brands.Dto;

namespace Ws.PalychExchangeApi.Features.Brands.Services.Validators;

internal sealed class BrandDtoValidator : AbstractValidator<BrandDto>
{
    public BrandDtoValidator()
    {
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("'Наименование' обязательно")
            .MaximumLength(128).WithMessage("'Наименование' не должно превышать 128 символов.");
        RuleFor(dto => dto.Uid)
            .NotEmpty().WithMessage("'UID' обязателен");
    }
}