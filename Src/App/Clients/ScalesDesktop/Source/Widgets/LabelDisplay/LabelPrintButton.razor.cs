using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using ScalesDesktop.Source.Shared.Services;
using Ws.Domain.Services.Features.Line;
using Ws.Labels.Service.Features.Generate;
using Ws.Labels.Service.Features.Generate.Exceptions.LabelGenerate;
using Ws.Labels.Service.Features.Generate.Features.Weight.Dto;
using Ws.Printers.Enums;
using Ws.Printers.Messages;
using Ws.Scales.Enums;
using Ws.Scales.Messages;

namespace ScalesDesktop.Source.Widgets.LabelDisplay;

public sealed partial class LabelPrintButton : ComponentBase, IAsyncDisposable,
    IRecipient<ScaleMassaMsg>, IRecipient<ScaleStatusMsg>, IRecipient<PrinterStatusMsg>
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private ExternalDevicesService ExternalDevices { get; set; } = default!;
    [Inject] private IPrintLabelService PrintLabelService { get; set; } = default!;
    [Inject] private ILineService LineService { get; set; } = default!;
    [Inject] private LabelContext LabelContext { get; set; } = default!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    #endregion

    private PrinterStatus PrinterStatus { get; set; } = PrinterStatus.Unknown;
    private bool IsScalesStable { get; set; }
    private bool IsScalesDisconnected { get; set; }
    private bool IsButtonClicked { get; set; }

    private const int PrinterRequestDelay = 100;
    private const int ButtonCooldownDelay = 500;

    protected override void OnInitialized()
    {
        WeakReferenceMessenger.Default.RegisterAll(this);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await JsRuntime.InvokeVoidAsync("subscribeMiddleMouseClickEvent", DotNetObjectReference.Create(this), nameof(HandleMiddleMouseClick));
    }

    [JSInvokable]
    public async Task HandleMiddleMouseClick()
    {
        if (GetPrintLabelDisabledStatus()) return;
        await PrintLabel();
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

        GenerateWeightLabelDto generateLabelDto = CreateLabelInfoDto();

        try
        {
            string zpl = PrintLabelService.GenerateWeightLabel(generateLabelDto).Zpl;
            LabelContext.Line.Counter += 1;
            LineService.Update(LabelContext.Line);
            ExternalDevices.Printer.PrintLabel(zpl);
        }
        catch (LabelGenerateException ex)
        {
            ToastService.ShowError(ex.Code switch
            {
                LabelGenExceptionEnum.Invalid => Localizer["LabelGenErrorInvalid"],
                LabelGenExceptionEnum.TemplateNotFound => Localizer["LabelGenErrorTemplateNotFound"],
                LabelGenExceptionEnum.StorageMethodNotFound => Localizer["LabelGenErrorStorageMethodNotFound"],
                _ => Localizer["UnknownError"]
            });
        }
        catch (Exception e)
        {
            ToastService.ShowError($"{Localizer["UnknownError"]}: {e}");
        }

        await InvokeAsync(StateHasChanged);
    }

    private bool ValidatePrinterStatus()
    {
        if (PrinterStatus is PrinterStatus.Ready or PrinterStatus.Busy) return true;
        PrintPrinterStatusMessage();
        return false;
    }

    private bool ValidateScalesStatus()
    {
        if (!IsScalesStable)
        {
            ToastService.ShowWarning(Localizer["ScalesStatusUnstable"]);
            return false;
        }

        if (GetWeight() <= 0)
        {
            ToastService.ShowWarning(Localizer["ScalesStatusTooLight"]);
            return false;
        }

        return true;
    }

    private GenerateWeightLabelDto CreateLabelInfoDto() =>
        new()
        {
            Plu = LabelContext.Plu,
            Line = LabelContext.Line,
            Weight = GetWeight(),
            Kneading = (short)LabelContext.KneadingModel.KneadingCount,
            ProductDt = GetProductDt(LabelContext.KneadingModel.ProductDate)
        };

    private static DateTime GetProductDt(DateTime time) =>
        new(time.Year, time.Month, time.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

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

    private bool GetPrintLabelDisabledStatus() =>
        LabelContext.Plu.IsNew || LabelContext.Plu.PluNesting.IsNew ||
        LabelContext.Plu.IsCheckWeight & IsScalesDisconnected;

    private decimal GetWeight() =>
        (decimal)LabelContext.KneadingModel.NetWeightG / 1000 - LabelContext.Plu.GetWeightWithNesting;

    # region Event Subscribe and Unsubscribe

    public void Receive(PrinterStatusMsg message) => PrinterStatus = message.Status;

    public void Receive(ScaleMassaMsg message) => IsScalesStable = message.IsStable;

    public void Receive(ScaleStatusMsg message)
    {
        IsScalesDisconnected = message.Status is not ScalesStatus.IsConnect;
        InvokeAsync(StateHasChanged);
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            await JsRuntime.InvokeVoidAsync("unsubscribeMiddleMouseClickEvent");
        }
        catch
        {
            // pass
        }
    }

    # endregion
}