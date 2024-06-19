namespace Ws.Domain.Services.Features.Printers.Validators;

internal sealed class PrinterNewValidator : PrinterValidator
{
    public PrinterNewValidator()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}