using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;

namespace ScalesHybrid.Pages;

public partial class Index : ComponentBase
{
    [Inject] private PageTitleService PageTitleService { get; set; }
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    protected override void OnInitialized()
    {
        PageTitleService.SetTitle(Localizer["PageTitleIndex"]);
    }
}