using Ws.Domain.Models.Entities.Print;

namespace Ws.Domain.Services.Features.Pallet.Validators;

internal class PalletCreateValidator : AbstractValidator<PalletEntity>
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