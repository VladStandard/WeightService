namespace Ws.Domain.Services.Features.Users.Validators;

internal sealed class UserUpdateValidator : UserValidator
{
    public UserUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}