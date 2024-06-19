using Ws.Domain.Models.Entities.Devices;

namespace Ws.Domain.Services.Features.Printers.Validators;

internal abstract class PrinterValidator : AbstractValidator<Printer>
{
    protected PrinterValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
    }
}