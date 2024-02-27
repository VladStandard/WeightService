using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using ScalesDesktop.Source.Widgets.PalletCreateForm;
using Ws.Domain.Models.Entities.Print;

namespace ScalesDesktop.Source.Widgets.PalletSelect;

public sealed partial class PalletSelect : ComponentBase, IDisposable
{
    [Inject] private PalletContext PalletContext { get; set; } = null!;
    [Inject] private IModalService ModalService { get; set; } = null!;
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

    private async Task ShowCreateFormDialog() => await ModalService.Show<CreateFormModal>();

    public void Dispose() => PalletContext.OnStateChanged -= StateHasChanged;
}