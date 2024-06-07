using Blazor.Heroicons;
using Microsoft.AspNetCore.Components.Routing;
using Ws.Shared.Utils;

namespace DeviceControl.Source.Widgets.NavMenu;

public sealed partial class NavMenuItem : ComponentBase
{
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Parameter, EditorRequired] public string Label { get; set; } = string.Empty;
    [Parameter, EditorRequired] public string Icon { get; set; } = HeroiconName.Home;
    [Parameter, EditorRequired] public IEnumerable<NavMenuItemModel> Items { get; set; } = new List<NavMenuItemModel>();
    [Parameter, EditorRequired] public string? RequiredClaim { get; set; }

    private bool IsOpened { get; set; }
    private bool IsProduction { get; set; }


    protected override void OnInitialized()
    {
        NavigationManager.LocationChanged += HandleLocationChanged;
        IsProduction = !ConfigurationUtil.IsDevelop;
        IsOpened = GetIsAnyActive();
    }

    private bool GetIsActivePath(string relativePath)
    {
        string currentUri = NavigationManager.Uri;
        string uriToCheck = GetAbsolutePath(relativePath);
        bool startsWithFullPath = currentUri.StartsWith(uriToCheck, StringComparison.OrdinalIgnoreCase);
        bool isExactMatchOrSubpath = currentUri.Length >= uriToCheck.Length &&
                                     (currentUri.Length == uriToCheck.Length || currentUri[uriToCheck.Length] == '/');
        return startsWithFullPath && isExactMatchOrSubpath;
    }

    private void HandleLocationChanged(object? sender, LocationChangedEventArgs e) => StateHasChanged();

    private string GetAbsolutePath(string relativePath) =>
        new Uri(new(NavigationManager.BaseUri), relativePath).ToString();

    private bool GetIsAnyActive() => Items.Any(item => GetIsActivePath(item.Link));

    private void SwitchVisibility() => IsOpened = !IsOpened;
}