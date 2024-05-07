using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Pallet;
using Ws.Labels.Service.Features.Generate;
using Ws.Printers.Enums;
using Ws.Printers.Messages;
using Ws.Shared.Resources;

namespace ScalesDesktop.Source.Features;

public sealed partial class LabelsGrid : ComponentBase, IDisposable, IRecipient<PrinterStatusMsg>
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
    private PrinterStatus PrinterStatus { get; set; } = PrinterStatus.Unknown;
    private Action? StateChangedHandler { get; set; }
    private const int PrinterRequestDelay = 100;

    protected override void OnInitialized()
    {
        WeakReferenceMessenger.Default.Register(this);
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

        if (PrinterStatus is not (PrinterStatus.Ready or PrinterStatus.Busy))
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
            PrinterStatus.IsDisabled => Localizer["PrinterStatusIsDisabled"],
            PrinterStatus.IsForceDisconnected => Localizer["PrinterStatusIsForceDisconnected"],
            PrinterStatus.Paused => Localizer["PrinterStatusPaused"],
            PrinterStatus.HeadOpen => Localizer["PrinterStatusHeadOpen"],
            PrinterStatus.PaperOut => Localizer["PrinterStatusPaperOut"],
            PrinterStatus.PaperJam => Localizer["PrinterStatusPaperJam"],
            _ => Localizer["PrinterStatusUnknown"]
        });

    public void Dispose()
    {
        if (StateChangedHandler != null)
            PalletContext.OnStateChanged -= StateChangedHandler;
    }

    public void Receive(PrinterStatusMsg message) => PrinterStatus = message.Status;
}

internal record DataItem
{
    public int Id { get; init; }
    public LabelEntity Label { get; init; } = new();
}