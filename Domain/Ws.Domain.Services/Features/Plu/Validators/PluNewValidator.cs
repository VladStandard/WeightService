namespace Ws.Domain.Services.Features.Plu.Validators;

internal sealed class PluNewValidator : PluValidator
{
    public PluNewValidator()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}