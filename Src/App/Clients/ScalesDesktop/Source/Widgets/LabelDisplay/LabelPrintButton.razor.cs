using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using ScalesDesktop.Source.Shared.Services;
using TscZebra.Plugin.Abstractions.Enums;
using TscZebra.Plugin.Abstractions.Exceptions;
using Ws.Domain.Services.Features.Arms;
using Ws.Labels.Service.Features.Generate;
using Ws.Labels.Service.Features.Generate.Exceptions.LabelGenerate;
using Ws.Labels.Service.Features.Generate.Features.Weight.Dto;
using Ws.Scales.Enums;

namespace ScalesDesktop.Source.Widgets.LabelDisplay;

public sealed partial class LabelPrintButton : ComponentBase, IAsyncDisposable
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private PrinterService PrinterService { get; set; } = default!;
    [Inject] private ScalesService ScalesService { get; set; } = default!;
    [Inject] private IPrintLabelService PrintLabelService { get; set; } = default!;
    [Inject] private IArmService ArmService { get; set; } = default!;
    [Inject] private LabelContext LabelContext { get; set; } = default!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    #endregion

    private bool IsButtonClicked { get; set; }

    private const int ButtonCooldownDelay = 500;

    protected override void OnInitialized()
    {
        PrinterService.OnStatusChanged += StateHasChanged;
        ScalesService.OnStatusChanged += StateHasChanged;
        ScalesService.OnStableChanged += StateHasChanged;
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

        GenerateWeightLabelDto generateLabelDto = CreateLabelInfoDto();

        try
        {
            string zpl = PrintLabelService.GenerateWeightLabel(generateLabelDto).Zpl;
            LabelContext.Line.Counter += 1;
            ArmService.Update(LabelContext.Line);
            await PrinterService.PrintZplAsync(zpl);
        }
        catch (LabelGenerateException ex)
        {
            ToastService.ShowError(ex.Code switch
            {
                LabelGenExceptions.Invalid => Localizer["LabelGenErrorInvalid"],
                LabelGenExceptions.TemplateNotFound => Localizer["LabelGenErrorTemplateNotFound"],
                LabelGenExceptions.StorageMethodNotFound => Localizer["LabelGenErrorStorageMethodNotFound"],
                _ => Localizer["UnknownError"]
            });
        }
        catch (PrinterCommandBodyException)
        {
            ToastService.ShowError("Invalid zpl");
        }
        catch (PrinterStatusException)
        {
            ToastService.ShowError("Printer status error");
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
        PrinterStatuses printerStatus = await PrinterService.GetStatusAsync();
        if (printerStatus is PrinterStatuses.Ready or PrinterStatuses.Busy) return true;
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
        ToastService.ShowWarning(PrinterService.Status switch
        {
            PrinterStatuses.IsDisconnected => Localizer["PrinterStatusIsForceDisconnected"],
            PrinterStatuses.Paused => Localizer["PrinterStatusPaused"],
            PrinterStatuses.HeadOpen => Localizer["PrinterStatusHeadOpen"],
            PrinterStatuses.PaperOut => Localizer["PrinterStatusPaperOut"],
            PrinterStatuses.PaperJam => Localizer["PrinterStatusPaperJam"],
            _ => Localizer["PrinterStatusUnknown"]
        });

    private bool GetPrintLabelDisabledStatus() =>
        LabelContext.Plu.IsNew ||
        LabelContext.Plu.PluNesting.IsNew ||
        LabelContext.Plu.IsCheckWeight & ScalesService.Status != ScalesStatus.IsConnect;

    private decimal GetWeight() =>
        (decimal)LabelContext.KneadingModel.NetWeightG / 1000 - LabelContext.Plu.GetWeightWithNesting;

    # region Event Subscribe and Unsubscribe

    public async ValueTask DisposeAsync()
    {
        PrinterService.OnStatusChanged -= StateHasChanged;
        ScalesService.OnStatusChanged -= StateHasChanged;
        ScalesService.OnStableChanged -= StateHasChanged;

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