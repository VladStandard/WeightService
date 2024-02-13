using Microsoft.AspNetCore.Components;
using ScalesDesktop.Services;

namespace ScalesDesktop.Features.Pallet.Viewer;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed partial class PalletPage : ComponentBase, IDisposable
{
    [Inject] private PalletContext PalletContext { get; set; } = null!;
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