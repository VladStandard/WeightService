using System.Net.Sockets;
using System.Text;
using CommunityToolkit.Mvvm.Messaging;
using Ws.Printers.Common;
using Ws.Printers.Enums;
using Ws.Printers.Features.Zebra.Utils;
using Ws.Printers.Messages;

namespace Ws.Printers.Features.Zebra.Commands;

internal class ZebraGetStatusCommands(TcpClient tcp) : PrinterCommandBase(tcp, ZebraCommandUtil.GetStatus)
{
    protected override void Response(NetworkStream stream)
    {
        byte[] buffer = new byte[76];
        _ = stream.Read(buffer, 0, buffer.Length);
        string strStatus = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        PrinterStatusEnum status = ZebraStatusParserUtils.ParseStatusString(strStatus);
        WeakReferenceMessenger.Default.Send(new PrinterStatusMsg(status));
    }
}