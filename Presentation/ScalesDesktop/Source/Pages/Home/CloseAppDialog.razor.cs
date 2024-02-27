// ReSharper disable ClassNeverInstantiated.Global

using Blazorise;
using Microsoft.AspNetCore.Components;

namespace ScalesDesktop.Source.Pages.Home;

public sealed partial class CloseAppDialog : ComponentBase
{
    [Inject] private IModalService ModalService { get; set; } = null!;

    private static void ExitApp() => MauiWinUIApplication.Current.Exit();
    private async Task CloseDialog() => await ModalService.Hide();
}