namespace Ws.Domain.Services.Features.Printer.Validators;

internal sealed class PrinterUpdateValidator : PrinterValidator
{
    public PrinterUpdateValidator() : base()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}