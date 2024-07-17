namespace Ws.Domain.Services.Features.Printers.Validators;

internal sealed class PrinterUpdateValidator : PrinterValidator
{
    public PrinterUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}