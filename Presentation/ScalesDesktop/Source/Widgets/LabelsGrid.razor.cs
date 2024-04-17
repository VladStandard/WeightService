using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Pallet;
using Ws.Labels.Service.Features.PrintLabel;
using Ws.Printers.Enums;
using Ws.Printers.Events;
using Ws.Shared.Resources;

namespace ScalesDesktop.Source.Widgets;

public sealed partial class LabelsGrid : ComponentBase, IDisposable
{
    # region Injects

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IPalletService PalletService { get; set; } = default!;
    [Inject] private PalletContext PalletContext { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private ExternalDevicesService ExternalDevices { get; set; } = default!;
    [Inject] private IPrintLabelService PrintLabelService { get; set; } = default!;

    # endregion

    private List<LabelEntity> SelectedItems { get; set; } = [];
    private string SearchingNumber { get; set; } = string.Empty;
    private IQueryable<DataItem> LabelData { get; set; } = Enumerable.Empty<DataItem>().AsQueryable();
    private PrinterStatusEnum PrinterStatus { get; set; } = PrinterStatusEnum.Unknown;
    private Action? StateChangedHandler { get; set; }
    private const int PrinterRequestDelay = 100;

    protected override void OnInitialized()
    {
        PrinterStatusSubscribe();
        StateChangedHandler = async () => await ResetDataGrid();
        PalletContext.OnStateChanged += StateChangedHandler;
    }

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

    private Task<IQueryable<DataItem>> InitializeData() =>
        Task.Run(() => LabelData = GetLabelsData()
            .Select((label, index) => new DataItem { Id = index + 1, Label = label })
            .AsQueryable());


    private IEnumerable<LabelEntity> GetLabelsData()
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
        ExternalDevices.Printer.RequestStatus();
        await Task.Delay(PrinterRequestDelay);

        if (PrinterStatus is not (PrinterStatusEnum.Ready or PrinterStatusEnum.Busy))
        {
            PrintPrinterStatusMessage();
            return;
        }

        int errorIndex = 0;
        foreach (LabelEntity item in SelectedItems)
        {
            try
            {
                errorIndex += 1;
                ExternalDevices.Printer.PrintLabel(item.Zpl);
            }
            catch
            {
                ToastService.ShowError($"{errorIndex} не распечаталась");
            }
            await Task.Delay(300);
        }

        await InvokeAsync(StateHasChanged);
    }

    private void PrintPrinterStatusMessage() =>
        ToastService.ShowWarning(PrinterStatus switch
        {
            PrinterStatusEnum.IsDisabled => Localizer["PrinterStatusIsDisabled"],
            PrinterStatusEnum.IsForceDisconnected => Localizer["PrinterStatusIsForceDisconnected"],
            PrinterStatusEnum.Paused => Localizer["PrinterStatusPaused"],
            PrinterStatusEnum.HeadOpen => Localizer["PrinterStatusHeadOpen"],
            PrinterStatusEnum.PaperOut => Localizer["PrinterStatusPaperOut"],
            PrinterStatusEnum.PaperJam => Localizer["PrinterStatusPaperJam"],
            _ => Localizer["PrinterStatusUnknown"]
        });

    private void PrintNotification(object sender, GetPrinterStatusEvent payload) => PrinterStatus = payload.Status;

    private void PrinterStatusSubscribe() =>
        WeakReferenceMessenger.Default.Register<GetPrinterStatusEvent>(this, PrintNotification);

    private void PrinterStatusUnsubscribe() =>
        WeakReferenceMessenger.Default.Unregister<GetPrinterStatusEvent>(this);

    public void Dispose()
    {
        PrinterStatusUnsubscribe();
        if (StateChangedHandler != null)
            PalletContext.OnStateChanged -= StateChangedHandler;
    }
}

internal record DataItem
{
    public int Id { get; init; }
    public LabelEntity Label { get; init; } = new();
}