using Microsoft.AspNetCore.Components;
using ScalesHybrid.Services;

namespace ScalesHybrid.Components.Layout;

public partial class MainLayout : LayoutComponentBase
{
    [Inject] private PluService PluService { get; set; }

    protected override void OnInitialized()
    {
        PluService.InitData();
    }
}