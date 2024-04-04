using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Scales.Events;
using Ws.Shared.TypeUtils;
using Ws.Shared.Resources;

namespace ScalesDesktop.Source.Widgets.LabelDisplay;

public sealed partial class LabelDisplayNetWeight : ComponentBase, IDisposable
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private LabelContext LabelContext { get; set; } = default!;
    [Inject] private LineContext LineContext { get; set; } = default!;

    # endregion

    private bool IsStable { get; set; }

    protected override void OnInitialized()
    {
        LabelContext.OnStateChanged += StateHasChanged;
        LineContext.StartWeightPolling();
        MassaSubscribe();
    }

    private decimal GetNetWeight => (decimal)LabelContext.KneadingModel.NetWeightG / 1000 - GetTareWeight;

    private decimal GetTareWeight => LabelContext.PluNesting.WeightTare;

    private string Sign => GetNetWeight >= 0 ? string.Empty : "-";

    private string IntegerPart => DecimalUtils.ToStrToLen(Math.Truncate(Math.Abs(GetNetWeight)), 4);

    private string DecimalPart => Math.Abs(GetNetWeight % 1).ToString(".000")[1..];

    private void UpdateScalesInfo(object sender, GetScaleMassaEvent payload)
    {
        IsStable = payload.IsStable;
        LabelContext.KneadingModel.NetWeightG = payload.Weight;
        InvokeAsync(StateHasChanged);
    }

    private void MassaSubscribe() =>
        WeakReferenceMessenger.Default.Register<GetScaleMassaEvent>(this, UpdateScalesInfo);

    private void MassaUnsubscribe() =>
        WeakReferenceMessenger.Default.Unregister<GetScaleMassaEvent>(this);

    public void Dispose()
    {
        LabelContext.OnStateChanged -= StateHasChanged;
        MassaUnsubscribe();
    }
}