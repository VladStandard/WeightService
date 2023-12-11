using Blazorise;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Events;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;
using Ws.Printers.Enums;
using Ws.Printers.Events;
using Ws.Scales.Enums;
using Ws.Scales.Events;
using Ws.Services.Dto;
using Ws.Services.Exceptions;
using Ws.Services.Services.PrintLabel;

namespace ScalesHybrid.Components.Controls;

public sealed partial class PrintLabelButton: ComponentBase, IDisposable
{
    # region Injects
    
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; }
    [Inject] private INotificationService NotificationService { get; set; }
    [Inject] private ExternalDevicesService ExternalDevices { get; set; }
    [Inject] private IPrintLabelService PrintLabelService { get; set; }
    [Inject] private LineContext LineContext { get; set; }
    
    #endregion
    
    private bool IsScalesStable { get; set; }
    private bool IsScalesDisconnected { get; set; }
    private bool IsPrinterDisconnected { get; set; }
    
    protected override void OnInitialized()
    {
        MouseSubscribe();
        PrinterStatusSubscribe();
        ScalesSubscribe();
    }
    
    private async Task PrintLabel()
    {
        ExternalDevices.Printer.RequestStatus();

        await Task.Delay(100);

        if (IsPrinterDisconnected)
        {
            await NotificationService.Warning("Принтер не активен", "Печать этикеток");
            return;
        }

        if (LineContext.Plu.IsCheckWeight && !IsScalesStable)
        {
            await NotificationService.Warning("Весы не стабильны", "Печать этикеток");
            return;
        }

        if (LineContext.Plu.IsCheckWeight && GetWeight() <= 0)
        {
            await NotificationService.Warning("На весах слишком маленький вес", "Печать этикеток");
            return;
        }

        LabelInfoDto labelDto = CreateLabelInfoDto();

        try
        {
            string zpl = PrintLabelService.GenerateLabel(labelDto);
            ExternalDevices.Printer.PrintLabel(zpl);
        }
        catch (LabelException ex)
        {
            await NotificationService.Error(ex.ToString(), "Печать этикеток");
        }
    }
    
    private LabelInfoDto CreateLabelInfoDto() =>
        new()
        {
            Kneading = (short)LineContext.KneadingModel.KneadingCount,
            Weight = GetWeight(),
            LineCounter = LineContext.Line.LabelCounter,
            BundleCount = LineContext.PluNesting.BundleCount,
            IsCheckWeight = LineContext.Plu.IsCheckWeight,
            Itf = LineContext.Plu.Itf14,
            Gtin = LineContext.Plu.Gtin,
            Address = LineContext.Line.WorkShop.ProductionSite.Address,
            PluFullName = LineContext.Plu.FullName,
            PluDescription = LineContext.Plu.Description,
            ProductDt = LineContext.KneadingModel.ProductDate,
            ExpirationDt = LineContext.KneadingModel.ProductDate.AddDays(LineContext.Plu.ShelfLifeDays),
            LineNumber = LineContext.Line.Number,
            PluNumber = LineContext.Plu.Number,
            LineName = LineContext.Line.Description,
            Template = LineContext.PluTemplate.Data
        };
    
    private bool GetPrintLabelDisabledStatus() =>
        LineContext.Plu.IsNew || LineContext.PluNesting.IsNew || LineContext.Plu.IsCheckWeight & IsScalesDisconnected;

    private decimal GetWeight() =>
        (decimal)LineContext.KneadingModel.NetWeightG / 1000 - LineContext.PluNesting.WeightTare;

    private void PrintNotification(object sender, GetPrinterStatusEvent payload) =>
        IsPrinterDisconnected = payload.Status is not (PrinterStatusEnum.Ready or PrinterStatusEnum.Busy);

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
        WeakReferenceMessenger.Default.Register<MiddleBtnIsClickedEvent>(this, (_, _) => Task.Run(PrintLabel));
    
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