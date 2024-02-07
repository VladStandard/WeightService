using Ws.Printers.Enums;

namespace Ws.Printers.Events;

public class GetPrinterStatusEvent(PrinterStatusEnum status)
{
    public PrinterStatusEnum Status { get; } = status;
}