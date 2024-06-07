namespace Ws.Domain.Services.Features.Plus.Validators;

internal sealed class PluNewValidator : PluValidator
{
    public PluNewValidator()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}