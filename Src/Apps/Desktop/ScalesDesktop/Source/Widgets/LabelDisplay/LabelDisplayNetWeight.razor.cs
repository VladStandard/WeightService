using Ws.Shared.Extensions;

namespace ScalesDesktop.Source.Widgets.LabelDisplay;

public sealed partial class LabelDisplayNetWeight : ComponentBase, IDisposable
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private LabelContext LabelContext { get; set; } = default!;
    [Inject] private ScalesService ScalesService { get; set; } = default!;

    # endregion

    private Action? StableStatusChangedHandler { get; set; }

    #region Weight

    private string Sign => GetNetWeight >= 0 ? string.Empty : "-";
    private decimal GetNetWeight => LabelContext.KneadingModel.NetWeight;
    private string NetWeightAbsStr => Math.Abs(GetNetWeight).ToSepStr(',').PadLeft(8, '0');

    #endregion

    protected override void OnInitialized()
    {
        StableStatusChangedHandler = () =>
        {
            LabelContext.KneadingModel.NetWeight = (decimal)ScalesService.CurrentWeight / 1000 - LabelContext.Plu?.TareWeight ?? 0;
            StateHasChanged();
        };
        LabelContext.StateChanged += StateHasChanged;
        ScalesService.WeightChanged += StableStatusChangedHandler;
        ScalesService.StartPolling();
    }

    public void Dispose()
    {
        ScalesService.StopPolling();
        LabelContext.StateChanged -= StateHasChanged;
        ScalesService.WeightChanged -= StableStatusChangedHandler;
    }
}