using System.IO.Ports;
using Ws.Scales.Common;
using Ws.Scales.Utils;

namespace Ws.Scales.Commands;

internal class CalibrateCommand(SerialPort port) : ScaleCommandBase(port, MassaKCommands.CmdSetZero.Value)
{
    protected override void Response()
    {
        byte[] buffer = new byte[9];
        Port.Read(buffer, 0, buffer.Length);
    }
}