using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Models;
using ScalesHybrid.Resources;

namespace ScalesHybrid.Components;

public sealed partial class KneadingDisplayWeight: ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    [Parameter] public WeightKneadingModel KneadingModel { get; set; } = new();
 
    private string Sign => KneadingModel.NetWeight >= 0 ? string.Empty : "-";
    private string IntegerPart => ((int)Math.Truncate(Math.Abs(KneadingModel.NetWeight))).ToString("D4");
    private string DecimalPart => Math.Abs(KneadingModel.NetWeight % 1).ToString(".000").Substring(1);

    private void IncreaseDate()
    {
        KneadingModel.ProductDate = KneadingModel.ProductDate.AddDays(1);
        StateHasChanged();
    }
    
    private void DecreaseDate()
    {
        KneadingModel.ProductDate = KneadingModel.ProductDate.AddDays(-1);
        StateHasChanged();
    }
}