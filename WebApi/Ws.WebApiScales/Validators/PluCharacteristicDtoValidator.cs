using FluentValidation;
using Ws.WebApiScales.Dto.PluCharacteristic;

namespace Ws.WebApiScales.Validators;

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
        RuleFor(dto => dto.AttachmentsCount)
            .NotEmpty().WithMessage("Поле 'AttachmentsCount' обязательно.")
            .Must(item=>!item.Contains(".") && !item.Contains(",")).WithMessage("Поле 'AttachmentsCount' не должно быть дробным.");
        RuleFor(dto => dto.AttachmentsCountAsInt)
            .Must(item=>item > 0).WithMessage("Поле 'AttachmentsCount' должно быть больше 0.");
    }
}