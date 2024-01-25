using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Features.Shared;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;
using Ws.Domain.Models.Entities.Print;

namespace ScalesHybrid.Features.Pallet;

public sealed partial class LabelsDataGrid: DataGridBase<LabelEntity>, IDisposable
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private PalletContext PalletContext { get; set; } = null!;

    [Parameter] public int MaxItemsPerPage { get; set; } = 8;
    [Parameter] public List<LabelEntity> SelectedItems { get; set; } = [];
    [Parameter] public Action<List<LabelEntity>>? SelectedItemsChanged { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        PalletContext.OnStateChanged += ReloadData;
    }

    private void ReloadData()
    {
        SelectedItems = [];
        SelectedItemsChanged?.Invoke(SelectedItems);
        GetGridData();
        DataGridWrapper.DataGrid.Reload();
    }
    
    protected override void GetGridData() => GridData = PalletContext.Pallet.Labels;

    private void OnSelectedItemsChanged() => SelectedItemsChanged?.Invoke(SelectedItems);
    
    public void Dispose() => PalletContext.OnStateChanged -= ReloadData;
}