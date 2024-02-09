using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Resources;
using ScalesDesktop.Services;

namespace ScalesDesktop.Features.Pallet.Viewer;

public sealed partial class Overview : ComponentBase, IDisposable
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private PalletContext PalletContext { get; set; } = null!;

    protected override void OnInitialized() => PalletContext.OnStateChanged += StateHasChanged;

    public void Dispose() => PalletContext.OnStateChanged -= StateHasChanged;
}