using System.Net;
using ScalesDesktop.Source.Shared.Api;
using Ws.Desktop.Models.Features.Arms.Output;

namespace ScalesDesktop.Source.Shared.Services;

public class ArmContext(IDesktopApi desktopApi)
{
    public ArmValue? Arm { get; private set; }
    public PrinterValue? Printer => Arm?.Printer;
    public event Action? ArmChanged;

    private async Task InitializeDataAsync()
    {
        try
        {
            Arm = await desktopApi.GetArmByName(Dns.GetHostName());
        }
        catch
        {
            // pass
        }
    }

    public void UpdateLineCounter(uint counter)
    {
        if (Arm == null) return;
        Arm = Arm with { Counter = counter };
    }

    public async Task UpdateArmData()
    {
        await InitializeDataAsync();
        ArmChanged?.Invoke();
    }
}
