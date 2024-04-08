using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Shared.Resources;

namespace ScalesDesktop.Source.Pages.Home;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class HomePage : ComponentBase, IDisposable
{
    # region Injects

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private LineContext LineContext { get; set; } = default!;
    [Inject] private IDialogService DialogService { get; set; } = default!;

    # endregion

    protected override void OnInitialized() => LineContext.OnLineChanged += StateHasChanged;

    private async Task ShowCloseAppDialog() => await DialogService.ShowDialogAsync<CloseAppDialog>(new());

    public void Dispose() => LineContext.OnLineChanged -= StateHasChanged;
}