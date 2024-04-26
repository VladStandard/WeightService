using Ws.Printers.Enums;

namespace Ws.Printers.Messages;

public class PrinterStatusMsg(PrinterStatusEnum status)
{
    public PrinterStatusEnum Status { get; } = status;
}