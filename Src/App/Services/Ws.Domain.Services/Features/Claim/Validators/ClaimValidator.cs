using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.Claim.Validators;

public abstract class ClaimValidator : AbstractValidator<ClaimEntity>
{
    protected ClaimValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}