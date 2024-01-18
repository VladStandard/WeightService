using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;

namespace ScalesHybrid.Features.Labels.Modules;

public sealed partial class LabelDisplayTareWeight: ComponentBase, IDisposable
{
    [Inject] private LineContext LineContext { get; set; } = null!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    protected override void OnInitialized() => LineContext.OnStateChanged += StateHasChanged;
    
    private decimal GetTareWeight => LineContext.PluNesting.WeightTare;
    
    public void Dispose() => LineContext.OnStateChanged -= StateHasChanged;
}