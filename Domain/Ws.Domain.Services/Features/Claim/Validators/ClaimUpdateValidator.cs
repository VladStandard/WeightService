namespace Ws.Domain.Services.Features.Claim.Validators;

internal sealed class ClaimUpdateValidator : ClaimValidator
{
    public ClaimUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}