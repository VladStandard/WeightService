using FluentValidation;
using Ws.WebApiScales.Features.Plus.Dto;

namespace Ws.WebApiScales.Features.Plus.Validators;

internal sealed class ValidatorPluDto : AbstractValidator<PluDto>
{
    public ValidatorPluDto()
    {
        RuleFor(dto => dto.Uid)
            .NotEmpty().WithMessage("'UID' обязателен");
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("'Наименование' обязательно")
            .MaximumLength(150).WithMessage("'Наименование' не должно превышать 150 символов");
        RuleFor(dto => dto.FullName)
            .NotEmpty().WithMessage("'Полное наименование' обязательно");
        RuleFor(dto => dto.Description)
            .NotEmpty().WithMessage("'Описание' обязательно");

        RuleFor(dto => dto.Number)
            .GreaterThan(0).WithMessage("'Номер ПЛУ' должен быть больше 0")
            .LessThanOrEqualTo(999).WithMessage("'Номер ПЛУ' должен быть < 1000.");
        RuleFor(dto => dto.BundleCount)
            .GreaterThan((short)0).WithMessage("'Кол-во вложений' должно быть больше 0");
        RuleFor(dto => dto.ShelfLifeDays)
            .GreaterThan(0).WithMessage("'Срок годности' обязательны");

        RuleFor(dto => dto.BoxUid)
            .NotEqual(Guid.Empty).WithMessage("'Коробка' обязательна");
        RuleFor(dto => dto.BundleUid)
            .NotEqual(Guid.Empty).WithMessage("'Пакет' обязателен");

        RuleFor(dto => dto.Ean13)
            .Length(13).WithMessage("'Ean13' должен состоять из 13 символов");
        RuleFor(dto => dto.Itf14)
            .Empty().When(item => item.IsCheckWeight).WithMessage("У весовой ПЛУ 'Itf14' должен отсутствовать")
            .Length(14).When(item => !item.IsCheckWeight).WithMessage("'Itf14' должен состоять из 14 символов");

        RuleFor(dto => dto.StorageMethod)
            .Must(value => value is "Замороженное" or "Охлаждённое")
            .WithMessage("'Способ хранения' должен быть ['Замороженное', 'Охлаждённое']");
    }
}