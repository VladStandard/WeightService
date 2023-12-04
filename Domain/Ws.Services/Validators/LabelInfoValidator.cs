using FluentValidation;
using Ws.Services.Dto;

namespace Ws.Services.Validators;

public class LabelInfoValidator : AbstractValidator<LabelInfoDto>
{
    public LabelInfoValidator()
    {
        RuleFor(i => i.Gtin).Length(14).WithMessage("Длина ГТИН должна быть 14 символов");
        RuleFor(i => i.Itf).Length(14).WithMessage("Длина ИТФ должна быть 14 символов").When(i => !i.IsCheckWeight);
        RuleFor(i => i.Weight).GreaterThanOrEqualTo((decimal)0.100)
            .WithMessage("Вес должен быть > 0.100 у весовой ПЛУ").When(i => i.IsCheckWeight);
        RuleFor(i => i.Kneading).GreaterThanOrEqualTo((short)0).WithMessage("Замес должен быть >= 0");
        RuleFor(i => i.LineCounter).GreaterThanOrEqualTo(0).WithMessage("Счетчик линии должен быть >= 0");
        RuleFor(i => i.BundleCount).GreaterThanOrEqualTo((short)0).WithMessage("Кол-во пакетов должно быть >= 0");
        RuleFor(i => i.Address).NotEmpty().WithMessage("Адресс не должен быть пустым");
        RuleFor(i => i.PluName).NotEmpty().WithMessage("Имя плу не должно быть пустым");
        RuleFor(i => i.PluFullName).NotEmpty().WithMessage("Полное имя плу не должно быть пустым");
        RuleFor(i => i.PluDescription).NotEmpty().WithMessage("Описание плу не должно быть пустым");
        RuleFor(i => i.ExpirationDt).NotEmpty().WithMessage("Срок годности должен быть установлен");
        RuleFor(i => i.ProductDt)
            .NotEmpty().WithMessage("Дата изготовления должна быть установлена")
            .LessThan(x => x.ExpirationDt).WithMessage("Срок годности должен быть > Даты изготовления");
    }
}