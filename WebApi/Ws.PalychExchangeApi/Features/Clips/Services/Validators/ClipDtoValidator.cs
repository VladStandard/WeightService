using System.Globalization;
using FluentValidation;
using Ws.PalychExchangeApi.Features.Clips.Dto;

namespace Ws.PalychExchangeApi.Features.Clips.Services.Validators;

public class ClipDtoValidator : AbstractValidator<ClipDto>
{
    public ClipDtoValidator()
    {
        RuleFor(dto => dto.Uid)
            .NotEmpty().WithMessage("'UID' обязателен");
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("'Наименование' обязательно")
            .MaximumLength(64).WithMessage("'Наименование' не должно превышать 64 символов.");
        RuleFor(dto => dto.Weight)
            .Must(BeValidWeight)
            .WithMessage("'Вес клипсы' должен быть в диапазоне от 0 до 1.");
    }

    private static bool BeValidWeight(decimal number)
    {
        if (number is <= 0 or >= 1) return false;

        string numberString = number.ToString(CultureInfo.InvariantCulture);
        string[] parts = numberString.Split('.');
        string integerPart = parts[0];
        string decimalPart = parts.Length > 1 ? parts[1] : string.Empty;
        return integerPart.Length <= 1 && decimalPart.Length <= 3;
    }
}