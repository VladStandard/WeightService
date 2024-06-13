namespace Ws.Domain.Services.Features.Users.Validators;

internal sealed class UserNewValidator : UserValidator
{
    public UserNewValidator()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}