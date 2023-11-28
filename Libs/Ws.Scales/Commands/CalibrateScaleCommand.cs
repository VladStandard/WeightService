using System.IO.Ports;
using Ws.Scales.Common;
using Ws.Scales.Utils;

namespace Ws.Scales.Commands;

public class CalibrateMassaCommand : ScaleCommandBase
{
    private static readonly byte[] Command = MassaKCommands.CmdSetZero;
    
    public CalibrateMassaCommand(SerialPort port) : base(port)
    {
    }
    
    public override void Activate()
    {
       Request(Command);
    }
}