namespace Ws.Domain.Services.Features.Arms.Validators;

internal sealed class ArmUpdateValidator : ArmValidator
{
    public ArmUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}