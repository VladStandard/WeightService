using System.Net;
using Ws.Printers.Common;
using Ws.Printers.Features.Zebra.Commands;

namespace Ws.Printers.Features.Zebra;

internal class ZebraPrinter(IPAddress ip, int port) : PrinterBase(ip, port)
{
    public override void RequestStatus() => ExecuteCommand(new ZebraGetStatusCommands(TcpClient));
}