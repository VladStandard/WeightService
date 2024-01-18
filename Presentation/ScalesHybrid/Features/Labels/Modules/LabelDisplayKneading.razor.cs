using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Features.Shared;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;

namespace ScalesHybrid.Features.Labels.Modules;

public sealed partial class LabelDisplayKneading: ComponentBase, IDisposable
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private LineContext LineContext { get; set; } = null!;
    [Inject] private IModalService ModalService { get; set; } = null!;

    protected override void OnInitialized() => LineContext.OnStateChanged += StateHasChanged;

    private void SetNewKneading(int newKneading)
    {
        LineContext.KneadingModel.KneadingCount = newKneading;
        StateHasChanged();
    }
    
    private async Task ShowNumericKeyboard() => await ModalService.Show<NumericKeyboardDialog>(p =>
        p.Add(x => x.CallbackFunction, SetNewKneading), new(){ Size = ModalSize.Default });

    public void Dispose() => LineContext.OnStateChanged -= StateHasChanged;
}