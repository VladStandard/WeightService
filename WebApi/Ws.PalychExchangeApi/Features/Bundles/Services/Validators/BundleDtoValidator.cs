using FluentValidation;
using Ws.PalychExchangeApi.Features.Bundles.Dto;
using Ws.PalychExchangeApi.Utils;

namespace Ws.PalychExchangeApi.Features.Bundles.Services.Validators;

public class BundleDtoValidator : AbstractValidator<BundleDto>
{
    public BundleDtoValidator()
    {
        RuleFor(dto => dto.Uid)
            .NotEqual(Guid.Empty).WithMessage("UID - обязателен");
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("Наименование - обязательно")
            .MaximumLength(64).WithMessage("Наименование - не должно превышать 64 символа");
        RuleFor(dto => dto.Weight)
            .Must(ValidatorUtils.BeValidWeightDefault)
            .WithMessage("Вес - должен быть в [0, 1)");
    }
}