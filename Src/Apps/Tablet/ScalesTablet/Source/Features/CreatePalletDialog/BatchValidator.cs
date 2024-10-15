using FluentValidation;
using ScalesTablet.Source.Shared.Models;

namespace ScalesTablet.Source.Features.CreatePalletDialog;

public class BatchValidator : AbstractValidator<Batch>
{
    public BatchValidator()
    {
        RuleFor(p => p.Plu).Length(5).Matches("^[0-9]*$").WithName("Номер ПЛУ");
        RuleFor(p => p.Date).MinimumLength(2).MaximumLength(6).WithName("Дата");
        RuleFor(p => p.Weight).LessThanOrEqualTo(9999).GreaterThan(0).WithName("Вес");
    }
}