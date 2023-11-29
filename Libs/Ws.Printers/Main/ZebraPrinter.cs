using Ws.Printers.Commands.Zebra;
using Ws.Printers.Common;

namespace Ws.Printers.Main;

public class ZebraPrinter : PrinterBase
{
    public ZebraPrinter(string ip, int port) : base(ip, port)
    {
    }
    
    public override void RequestStatus()
    {
        ExecuteCommand(new ZebraGetStatusCommands(TcpClient));
    }
}