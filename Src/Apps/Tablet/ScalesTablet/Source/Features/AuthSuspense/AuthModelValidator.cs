namespace ScalesTablet.Source.Features.AuthSuspense;

using FluentValidation;

public class AuthModelValidator : AbstractValidator<AuthFormModel>
{
    public AuthModelValidator()
    {
        RuleFor(p => p.Password).Must(value => ushort.TryParse(value, out _)).WithName("Пароль");
    }
}