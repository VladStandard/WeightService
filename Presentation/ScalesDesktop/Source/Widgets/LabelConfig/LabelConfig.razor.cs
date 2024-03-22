using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesDesktop.Source.Features;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Shared.Resources;

namespace ScalesDesktop.Source.Widgets.LabelConfig;

public sealed partial class LabelConfig : ComponentBase, IDisposable
{
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private LabelContext LabelContext { get; set; } = null!;
    
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
    [Inject] private IStringLocalizer<Resources> LabelsLocalizer { get; set; } = null!;

    protected override void OnInitialized() => LabelContext.OnStateChanged += StateHasChanged;

    private async Task ShowPluSelectDialog()
    {
        PluDialogContent data = new() { Data = LabelContext.PluEntities.AsQueryable() };
        IDialogReference dialog = await DialogService.ShowDialogAsync<PluDialog>(data, new());
        DialogResult result = await dialog.Result;
        if (result is { Cancelled: false, Data: PluEntity pluEntity })
            LabelContext.ChangePlu(pluEntity);
    } 
    
    public void Dispose() => LabelContext.OnStateChanged -= StateHasChanged;
}