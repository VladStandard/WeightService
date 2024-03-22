using System.Net.Sockets;
using System.Text;

namespace Ws.Printers.Common;

internal class PrinterCommandBase(TcpClient tcp, string command)
{
    public virtual void Request()
    {
        byte[] commandBytes = Encoding.UTF8.GetBytes(command);
        NetworkStream stream = tcp.GetStream();
        stream.Write(commandBytes, 0, commandBytes.Length);
        Response(stream);
    }

    protected virtual void Response(NetworkStream stream) { }
}