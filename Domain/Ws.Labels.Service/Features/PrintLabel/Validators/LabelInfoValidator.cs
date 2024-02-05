using FluentValidation;
using Ws.Labels.Service.Features.PrintLabel.Dto;

namespace Ws.Labels.Service.Features.PrintLabel.Validators;

public class LabelInfoValidator : AbstractValidator<LabelWeightInfoDto>
{
    public LabelInfoValidator()
    {
        RuleFor(i => i.Gtin).Length(14).WithMessage("Длина ГТИН должна быть 14 символов");
        RuleFor(i => i.Weight).GreaterThanOrEqualTo((decimal)0.100)
            .WithMessage("Вес должен быть > 0.100 у весовой ПЛУ").When(i => i.IsCheckWeight);
        RuleFor(i => i.LineNumber).GreaterThanOrEqualTo(0).WithMessage("Номер линии должен быть >= 1");
        RuleFor(i => i.Kneading).GreaterThanOrEqualTo((short)1).WithMessage("Замес должен быть >= 1");
        RuleFor(i => i.LineCounter).GreaterThanOrEqualTo(0).WithMessage("Счетчик линии должен быть >= 0");
        RuleFor(i => i.BundleCount).GreaterThanOrEqualTo((short)0).WithMessage("Кол-во пакетов должно быть >= 0");
        RuleFor(i => i.Address).NotEmpty().WithMessage("Адрес не должен быть пустым");
        RuleFor(i => i.PluFullName).NotEmpty().WithMessage("Полное имя плу не должно быть пустым");
        RuleFor(i => i.PluNumber).GreaterThanOrEqualTo((short)0).WithMessage("Номер плу должен быть >= 0");
        RuleFor(i => i.PluDescription).NotEmpty().WithMessage("Описание плу не должно быть пустым");
        RuleFor(i => i.ExpirationDt).NotEmpty().WithMessage("Срок годности должен быть установлен");
        RuleFor(i => i.Template).NotEmpty().WithMessage("Шаблон не установлен");
        RuleFor(i => i.ProductDt)
            .NotEmpty().WithMessage("Дата изготовления должна быть установлена")
            .LessThan(x => x.ExpirationDt).WithMessage("Срок годности должен быть > Даты изготовления");
    }
}