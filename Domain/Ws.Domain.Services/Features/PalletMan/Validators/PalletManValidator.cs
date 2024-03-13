using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.PalletMan.Validators;

internal abstract class PalletManValidator : AbstractValidator<PalletManEntity>
{
    protected PalletManValidator()
    {
        RuleFor(item => item.Uid1C)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Surname)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Patronymic)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Password)
            .Length(4).WithMessage("'Password' должен состоять из 4х символов.")
            .Matches("^[0-9]+$").WithMessage("Пароль должен содержать только цифры.");
    }
}