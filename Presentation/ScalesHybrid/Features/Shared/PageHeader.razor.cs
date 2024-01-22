using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;
using Ws.Shared.Utils;

namespace ScalesHybrid.Features.Shared;

public sealed partial class PageHeader: ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    [Parameter] public RenderFragment? ChildContent { get; set; }

    private string GetAppTitle()
    {
        string devString = ConfigurationUtil.IsDevelop ? "[DEV] " : string.Empty;
        string versionString = VersionTracking.CurrentVersion;
        return $"{devString}{Localizer["AppName"]} {versionString}";
    }
}