using System.Net;
using TscZebra.Plugin;
using TscZebra.Plugin.Abstractions;
using TscZebra.Plugin.Abstractions.Enums;
using TscZebra.Plugin.Abstractions.Exceptions;

namespace ScalesDesktop.Source.Shared.Services;

public class PrinterService(IDispatcher dispatcher): IDisposable
{
    private IZplPrinter Printer { get; set; } = PrinterFactory.Create(IPAddress.Parse("127.0.0.1"), 9100, PrinterTypes.Tsc);
    public PrinterStatus Status { get; private set; } = PrinterStatus.Disconnected;
    public event Action? StatusChanged;

    public void Setup(IPAddress ip, int port, PrinterTypes types)
    {
        Printer.OnStatusChanged -= OnPrinterStatusChanged;
        Printer.Disconnect();
        Printer = PrinterFactory.Create(ip, port, types);
        Printer.OnStatusChanged += OnPrinterStatusChanged;
    }

    public async Task ConnectAsync()
    {
        try
        {
            await Printer.ConnectAsync();
            Status = PrinterStatus.Ready;
            Printer.StartStatusPolling(10);
        }
        catch (PrinterConnectionException)
        {
            Status = PrinterStatus.Disconnected;
        }
        StatusChanged?.Invoke();
    }

    public async Task<PrinterStatus> GetStatusAsync()
    {
        try
        {
            Status = await Printer.RequestStatusAsync();
        }
        catch
        {
            Status = PrinterStatus.Disconnected;
        }
        StatusChanged?.Invoke();
        return Status;
    }

    public Task PrintZplAsync(string zpl) => Printer.PrintZplAsync(zpl);

    public void Disconnect()
    {
        Printer.Disconnect();
        Status = PrinterStatus.Disconnected;
        StatusChanged?.Invoke();
    }

    private async void OnPrinterStatusChanged(object? sender, PrinterStatus e)
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
        Printer.OnStatusChanged -= OnPrinterStatusChanged;
        Printer.Dispose();
        GC.SuppressFinalize(this);
    }
}