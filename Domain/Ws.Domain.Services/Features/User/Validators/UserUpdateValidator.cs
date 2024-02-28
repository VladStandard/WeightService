namespace Ws.Domain.Services.Features.User.Validators;

internal sealed class UserUpdateValidator : UserValidator
{
    public UserUpdateValidator() : base()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}