using System.Text;
using CommunityToolkit.Mvvm.Messaging;
using SuperSimpleTcp;
using Ws.Printers.Commands;
using Ws.Printers.Common;
using Ws.Printers.Enums;
using Ws.Printers.Events;

namespace Ws.Printers.Main.Zebra;

public class ZebraPrinter : PrinterBase
{
    public ZebraPrinter(string ip, int port) : base(ip, port)
    {
        Commands = new ZebraCommands();
    }
    
    protected override void TcpClientDataReceived(Object? sender, DataReceivedEventArgs e)
    {
        if (e.Data.Array == null)
        {
            State = PrinterStatusEnum.Unknown;
            return;
        }
        string strStatus = Encoding.UTF8.GetString(e.Data.Array, e.Data.Offset, e.Data.Count);
        State = ZebraStatusParserUtils.ParseStatusString(strStatus);
        WeakReferenceMessenger.Default.Send(new GetPrinterStatusEvent(State));
    }
}