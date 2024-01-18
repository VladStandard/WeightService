using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;

namespace ScalesHybrid.Features.Home;

public sealed partial class CloseAppDialog: ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IModalService ModalService { get; set; } = null!;
    
    private static void ExitApp() => MauiWinUIApplication.Current.Exit();
    private async Task CloseDialog() => await ModalService.Hide();
}