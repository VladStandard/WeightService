using System.IO.Ports;
using Ws.Scales.Common;
using Ws.Scales.Main;

namespace Ws.Scales.Commands;

public class CalibrateMassaCommand : ScaleCommandBase, IScaleCommand
{
    private static readonly byte[] Command = MassaKCommands.CmdSetZero;
    
    public CalibrateMassaCommand(SerialPort port) : base(port)
    {
        
    }
    
    public void Request()
    {
        Send(Command);
    }
}