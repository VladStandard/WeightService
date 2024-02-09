using System.Net.Sockets;
using CommunityToolkit.Mvvm.Messaging;
using Ws.Printers.Common;
using Ws.Printers.Enums;
using Ws.Printers.Events;

namespace Ws.Printers.Features.Tsc.Commands;

public class TscGetStatusCommand(TcpClient tcp) : PrinterCommandBase(tcp, "\x1B!?")
{
    protected override void Response(NetworkStream stream)
    {
        byte[] buffer = new byte[1];
        _ = stream.Read(buffer, 0, buffer.Length);
        WeakReferenceMessenger.Default.Send(new GetPrinterStatusEvent(GetStatus(buffer[0])));
    }

    private static PrinterStatusEnum GetStatus(byte value)
    {
        return value switch
        {
            0x00 => PrinterStatusEnum.Ready,
            0x01 => PrinterStatusEnum.HeadOpen,
            0x02 => PrinterStatusEnum.PaperJam,
            0x04 => PrinterStatusEnum.PaperOut,
            0x08 => PrinterStatusEnum.RibbonOut,
            0x10 => PrinterStatusEnum.Paused,
            0x20 => PrinterStatusEnum.Busy,
            _ => PrinterStatusEnum.Unknown
        };
    }
}