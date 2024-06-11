using MassaK.Plugin.Abstractions.Enums;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using ScalesDesktop.Source.Shared.Api;
using ScalesDesktop.Source.Shared.Services;
using TscZebra.Plugin.Abstractions.Enums;
using TscZebra.Plugin.Abstractions.Exceptions;
using Ws.Desktop.Models.Features.Labels.Input;
using Ws.Desktop.Models.Features.Labels.Output;

namespace ScalesDesktop.Source.Features;

public sealed partial class LabelPrintButton : ComponentBase, IAsyncDisposable
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private PrinterService PrinterService { get; set; } = default!;
    [Inject] private ScalesService ScalesService { get; set; } = default!;
    [Inject] private LabelContext LabelContext { get; set; } = default!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;
    [Inject] private ArmContext ArmContext { get; set; } = default!;
    [Inject] private IDesktopApi DesktopApi { get; set; } = default!;

    #endregion

    private bool IsButtonClicked { get; set; }

    private const int ButtonCooldownDelay = 500;

    protected override void OnInitialized()
    {
        PrinterService.StatusChanged += StateHasChanged;
        ScalesService.StatusChanged += StateHasChanged;
        ScalesService.WeightChanged += StateHasChanged;
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
        if (!ValidateScalesStatus() || !await ValidatePrinterStatus()) return;

        CreateWeightLabelDto createDto = new()
        {
            Kneading = (ushort)LabelContext.KneadingModel.KneadingCount,
            ProductDt = GetProductDt(LabelContext.KneadingModel.ProductDate),
            WeightNet = (decimal)LabelContext.KneadingModel.NetWeightG / 1000,
            WeightTare = LabelContext.Plu?.TareWeight ?? 0
        };

        try
        {
            WeightLabel label = await DesktopApi.CreatePluWeightLabel(ArmContext.Arm!.Id, LabelContext.Plu!.Id, createDto);
            ArmContext.UpdateLineCounter(label.ArmCounter);
            await PrinterService.PrintZplAsync(label.Zpl);
        }
        catch (PrinterCommandBodyException)
        {
            ToastService.ShowError("Invalid zpl");
        }
        catch (PrinterStatusException)
        {
            PrintPrinterStatusMessage();
        }
        catch (PrinterConnectionException)
        {
            ToastService.ShowError("Printer connection error");
        }
        catch (Exception e)
        {
            ToastService.ShowError($"{Localizer["UnknownError"]}: {e}");
        }

        await InvokeAsync(StateHasChanged);
    }

    private async Task<bool> ValidatePrinterStatus()
    {
        PrinterStatus printerStatus = await PrinterService.GetStatusAsync();
        if (printerStatus is PrinterStatus.Ready or PrinterStatus.Busy) return true;
        PrintPrinterStatusMessage();
        return false;
    }

    private bool ValidateScalesStatus()
    {
        if (!ScalesService.IsStable)
        {
            ToastService.ShowWarning(Localizer["ScalesStatusUnstable"]);
            return false;
        }

        if (((decimal)LabelContext.KneadingModel.NetWeightG / 1000 - LabelContext.Plu?.TareWeight ?? 0) < 0)
        {
            ToastService.ShowWarning(Localizer["ScalesStatusTooLight"]);
            return false;
        }

        return false;
    }

    private static DateTime GetProductDt(DateTime time) =>
        new(time.Year, time.Month, time.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

    private void PrintPrinterStatusMessage() =>
        ToastService.ShowWarning(PrinterService.Status switch
        {
            PrinterStatus.Disconnected => Localizer["PrinterStatusIsForceDisconnected"],
            PrinterStatus.Paused => Localizer["PrinterStatusPaused"],
            PrinterStatus.HeadOpen => Localizer["PrinterStatusHeadOpen"],
            PrinterStatus.PaperOut => Localizer["PrinterStatusPaperOut"],
            PrinterStatus.PaperJam => Localizer["PrinterStatusPaperJam"],
            _ => Localizer["PrinterStatusUnknown"]
        });

    private bool GetPrintLabelDisabledStatus() =>
        LabelContext.Plu == null || ScalesService.Status != MassaKStatus.Ready;

    # region Event Subscribe and Unsubscribe

    public async ValueTask DisposeAsync()
    {
        PrinterService.StatusChanged -= StateHasChanged;
        ScalesService.StatusChanged -= StateHasChanged;
        ScalesService.WeightChanged -= StateHasChanged;

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