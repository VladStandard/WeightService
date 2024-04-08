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
    # region Injects

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IPalletService PalletService { get; set; } = default!;
    [Inject] private PalletContext PalletContext { get; set; } = default!;

    # endregion

    private List<LabelEntity> SelectedItems { get; set; } = [];
    private string SearchingNumber { get; set; } = string.Empty;
    private IQueryable<DataItem> LabelData { get; set; } = Enumerable.Empty<DataItem>().AsQueryable();

    protected override void OnInitialized() => PalletContext.OnStateChanged += async() => await ResetDataGrid();

    protected override async Task OnInitializedAsync() => await InitializeData();

    private IQueryable<DataItem> FilteredLabelData
    {
        get => string.IsNullOrEmpty(SearchingNumber) ? LabelData : LabelData.Where(item => item.Id.ToString().Contains(SearchingNumber));
        set => LabelData = value;
    }

    private void ToggleItem(LabelEntity item)
    {
        if (!SelectedItems.Remove(item))
            SelectedItems.Add(item);
    }

    private void SelectAllItems() => SelectedItems = LabelData.Select(item => item.Label).ToList();

    private Task<IQueryable<DataItem>> InitializeData()
    {
        return Task.Run(() => LabelData = PalletService.GetAllLabels(PalletContext.CurrentPallet.Uid)
            .Select((label, index) => new DataItem { Id = index + 1, Label = label })
            .AsQueryable());
    }

    private async Task ResetDataGrid()
    {
        SelectedItems = [];
        await InitializeData();
        StateHasChanged();
    }
}

internal record DataItem
{
    public int Id { get; init; }
    public LabelEntity Label { get; init; } = new();
}