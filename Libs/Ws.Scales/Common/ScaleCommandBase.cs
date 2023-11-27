using System.IO.Ports;

namespace Ws.Scales.Common;

public abstract class ScaleCommandBase
{
    private readonly SerialPort _port;
    
    protected ScaleCommandBase(SerialPort port)
    {
        _port = port;
    }

    protected void Send(byte[] bytes)
    {
        try
        {
            _port.Write(bytes, 0, bytes.Length);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}