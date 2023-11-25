using CommunityToolkit.Mvvm.Messaging;
using SuperSimpleTcp;
using Ws.Printers.Commands;
using Ws.Printers.Enums;
using Ws.Printers.Events;

namespace Ws.Printers.Common;

public abstract class PrinterBase : IPrinter
{
    protected PrinterStatusEnum State { get; set; }
    protected SimpleTcpClient TcpClient { get; set; }
    protected IPrinterCommands Commands { get; set; }

    public PrinterBase(string ip, int port)
    {
        TcpClient = new(ip, port);
        Commands = new TscCommands();
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
    
    public void RequestStatus()
    {
       TcpClient.Send(Commands.GetStatus);
    }

    private static void TcpClientConnected(Object? sender, ConnectionEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new PrinterConnectedEvent());
    }

    private static void TcpClientDisconnected(Object? sender, ConnectionEventArgs e)
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