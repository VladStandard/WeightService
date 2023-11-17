using System.Diagnostics;
using Microsoft.AspNetCore.Components;
using ScalesHybrid.Models;
using Ws.Services.Services.Host;
using Ws.Services.Services.Line;
using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace ScalesHybrid.Pages;

public partial class Index : ComponentBase
{
    private DateTime ProductDate { get; set; }
    [Inject] private IHostService HostService { get; set; }
    [Inject] private ILineService LineService { get; set; }
    private SqlHostEntity Host { get; set; }
    private SqlScaleEntity Line { get; set; }


    private KneadingModel Kneading { get; set; }
    
    protected override void OnInitialized()
    {
        ProductDate = DateTime.Now;
        Host = HostService.GetCurrentHostOrCreate();
        Line = LineService.GetLineByHost(Host);
        Kneading = new();
    }
    
    private static void OpenScalesTerminal()
    {
        Process process = new() {
            StartInfo = new(@"C:\Program Files (x86)\Massa-K\ScalesTerminal 100\ScalesTerminal.exe")
        };
        process.Start();
        process.WaitForExit();
    }
}