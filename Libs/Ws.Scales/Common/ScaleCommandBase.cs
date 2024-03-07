using System.IO.Ports;

namespace Ws.Scales.Common;

internal abstract class ScaleCommandBase(SerialPort port, byte[] command)
{
    protected readonly SerialPort Port = port;

    public virtual void Activate()
    {
        Port.Write(command, 0, command.Length);
        Response();
    }

    protected virtual void Response() {}
}