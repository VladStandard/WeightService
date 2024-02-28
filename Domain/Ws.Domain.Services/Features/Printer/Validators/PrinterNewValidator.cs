namespace Ws.Domain.Services.Features.Printer.Validators;

internal sealed class PrinterNewValidator : PrinterValidator
{
    public PrinterNewValidator() : base()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}