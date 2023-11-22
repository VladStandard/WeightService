using System.Text;
using SuperSimpleTcp;
using Ws.Printers.Common;
using Ws.Printers.Enums;
using Ws.Printers.Utils;

namespace Ws.Printers.Main.Zebra;

public class ZebraPrinter : PrinterBase
{
    protected override String GetStatusCommand => "! U1 getvar \"device.host_status\"\r\n";

    public ZebraPrinter(String ip, Int32 port) : base(ip, port)
    {
    }
    
    protected override void TcpClientDataReceived(Object? sender, DataReceivedEventArgs e)
    {
        if (e.Data.Array == null)
        {
            State = PrinterStatusEnum.Unknown;
            return;
        }
        string strStatus = Encoding.UTF8.GetString(e.Data.Array, e.Data.Offset, e.Data.Count);
        PrinterStatusEnum status = ZebraStatusParserUtils.ParseStatusString(strStatus);
    }
}