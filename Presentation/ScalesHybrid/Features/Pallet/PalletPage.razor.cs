using Microsoft.AspNetCore.Components;
using ScalesHybrid.Services;

namespace ScalesHybrid.Features.Pallet;

public sealed partial class PalletPage: ComponentBase, IDisposable
{
    [Inject] private PalletContext PalletContext { get; set; } = null!;

    protected override void OnInitialized() => PalletContext.InitializeData();

    public void Dispose() => PalletContext.InitializeData();
}