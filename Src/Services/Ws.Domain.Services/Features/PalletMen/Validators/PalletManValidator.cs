using Ws.Domain.Models.Entities.Users;

namespace Ws.Domain.Services.Features.PalletMen.Validators;

internal abstract class PalletManValidator : AbstractValidator<PalletMan>
{
    protected PalletManValidator()
    {
        RuleFor(item => item.Fio.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Fio.Surname)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Fio.Patronymic)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Password)
            .Length(4).WithMessage("'Password' должен состоять из 4х символов.")
            .Matches("^[0-9]+$").WithMessage("Пароль должен содержать только цифры.");
    }
}