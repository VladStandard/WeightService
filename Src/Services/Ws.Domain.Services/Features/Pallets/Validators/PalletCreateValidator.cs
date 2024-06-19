using Ws.Domain.Models.Entities.Print;

namespace Ws.Domain.Services.Features.Pallets.Validators;

internal class PalletCreateValidator : AbstractValidator<Pallet>
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