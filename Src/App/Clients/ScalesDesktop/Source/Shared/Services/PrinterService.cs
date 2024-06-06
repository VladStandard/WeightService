using System.Net;
using TscZebra.Plugin;
using TscZebra.Plugin.Abstractions.Common;
using TscZebra.Plugin.Abstractions.Enums;
using TscZebra.Plugin.Abstractions.Exceptions;

namespace ScalesDesktop.Source.Shared.Services;

public class PrinterService(IDispatcher dispatcher): IDisposable
{
    private IZplPrinter Printer { get; set; } = PrinterFactory.Create(IPAddress.Parse("127.0.0.1"), 9100, PrinterTypes.Tsc);
    public PrinterStatuses Status { get; private set; } = PrinterStatuses.IsDisconnected;
    public event Action? StatusChanged;

    public void Setup(IPAddress ip, int port, PrinterTypes types)
    {
        Printer.PrinterStatusChanged -= OnPrinterStatusChanged;
        Printer.Disconnect();
        Printer = PrinterFactory.Create(ip, port, types);
        Printer.PrinterStatusChanged += OnPrinterStatusChanged;
    }

    public async Task ConnectAsync()
    {
        try
        {
            await Printer.ConnectAsync();
            Status = PrinterStatuses.Ready;
            StartPolling(10);
        }
        catch (PrinterConnectionException)
        {
            Status = PrinterStatuses.IsDisconnected;
        }
        StatusChanged?.Invoke();
    }

    public async Task<PrinterStatuses> GetStatusAsync()
    {
        try
        {
            Status = await Printer.RequestStatusAsync();
        }
        catch
        {
            Status = PrinterStatuses.IsDisconnected;
        }
        StatusChanged?.Invoke();
        return Status;
    }

    public Task PrintZplAsync(string zpl) => Printer.PrintZplAsync(zpl);

    public void StopPolling() => Printer.StopStatusPolling();

    public void StartPolling(ushort interval = 30) => Printer.StartStatusPolling(interval);

    public void Disconnect()
    {
        Printer.Disconnect();
        Status = PrinterStatuses.IsDisconnected;
        StatusChanged?.Invoke();
    }

    private async void OnPrinterStatusChanged(object? sender, PrinterStatuses e)
    {
        await dispatcher.DispatchAsync(() =>
        {
            if (Status.Equals(e)) return;
            Status = e;
            StatusChanged?.Invoke();
        });
    }

    public void Dispose()
    {
        Printer.PrinterStatusChanged -= OnPrinterStatusChanged;
        Printer.Dispose();
        GC.SuppressFinalize(this);
    }
}