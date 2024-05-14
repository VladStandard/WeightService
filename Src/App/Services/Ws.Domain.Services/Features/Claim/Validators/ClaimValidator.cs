namespace Ws.Domain.Services.Features.Claim.Validators;

public abstract class ClaimValidator : AbstractValidator<Models.Entities.Users.Claim>
{
    protected ClaimValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}