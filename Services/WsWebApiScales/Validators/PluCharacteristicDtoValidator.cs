using FluentValidation;
using WsWebApiScales.Dto.PluCharacteristic;

namespace WsWebApiScales.Validators;

public class PluCharacteristicDtoValidator : AbstractValidator<PluCharacteristicDto>
{
    public PluCharacteristicDtoValidator()
    {
        RuleFor(dto => dto.Guid)
            .NotEmpty().WithMessage("Поле 'Guid' обязательно для заполнения.");
        RuleFor(dto => dto.PluGuid)
            .NotEmpty().WithMessage("Поле 'NomenclatureGuid' обязательно для заполнения.");
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("Поле 'Name' обязательно для заполнения.")
            .MaximumLength(128).WithMessage("Поле 'Name' не должно превышать 128 символов.");
        RuleFor(dto => dto.AttachmentsCountAsDecimal)
            .PrecisionScale(10, 3, false);
    }
}