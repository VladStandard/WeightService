using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;

namespace ScalesHybrid.Components.Layout;

public sealed partial class MainHeader: ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private PageTitleService PageTitleService { get; set; }
    [Parameter] public string Title { get; set; } = string.Empty;

    protected override void OnInitialized()
    {
        PageTitleService.OnTitleChanged += UpdateTitle;
    }

    private void UpdateTitle(string newTitle)
    {
        Title = newTitle;
        StateHasChanged();
    }

    private static void ExitApp() => MauiWinUIApplication.Current.Exit();
}

