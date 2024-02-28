namespace Ws.Domain.Services.Features.User.Validators;

internal sealed class UserNewValidator : UserValidator
{
    public UserNewValidator() : base()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}