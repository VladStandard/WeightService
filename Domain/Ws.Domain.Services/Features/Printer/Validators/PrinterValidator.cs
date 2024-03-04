using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.Printer.Validators;

internal abstract class PrinterValidator : AbstractValidator<PrinterEntity>
{
    protected PrinterValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
    }
}