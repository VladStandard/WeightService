using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.Maui;
using ScalesHybrid.Resources;

namespace ScalesHybrid.Components.Dialogs;

public sealed partial class DialogCloseApp: ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; }
    [Inject] private IModalService ModalService { get; set; }
    
    private static void ExitApp() => MauiWinUIApplication.Current.Exit();
    private async Task CloseDialog() => await ModalService.Hide();
}