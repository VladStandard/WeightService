using Microsoft.AspNetCore.Components;
using ScalesDesktop.Services;

namespace ScalesDesktop.Features.Pallet.Viewer;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed partial class PalletPage : ComponentBase, IDisposable
{
    [Inject] private PalletContext PalletContext { get; set; } = null!;
    protected override void OnInitialized()
    {
        PalletContext.InitializeData();
        PalletContext.OnStateChanged += StateHasChanged;
    }

    public void Dispose()
    {
        PalletContext.InitializeData();
        PalletContext.OnStateChanged -= StateHasChanged;
    } 
}