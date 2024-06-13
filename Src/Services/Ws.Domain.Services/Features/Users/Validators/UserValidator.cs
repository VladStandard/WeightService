using Ws.Domain.Models.Entities.Users;

namespace Ws.Domain.Services.Features.Users.Validators;

internal abstract class UserValidator : AbstractValidator<User>
{
    protected UserValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .Must(name => name.StartsWith("KOLBASA-VS\\", StringComparison.OrdinalIgnoreCase));
    }
}