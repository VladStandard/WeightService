using System.Net.Sockets;
using CommunityToolkit.Mvvm.Messaging;
using Ws.Printers.Common;
using Ws.Printers.Enums;
using Ws.Printers.Features.Tsc.Constants;
using Ws.Printers.Messages;

namespace Ws.Printers.Features.Tsc.Commands;

internal class TscGetStatusCmd(TcpClient tcp) : PrinterCommandBase(tcp, TscCommandConsts.GetStatus)
{
    protected override void Response(NetworkStream stream)
    {
        int buffer = stream.ReadByte();
        WeakReferenceMessenger.Default.Send(new PrinterStatusMsg(GetStatus((byte)buffer)));
    }

    internal static PrinterStatus GetStatus(byte value)
    {
        return value switch
        {
            0x00 => PrinterStatus.Ready,
            0x01 => PrinterStatus.HeadOpen,
            0x02 => PrinterStatus.PaperJam,
            0x04 => PrinterStatus.PaperOut,
            0x08 => PrinterStatus.RibbonOut,
            0x10 => PrinterStatus.Paused,
            0x20 => PrinterStatus.Busy,
            _ => PrinterStatus.Unknown
        };
    }
}