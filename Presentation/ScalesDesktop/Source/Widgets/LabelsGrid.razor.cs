using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Pallet;
using Ws.Shared.Resources;

namespace ScalesDesktop.Source.Widgets;

public sealed partial class LabelsGrid : ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> PalletLocalizer { get; set; } = null!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;
    [Inject] private IPalletService PalletService { get; set; } = null!;
    [Inject] private PalletContext PalletContext { get; set; } = null!;

    private List<LabelEntity> SelectedItems { get; set; } = [];
    private string SearchingNumber { get; set; } = string.Empty;
    private IQueryable<DataItem> Data => PalletService.GetAllLabels(PalletContext.CurrentPallet.Uid)
        .Select((label, index) => new DataItem { Id = index + 1, Label = label })
        .AsQueryable();

    protected override void OnInitialized() => PalletContext.OnStateChanged += ResetDataGrid;

    private void ToggleItem(LabelEntity item)
    {
        if (!SelectedItems.Remove(item))
            SelectedItems.Add(item);
    }

    private void ResetDataGrid()
    {
        SelectedItems = [];
        StateHasChanged();
    }
}

internal record DataItem
{
    public int Id { get; init; }
    public LabelEntity Label { get; init; } = new();
}