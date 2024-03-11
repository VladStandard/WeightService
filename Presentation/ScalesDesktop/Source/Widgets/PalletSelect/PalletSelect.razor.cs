using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesDesktop.Source.Features;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Domain.Models.Entities.Print;

namespace ScalesDesktop.Source.Widgets.PalletSelect;

public sealed partial class PalletSelect : ComponentBase, IDisposable
{
    [Inject] private PalletContext PalletContext { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private IStringLocalizer<Resources> PalletLocalizer { get; set; } = null!;

    private string InputSearchCounter { get; set; } = string.Empty;

    protected override void OnInitialized() => PalletContext.OnStateChanged += StateHasChanged;

    private void ReloadData() => PalletContext.UpdatePalletData();

    private IEnumerable<ViewPallet> GetFilteredPalletList()
    {
        IEnumerable<ViewPallet> pallets = PalletContext.PalletEntities;
        if (!string.IsNullOrEmpty(InputSearchCounter))
            pallets = pallets.Where(x => x.Counter.ToString().Contains(InputSearchCounter));
        return pallets.Take(100);
    }

    private async Task ShowCreateFormDialog() => await DialogService.ShowDialogAsync<PalletCreateFormDialog>(new());

    public void Dispose() => PalletContext.OnStateChanged -= StateHasChanged;
}