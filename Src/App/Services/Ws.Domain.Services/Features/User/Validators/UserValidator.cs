using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.User.Validators;

internal abstract class UserValidator : AbstractValidator<UserEntity>
{
    protected UserValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .Must(name => name.StartsWith("KOLBASA-VS\\", StringComparison.OrdinalIgnoreCase));
    }
}