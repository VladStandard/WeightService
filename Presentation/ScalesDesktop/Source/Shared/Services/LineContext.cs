using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Line;

namespace ScalesDesktop.Source.Shared.Services;

public class LineContext : IDisposable
{
    private ILineService LineService { get; }
    private ExternalDevicesService ExternalDevices { get; }

    public LineEntity Line { get; private set; } = new();
    public PrinterEntity PrinterEntity { get; private set; } = new();
    public event Action? OnLineChanged;

    public LineContext(ILineService lineService, ExternalDevicesService externalDevices)
    {
        LineService = lineService;
        ExternalDevices = externalDevices;
        InitializeData();
    }

    private void InitializeData()
    {
        Line = LineService.GetCurrentLine();

        if (Line.IsExists)
        {
            Line.Version = VersionTracking.CurrentVersion;
            LineService.Update(Line);
        }

        PrinterEntity = Line.Printer;
        ExternalDevices.SetupPrinter(Line.Printer.Ip, 9100, Line.Printer.Type);
        ExternalDevices.SetupScales();
    }

    public void ResetLine()
    {
        Line = LineService.GetCurrentLine();
        PrinterEntity = Line.Printer;
        ExternalDevices.SetupPrinter(PrinterEntity.Ip, 9100, PrinterEntity.Type);
        OnLineChanged?.Invoke();
    }

    public void DisconnectScale() => ExternalDevices.Scales.Disconnect();
    public void ConnectScale() => ExternalDevices.Scales.Connect();
    public void StartWeightPolling() => ExternalDevices.Scales.StartWeightPolling();
    public void StopWeightPolling() => ExternalDevices.Scales.StopWeightPolling();

    public void Dispose()
    {
        ExternalDevices.Dispose();
        GC.SuppressFinalize(this);
    }
}