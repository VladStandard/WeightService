using System.Diagnostics;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using NHibernate.Impl;
using NHibernate.Linq;
using Radzen;
using ScalesHybrid.Events;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;
using ScalesHybrid.Utils;
using Ws.Printers.Common;
using Ws.Printers.Events;
using Ws.Printers.Utils;
using Ws.Services.Services.Host;
using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Entities.SchemaScale.Scales;
using Ws.StorageCore.Enums;

namespace ScalesHybrid.Components.Controls;

public sealed partial class IndexControlBar : ComponentBase, IDisposable
{
    [Inject] private IHostService HostService { get; set; }
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; }
    [Inject] private NotificationService NotificationService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private ExternalDevicesService ExternalDevices { get; set; }
    
    private List<ControlBarButton> PluPrintButtonList { get; set; }
    private SqlHostEntity Host { get; set; }
    private SqlLineEntity Line { get; set; }
    
    protected override void OnInitialized()
    {
        
        Host = HostService.GetCurrentHostOrCreate();
        Line = HostService.GetLineByHost(Host);
        
        ExternalDevices.SetupPrinter(Line.Printer.Ip, Line.Printer.Port, Line.Printer.Type);
        ExternalDevices.SetupScales(Line.DeviceComPort);
        
        
        MouseSubscribe();
        PrinterStatusSubscribe();
        PluPrintButtonList = new()
        {
            new() { Title = Localizer["ButtonScaleCalibration"], OnClickAction = OpenScalesTerminal},
            new() { Title = Localizer["ButtonLabelPrint"], OnClickAction = PrintLabel},
            new() { Title = Localizer["ButtonPalletPage"] },
        };
    }

    
    private void OpenScalesTerminal()
    {
        ExternalDevices.Scales.Calibrate();
    }
    
    private void PrintLabel()
    {
        ExternalDevices.Printer.RequestStatus();
    }
    
    private void RedirectTo(string url) => NavigationManager.NavigateTo(url);
    
    private void MouseSubscribe()
    {
        WeakReferenceMessenger.Default.Register<MiddleBtnIsClickedEvent>(this, (_, _) => PrintLabel());
    }

    private void PrintNotification(object sender, GetPrinterStatusEvent payload)
    {
        NotificationMessage msg = new() { Severity = NotificationSeverity.Info, Summary = payload.Status.ToString()};
        NotificationService.Notify(msg);
    }

    private void PrinterStatusSubscribe()
    {
        WeakReferenceMessenger.Default.Register<GetPrinterStatusEvent>(this, PrintNotification);
    }
    
    private void PrinterStatusUnsubscribe()
    {
        WeakReferenceMessenger.Default.Unregister<GetPrinterStatusEvent>(this);
    }
    
    private void MouseUnsubscribe()
    {
        WeakReferenceMessenger.Default.Unregister<MiddleBtnIsClickedEvent>(this);
    }
    
    public void Dispose()
    {
        PrinterStatusUnsubscribe();
        MouseUnsubscribe();
    }
}

internal class ControlBarButton
{
    public string Title { get; init; } = string.Empty;
    public Action OnClickAction { get; init; }
}