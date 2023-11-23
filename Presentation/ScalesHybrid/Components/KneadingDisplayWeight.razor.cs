using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using NHibernate.Criterion;
using Radzen;
using Radzen.Blazor;
using ScalesHybrid.Models;
using ScalesHybrid.Resources;

namespace ScalesHybrid.Components;

public sealed partial class KneadingDisplayWeight: ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private DialogService DialogService { get; set; }
    
    [Parameter] public WeightKneadingModel KneadingModel { get; set; } = new();
 
    private int orderID = 10248;
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

    private void SetNewKneading(int newKneading)
    {
        KneadingModel.KneadingCount = newKneading;
        StateHasChanged();
    }

    private async Task ShowInlineDialog() => 
        await DialogService.OpenAsync<DialogCalculator>(string.Empty,
            new Dictionary<String, Object> { { "CallbackFunction", new Action<int>(SetNewKneading) } },
            new DialogOptions { ShowTitle = false, Style = "min-height:auto;min-width:auto;width:auto" });
    
}