namespace Ws.Printers.Common;

public interface IPrinter : IDisposable
{
    public void Connect(string ip, int port);
    public void GetStatus();
}