namespace Ws.Printers.Common;

public interface IPrinter : IDisposable
{
    public void GetStatus();
    public IPrinter Connect();
}