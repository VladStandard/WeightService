namespace Ws.Domain.Services.Features.Claims.Validators;

internal sealed class ClaimNewValidator : ClaimValidator
{
    public ClaimNewValidator()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}