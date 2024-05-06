using System.Net.Sockets;
using System.Text;
using CommunityToolkit.Mvvm.Messaging;
using Ws.Printers.Common;
using Ws.Printers.Enums;
using Ws.Printers.Features.Zebra.Constants;
using Ws.Printers.Messages;

namespace Ws.Printers.Features.Zebra.Commands;

internal partial class ZebraGetStatusCmd(TcpClient tcp) : PrinterCommandBase(tcp, ZebraCommandConsts.GetStatus)
{
    protected override void Response(NetworkStream stream)
    {
        byte[] buffer = new byte[76];
        _ = stream.Read(buffer, 0, buffer.Length);
        string strStatus = Encoding.UTF8.GetString(buffer, 0, buffer.Length).Replace("\"", string.Empty);
        string[] arrayStatus = strStatus.Split(CmdSeparators, StringSplitOptions.RemoveEmptyEntries);

        PrinterStatusEnum status = ParseStatusString(arrayStatus);
        WeakReferenceMessenger.Default.Send(new PrinterStatusMsg(status));
    }
}