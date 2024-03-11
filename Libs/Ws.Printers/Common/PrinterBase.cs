using System.Net;
using System.Net.Sockets;
using CommunityToolkit.Mvvm.Messaging;
using Ws.Printers.Enums;
using Ws.Printers.Events;
using Ws.Shared.Utils;

namespace Ws.Printers.Common;

internal abstract class PrinterBase(IPAddress ip, int port) : IPrinter
{
    protected TcpClient TcpClient { get; set; } = new();
    protected PrinterStatusEnum Status { get; set; } = PrinterStatusEnum.IsDisabled;

    #region Abstract

    public abstract void RequestStatus();

    #endregion
    
    #region Public

    public void Connect()
    {
        try
        {
            TcpClient.Dispose();
            TcpClient = new() { ReceiveTimeout = 200 };
            
            TcpClient.ConnectAsync(ip, port).WaitAsync(TimeSpan.FromMilliseconds(200));
            SetStatus(PrinterStatusEnum.Ready);
        }
        catch (Exception)
        {
            SetStatus(PrinterStatusEnum.IsForceDisconnected);
        }
    }
    public void Dispose() => Disconnect();
    public void PrintLabel(string zpl) => ExecuteCommand(new(TcpClient, zpl));

    #endregion

    #region Private

    private void Disconnect()
    {
        WeakReferenceMessenger.Default.Send(new GetPrinterStatusEvent(PrinterStatusEnum.IsDisabled));
        if (TcpClient.Connected) TcpClient.Close();
        TcpClient.Dispose();
    }
    private void SetStatus(PrinterStatusEnum state)
    {
        Status = state;
        WeakReferenceMessenger.Default.Send(new GetPrinterStatusEvent(Status));
    }
    protected void ExecuteCommand(PrinterCommandBase command)
    {
        if (Status is PrinterStatusEnum.IsDisabled) return;
        
        try
        {
            if (Status is PrinterStatusEnum.IsForceDisconnected)  Connect();
            ErrorUtil.Suppress<TimeoutException>(command.Request);
        }
        catch (Exception)
        {
            Connect();
        }
    }

    #endregion
}