using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Services;

namespace ScalesDesktop.Source.Pages.Home;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class HomePage : ComponentBase, IDisposable
{
    [Inject] private LineContext LineContext { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = default!;

    protected override void OnInitialized() => LineContext.OnLineChanged += StateHasChanged;

    private async Task ShowCloseAppDialog() => await DialogService.ShowDialogAsync<CloseAppDialog>(new());

    public void Dispose() => LineContext.OnLineChanged -= StateHasChanged;
}