using Ws.Printers.Enums;

namespace Ws.Printers.Events;

public class GetPrinterStatusEvent
{
    public PrinterStatusEnum Status { get; init; }
    
    public GetPrinterStatusEvent(PrinterStatusEnum status)
    {
        Status = status;
    }
}