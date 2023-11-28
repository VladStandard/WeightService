using System.IO.Ports;

namespace Ws.Scales.Common;

public abstract class ScaleCommandBase
{
    protected readonly SerialPort Port;
    
    protected ScaleCommandBase(SerialPort port)
    {
        Port = port;
    }

    protected void Send(byte[] bytes)
    {
        try
        {
            Port.Write(bytes, 0, bytes.Length);
        }
        catch (TimeoutException ex)
        {
            
        }
    }
}