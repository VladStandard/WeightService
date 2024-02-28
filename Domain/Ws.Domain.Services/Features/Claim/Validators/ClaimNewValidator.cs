namespace Ws.Domain.Services.Features.Claim.Validators;

internal sealed class ClaimNewValidator : ClaimValidator
{
    public ClaimNewValidator() : base()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}