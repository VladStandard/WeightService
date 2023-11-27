using System.IO.Ports;
using Ws.Scales.Common;
using Ws.Scales.Main;

namespace Ws.Scales.Commands;

public class CalibrateMassaCommand : ScaleCommandBase, IScaleCommand
{

    public CalibrateMassaCommand(SerialPort port) : base(port)
    {
    }
    public void Request()
    {
        Send(MassaKCommands.CmdSetZero);
    }
    
    public void Response(Byte[] bytes)
    {
        
    }
}