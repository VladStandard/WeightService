using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Services;
using TscZebra.Plugin.Abstractions.Enums;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace ScalesDesktop.Source.Features;

public sealed partial class LabelsGrid : ComponentBase
{
    # region Injects

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private PrinterService PrinterService { get; set; } = default!;
    [Inject] private ArmApi ArmApi { get; set; } = default!;
    [Inject] private PalletApi PalletApi { get; set; } = default!;

    # endregion

    [Parameter, EditorRequired] public PalletInfo Pallet { get; set; } = default!;

    private List<LabelInfo> SelectedItems { get; set; } = [];
    private string SearchingNumber { get; set; } = string.Empty;
    private LabelInfo? HoverItem { get; set; }
    private const int PrinterRequestDelay = 150;

    private void ToggleItem(LabelInfo item)
    {
        if (!SelectedItems.Remove(item))
            SelectedItems.Add(item);
    }

    private void SelectAllItems(List<LabelInfo> labels) => SelectedItems = labels;

    private IQueryable<DataItem> GetOrderedLabels(LabelInfo[] labels)
    {
        IEnumerable<DataItem> indexedLabels = labels.Select((label, index) => new DataItem { Id = index + 1, Label = label });
        if (string.IsNullOrEmpty(SearchingNumber)) return indexedLabels.AsQueryable();
        int.TryParse(SearchingNumber, out int number);
        indexedLabels = indexedLabels.Where(x => x.Id == number);
        return indexedLabels.AsQueryable();
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
        foreach (LabelInfo item in SelectedItems)
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
            await Task.Delay(PrinterRequestDelay);
        }

        await InvokeAsync(StateHasChanged);
    }

    private void PrintPrinterStatusMessage(PrinterStatus printerStatus) =>
        ToastService.ShowWarning(printerStatus switch
        {
            PrinterStatus.Disconnected => Localizer["PrinterStatusDisabled"],
            PrinterStatus.Paused => Localizer["PrinterStatusPaused"],
            PrinterStatus.HeadOpen => Localizer["PrinterStatusHeadOpen"],
            PrinterStatus.PaperOut => Localizer["PrinterStatusPaperOut"],
            PrinterStatus.PaperJam => Localizer["PrinterStatusPaperJam"],
            _ => Localizer["PrinterStatusUnknown"]
        });
}

internal record DataItem
{
    public int Id { get; init; }
    public LabelInfo Label { get; init; } = null!;
}