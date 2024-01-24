using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;
using Ws.Domain.Models.Entities.Print;

namespace ScalesHybrid.Features.Pallet.Modules;

public sealed partial class PalletLabels: ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private PalletContext PalletContext { get; set; } = null!;

    private IEnumerable<LabelEntity> GridData { get; set; } = [];
    private DataGrid<LabelEntity> DataGridRef { get; set; } = null!;
    private List<LabelEntity> SelectedItems { get; set; } = [];
    private string SearchingNumber { get; set; } = string.Empty;

    private IEnumerable<LabelEntity> GetGridData() => PalletContext.Pallet.Labels;

    protected override void OnInitialized()
    {
        InitializeData();
        PalletContext.OnStateChanged += async () => await ReloadGrid();
    }

    private async Task OnSearchingNumberChanged() => await DataGridRef.Reload();

    private void InitializeData() => GridData = GetGridData();

    private async Task ReloadGrid()
    {
        InitializeData();
        SelectedItems = [];
        await DataGridRef.Reload();
        StateHasChanged();
    }
    
    private bool OnCustomFilter(LabelEntity entity) =>
        string.IsNullOrEmpty(SearchingNumber) ||
        entity.Pallet.Plu.Number.ToString().Contains(SearchingNumber, StringComparison.OrdinalIgnoreCase);
}