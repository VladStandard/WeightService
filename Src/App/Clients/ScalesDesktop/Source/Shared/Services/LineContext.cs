using Ws.Domain.Models.Entities.Devices;
using Ws.Domain.Models.Entities.Devices.Arms;
using Ws.Domain.Services.Features.Arms;

namespace ScalesDesktop.Source.Shared.Services;

public class LineContext
{
    private IArmService ArmService { get; }
    public Arm Line { get; private set; } = new();
    public Printer Printer { get; private set; } = new();
    public event Action? LineChanged;

    public LineContext(IArmService armService)
    {
        ArmService = armService;
        InitializeLineData();
    }

    private void InitializeLineData()
    {
        Line = ArmService.GetCurrentLine();
        if (!Line.IsExists) return;

        if (Line.Version != VersionTracking.CurrentVersion)
            UpdateLineVersion();

        Printer = Line.Printer;
    }

    private void UpdateLineVersion()
    {
        Line.Version = VersionTracking.CurrentVersion;
        ArmService.Update(Line);
    }

    public void ResetLine()
    {
        InitializeLineData();
        LineChanged?.Invoke();
    }
}
