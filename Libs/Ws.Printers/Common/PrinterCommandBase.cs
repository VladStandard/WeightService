using System.Net.Sockets;
using System.Text;
using CommunityToolkit.Mvvm.Messaging;
using Ws.Printers.Events;

namespace Ws.Printers.Common;

public abstract class PrinterCommandBase
{
    protected readonly TcpClient Tcp;

    protected PrinterCommandBase(TcpClient tcp)
    {
        Tcp = tcp;
    }
    
    public virtual void Activate()
    {
        throw new NotImplementedException();
    }

    protected virtual void Response(NetworkStream stream) {}

    protected void Request(string command)
    {
        try
        {
            byte[] commandBytes = Encoding.UTF8.GetBytes(command);
            NetworkStream stream = Tcp.GetStream();
            stream.Write(commandBytes, 0, commandBytes.Length);
            Response(stream);
        }
        catch (TimeoutException ex)
        {

        }
        catch (Exception e)
        {
            WeakReferenceMessenger.Default.Send(new PrinterForceDisconnected());
        }
    }
}