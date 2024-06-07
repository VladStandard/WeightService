using Microsoft.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Services;

namespace ScalesDesktop.Source.Widgets.LabelDisplay;

public sealed partial class LabelDisplayNetWeight : ComponentBase, IDisposable
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private LabelContext LabelContext { get; set; } = default!;
    [Inject] private ScalesService ScalesService { get; set; } = default!;

    # endregion

    private Action? StableStatusChangedHandler { get; set; }

    protected override void OnInitialized()
    {
        StableStatusChangedHandler = () =>
        {
            LabelContext.KneadingModel.NetWeightG = ScalesService.CurrentWeight;
            StateHasChanged();
        };
        LabelContext.StateChanged += StateHasChanged;
        ScalesService.WeightChanged += StableStatusChangedHandler;
        ScalesService.StartPolling();
    }

    private decimal GetNetWeight => (decimal)LabelContext.KneadingModel.NetWeightG / 1000 - GetTareWeight;

    private decimal GetTareWeight => LabelContext.Plu.GetWeightWithNesting;

    private string Sign => GetNetWeight >= 0 ? string.Empty : "-";

    private string IntegerPart => $"{Math.Truncate(Math.Abs(GetNetWeight)):0000}";

    private string DecimalPart => Math.Abs(GetNetWeight % 1).ToString(".000")[1..];

    public void Dispose()
    {
        ScalesService.StopPolling();
        LabelContext.StateChanged -= StateHasChanged;
        ScalesService.WeightChanged -= StableStatusChangedHandler;
    }
}