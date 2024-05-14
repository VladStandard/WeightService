using Ws.Domain.Models.Entities.Devices;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Services.Features.Line;

namespace ScalesDesktop.Source.Shared.Services;

public class LineContext : IDisposable
{
    private ILineService LineService { get; }
    private ExternalDevicesService ExternalDevices { get; }

    public Arm Line { get; private set; } = new();
    public Printer Printer { get; private set; } = new();
    public event Action? OnLineChanged;

    public LineContext(ILineService lineService, ExternalDevicesService externalDevices)
    {
        LineService = lineService;
        ExternalDevices = externalDevices;
        ExternalDevices.SetupScales();
        InitializeLineData();
    }

    private void InitializeLineData()
    {
        Line = LineService.GetCurrentLine();
        if (!Line.IsExists) return;

        if (Line.Version != VersionTracking.CurrentVersion)
            UpdateLineVersion();

        Printer = Line.Printer;
        ExternalDevices.SetupPrinter(Line.Printer.Ip, 9100, Line.Printer.Type);
    }

    private void UpdateLineVersion()
    {
        Line.Version = VersionTracking.CurrentVersion;
        LineService.Update(Line);
    }

    public void ResetLine()
    {
        InitializeLineData();
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