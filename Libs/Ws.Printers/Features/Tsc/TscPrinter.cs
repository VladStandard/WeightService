using System.Net;
using Ws.Printers.Common;
using Ws.Printers.Features.Tsc.Commands;

namespace Ws.Printers.Features.Tsc;

internal class TscPrinter(IPAddress ip, int port) : PrinterBase(ip, port)
{
    public override void RequestStatus() => ExecuteCommand(new TscGetStatusCommand(TcpClient));
}