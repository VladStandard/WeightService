using Ws.PrinterCore.Enums;

namespace Ws.PrinterCore.Common;

public interface IPrinterConnector : IDisposable
{

    public bool IsConnected { get; } 
    public void Connect(string ip);
    public bool SendCommand(string cmd);
    public PrinterStates GetState();
    public void UpdateStatus();
}