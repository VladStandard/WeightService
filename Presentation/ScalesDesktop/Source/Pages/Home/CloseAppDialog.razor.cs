using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;

namespace ScalesDesktop.Source.Pages.Home;

public sealed partial class CloseAppDialog : ComponentBase, IDialogContentComponent
{
    [CascadingParameter] public FluentDialog Dialog { get; set; } = default!;

    private static void ExitApp() => MauiWinUIApplication.Current.Exit();
    private async Task CloseDialog() => await Dialog.CloseAsync();
}