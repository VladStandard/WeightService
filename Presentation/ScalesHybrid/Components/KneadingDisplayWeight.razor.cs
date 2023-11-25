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
    
    [Parameter] public WeightKneadingModel KneadingModel { get; set; } = new();

    protected override void OnInitialized()
    {
        KneadingModel = LineContext.KneadingModel;
    }

    private string Sign => KneadingModel.NetWeight >= 0 ? string.Empty : "-";
    
    private string IntegerPart => ((int)Math.Truncate(Math.Abs(KneadingModel.NetWeight))).ToString("D4");
    
    private string DecimalPart => Math.Abs(KneadingModel.NetWeight % 1).ToString(".000")[1..];
    
    private void IncreaseDate() => KneadingModel.ProductDate = KneadingModel.ProductDate.AddDays(1);
    
    private void DecreaseDate() => KneadingModel.ProductDate = KneadingModel.ProductDate.AddDays(-1);
    
    private void SetNewKneading(int newKneading) => KneadingModel.KneadingCount = newKneading;

    private async Task ShowInlineDialog() => 
        await DialogService.OpenAsync<DialogCalculator>(string.Empty,
            new() { { "CallbackFunction", new Action<int>(SetNewKneading) } },
            new() { ShowTitle = false, Style = "min-height:auto;min-width:auto;width:auto" });
}