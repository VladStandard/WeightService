using Append.Blazor.Printing;
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
    [Inject] private IPrintingService PrintingService { get; set; } = default!;
    [Inject] private PalletDocumentGenerator PalletDocumentGenerator { get; set; } = default!;

    # endregion

    [Parameter, EditorRequired] public PalletInfo Pallet { get; set; } = default!;

    private IEnumerable<DataItem> SelectedItems { get; set; } = [];
    private string SearchingNumber { get; set; } = string.Empty;
    private bool IsPrinting { get; set; } = false;
    private const ushort PrinterRequestDelay = 500;
    private const ushort MaxPrinterErrors = 3;

    private void ToggleAllLabels(DataItem[] labels) =>
        SelectedItems = SelectedItems.Count().Equals(labels.Length) ? [] : labels.ToList();

    private void SelectAllItems(List<DataItem> labels) => SelectedItems = labels;

    private IQueryable<DataItem> GetOrderedLabels(LabelInfo[] labels)
    {
        IEnumerable<DataItem> indexedLabels = labels.Select((label, index) => new DataItem { Id = index + 1, Label = label });
        if (string.IsNullOrEmpty(SearchingNumber)) return indexedLabels.AsQueryable();
        indexedLabels = indexedLabels.Where(x => x.Id.ToString().Contains(SearchingNumber));
        return indexedLabels.AsQueryable();
    }

    private async Task PrintLabelsAsync()
    {
        if (IsPrinting) return;

        IsPrinting = true;

        PrinterStatus printerStatus = await PrinterService.GetStatusAsync();

        if (printerStatus is not (PrinterStatus.Ready or PrinterStatus.Busy))
        {
            PrintPrinterStatusMessage(printerStatus);
            IsPrinting = false;
            return;
        }

        string toastUid = Guid.NewGuid().ToString();
        double percentagesPerLabel = 100.0 / SelectedItems.Count();
        ToastParameters<ProgressToastContent> toastData = new()
        {
            Id = toastUid,
            Intent = ToastIntent.Upload,
            Title = Localizer["LabelsPrintingToastTitle"],
            Timeout = 0,
            TopAction = "Cancel",
            Content = new()
            {
                Details = Localizer["LabelsPrintingToastDescription"],
                Progress = 0,
            }
        };
        ToastService.ShowProgressToast(toastData);

        IOrderedEnumerable<DataItem> orderedLabels = SelectedItems.OrderBy(x => x.Id);
        bool isPrintedSuccessfully = true;
        ushort printedLabelsCount = 0;
        List<DataItem> itemsToDelete = [];

        foreach (DataItem item in orderedLabels)
        {
            for (int attempt = 0 ; attempt < MaxPrinterErrors ; attempt++)
            {
                try
                {
                    await PrinterService.PrintZplAsync(item.Label.Zpl);
                    itemsToDelete.Add(item);
                    isPrintedSuccessfully = true;
                    printedLabelsCount += 1;
                    toastData.Content.Progress = (int)(printedLabelsCount * percentagesPerLabel);
                    ToastService.UpdateToast(toastUid, toastData);
                    break;
                }
                catch
                {
                    isPrintedSuccessfully = false;
                    await Task.Delay(PrinterRequestDelay);
                }
            }

            if (!isPrintedSuccessfully)
            {
                ToastService.ShowError(string.Format(Localizer["IndexedLabelNotPrinted"], item.Id));
                break;
            }

            await Task.Delay(PrinterRequestDelay);
        }

        SelectedItems = SelectedItems.Except(itemsToDelete);

        ToastService.CloseToast(toastUid);
        if (isPrintedSuccessfully)
            ToastService.ShowSuccess(Localizer["LabelsPrintingSuccess"]);
        else
            ToastService.ShowError(Localizer["PrintingStoppedDueToErrors"]);

        IsPrinting = false;

        await InvokeAsync(StateHasChanged);
    }

    private void PrintPrinterStatusMessage(PrinterStatus printerStatus) =>
        ToastService.ShowWarning(printerStatus switch
        {
            PrinterStatus.Disconnected => Localizer["PrinterStatusDisconnected"],
            PrinterStatus.Paused => Localizer["PrinterStatusPaused"],
            PrinterStatus.HeadOpen => Localizer["PrinterStatusHeadOpen"],
            PrinterStatus.PaperOut => Localizer["PrinterStatusPaperOut"],
            PrinterStatus.PaperJam => Localizer["PrinterStatusPaperJam"],
            _ => Localizer["PrinterStatusUnknown"]
        });

    private async Task PrintPalletCard()
    {
        try
        {
            string palletCardBase64 = PalletDocumentGenerator.CreateBase64(Pallet);
            await PrintingService.Print(new(palletCardBase64) { Base64 = true });
        }
        catch
        {
            ToastService.ShowError(Localizer["PalletDocumentGenerationError"]);
        }
    }
}

internal record DataItem
{
    public int Id { get; init; }
    public LabelInfo Label { get; init; } = null!;
}