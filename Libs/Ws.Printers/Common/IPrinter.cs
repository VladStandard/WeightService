namespace Ws.Printers.Common;

public interface IPrinter : IDisposable
{
    public void RequestStatus();
    public void PrintLabel(string zpl);
    public void Connect();
}