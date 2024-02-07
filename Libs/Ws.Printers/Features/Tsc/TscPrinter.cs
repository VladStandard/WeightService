using Ws.Printers.Common;
using Ws.Printers.Features.Tsc.Commands;

namespace Ws.Printers.Features.Tsc;

public class TscPrinter(string ip, int port) : PrinterBase(ip, port)
{
    public override void RequestStatus()
    {
        ExecuteCommand(new TscGetStatusCommand(TcpClient));
    }
}