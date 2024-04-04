using Microsoft.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Services;

namespace ScalesDesktop.Source.Pages.Pallet;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed partial class PalletPage : ComponentBase, IDisposable
{
    [Inject] private PalletContext PalletContext { get; set; } = default!;

    protected override void OnInitialized()
    {
        PalletContext.InitializeContext();
        PalletContext.OnStateChanged += StateHasChanged;
    }

    public void Dispose()
    {
        PalletContext.ResetPalletMan();
        PalletContext.OnStateChanged -= StateHasChanged;
    }
}