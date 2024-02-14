using Blazorise;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Events;
using ScalesDesktop.Resources;
using ScalesDesktop.Services;
using Ws.Database.Core.Helpers;
using Ws.Labels.Service.Features.PrintLabel;
using Ws.Labels.Service.Features.PrintLabel.Weight.Dto;
using Ws.Printers.Enums;
using Ws.Printers.Events;
using Ws.Scales.Enums;
using Ws.Scales.Events;

namespace ScalesDesktop.Features.Labels.Modules;

public sealed partial class LabelPrintButton : ComponentBase, IDisposable
{
    # region Injects

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private INotificationService NotificationService { get; set; } = null!;
    [Inject] private ExternalDevicesService ExternalDevices { get; set; } = null!;
    [Inject] private IPrintLabelService PrintLabelService { get; set; } = null!;

    #endregion

    [Inject] private LabelContext LabelContext { get; set; } = null!;

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

        if (!await ValidateScalesStatus() || !await ValidatePrinterStatus()) return;

        try
        {
            LabelWeightDto labelDto = CreateLabelInfoDto();
            string zpl = PrintLabelService.GenerateWeightLabel(labelDto);
            ExternalDevices.Printer.PrintLabel(zpl);
            LabelContext.Line.Counter += 1;
            SqlCoreHelper.Update(LabelContext.Line);
            await InvokeAsync(StateHasChanged);
        }
        catch (Exception ex)
        {
            await NotificationService.Error(ex.ToString());
        }
    }

    private async Task<bool> ValidatePrinterStatus()
    {
        if (PrinterStatus is PrinterStatusEnum.Ready or PrinterStatusEnum.Busy) return true;
        await PrintPrinterStatusMessage();
        return false;
    }

    private async Task<bool> ValidateScalesStatus()
    {
        switch (LabelContext.Plu.IsCheckWeight)
        {
            case true when !IsScalesStable:
                await NotificationService.Warning(Localizer["ScalesStatusUnstable"]);
                return false;
            case true when GetWeight() <= 0:
                await NotificationService.Warning(Localizer["ScalesStatusTooLight"]);
                return false;
            default:
                return true;
        }
    }

    private LabelWeightDto CreateLabelInfoDto() =>
        new()
        {
            Kneading = (short)LabelContext.KneadingModel.KneadingCount,
            Weight = GetWeight(),
            Nesting = LabelContext.PluNesting,
            Line = LabelContext.Line,
            Template = LabelContext.PluTemplate.Body,
            ProductDt = GetProductDt(LabelContext.KneadingModel.ProductDate),
            ExpirationDt = GetProductDt(LabelContext.KneadingModel.ProductDate)
                .AddDays(LabelContext.Plu.ShelfLifeDays)
        };

    private static DateTime GetProductDt(DateTime time) =>
        new(time.Year, time.Month, time.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

    private async Task PrintPrinterStatusMessage() =>
        await NotificationService.Warning(PrinterStatus switch
        {
            PrinterStatusEnum.IsDisabled => Localizer["PrinterStatusIsDisabled"],
            PrinterStatusEnum.IsForceDisconnected => Localizer["PrinterStatusIsForceDisconnected"],
            PrinterStatusEnum.Paused => Localizer["PrinterStatusPaused"],
            PrinterStatusEnum.HeadOpen => Localizer["PrinterStatusHeadOpen"],
            PrinterStatusEnum.PaperOut => Localizer["PrinterStatusPaperOut"],
            PrinterStatusEnum.PaperJam => Localizer["PrinterStatusPaperJam"],
            _ => Localizer["PrinterStatusUnknown"]
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