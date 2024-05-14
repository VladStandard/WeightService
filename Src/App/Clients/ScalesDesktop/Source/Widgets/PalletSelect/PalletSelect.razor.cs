using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesDesktop.Source.Features.PalletCreate;
using ScalesDesktop.Source.Shared.Services;
using Ws.Domain.Models.Entities.Print;

namespace ScalesDesktop.Source.Widgets.PalletSelect;

public sealed partial class PalletSelect : ComponentBase, IDisposable
{
    # region Injects

    [Inject] private PalletContext PalletContext { get; set; } = default!;
    [Inject] private IDialogService DialogService { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;

    # endregion

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

    private async Task ShowCreateFormDialog() => await DialogService.ShowDialogAsync<PalletCreateDialog>(new());

    public void Dispose() => PalletContext.OnStateChanged -= StateHasChanged;
}