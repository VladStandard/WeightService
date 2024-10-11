using FluentValidation;
using ScalesTablet.Source.Shared.Models;

namespace ScalesTablet.Source.Features.CreatePalletDialog;

public class PluStageValidator : AbstractValidator<Pallet>
{
    public PluStageValidator()
    {
        RuleFor(p => p.DefaultPlu).Length(5).Matches("^[0-9]*$").When(p => p.Mono).WithName("Номер ПЛУ");
        RuleFor(p => p.DefaultPlu).Empty().When(p => !p.Mono).WithName("Номер ПЛУ");
    }
}