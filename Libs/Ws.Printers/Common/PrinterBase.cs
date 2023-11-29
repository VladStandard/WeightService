using System.Net.Sockets;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.IdentityModel.Tokens;
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
        WeakReferenceMessenger.Default.Register<PrinterForceDisconnected>(this, ForceReconnect);
    }
    
    private void ForceReconnect(Object recipient, PrinterForceDisconnected message)
    {
        if (Status == PrinterStatusEnum.IsDisabled)
            return;
        try
        {
            if (TcpClient.Connected) 
                TcpClient.Close();
            TcpClient = new();
            TcpClient.ReceiveTimeout = 200;
            bool isConnected = TcpClient.ConnectAsync(_ip, _port).Wait(100);
            if (isConnected)
            {
                SetStatus(PrinterStatusEnum.Ready);
                return;
            }
            SetStatus(PrinterStatusEnum.IsForceDisconnected);
        }
        catch (Exception _)
        {
            SetStatus(PrinterStatusEnum.IsForceDisconnected);
        }
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