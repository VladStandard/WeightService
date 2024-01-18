using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;

namespace ScalesHybrid.Features.Labels.Modules;

public sealed partial class LabelDisplayDate: ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private LineContext LineContext { get; set; } = null!;
    
    private void IncreaseDate() => 
        LineContext.KneadingModel.ProductDate = LineContext.KneadingModel.ProductDate.AddDays(1);
    
    private void DecreaseDate() => 
        LineContext.KneadingModel.ProductDate = LineContext.KneadingModel.ProductDate.AddDays(-1);

    private void ResetDate() =>
        LineContext.KneadingModel.ProductDate = DateTime.Now;
}