using CommunityToolkit.Mvvm.Messaging;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Services;
using Ws.Scales.Events;
using Ws.Shared.TypeUtils;
using Ws.SharedUI.Resources;

namespace ScalesDesktop.Features.Labels.Modules;

public sealed partial class LabelDisplayNetWeight : ComponentBase, IDisposable
{
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;

    [Inject] private LabelContext LabelContext { get; set; } = null!;

    private bool IsStable { get; set; }

    protected override void OnInitialized()
    {
        LabelContext.OnStateChanged += StateHasChanged;
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