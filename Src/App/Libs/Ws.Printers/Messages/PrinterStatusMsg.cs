using Ws.Printers.Enums;

namespace Ws.Printers.Messages;

public class PrinterStatusMsg(PrinterStatus status)
{
    public PrinterStatus Status { get; } = status;
}