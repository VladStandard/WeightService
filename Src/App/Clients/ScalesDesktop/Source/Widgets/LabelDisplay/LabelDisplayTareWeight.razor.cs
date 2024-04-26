using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Shared.Resources;

namespace ScalesDesktop.Source.Widgets.LabelDisplay;

public sealed partial class LabelDisplayTareWeight : ComponentBase, IDisposable
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private LabelContext LabelContext { get; set; } = default!;

    # endregion

    protected override void OnInitialized() => LabelContext.OnStateChanged += StateHasChanged;

    private decimal GetTareWeight => LabelContext.Plu.GetWeightWithNesting;

    public void Dispose() => LabelContext.OnStateChanged -= StateHasChanged;
}