using System.Net.Sockets;
using Ws.Printers.Common;

namespace Ws.Printers.Commands;

public class SendLabelCommand :  PrinterCommandBase
{
    private string Zpl { get; set; }

    public SendLabelCommand(TcpClient tcp, string zpl) : base(tcp)
    {
        Zpl = zpl;
    }
    
    public override void Activate()
    {
        Request(Zpl);
    }
}