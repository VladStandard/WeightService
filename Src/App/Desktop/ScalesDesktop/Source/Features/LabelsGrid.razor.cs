using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Services;
using TscZebra.Plugin.Abstractions.Enums;

namespace ScalesDesktop.Source.Features;

public sealed partial class LabelsGrid : ComponentBase, IDisposable
{
    # region Injects

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IPalletService PalletService { get; set; } = default!;
    [Inject] private PalletContext PalletContext { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private PrinterService PrinterService { get; set; } = default!;
    [Inject] private IPrintLabelService PrintLabelService { get; set; } = default!;

    # endregion

    private List<Label> SelectedItems { get; set; } = [];
    private string SearchingNumber { get; set; } = string.Empty;
    private IQueryable<DataItem> LabelData { get; set; } = Enumerable.Empty<DataItem>().AsQueryable();
    private Action? StateChangedHandler { get; set; }
    private const int PrinterRequestDelay = 100;

    protected override void OnInitialized()
    {
        StateChangedHandler = async () => await ResetDataGrid();
        PalletContext.StateChanged += StateChangedHandler;
    }

    protected override async Task OnInitializedAsync() => await InitializeData();

    private IQueryable<DataItem> FilteredLabelData
    {
        get => string.IsNullOrEmpty(SearchingNumber) ? LabelData : LabelData.Where(item => item.Id.ToString().Contains(SearchingNumber));
        set => LabelData = value;
    }

    private void ToggleItem(Label item)
    {
        if (!SelectedItems.Remove(item))
            SelectedItems.Add(item);
    }

    private void SelectAllItems() => SelectedItems = LabelData.Select(item => item.Label).ToList();

    private Task<IQueryable<DataItem>> InitializeData() =>
        Task.Run(() => LabelData = GetLabelsData()
            .Select((label, index) => new DataItem { Id = index + 1, Label = label })
            .AsQueryable());


    private IEnumerable<Label> GetLabelsData()
    {
        try
        {
            return PalletService.GetAllLabels(PalletContext.CurrentPallet.Uid);
        }
        catch
        {
            ToastService.ShowError(Localizer["ToastErrorWhileGettingData"]);
            return [];
        }
    }

    private async Task ResetDataGrid()
    {
        SelectedItems = [];
        await InitializeData();
        StateHasChanged();
    }

    private async Task PrintLabelsAsync()
    {
        PrinterStatus printerStatus = await PrinterService.GetStatusAsync();

        if (printerStatus is not (PrinterStatus.Ready or PrinterStatus.Busy))
        {
            PrintPrinterStatusMessage(printerStatus);
            return;
        }

        int errorIndex = 0;
        foreach (Label item in SelectedItems)
        {
            try
            {
                errorIndex += 1;
                await PrinterService.PrintZplAsync(item.Zpl);
            }
            catch
            {
                ToastService.ShowError($"{errorIndex} не распечаталась");
            }
            await Task.Delay(300);
        }

        await InvokeAsync(StateHasChanged);
    }

    private void PrintPrinterStatusMessage(PrinterStatus printerStatus) =>
        ToastService.ShowWarning(printerStatus switch
        {
            PrinterStatus.Disconnected => Localizer["PrinterStatusIsDisabled"],
            PrinterStatus.Paused => Localizer["PrinterStatusPaused"],
            PrinterStatus.HeadOpen => Localizer["PrinterStatusHeadOpen"],
            PrinterStatus.PaperOut => Localizer["PrinterStatusPaperOut"],
            PrinterStatus.PaperJam => Localizer["PrinterStatusPaperJam"],
            _ => Localizer["PrinterStatusUnknown"]
        });

    public void Dispose()
    {
        if (StateChangedHandler != null)
            PalletContext.StateChanged -= StateChangedHandler;
    }
}

internal record DataItem
{
    public int Id { get; init; }
    public Label Label { get; init; } = new();
}