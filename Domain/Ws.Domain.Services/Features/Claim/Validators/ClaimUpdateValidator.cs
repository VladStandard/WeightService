namespace Ws.Domain.Services.Features.Claim.Validators;

internal sealed class ClaimUpdateValidator : ClaimValidator
{
    public ClaimUpdateValidator() : base()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}