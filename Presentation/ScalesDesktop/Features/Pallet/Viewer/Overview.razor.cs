using System.Text;
using Append.Blazor.Printing;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Features.Pallet.Resources;
using ScalesDesktop.Resources;
using ScalesDesktop.Services;
using Ws.SharedUI.Resources;


namespace ScalesDesktop.Features.Pallet.Viewer;

public sealed partial class Overview : ComponentBase, IDisposable
{
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
    [Inject] private IStringLocalizer<PalletResources> PalletLocalizer { get; set; } = null!;
    [Inject] private PalletContext PalletContext { get; set; } = null!;
    [Inject] private IPrintingService PrintingService { get; set; } = null!;
    
    private PalletCard PalletCard { get; set; } = new();

    protected override void OnInitialized() => PalletContext.OnStateChanged += StateHasChanged;

    private async Task PrintPalletCard()
    {
        string palletCardBase64 = PalletCard.CreateBase64(PalletContext.CurrentPallet);
        await PrintingService.Print(new(palletCardBase64) { Base64 = true });
    }

    public void Dispose() => PalletContext.OnStateChanged -= StateHasChanged;
}
