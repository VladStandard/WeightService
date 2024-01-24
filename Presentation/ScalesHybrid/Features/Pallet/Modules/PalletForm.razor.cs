using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;

namespace ScalesHybrid.Features.Pallet.Modules;

public sealed partial class PalletForm: ComponentBase, IDisposable
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private PalletContext PalletContext { get; set; } = null!;

    protected override void OnInitialized() => PalletContext.OnStateChanged += StateHasChanged;

    public void Dispose() => PalletContext.OnStateChanged -= StateHasChanged;
}