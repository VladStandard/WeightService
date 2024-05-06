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

    internal static PrinterStatusEnum GetStatus(byte value)
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