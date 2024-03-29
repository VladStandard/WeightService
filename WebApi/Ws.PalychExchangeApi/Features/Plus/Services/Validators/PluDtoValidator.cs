using FluentValidation;
using Ws.PalychExchangeApi.Features.Plus.Dto;

namespace Ws.PalychExchangeApi.Features.Plus.Services.Validators;

internal sealed class PluDtoValidator : AbstractValidator<PluDto>
{
    public PluDtoValidator()
    {
        RuleFor(dto => dto.Uid)
            .NotEqual(Guid.Empty).WithMessage("UID - обязателен");

        #region Description

        RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("Наименование - обязательно")
            .MaximumLength(100).WithMessage("Наименование - не должно превышать 100 символов");

        RuleFor(dto => dto.FullName)
            .NotEmpty().WithMessage("Полное наименование - обязательно")
            .MaximumLength(150).WithMessage("Полное наименование - не должно превышать 150 символов");

        RuleFor(dto => dto.Description)
            .NotEmpty().WithMessage("Описание - обязательно")
            .MaximumLength(150).WithMessage("Описание - не должно превышать 150 символов");

        #endregion

        #region Itf/Ean

        RuleFor(dto => dto.Ean13)
            .Length(13).WithMessage("Ean13 - должен состоять из 13 символов");

        RuleFor(dto => dto.Itf14)
            .Empty().When(item => item.IsWeight).WithMessage("У весовой ПЛУ Itf14 - должен быть пустой")
            .Length(14).When(item => !item.IsWeight).WithMessage("Itf14 - должен состоять из 14 символов");


        #endregion

        #region Fk

        RuleFor(dto => dto.BoxUid)
            .NotEqual(Guid.Empty).WithMessage("Коробка - обязательна");

        RuleFor(dto => dto.BrandUid)
            .NotEqual(Guid.Empty).WithMessage("Бренд - обязателен");

        // RuleFor(dto => dto.ClipUid)
        //     .NotEqual(Guid.Empty).WithMessage("Клипса - обязательна");
        // RuleFor(dto => dto.BundleUid)
        //     .NotEqual(Guid.Empty).WithMessage("Пакет - обязателен");

        #endregion

        #region Other

        RuleFor(dto => dto.Number)
            .Must(number => number is > 0 and < 1000)
            .WithMessage("Номер - должен быть в диапазоне [1, 999]");

        RuleFor(dto => dto.BundleCount)
            .GreaterThan((short)0).WithMessage("Кол-во пакетов - обязательно")
            .LessThanOrEqualTo((short)100).WithMessage("Кол-во пакетов - должно быть < 100");

        RuleFor(dto => dto.ShelfLifeDays)
            .Must(days => days > 0)
            .WithMessage("Срок годности - должны быть в диапазоне [1, 255]");

        RuleFor(dto => dto.StorageMethod)
            .Must(value => value is "Замороженное" or "Охлаждённое")
            .WithMessage("Способ хранения - должен быть ['Замороженное', 'Охлаждённое']");

        #endregion
    }
}