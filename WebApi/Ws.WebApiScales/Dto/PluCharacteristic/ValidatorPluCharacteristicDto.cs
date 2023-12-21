namespace Ws.WebApiScales.Dto.PluCharacteristic;

public class ValidatorPluCharacteristicDto : AbstractValidator<PluCharacteristicDto>
{
    public ValidatorPluCharacteristicDto()
    {
        RuleFor(dto => dto.Guid)
            .NotEmpty().WithMessage("'UID' обязателен");
        RuleFor(dto => dto.PluGuid)
            .NotEmpty().WithMessage("'Плу' обязательна");
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("'Наименование' обязательно")
            .MaximumLength(128).WithMessage("'Наименование' не должно превышать 128 символов");
        RuleFor(dto => dto.AttachmentsCount)
            .NotEmpty().WithMessage("'Кол-во вложений' обязательно.")
            .Must(item=>!item.Contains('.') && !item.Contains(',')).WithMessage("'Кол-во вложений' не должно быть дробным");
        RuleFor(dto => dto.AttachmentsCountAsInt)
            .Must(item=>item > 0).WithMessage("'Кол-во вложений' должно быть больше 0");
    }
}