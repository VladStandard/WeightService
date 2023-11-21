using System.Diagnostics;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Events;
using ScalesHybrid.Resources;
using ScalesHybrid.Utils;
using Ws.Printers.Common;
using Ws.Printers.Main.Tsc;
using Ws.Printers.Main.Zebra;
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
    [Inject] private NavigationManager NavigationManager { get; set; }
    private IPrinter Printer { get; set; }
    private List<ControlBarButton> PluConfigButtonList { get; set; }
    private List<ControlBarButton> PluPrintButtonList { get; set; }
    
    private SqlHostEntity Host { get; set; }
    private SqlScaleEntity Line { get; set; }
    
    protected override void OnInitialized()
    {
        
        Host = HostService.GetCurrentHostOrCreate();
        Line = HostService.GetLineByHost(Host);
        Printer = PrinterFactory.Create(Line.Printer.Type);
        Printer.Connect(Line.Printer.Ip, Line.Printer.Port);
        
        MouseSubscribe();
        PluConfigButtonList = new()
        {
            new() { Title = Localizer["ButtonLineChange"] },
            new() { Title = Localizer["ButtonPLUChange"], OnClickAction = () => RedirectTo(RouterUtils.PluSelect)},
            new() { Title = Localizer["ButtonPLUNestingChange"] },
        };
        PluPrintButtonList = new()
        {
            new() { Title = Localizer["ButtonKneadingChange"] },
            new() { Title = Localizer["ButtonLabelPrint"], OnClickAction = PrintLabel},
            new() { Title = Localizer["ButtonScaleTerminal"], OnClickAction = OpenScalesTerminal},
        };
    }

    
    private void OpenScalesTerminal()
    {
        MouseUnsubscribe();
        try
        {
            Process process = new()
            {
                StartInfo = new(@"C:\Program Files (x86)\Massa-K\ScalesTerminal 100\ScalesTerminal.exe")
            };
            process.Start();
            process.WaitForExit();
        }
        catch
        {
            // TODO: Handle error
        }
        MouseSubscribe();
    }
    
    private void PrintLabel()
    {
        Printer.GetStatus();
    }
    
    private void RedirectTo(string url) => NavigationManager.NavigateTo(url);
    
    private void MouseSubscribe()
    {
        WeakReferenceMessenger.Default.Register<MiddleBtnIsClickedEvent>(this, (_, _) => PrintLabel());
    }
    
    private void MouseUnsubscribe()
    {
        WeakReferenceMessenger.Default.Unregister<MiddleBtnIsClickedEvent>(this);
    }
    
    public void Dispose()
    {
        MouseUnsubscribe();
        Printer.Dispose();
    }
}

internal class ControlBarButton
{
    public string Title { get; init; } = string.Empty;
    public Action OnClickAction { get; init; }
}