using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Events;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Domain.Services.Features.Line;
using Ws.Labels.Service.Features.PrintLabel;
using Ws.Labels.Service.Features.PrintLabel.Types.Weight.Dto;
using Ws.Printers.Enums;
using Ws.Printers.Events;
using Ws.Scales.Enums;
using Ws.Scales.Events;
using Ws.SharedUI.Resources;

namespace ScalesDesktop.Source.Features;

public sealed partial class LabelPrintButton : ComponentBase, IDisposable
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
    [Inject] private IStringLocalizer<Resources> LabelsLocalizer { get; set; } = null!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private ExternalDevicesService ExternalDevices { get; set; } = null!;
    [Inject] private IPrintLabelService PrintLabelService { get; set; } = null!;
    [Inject] private ILineService LineService { get; set; } = null!;
    [Inject] private LabelContext LabelContext { get; set; } = null!;

    #endregion
    
    private PrinterStatusEnum PrinterStatus { get; set; } = PrinterStatusEnum.Unknown;
    private bool IsScalesStable { get; set; }
    private bool IsScalesDisconnected { get; set; }
    private bool IsButtonClicked { get; set; }

    private const int PrinterRequestDelay = 100;
    private const int ButtonCooldownDelay = 500;

    protected override void OnInitialized()
    {
        MouseSubscribe();
        PrinterStatusSubscribe();
        ScalesSubscribe();
    }

    private async Task PrintLabel()
    {
        if (IsButtonClicked) return;
        IsButtonClicked = true;

        await PrintLabelAsync();

        await Task.Delay(ButtonCooldownDelay);
        IsButtonClicked = false;
    }

    private async Task PrintLabelAsync()
    {
        ExternalDevices.Printer.RequestStatus();
        await Task.Delay(PrinterRequestDelay);

        if (!ValidateScalesStatus() || !ValidatePrinterStatus()) return;

        try
        {
            LabelWeightDto labelDto = CreateLabelInfoDto();
            string zpl = PrintLabelService.GenerateWeightLabel(labelDto);
            ExternalDevices.Printer.PrintLabel(zpl);
            LabelContext.Line.Counter += 1;
            LineService.Update(LabelContext.Line);
            await InvokeAsync(StateHasChanged);
        }
        catch (Exception ex)
        {
            ToastService.ShowError(ex.ToString());
        }
    }

    private bool ValidatePrinterStatus()
    {
        if (PrinterStatus is PrinterStatusEnum.Ready or PrinterStatusEnum.Busy) return true;
        PrintPrinterStatusMessage();
        return false;
    }

    private bool ValidateScalesStatus()
    {
        if (!IsScalesStable)
        {
            ToastService.ShowWarning(LabelsLocalizer["ScalesStatusUnstable"]);
            return false;
        }

        if (GetWeight() <= 0)
        {
            ToastService.ShowWarning(LabelsLocalizer["ScalesStatusTooLight"]);
            return false;
        }

        return true;
    }

    private LabelWeightDto CreateLabelInfoDto() =>
        new()
        {
            Kneading = (short)LabelContext.KneadingModel.KneadingCount,
            Weight = GetWeight(),
            Nesting = LabelContext.PluNesting,
            Line = LabelContext.Line,
            ProductDt = GetProductDt(LabelContext.KneadingModel.ProductDate),
            ExpirationDt = GetProductDt(LabelContext.KneadingModel.ProductDate)
                .AddDays(LabelContext.Plu.ShelfLifeDays)
        };

    private static DateTime GetProductDt(DateTime time) =>
        new(time.Year, time.Month, time.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

    private void PrintPrinterStatusMessage() =>
        ToastService.ShowWarning(PrinterStatus switch
        {
            PrinterStatusEnum.IsDisabled => LabelsLocalizer["PrinterStatusIsDisabled"],
            PrinterStatusEnum.IsForceDisconnected => LabelsLocalizer["PrinterStatusIsForceDisconnected"],
            PrinterStatusEnum.Paused => LabelsLocalizer["PrinterStatusPaused"],
            PrinterStatusEnum.HeadOpen => LabelsLocalizer["PrinterStatusHeadOpen"],
            PrinterStatusEnum.PaperOut => LabelsLocalizer["PrinterStatusPaperOut"],
            PrinterStatusEnum.PaperJam => LabelsLocalizer["PrinterStatusPaperJam"],
            _ => LabelsLocalizer["PrinterStatusUnknown"]
        });

    private bool GetPrintLabelDisabledStatus() =>
        LabelContext.Plu.IsNew || LabelContext.PluNesting.IsNew ||
        LabelContext.Plu.IsCheckWeight & IsScalesDisconnected;

    private decimal GetWeight() =>
        (decimal)LabelContext.KneadingModel.NetWeightG / 1000 - LabelContext.PluNesting.WeightTare;

    private void PrintNotification(object sender, GetPrinterStatusEvent payload) =>
        PrinterStatus = payload.Status;

    private void UpdateScalesInfo(object sender, GetScaleMassaEvent payload) =>
        IsScalesStable = payload.IsStable;

    private void UpdateScalesStatus(object recipient, GetScaleStatusEvent message)
    {
        IsScalesDisconnected = message.Status is not ScalesStatus.IsConnect;
        InvokeAsync(StateHasChanged);
    }

    # region Event Subscribe and Unsubscribe

    private void ScalesSubscribe()
    {
        WeakReferenceMessenger.Default.Register<GetScaleStatusEvent>(this, UpdateScalesStatus);
        WeakReferenceMessenger.Default.Register<GetScaleMassaEvent>(this, UpdateScalesInfo);
    }

    private void ScalesUnsubscribe()
    {
        WeakReferenceMessenger.Default.Unregister<GetScaleStatusEvent>(this);
        WeakReferenceMessenger.Default.Unregister<GetScaleMassaEvent>(this);
    }

    private void PrinterStatusSubscribe() =>
        WeakReferenceMessenger.Default.Register<GetPrinterStatusEvent>(this, PrintNotification);

    private void PrinterStatusUnsubscribe() =>
        WeakReferenceMessenger.Default.Unregister<GetPrinterStatusEvent>(this);

    private void MouseSubscribe() =>
        WeakReferenceMessenger.Default.Register<MiddleBtnIsClickedEvent>(this, (_, _) => {
            if (!GetPrintLabelDisabledStatus()) Task.Run(PrintLabel);
        }
        );

    private void MouseUnsubscribe() =>
        WeakReferenceMessenger.Default.Unregister<MiddleBtnIsClickedEvent>(this);

    public void Dispose()
    {
        PrinterStatusUnsubscribe();
        MouseUnsubscribe();
        ScalesUnsubscribe();
    }

    # endregion
}