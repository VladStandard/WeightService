using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Models;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;
using Ws.Services.Services.Host;
using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace ScalesHybrid.Pages;

public partial class Index : ComponentBase
{
    [Inject] private IHostService HostService { get; set; }
    [Inject] private PageTitleService PageTitleService { get; set; }
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlHostEntity Host { get; set; }
    private SqlLineEntity Line { get; set; }
    private DateTime ProductDate { get; set; }
    private WeightKneadingModel KneadingModel { get; set; }
    
    protected override void OnInitialized()
    {
        ProductDate = DateTime.Now;
        Host = HostService.GetCurrentHostOrCreate();
        Line = HostService.GetLineByHost(Host);
        PageTitleService.SetTitle(Localizer["PageTitleIndex"]);
        KneadingModel = new()
        {
            PluName = "ПЛУ (вес) | 349 | Классическая (Светофор)",
            PluNesting = "15x45",
            ProductDate = DateOnly.FromDateTime(DateTime.Today),
            KneadingCount = 30,
            NetWeight = -1.504m,
            TareWeight = 1.504m
        };
    }
}