namespace ScalesDesktop.Source.Features;

public sealed partial class CloseAppDialog : ComponentBase, IDialogContentComponent
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;

    [CascadingParameter] public FluentDialog Dialog { get; set; } = default!;

    private static void ExitApp()
    {
        if (Application.Current == null) return;
        Application.Current.Quit();
    }

    private async Task CloseDialog() => await Dialog.CloseAsync();
}