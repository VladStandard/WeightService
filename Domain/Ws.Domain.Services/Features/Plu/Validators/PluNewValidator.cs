namespace Ws.Domain.Services.Features.Plu.Validators;

internal sealed class PluNewValidator : PluValidator
{
    public PluNewValidator() : base()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}