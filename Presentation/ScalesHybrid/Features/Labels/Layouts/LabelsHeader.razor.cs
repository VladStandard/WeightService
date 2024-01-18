using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Features.Home;
using ScalesHybrid.Resources;
using Ws.Shared.Utils;

namespace ScalesHybrid.Features.Labels.Layouts;

public sealed partial class LabelsHeader: ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IModalService ModalService { get; set; } = null!;

    private bool IsDevelop { get; } = ConfigurationUtil.IsDevelop;

    private string GetAppTitle()
    {
        string devString = IsDevelop ? "[DEV] " : string.Empty;
        string versionString = VersionTracking.CurrentVersion;
        return $"{devString}{Localizer["AppName"]} {versionString}";
    }
    
    
}

