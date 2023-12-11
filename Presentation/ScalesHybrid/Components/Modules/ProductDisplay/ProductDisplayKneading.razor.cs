using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Components.Dialogs;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;
using ScalesHybrid.Utils;
using VerticalAlignment = Blazorise.VerticalAlignment;

namespace ScalesHybrid.Components.Modules.ProductDisplay;

public sealed partial class ProductDisplayKneading: ComponentBase, IDisposable
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; }
    [Inject] private LineContext LineContext { get; set; }
    [Inject] private IModalService ModalService { get; set; }

    protected override void OnInitialized()
    {
        LineContext.OnStateChanged += StateHasChanged;
    }

    private string GetFormattedPluNestingName() => NameFormatting.GetPluNestingFormattedName(LineContext.PluNesting);
    private void IncreaseDate() => 
        LineContext.KneadingModel.ProductDate = LineContext.KneadingModel.ProductDate.AddDays(1);
    
    private void DecreaseDate() => 
        LineContext.KneadingModel.ProductDate = LineContext.KneadingModel.ProductDate.AddDays(-1);
    
    private void SetNewKneading(int newKneading)
    {
        LineContext.KneadingModel.KneadingCount = newKneading;
        StateHasChanged();
    }

    private async Task ShowCalculatorDialog() => await ModalService.Show<DialogCalculator>(p =>
            p.Add(x => x.CallbackFunction, SetNewKneading), new(){ Size = ModalSize.Default });

    public void Dispose()
    {
        LineContext.OnStateChanged -= StateHasChanged;
    }
}