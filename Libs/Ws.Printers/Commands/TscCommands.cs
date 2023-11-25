using Ws.Printers.Common;

namespace Ws.Printers.Commands;

public class TscCommands : IPrinterCommands
{
    public String GetStatus => "\x1B!?";
}