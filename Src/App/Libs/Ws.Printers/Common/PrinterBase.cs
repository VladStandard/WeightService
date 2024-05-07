using System.Net;
using System.Net.Sockets;
using CommunityToolkit.Mvvm.Messaging;
using Ws.Printers.Enums;
using Ws.Printers.Messages;

namespace Ws.Printers.Common;

internal abstract class PrinterBase(IPAddress ip, int port) : IPrinter
{
    protected TcpClient TcpClient { get; set; } = new();
    protected PrinterStatus Status { get; set; } = PrinterStatus.IsDisabled;

    #region Abstract

    public abstract void RequestStatus();

    #endregion

    #region Public

    public async void Connect()
    {
        try
        {
            TcpClient.Dispose();
            TcpClient = new() { ReceiveTimeout = 200 };
            await TcpClient.ConnectAsync(ip, port).WaitAsync(TimeSpan.FromMilliseconds(200));

            SetStatus(PrinterStatus.Ready);
        }
        catch (Exception)
        {
            SetStatus(PrinterStatus.IsForceDisconnected);
        }
    }
    public void Dispose() => Disconnect();
    public void PrintLabel(string zpl) => ExecuteCommand(new(TcpClient, zpl));

    #endregion

    #region Private

    private void Disconnect()
    {
        WeakReferenceMessenger.Default.Send(new PrinterStatusMsg(PrinterStatus.IsDisabled));
        if (TcpClient.Connected) TcpClient.Close();
        TcpClient.Dispose();
    }
    private void SetStatus(PrinterStatus state)
    {
        Status = state;
        WeakReferenceMessenger.Default.Send(new PrinterStatusMsg(Status));
    }

    protected void ExecuteCommand(PrinterCommandBase command)
    {
        if (Status is PrinterStatus.IsDisabled) return;
        if (Status is PrinterStatus.IsForceDisconnected) Connect();

        try
        {
            command.Request();
        }
        catch (Exception)
        {
            SetStatus(PrinterStatus.IsForceDisconnected);
        }
    }

    #endregion
}