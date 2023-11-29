using System.Net.Sockets;
using System.Text;
using CommunityToolkit.Mvvm.Messaging;
using Ws.Printers.Common;
using Ws.Printers.Enums;
using Ws.Printers.Events;
using Ws.Printers.Utils;

namespace Ws.Printers.Commands.Zebra;

public class ZebraGetStatusCommands : PrinterCommandBase
{
    private static string Command => "! U1 getvar \"device.host_status\"\r\n";
    public ZebraGetStatusCommands(TcpClient tcp) : base(tcp)
    {
    }

    public override void Activate()
    {
        Request(Command);
    }

    protected override void Response(NetworkStream stream)
    {
        byte[] buffer = new byte[76];
        _ = stream.Read(buffer, 0, buffer.Length);
        string strStatus = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        PrinterStatusEnum status = ZebraStatusParserUtils.ParseStatusString(strStatus);
        WeakReferenceMessenger.Default.Send(new GetPrinterStatusEvent(status));
    }
}