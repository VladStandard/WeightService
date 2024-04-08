using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Localization;

namespace ScalesDesktop.Source.Pages.Home;

public sealed partial class CloseAppDialog : ComponentBase, IDialogContentComponent
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;

    [CascadingParameter] public FluentDialog Dialog { get; set; } = default!;

    private static void ExitApp() => MauiWinUIApplication.Current.Exit();
    private async Task CloseDialog() => await Dialog.CloseAsync();
}