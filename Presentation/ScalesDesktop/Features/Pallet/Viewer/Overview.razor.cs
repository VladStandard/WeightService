using System.Text;
using Append.Blazor.Printing;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Resources;
using ScalesDesktop.Services;


namespace ScalesDesktop.Features.Pallet.Viewer;

public sealed partial class Overview : ComponentBase, IDisposable
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
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
