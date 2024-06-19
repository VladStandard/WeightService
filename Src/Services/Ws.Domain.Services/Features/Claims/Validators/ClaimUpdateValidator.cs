namespace Ws.Domain.Services.Features.Claims.Validators;

internal sealed class ClaimUpdateValidator : ClaimValidator
{
    public ClaimUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}