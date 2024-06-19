namespace Ws.Domain.Services.Features.Plus.Validators;

internal class PluUpdateValidator : PluValidator
{
    public PluUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}