using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;
using ScalesHybrid.Utils;
using Ws.Services.Services.Host;
using Ws.Services.Services.Line;
using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.Scales;

namespace ScalesHybrid.Pages;

public sealed partial class PluSelect: ComponentBase
{
    [Inject] private IHostService HostService { get; set; }
    [Inject] private ILineService LineService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private PageTitleService PageTitleService { get; set; }
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlHostEntity Host { get; set; }
    private SqlScaleEntity Line { get; set; }
    private IEnumerable<SqlPluEntity> SqlPluEntities { get; set; }
    private const int MaxPluCount = 16;

    private void RedirectToIndex() => NavigationManager.NavigateTo(RouterUtils.Index);

    protected override void OnInitialized()
    {
        PageTitleService.SetTitle(Localizer["PageTitleIndex"]);
        Host = HostService.GetCurrentHostOrCreate();
        Line = HostService.GetLineByHost(Host);
        SqlPluEntities = LineService.GetLinePlus(Line);
    }
}
