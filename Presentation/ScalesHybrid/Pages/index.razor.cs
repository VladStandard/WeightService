using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;
using Ws.Services.Services.Host;
using Ws.Services.Services.Line;
using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace ScalesHybrid.Pages;

public partial class Index : ComponentBase
{
    [Inject] private IHostService HostService { get; set; }
    [Inject] private ILineService LineService { get; set; }
    [Inject] private PageTitleService PageTitleService { get; set; }

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlHostEntity Host { get; set; }
    private SqlScaleEntity Line { get; set; }
    private DateTime ProductDate { get; set; }
    
    protected override void OnInitialized()
    {
        ProductDate = DateTime.Now;
        Host = HostService.GetCurrentHostOrCreate();
        Line = LineService.GetLineByHost(Host);
        PageTitleService.SetTitle(Localizer["PageTitleIndex"]);
    }
}