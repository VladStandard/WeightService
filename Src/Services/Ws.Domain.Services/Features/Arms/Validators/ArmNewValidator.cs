namespace Ws.Domain.Services.Features.Arms.Validators;

internal sealed class ArmNewValidator : ArmValidator
{
    public ArmNewValidator()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}