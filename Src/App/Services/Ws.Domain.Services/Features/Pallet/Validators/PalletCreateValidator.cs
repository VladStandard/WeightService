namespace Ws.Domain.Services.Features.Pallet.Validators;

internal class PalletCreateValidator : AbstractValidator<Models.Entities.Print.Pallet>
{
    public PalletCreateValidator()
    {
        RuleFor(item => item.Barcode)
            .NotEmpty();
        RuleFor(item => item.Counter)
            .GreaterThan(0);
        RuleFor(item => item.Counter)
            .GreaterThan(0);
    }
}