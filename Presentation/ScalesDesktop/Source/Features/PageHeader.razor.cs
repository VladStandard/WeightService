using Microsoft.AspNetCore.Components;
using Ws.Shared.Utils;

namespace ScalesDesktop.Source.Features;

public sealed partial class PageHeader : ComponentBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private string GetAppTitle()
    {
        string devString = ConfigurationUtil.IsDevelop ? "[DEV] " : string.Empty;
        string versionString = VersionTracking.CurrentVersion;
        return $"{devString} Весовая служба {versionString}";
    }
}