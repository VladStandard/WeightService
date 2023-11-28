using System.IO.Ports;
using CommunityToolkit.Mvvm.Messaging;
using Ws.Scales.Events;

namespace Ws.Scales.Common;

public abstract class ScaleCommandBase
{
    protected readonly SerialPort Port;

    protected ScaleCommandBase(SerialPort port)
    {
        Port = port;
    }
    
    public virtual void Activate()
    {
        throw new NotImplementedException();
    }

    protected virtual void Response() {}

    protected void Request(byte[] command)
    {
        try
        {
            Port.Write(command, 0, command.Length);
            Response();
        }
        catch (TimeoutException ex)
        {

        }
        catch (Exception e)
        {
            WeakReferenceMessenger.Default.Send(new ScalesForceDisconnected());
        }
    }
}