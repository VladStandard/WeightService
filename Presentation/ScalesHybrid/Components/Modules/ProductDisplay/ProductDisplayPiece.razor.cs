using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;

namespace ScalesHybrid.Components.Modules.ProductDisplay;

public sealed partial class ProductDisplayPiece: ComponentBase, IDisposable
{
    [Inject] private LineContext LineContext { get; set; }
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; }
    
    protected override void OnInitialized() => LineContext.OnStateChanged += StateHasChanged;
    public void Dispose() => LineContext.OnStateChanged -= StateHasChanged;
}