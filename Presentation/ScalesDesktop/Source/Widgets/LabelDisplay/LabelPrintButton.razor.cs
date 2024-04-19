using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Domain.Services.Features.Line;
using Ws.Labels.Service.Features.PrintLabel;
using Ws.Labels.Service.Features.PrintLabel.Features.Weight.Dto.PrintWeightPlu;
using Ws.Labels.Service.Features.PrintLabel.Features.Weight.Exceptions.LabelGenerate;
using Ws.Printers.Enums;
using Ws.Printers.Events;
using Ws.Scales.Enums;
using Ws.Scales.Events;
using Ws.Shared.Resources;

namespace ScalesDesktop.Source.Widgets.LabelDisplay;

public sealed partial class LabelPrintButton : ComponentBase, IAsyncDisposable
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

    private PrinterStatusEnum PrinterStatus { get; set; } = PrinterStatusEnum.Unknown;
    private bool IsScalesStable { get; set; }
    private bool IsScalesDisconnected { get; set; }
    private bool IsButtonClicked { get; set; }
    private IJSObjectReference Module { get; set; } = null!;

    private const int PrinterRequestDelay = 100;
    private const int ButtonCooldownDelay = 500;

    protected override void OnInitialized()
    {
        PrinterStatusSubscribe();
        ScalesSubscribe();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        Module = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./libs/handle-mouse.js");
        await Module.InvokeVoidAsync("initializeMiddleMouseEvent", DotNetObjectReference.Create(this));
    }

    [JSInvokable("HandleMiddleMouseClick")]
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
        catch (LabelWeightGenerateException ex)
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
        if (PrinterStatus is PrinterStatusEnum.Ready or PrinterStatusEnum.Busy) return true;
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
            PrinterStatusEnum.IsDisabled => Localizer["PrinterStatusIsDisabled"],
            PrinterStatusEnum.IsForceDisconnected => Localizer["PrinterStatusIsForceDisconnected"],
            PrinterStatusEnum.Paused => Localizer["PrinterStatusPaused"],
            PrinterStatusEnum.HeadOpen => Localizer["PrinterStatusHeadOpen"],
            PrinterStatusEnum.PaperOut => Localizer["PrinterStatusPaperOut"],
            PrinterStatusEnum.PaperJam => Localizer["PrinterStatusPaperJam"],
            _ => Localizer["PrinterStatusUnknown"]
        });

    private bool GetPrintLabelDisabledStatus() =>
        LabelContext.Plu.IsNew || LabelContext.Plu.Nesting.IsNew ||
        LabelContext.Plu.IsCheckWeight & IsScalesDisconnected;

    private decimal GetWeight() =>
        (decimal)LabelContext.KneadingModel.NetWeightG / 1000 - LabelContext.Plu.DefaultWeightTare;

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


    public async ValueTask DisposeAsync()
    {
        try
        {
            PrinterStatusUnsubscribe();
            ScalesUnsubscribe();
            await Module.InvokeVoidAsync("removeMiddleMouseEvent");
            await Module.DisposeAsync();
        }
        catch
        {
            // pass
        }
    }

    # endregion
}