using Ws.Printers.Commands.Tsc;
using Ws.Printers.Common;

namespace Ws.Printers.Main;

public class TscPrinter : PrinterBase
{
    public TscPrinter(string ip, int port) : base(ip, port)
    {
    }

    public override void RequestStatus()
    {
        ExecuteCommand(new TscGetStatusCommand(TcpClient));
    }
}