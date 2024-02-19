using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Resources;
using ScalesDesktop.Services;
using Ws.SharedUI.Resources;

namespace ScalesDesktop.Features.Labels.Modules;

public sealed partial class LabelDisplayTareWeight : ComponentBase, IDisposable
{
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
    [Inject] private LabelContext LabelContext { get; set; } = null!;

    protected override void OnInitialized() => LabelContext.OnStateChanged += StateHasChanged;

    private decimal GetTareWeight => LabelContext.PluNesting.WeightTare;

    public void Dispose() => LabelContext.OnStateChanged -= StateHasChanged;
}