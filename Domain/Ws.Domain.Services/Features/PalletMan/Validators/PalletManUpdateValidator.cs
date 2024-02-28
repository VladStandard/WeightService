namespace Ws.Domain.Services.Features.PalletMan.Validators;

internal class PalletManUpdateValidator : PalletManValidator
{
    public PalletManUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}