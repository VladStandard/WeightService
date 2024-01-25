using FluentValidation;
using Ws.WebApiScales.Features.PluOld.Dto;

namespace Ws.WebApiScales.Features.PluOld.Validators;

internal sealed class ValidatorPluDto : AbstractValidator<PluDto>
{
    public ValidatorPluDto()
    {
        RuleFor(dto => dto.Uid)
            .NotEmpty().WithMessage("'UID' обязателен");
        RuleFor(dto => dto.PluNumber)
            .Equal(0).When(item => item.IsGroup).WithMessage("У группы 'Номер ПЛУ' должен быть 0")
            .GreaterThan(0).When(item => !item.IsGroup).WithMessage("'Номер ПЛУ' должен быть больше 0")
            .LessThanOrEqualTo(999).WithMessage("'Номер ПЛУ' должен быть < 1000.");
        RuleFor(dto => dto.Description)
            .Empty().When(item => item.IsGroup).WithMessage("У группы 'Описание' должно отсутствовать")
            .NotEmpty().When(item => !item.IsGroup).WithMessage("'Описание' обязательно");
        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("'Наименование' обязательно")
            .MaximumLength(150).WithMessage("'Наименование' не должно превышать 150 символов");
        RuleFor(dto => dto.FullName)
            .NotEmpty().When(item => !item.IsGroup).WithMessage("'Полное наименование' обязательно");
        RuleFor(dto => dto.MeasurementType)
            .Must(value => value is "шт" or "кг").When(item=>!item.IsGroup).WithMessage("'Тип ПЛУ' должен быть ['шт', 'кг']");
        RuleFor(dto => dto.Code)
            .NotEmpty()
            .Length(11).WithMessage("'Код ПЛУ' должен состоять из 11 символов.");
        RuleFor(dto => dto.Itf14)
            .Empty().When(item => item.IsGroup).WithMessage("У группы ПЛУ 'Itf14' должен отсутствовать")
            .Empty().When(item => item.IsCheckWeight).WithMessage("У весовой ПЛУ 'Itf14' должен отсутствовать")
            .Length(14).When(item => !item.IsGroup && !item.IsCheckWeight).WithMessage("'Itf14' должен состоять из 14 символов");
        RuleFor(dto => dto.StorageMethod)
            .Empty().When(item => item.IsGroup).WithMessage("У группы ПЛУ 'Способ хранения' должен отсутствовать")
            .Must(value => value is "Замороженное" or "Охлаждённое").When(item=>!item.IsGroup).WithMessage("'Способ хранения' должен быть ['Замороженное', 'Охлаждённое']");
        RuleFor(dto => dto.Ean13)
            .Empty().When(item => item.IsGroup).WithMessage("У группы ПЛУ 'Ean13' должен отсутствовать")
            .Length(13).When(item => !item.IsGroup).WithMessage("'Ean13' должен состоять из 13 символов");
        RuleFor(dto => dto.ShelfLife)
            .Empty().When(item => item.IsGroup).WithMessage("У группы ПЛУ 'Срок годности' должны отсутствовать")
            .NotEmpty().When(item => !item.IsGroup).WithMessage("'Срок годности' обязательны");
        RuleFor(dto => dto.BrandGuid)
            .Empty().When(item => item.IsGroup).WithMessage("У группы 'Бренд' должен отсутствовать")
            .NotEmpty().When(item => !item.IsGroup).WithMessage("'Бренд' обязателен.");
        RuleFor(dto => dto.BoxTypeGuid)
            .Empty().When(item => item.IsGroup).WithMessage("У группы ПЛУ 'Коробка' должна отсутствовать")
            .NotEmpty().When(item => !item.IsGroup).WithMessage("'Коробка' обязательна");
        RuleFor(dto => dto.BoxTypeName)
            .Empty().When(item => item.IsGroup).WithMessage("У группы ПЛУ 'Наименование коробки' должно отсутствовать")
            .NotEmpty().When(item => !item.IsGroup).WithMessage("'Наименование коробки' обязательно");
        RuleFor(dto => dto.BoxTypeWeight)
            .GreaterThan(0.001m).When(item => !item.IsGroup).WithMessage("'Вес коробки' обязателен");
        RuleFor(dto => dto.PackageTypeGuid)
            .Empty().When(item => item.IsGroup).WithMessage("У группы ПЛУ 'Пакет' должен отсутствовать");
        RuleFor(dto => dto.PackageTypeName)
            .NotEmpty().When(item => item.PackageTypeGuid != Guid.Empty).WithMessage("'Наименование пакета' обязательно");
        RuleFor(dto => dto.PackageTypeWeight)
            .GreaterThan(0.001m).When(item => item.PackageTypeGuid != Guid.Empty).WithMessage("'Вес пакета' обязателен");
    }
}