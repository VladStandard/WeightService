using System.Net;
using System.Net.Sockets;
using CommunityToolkit.Mvvm.Messaging;
using Ws.Printers.Enums;
using Ws.Printers.Events;
using Ws.Shared.Utils;

namespace Ws.Printers.Common;

public abstract class PrinterBase : IPrinter
{
    private readonly IPAddress _ip;
    private readonly int _port;

    protected PrinterStatusEnum Status { get; set; }
    protected TcpClient TcpClient { get; set; }

    protected PrinterBase(IPAddress ip, int port)
    {
        _ip = ip;
        _port = port;

        TcpClient = new();
        TcpClient.ReceiveTimeout = 0_200;

        SetStatus(PrinterStatusEnum.IsDisabled);
    }

    public async void Connect()
    {
        try
        {
            TcpClient.Dispose();
            TcpClient = new();
            TcpClient.ReceiveTimeout = 200;

            await TcpClient.ConnectAsync(_ip, _port).WaitAsync(TimeSpan.FromMilliseconds(200));
            SetStatus(PrinterStatusEnum.Ready);
        }
        catch (Exception)
        {
            SetStatus(PrinterStatusEnum.IsForceDisconnected);
        }
    }

    public void PrintLabel(string zpl)
    {
        ExecuteCommand(new(TcpClient, zpl));
    }

    public void Disconnect()
    {
        WeakReferenceMessenger.Default.Send(new GetPrinterStatusEvent(PrinterStatusEnum.IsDisabled));
        if (TcpClient.Connected) TcpClient.Close();
        TcpClient.Dispose();
    }

    public virtual void RequestStatus()
    {
        throw new NotImplementedException();
    }


    private void SetStatus(PrinterStatusEnum state)
    {
        Status = state;
        WeakReferenceMessenger.Default.Send(new GetPrinterStatusEvent(Status));
    }

    protected void ExecuteCommand(PrinterCommandBase command)
    {
        try
        {
            ErrorUtil.Suppress<TimeoutException>(() => {
                if (Status == PrinterStatusEnum.IsDisabled) return;
                if (Status == PrinterStatusEnum.IsForceDisconnected)
                {
                    Connect();
                    return;
                }
                command.Request();
            });
        }
        catch (Exception)
        {
            Connect();
        }
    }

    public void Dispose()
    {
        Disconnect();
    }
}