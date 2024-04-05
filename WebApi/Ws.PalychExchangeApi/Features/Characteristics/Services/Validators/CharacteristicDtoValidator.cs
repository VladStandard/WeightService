using FluentValidation;
using Ws.PalychExchangeApi.Features.Characteristics.Dto;

namespace Ws.PalychExchangeApi.Features.Characteristics.Services.Validators;

public class CharacteristicDtoValidator : AbstractValidator<CharacteristicDto>
{
    public CharacteristicDtoValidator()
    {
        RuleFor(dto => dto.Uid)
            .NotEqual(Guid.Empty).WithMessage("UID - обязателен");

        RuleFor(dto => dto.BundleCount)
            .Must(count => count is > 0 and <= 100)
            .WithMessage("Кол-во пакетов должно быть от 1 до 100");

        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("Наименование - обязательно")
            .MaximumLength(64).WithMessage("Наименование - не должно превышать 64 символа");

        RuleFor(dto => dto.BoxUid)
            .NotEqual(Guid.Empty).WithMessage("Короб - обязателен");
    }
}