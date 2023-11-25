namespace Ws.Printers.Common;

public interface IPrinter : IDisposable
{
    public void RequestStatus();
    public IPrinter Connect();
}