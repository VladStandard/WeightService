namespace Ws.Domain.Services.Features.Printer.Validators;

internal abstract class PrinterValidator : AbstractValidator<Models.Entities.Devices.Printer>
{
    protected PrinterValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
    }
}