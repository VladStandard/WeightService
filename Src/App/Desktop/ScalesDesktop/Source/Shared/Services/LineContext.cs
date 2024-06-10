using System.Net;
using ScalesDesktop.Source.Shared.Api;
using Ws.Desktop.Models.Features.Arms.Output;

namespace ScalesDesktop.Source.Shared.Services;

public class LineContext(IDesktopApi desktopApi)
{
    public ArmValue? Line { get; private set; }
    public PrinterValue? Printer => Line?.Printer;
    public event Action? LineChanged;

    private async Task InitializeDataAsync()
    {
        try
        {
            Line = await desktopApi.GetArmByName(Dns.GetHostName());
            await Task.Delay(2000);
        }
        catch
        {
            // pass
        }

        // if (Line.Version != VersionTracking.CurrentVersion)
        //     UpdateArmAppVersion();
    }

    // private void UpdateArmAppVersion()
    // {
    //     Line.Version = VersionTracking.CurrentVersion;
    //     ArmService.Update(Line);
    // }

    public async Task UpdateArmData()
    {
        await InitializeDataAsync();
        LineChanged?.Invoke();
    }
}
