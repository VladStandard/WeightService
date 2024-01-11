using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Components.Dialogs;
using ScalesHybrid.Resources;
using Ws.Shared.Utils;

namespace ScalesHybrid.Components.Layout;

public sealed partial class MainHeader: ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IModalService ModalService { get; set; }

    private bool IsDevelop { get; set; } = ConfigurationUtil.IsDevelop;

    private string GetAppTitle()
    {
        string devString = IsDevelop ? "[DEV] " : string.Empty;
        string versionString = VersionTracking.CurrentVersion;
        return $"{devString}{Localizer["AppName"]} {versionString}";
    }
    
    private async Task ShowExitDialog() => await ModalService.Show<DialogCloseApp>("",
        new ModalInstanceOptions{Size = ModalSize.Default});
}

