using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Components.Dialogs;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;

namespace ScalesHybrid.Components.Layout;

public sealed partial class MainHeader: ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IModalService ModalService { get; set; }

    private string GetAppTitle() => $"{Localizer["AppName"]} {VersionTracking.CurrentVersion}";
    
    private async Task ShowExitDialog() => await ModalService.Show<DialogCloseApp>("",
        new ModalInstanceOptions{Size = ModalSize.Default});
}

