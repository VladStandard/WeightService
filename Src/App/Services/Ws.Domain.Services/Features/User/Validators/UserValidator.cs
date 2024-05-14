namespace Ws.Domain.Services.Features.User.Validators;

internal abstract class UserValidator : AbstractValidator<Models.Entities.Users.User>
{
    protected UserValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .Must(name => name.StartsWith("KOLBASA-VS\\", StringComparison.OrdinalIgnoreCase));
    }
}