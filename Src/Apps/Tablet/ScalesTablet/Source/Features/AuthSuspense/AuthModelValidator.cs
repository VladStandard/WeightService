namespace ScalesTablet.Source.Features.AuthSuspense;

using FluentValidation;

public class AuthModelValidator : AbstractValidator<AuthFormModel>
{
    public AuthModelValidator()
    {
        RuleFor(p => p.Password).Length(4).Matches("^[0-9]*$").WithName("Пароль");
    }
}