using CommunityToolkit.Mvvm.Messaging;
using SuperSimpleTcp;
using Ws.Printers.Enums;
using Ws.Printers.Events;

namespace Ws.Printers.Common;

public abstract class PrinterBase : IPrinter
{
    protected PrinterStatusEnum State { get; set; }
    protected SimpleTcpClient TcpClient { get; set; }
    protected virtual string GetStatusCommand => throw new NotImplementedException();
    
    public PrinterBase(string ip, int port)
    {
        TcpClient = new(ip, port);
        State = PrinterStatusEnum.Unknown;
    }
    
    public IPrinter Connect()
    {
        if (TcpClient.IsConnected)
            Dispose();
        TcpClient.Connect();
        TcpSubscribe();
        return this;
    }
    
    public void GetStatus()
    {
       TcpClient.Send(GetStatusCommand);
    }

    protected virtual void TcpClientConnected(Object? sender, ConnectionEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new PrinterDisconnectedEvent());
    }

    protected virtual void TcpClientDisconnected(Object? sender, ConnectionEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new PrinterDisconnectedEvent());
    }

    protected virtual void TcpClientDataReceived(Object? sender, DataReceivedEventArgs dataReceivedEventArgs)
    {
        
    }
    
    private void TcpSubscribe()
    {
        TcpClient.Events.Connected += TcpClientConnected;
        TcpClient.Events.DataReceived += TcpClientDataReceived;
        TcpClient.Events.Disconnected += TcpClientDisconnected;

    }

    private void TcpUnSubscribe()
    {
        TcpClient.Events.Connected -= TcpClientConnected;
        TcpClient.Events.DataReceived -= TcpClientDataReceived;
        TcpClient.Events.Disconnected -= TcpClientDisconnected;

    }
    
    public void Dispose()
    {
        if (TcpClient.IsConnected) TcpClient.Disconnect();

        TcpClient.Dispose();
        TcpUnSubscribe();
    }
}