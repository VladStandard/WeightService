using CommunityToolkit.Mvvm.Messaging;
using SuperSimpleTcp;
using Ws.Printers.Common;
using Ws.Printers.Enums;
using Ws.Printers.Events;

namespace Ws.Printers.Main.Tsc;

public class TscPrinter : PrinterBase
{
    protected override String GetStatusCommand => "\x1B!?";
    
    public TscPrinter(String ip, Int32 port) : base(ip, port)
    {
    }
    
    protected override void TcpClientDataReceived(Object? sender, DataReceivedEventArgs e)
    {
        ArraySegment<byte> receivedBytes = e.Data;
        if (receivedBytes.Array == null)
        {
            State = PrinterStatusEnum.Unknown;
            return;
        }
        State = GetStatusAsEnum(receivedBytes.Array[receivedBytes.Offset]);
        WeakReferenceMessenger.Default.Send(new GetPrinterStatusEvent(State));
    }
    
    private static PrinterStatusEnum GetStatusAsEnum(byte value)
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
            _ => PrinterStatusEnum.Unknown,
        };
    }

}