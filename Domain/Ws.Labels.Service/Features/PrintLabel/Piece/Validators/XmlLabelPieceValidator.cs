using FluentValidation;
using Ws.Labels.Service.Features.PrintLabel.Piece.Models;

namespace Ws.Labels.Service.Features.PrintLabel.Piece.Validators;

public class XmlLabelPieceValidator : AbstractValidator<XmlPieceLabelModel>
{
    public XmlLabelPieceValidator()
    {
        RuleFor(i => i.LineNumber).GreaterThanOrEqualTo(0).WithMessage("Номер линии должен быть >= 1");
        RuleFor(i => i.Kneading).GreaterThanOrEqualTo((short)1).WithMessage("Замес должен быть >= 1");
        RuleFor(i => i.LineCounter).GreaterThanOrEqualTo(0).WithMessage("Счетчик линии должен быть >= 0");
        RuleFor(i => i.LineAddress).NotEmpty().WithMessage("Адрес не должен быть пустым");
        RuleFor(i => i.PluFullName).NotEmpty().WithMessage("Полное имя плу не должно быть пустым");
        RuleFor(i => i.PluNumber).GreaterThanOrEqualTo((short)0).WithMessage("Номер плу должен быть >= 0");
        RuleFor(i => i.PluDescription).NotEmpty().WithMessage("Описание плу не должно быть пустым");
        RuleFor(i => i.ExpirationDtValue).NotEmpty().WithMessage("Срок годности должен быть установлен");
        RuleFor(i => i.ProductDtValue)
            .NotEmpty().WithMessage("Дата изготовления должна быть установлена")
            .LessThan(x => x.ExpirationDtValue).WithMessage("Срок годности должен быть > Даты изготовления");
    }
}