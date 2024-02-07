using Microsoft.AspNetCore.Components;
using ScalesDesktop.Services;

namespace ScalesDesktop.Features.Pallet.Viewer;

public sealed partial class PalletSelect : ComponentBase, IDisposable
{
    [Inject] private PalletContext PalletContext { get; set; } = null!;

    protected override void OnInitialized() => PalletContext.OnStateChanged += StateHasChanged;

    public void Dispose() => PalletContext.OnStateChanged -= StateHasChanged;
}