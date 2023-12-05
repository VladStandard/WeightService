using System.Net.Sockets;
using CommunityToolkit.Mvvm.Messaging;
using Ws.Printers.Commands;
using Ws.Printers.Enums;
using Ws.Printers.Events;

namespace Ws.Printers.Common;

public abstract class PrinterBase : IPrinter
{
    protected PrinterStatusEnum Status { get; set; }
    protected TcpClient TcpClient { get; set; }

    private readonly string _ip;
    private readonly int _port;
    
    public PrinterBase(string ip, int port)
    {
        _ip = ip;
        _port = port;
        
        TcpClient = new();
        TcpClient.ReceiveTimeout = 0_200;
        
        SetStatus(PrinterStatusEnum.IsDisabled);
        WeakReferenceMessenger.Default.Register<PrinterForceDisconnected>(this, ForceReconnectAsync);
    }

    private async void ForceReconnectAsync(Object recipient, PrinterForceDisconnected message)
    {
        if (Status == PrinterStatusEnum.IsDisabled)
            return;
        try
        {
            TcpClient.Dispose();
            TcpClient = new();
            TcpClient.ReceiveTimeout = 200;
            
            await TcpClient.ConnectAsync(_ip, _port).WaitAsync(TimeSpan.FromMilliseconds(200));
            SetStatus(PrinterStatusEnum.Ready);
        }
        catch (Exception _)
        {
            SetStatus(PrinterStatusEnum.IsForceDisconnected);
        }
    }

    public void PrintLabel(string zpl)
    {
        ExecuteCommand(new SendLabelCommand(TcpClient, zpl));
    }
    
    public IPrinter Connect()
    {
        Disconnect();
        SetStatus(PrinterStatusEnum.IsForceDisconnected);
        WeakReferenceMessenger.Default.Send(new PrinterForceDisconnected());
        return this;
    }
    
    public void Disconnect()
    {
        WeakReferenceMessenger.Default.Send(new GetPrinterStatusEvent(PrinterStatusEnum.IsDisabled));
        if (TcpClient.Connected) TcpClient.Close();
        TcpClient.Dispose();
    }
    
    public virtual void RequestStatus()
    {
    }
    
    
    private void SetStatus(PrinterStatusEnum state)
    {
        Status = state;
        WeakReferenceMessenger.Default.Send(new GetPrinterStatusEvent(Status));
    }
    
    protected void ExecuteCommand(PrinterCommandBase command)
    {
        if (Status == PrinterStatusEnum.IsDisabled) return;
        if (Status == PrinterStatusEnum.IsForceDisconnected)
        {
            WeakReferenceMessenger.Default.Send(new PrinterForceDisconnected());
            return;
        }
        command.Activate();
    }
    
    public void Dispose()
    {
        Disconnect();
        WeakReferenceMessenger.Default.Unregister<PrinterForceDisconnected>(this);
    }
}