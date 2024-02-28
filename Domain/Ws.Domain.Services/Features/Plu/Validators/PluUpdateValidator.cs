namespace Ws.Domain.Services.Features.Plu.Validators;

internal class PluUpdateValidator : PluValidator
{
    public PluUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}