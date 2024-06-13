using Ws.Domain.Models.Entities.Users;

namespace Ws.Domain.Services.Features.Claims.Validators;

public abstract class ClaimValidator : AbstractValidator<Claim>
{
    protected ClaimValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}