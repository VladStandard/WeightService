using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Pallet;
using Ws.SharedUI.Resources;

namespace ScalesDesktop.Source.Widgets;

public sealed partial class LabelsGrid : ComponentBase
{
    [Inject] private IStringLocalizer<Resources> PalletLocalizer { get; set; } = null!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;
    [Inject] private IPalletService PalletService { get; set; } = null!;
    [Inject] private PalletContext PalletContext { get; set; } = null!;
    
    private List<LabelEntity> SelectedItems { get; set; } = [];
    private string SearchingNumber { get; set; } = string.Empty;
    private IQueryable<DataItem> Data => PalletService.GetAllLabels(PalletContext.CurrentPallet.Uid)
        .Select((label, index) => new DataItem { Id = index, Label = label })
        .AsQueryable();
    
    protected override void OnInitialized() => PalletContext.OnStateChanged += StateHasChanged;

    private void ToggleItem(LabelEntity item)
    {
        if (!SelectedItems.Remove(item))
            SelectedItems.Add(item);
    }

    // private bool OnCustomFilter(LabelEntity entity) =>
    //     string.IsNullOrEmpty(SearchingNumber) ||
    //     entity.Plu.Number.ToString().Contains(SearchingNumber, StringComparison.OrdinalIgnoreCase);
}

internal record DataItem
{
    public int Id { get; set; }
    public LabelEntity Label { get; set; } = new();
}