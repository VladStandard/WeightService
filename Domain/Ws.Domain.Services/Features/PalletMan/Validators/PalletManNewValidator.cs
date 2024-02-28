namespace Ws.Domain.Services.Features.PalletMan.Validators;

internal sealed class PalletManNewValidator : PalletManValidator
{
    public PalletManNewValidator() : base()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}