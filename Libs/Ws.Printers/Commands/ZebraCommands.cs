using Ws.Printers.Common;

namespace Ws.Printers.Commands;

public class ZebraCommands : IPrinterCommands
{ 
    public String GetStatus => "! U1 getvar \"device.host_status\"\r\n";
}