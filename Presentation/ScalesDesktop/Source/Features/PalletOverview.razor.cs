using Append.Blazor.Printing;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Features.Pallet.Viewer;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Shared.Resources;

namespace ScalesDesktop.Source.Features;

public sealed partial class PalletOverview : ComponentBase, IDisposable
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private PalletContext PalletContext { get; set; } = default!;
    [Inject] private IPrintingService PrintingService { get; set; } = default!;

    # endregion

    private PalletCard PalletCard { get; set; } = new();

    protected override void OnInitialized() => PalletContext.OnStateChanged += StateHasChanged;

    private async Task PrintPalletCard()
    {
        string palletCardBase64 = PalletCard.CreateBase64(PalletContext.CurrentPallet);
        await PrintingService.Print(new(palletCardBase64) { Base64 = true });
    }

    public void Dispose() => PalletContext.OnStateChanged -= StateHasChanged;
}