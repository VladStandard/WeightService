namespace Ws.Domain.Services.Features.PalletMen.Validators;

internal class PalletManUpdateValidator : PalletManValidator
{
    public PalletManUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}