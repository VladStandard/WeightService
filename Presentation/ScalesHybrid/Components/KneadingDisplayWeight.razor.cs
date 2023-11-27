using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using NHibernate.Criterion;
using Radzen;
using Radzen.Blazor;
using ScalesHybrid.Components.Dialogs;
using ScalesHybrid.Models;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;

namespace ScalesHybrid.Components;

public sealed partial class KneadingDisplayWeight: ComponentBase
{
    [Inject] private LineContext LineContext { get; set; }
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; }
    [Inject] private DialogService DialogService { get; set; }

    private string Sign => LineContext.KneadingModel.NetWeight >= 0 ? string.Empty : "-";
    
    private string IntegerPart => ((int)Math.Truncate(Math.Abs(LineContext.KneadingModel.NetWeight))).ToString("D4");
    
    private string DecimalPart => Math.Abs(LineContext.KneadingModel.NetWeight % 1).ToString(".000")[1..];
    
    private void IncreaseDate() => LineContext.KneadingModel.ProductDate = LineContext.KneadingModel.ProductDate.AddDays(1);
    
    private void DecreaseDate() => LineContext.KneadingModel.ProductDate = LineContext.KneadingModel.ProductDate.AddDays(-1);
    
    private void SetNewKneading(int newKneading) => LineContext.KneadingModel.KneadingCount = newKneading;

    private async Task ShowInlineDialog() => 
        await DialogService.OpenAsync<DialogCalculator>(string.Empty,
            new() { { "CallbackFunction", new Action<int>(SetNewKneading) } },
            new() { ShowTitle = false, Style = "min-height:auto;min-width:auto;width:auto" });
}